namespace QuizQuerBeet.ViewModels;

public sealed partial class MainPageViewModel: ViewModelBase
{
    private readonly IUnitOfWork unitOfWork;

    [ObservableProperty]
    public string userName;

    public MainPageViewModel(IUnitOfWork unitOfWork)
    {
        this.UserName = "Fabian";
        this.unitOfWork = unitOfWork;
    }

    [RelayCommand]
    public static async Task NavigateToCategorieWithModeAsync(CategorieMode mode)
    {
        await Shell.Current.GoToAsync($"//categories?mode={(int)mode}");
    }

    [RelayCommand]
    public async Task CreateNewCategorieAsync()
    {
        await ShellService.AddCategorieAsync(unitOfWork);
    }

    [RelayCommand]
    public static async Task CreateNewQuizAsync()
    {
        await ShellService.GoToQuizEditAsync();
    }

}
