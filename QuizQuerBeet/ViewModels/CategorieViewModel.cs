using System.Collections.ObjectModel;
using QuizQuerBeet.Domain.Enums;

namespace QuizQuerBeet.ViewModels;

[QueryProperty(nameof(Mode), "mode")]
public sealed partial class CategorieViewModel : ViewModelBase
{
    readonly IUnitOfWork unitOfWork;

    #region Properties
    CategorieMode quizMode;
    public string Mode
    {
        get => quizMode.ToString();
        set
        {
            if (string.IsNullOrEmpty(value))
                quizMode = CategorieMode.None;

            var intValue = int.Parse(value);
            quizMode = (CategorieMode)intValue;
            OnPropertyChanged();
        }
    }
    
    [ObservableProperty]
    public ObservableCollection<Category> categories;

    [ObservableProperty]
    Category selectedCategory;

    [ObservableProperty]
    public ObservableCollection<Quiz> quizzes;
    #endregion

    public CategorieViewModel(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;

        this.Categories = new(this.unitOfWork.Categories.GetAllAsync().GetAwaiter().GetResult());
    }

    #region Events
    public void OnNavigatingAway(object sender, NavigatingFromEventArgs eventArgs)
    {
        this.Mode = ((int)CategorieMode.None).ToString();
    }
    #endregion

    #region Commands
    [RelayCommand]
    public async Task AddElementAsync()
    {
        var elementToAdd = await ShellService.DisplayActionSheetAsync("Element hinzufügen", "Abbrechen",
            "Kategorie", "Quiz");

        if (Equals(elementToAdd, "Kategorie"))
            await AddCategoryAsync();
        else if (Equals(elementToAdd, "Quiz"))
            await ShellService.GoToQuizEditAsync();
    }

    [RelayCommand]
    public async Task ChangeCategory(Category newSelectedCategory)
    {
        if (Equals(newSelectedCategory, this.SelectedCategory))
            return;

        this.SelectedCategory = newSelectedCategory;
        if (this.SelectedCategory.Quizzes is null)
            this.SelectedCategory.Quizzes = new List<Quiz>();

        this.SelectedCategory.Quizzes = await this.unitOfWork.Quizzes.FindAsync(q => Equals(newSelectedCategory, q.Category));

        this.Quizzes = new(this.SelectedCategory.Quizzes);
    }
    #endregion

    #region Functions
    async Task AddCategoryAsync()
    {
        var categorie = await ShellService.AddCategorieAsync(this.unitOfWork);

        if (categorie is default(Category))
            return;

        await this.unitOfWork.Categories.AddAsync(categorie);
        await this.unitOfWork.SaveChangesAsync();

        this.Categories = new(await this.unitOfWork.Categories.GetAllAsync());
    }
    #endregion
}