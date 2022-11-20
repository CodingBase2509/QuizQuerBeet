namespace QuizQuerBeet.ViewModels;

public sealed partial class MainPageViewModel: ViewModelBase
{
    [ObservableProperty]
    public string userName;

    public MainPageViewModel()
    {
        this.UserName = "Fabian";
    }

    [RelayCommand]
    public static async Task NavigateToCategorieWithModeAsync(CategorieMode mode)
    {
        await Shell.Current.GoToAsync($"//categories?mode={(int)mode}");
    }

    [RelayCommand]
    public static async Task CreateNewCategorieAsync()
    {
        await Shell.Current.DisplayAlert("WARNUNG", "Diese Seite wurde noch nicht implementiert", "Ok");
    }

    [RelayCommand]
    public static async Task CreateNewQuizAsync()
    {
        await Shell.Current.DisplayAlert("WARNUNG", "Diese Seite wurde noch nicht implementiert", "Ok");
    }

}
