namespace QuizQuerBeet.ViewModels;

[QueryProperty(nameof(QuizId), "id")]
public sealed partial class QuizEditingViewModel : ViewModelBase
{
    private readonly IUnitOfWork unitOfWork;

    #region Properties
    Guid id = Guid.Empty;
    public string QuizId
    {
        get => id.ToString();
        set
        {
            id = new Guid(value);
        }
    }

    [ObservableProperty]
    public Quiz newQuiz;

    [ObservableProperty]
    public ObservableCollection<Category> categories;

    [ObservableProperty]
    private ObservableCollection<Question> questions;

    [ObservableProperty]
    public Category selectedCategory;

    public double InputWith => DeviceDisplay.MainDisplayInfo.Width * 0.22;

    public double SwipeThreshold => DeviceDisplay.MainDisplayInfo.Width * 0.5;

    #endregion

    public QuizEditingViewModel(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.loadData().Wait();
    }

    async Task loadData()
    {
        this.Categories = new(await unitOfWork.Categories.GetAllAsync());
        this.Questions = new();

        if (this.id != Guid.Empty)
            this.NewQuiz = await unitOfWork.Quizzes.GetByIdAsync(id);
        else
        {
            this.NewQuiz = new()
            {
                Id = Guid.NewGuid(),
                Name = "",
                Category = null,
                Questions = new List<Question>(),
            };
        }
    }

    #region Commands

    partial void OnSelectedCategoryChanged(Category value)
    {
        this.NewQuiz.Category = value;
    }

    [RelayCommand]
    public async Task OpenContextmenuAsync(Question question)
    {
        var action = await Shell.Current.DisplayActionSheet(question.Value, "Abbrechen", "Löschen", "Bearbeiten");

        switch (action)
        {
            case "Löschen":
                await this.unitOfWork.Questions.RemoveAsync(question);
                this.Questions = new (this.Questions.Where(q => !Equals(q.Id, question.Id)).ToList());
                break;
            case "Bearbeiten":
                await ShellService.GoToQuestionEditasync(question.Id);
                break;
            default:
                break;
        }
    }

    [RelayCommand]
    public void AddQuestion()
    {
        this.Questions.Add(new Question()
        {
            Id = Guid.NewGuid(),
            Quiz = this.NewQuiz,
            Value = "",
            Answers = new List<Answer>(),
            Image = Array.Empty<byte>(),
        });
    }

    [RelayCommand]
    public async Task DeleteAndGoBackAsync()
    {
        this.Questions.Clear();
        this.NewQuiz = null;

        if (this.SelectedCategory is not null)
        this.SelectedCategory = null;

        this.unitOfWork.Rollback();
        await ShellService.GoBackAsync();
        
    }

    [RelayCommand]
    public async Task SaveAndGoBackAsync()
    {
        if (this.SelectedCategory is null)
        {
            await ShellService.DisplayAlertAsync("Kategorie auswählen", "Es muss eine Kategorie für das Quiz hinzugefügt werden");
            return;
        }

        if (string.IsNullOrEmpty(this.newQuiz.Name))
        {
            await ShellService.DisplayAlertAsync("Quiz Name eingeben", "Es muss ein Name für das Quiz angegeben werden");
            return;
        }

        foreach (var question in this.Questions)
        {
            (this.NewQuiz.Questions as IList<Question>).Add(question);
            await this.unitOfWork.Questions.AddAsync(question);
        }
        await this.unitOfWork.Quizzes.AddAsync(this.NewQuiz);


        await this.unitOfWork.SaveChangesAsync();
        await ShellService.GoBackAsync();
    }

    #endregion
}
