namespace QuizQuerBeet.ViewModels;

[QueryProperty(nameof(QuizId), "id")]
public sealed partial class QuestionEditingViewModel : ViewModelBase
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ImageService imagePicker;
    private readonly IDispatcher dispatcher;

    #region Properties
    Guid id = Guid.Empty;
    public string QuizId
    {
        get => id.ToString();
        set
        {
            id = new Guid(value);
            this.LoadData()
                .GetAwaiter()
                .GetResult();
        }
    }

    [ObservableProperty]
    public string headerTitle;

    [ObservableProperty]
    public Question newQuestion;

    [ObservableProperty]
    public ObservableCollection<Answer> answers;

    public ImageSource Image
        => ImageSource.FromStream(() => new MemoryStream(this.NewQuestion?.Image));

    #endregion

    public QuestionEditingViewModel(IUnitOfWork unitOfWork, ImageService imageService, IDispatcher dispatcher)
    {
        this.unitOfWork = unitOfWork;
        this.imagePicker = imageService;
        this.dispatcher = dispatcher;
    }

    async Task LoadData()
    {
        this.NewQuestion = await unitOfWork.Questions.GetByIdAsync(id);

        if (this.NewQuestion is not null)
        {
            if (NewQuestion.Answers.Any())
            {
                this.Answers = new(this.NewQuestion.Answers);
            }

            this.HeaderTitle = "Bearbeiten";
        }
        else
        {
            this.NewQuestion = new()
            {
                Id = Guid.NewGuid(),
                Value = "",
                Answers = new List<Answer>(),
                Quiz = null,
                Image = Array.Empty<byte>()
            };

            this.HeaderTitle = "Neue Frage";
        }

        //if (this.NewQuestion.Image is not null)
        //{
        //    using var stream = new MemoryStream(this.NewQuestion.Image);
        //    this.Image = ImageSource.FromStream(() => stream);
        //}

        this.Answers ??= new ObservableCollection<Answer>();
    }

    #region Commands
    [RelayCommand]
    public async Task AddAnswerAsync()
    {
        if (this.Answers.Count == 4)
        {
            await ShellService.DisplayAlertAsync("Frage Hinzufügen",
                "Pro Frage können maximal 4 Antworten hinzugefügt werden.");
            return;
        }

        var newAnswer = new Answer()
        {
            Id = Guid.NewGuid(),
            IsCorrectAnswer = false,
            IsSelected = false,
            Question = this.NewQuestion,
            Value = string.Empty
        };

        await dispatcher.DispatchAsync(() => this.Answers.Add(newAnswer));

        var less = newAnswer;
    }

    [RelayCommand]
    public void DeleteAnswer(Answer answerToDelete)
    {
        this.Answers.Remove(answerToDelete);
    }

    [RelayCommand]
    public async Task UploadPictureAsync()
    {

        var imageAsByteArray = await imagePicker.SelectPictureAsync();
        if (imageAsByteArray is null)
            return;

        if (imageAsByteArray.Any())
            this.NewQuestion.Image = imageAsByteArray;

        await Shell.Current.DisplayAlert("Test", "Test Message", "Ok");
    }

    [RelayCommand]
    public async Task DeleteAndGoBackAsync()
    {
        await ShellService.GoBackAsync();
    }

    [RelayCommand]
    public async Task SaveAndGoBackAsync()
    {
        this.NewQuestion.Answers = new List<Answer>(this.NewQuestion.Answers);
        foreach (var answer in this.Answers)
        {
            if (this.NewQuestion.Answers.Contains(answer))
            {
                await this.unitOfWork.Answers.UpdateAsync(answer);
            }
            else
            {
                (this.NewQuestion.Answers as List<Answer>).Add(answer);
                await this.unitOfWork.Answers.AddAsync(answer);
            }
        }

        foreach (var answer in this.NewQuestion.Answers)
        {
            if (!this.Answers.Contains(answer))
                await this.unitOfWork.Answers.RemoveAsync(answer);
        }

        await this.unitOfWork.Questions.UpdateAsync(this.NewQuestion);

        await this.unitOfWork.SaveChangesAsync();
        await ShellService.GoBackAsync();
    }
    #endregion
}
