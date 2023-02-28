namespace QuizQuerBeet.ViewModels;

public sealed partial class QuestionEditingViewModel: ViewModelBase
{
    private readonly IUnitOfWork unitOfWork;

    public QuestionEditingViewModel(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    [RelayCommand]
    public async Task DeleteAndGoBackAsync()
    {
        await ShellService.GoBackAsync();
    }

    [RelayCommand]
    public async Task SaveAndGoBackAsync()
    {
        await Task.Delay(10);
    }
}
