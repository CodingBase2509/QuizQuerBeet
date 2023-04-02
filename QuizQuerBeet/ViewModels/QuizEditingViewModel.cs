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
            this.loadData().Wait();
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

    #endregion

    public QuizEditingViewModel(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    async Task loadData()
    {
        this.Categories = new(await unitOfWork.Categories.GetAllAsync());
        this.Questions = new();

        if (this.id != Guid.Empty)
        {
            this.NewQuiz = await unitOfWork.Quizzes.GetByIdAsync(id);
            if (NewQuiz.Questions.Any())
            {
                this.Questions = new(this.NewQuiz.Questions);
            }
            this.SelectedCategory = this.NewQuiz.Category;
        }
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

    [RelayCommand]
    public async Task OpenContextmenuAsync(Question question)
    {
        var action = await Shell.Current.DisplayActionSheet(question.Value, "Abbrechen", "Löschen", "Bearbeiten");

        switch (action)
        {
            case "Löschen":
                this.Questions = new(this.Questions.Where(q => !Equals(q.Id, question.Id)).ToList());
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

        if (string.IsNullOrEmpty(this.NewQuiz.Name))
        {
            await ShellService.DisplayAlertAsync("Quiz Name eingeben", "Es muss ein Name für das Quiz angegeben werden");
            return;
        }

        if (!Equals(this.NewQuiz.Category, this.SelectedCategory))
        {
            if (this.NewQuiz.Category is not null)
            {
                this.NewQuiz.Category.Quizzes = this.NewQuiz.Category.Quizzes.Where(q => q.Id != NewQuiz.Id).ToList();
                await this.unitOfWork.Categories.UpdateAsync(this.NewQuiz.Category);
            }

            this.NewQuiz.Category = this.SelectedCategory;
            if (!this.SelectedCategory.Quizzes.Contains(this.NewQuiz))
                this.SelectedCategory.Quizzes = this.SelectedCategory.Quizzes.Append(this.NewQuiz).ToList();

            if (await this.unitOfWork.Quizzes.GetByIdAsync(this.NewQuiz.Id) is null)
            {
                await this.unitOfWork.Quizzes.AddAsync(this.NewQuiz);
                await this.unitOfWork.SaveChangesAsync();
            }

            await this.unitOfWork.Categories.UpdateAsync(this.SelectedCategory);
        }

        this.NewQuiz.Questions = new List<Question>(this.NewQuiz.Questions);
        foreach (var question in this.Questions)
        {
            if (this.NewQuiz.Questions.Contains(question))
            {
                await this.unitOfWork.Questions.UpdateAsync(question);
            }
            else
            {
                (this.NewQuiz.Questions as List<Question>).Add(question);
                await this.unitOfWork.Questions.AddAsync(question);
            }
        }
        foreach (var question in this.NewQuiz.Questions)
        {
            if (!this.Questions.Contains(question))
                await this.unitOfWork.Questions.RemoveAsync(question);
        }

        await this.unitOfWork.Quizzes.UpdateAsync(this.NewQuiz);

        await this.unitOfWork.SaveChangesAsync();
        await ShellService.GoBackAsync();
    }

#endregion
}
