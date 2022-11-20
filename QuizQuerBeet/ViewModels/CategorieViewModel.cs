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
    public void ChangeCategory(Category newSelectedCategory)
    {
        if (Equals(newSelectedCategory, this.SelectedCategory))
            return;

        this.SelectedCategory = newSelectedCategory;
        if (this.SelectedCategory.Quizzes is null)
            this.SelectedCategory.Quizzes = new List<Quiz>();

        this.Quizzes = new(this.SelectedCategory.Quizzes);
    }
    #endregion

    #region Functions
    async Task AddCategoryAsync()
    {
        var categorie = await ShellService.AddCategorieAsync(this.unitOfWork);

        await this.unitOfWork.Categories.AddAsync(categorie);
        await this.unitOfWork.SaveChangesAsync();

        this.Categories = new(await this.unitOfWork.Categories.GetAllAsync());
    }

    static ObservableCollection<Category> CreateTestData()
    {
        return new ObservableCollection<Category>()
        {
            new Category(){ Id = Guid.NewGuid(), Name = "Testen", Quizzes = new List<Quiz>() },
            new Category(){ Id = Guid.NewGuid(), Name = "Kochen", Quizzes = new List<Quiz>() },
            new Category(){ Id = Guid.NewGuid(), Name = "Mathematik", Quizzes = new List<Quiz>()
            {
                new Quiz()
                {
                    Id = Guid.NewGuid(),
                    Name = "Lineare Algebra",
                    Category = null,
                    Questions = new List<Question>()
                },
                new Quiz()
                {
                    Id = Guid.NewGuid(),
                    Name = "Stochastik",
                    Category = null,
                    Questions = new List<Question>()
                },
                new Quiz()
                {
                    Id = Guid.NewGuid(),
                    Name = "Exponentioal Funktionen",
                    Category = null,
                    Questions = new List<Question>()
                },
                new Quiz()
                {
                    Id = Guid.NewGuid(),
                    Name = "Testen 1234",
                    Category = null,
                    Questions = new List<Question>()
                },
                new Quiz()
                {
                    Id = Guid.NewGuid(),
                    Name = "Lineare Algebra",
                    Category = null,
                    Questions = new List<Question>()
                },
                new Quiz()
                {
                    Id = Guid.NewGuid(),
                    Name = "Stochastik",
                    Category = null,
                    Questions = new List<Question>()
                },
                new Quiz()
                {
                    Id = Guid.NewGuid(),
                    Name = "Exponentioal Funktionen",
                    Category = null,
                    Questions = new List<Question>()
                },
                new Quiz()
                {
                    Id = Guid.NewGuid(),
                    Name = "Testen 1234",
                    Category = null,
                    Questions = new List<Question>()
                },
            }},
            new Category(){ Id = Guid.NewGuid(), Name = "Sportarten", Quizzes = new List<Quiz>() },
            new Category(){ Id = Guid.NewGuid(), Name = "Physik", Quizzes = new List<Quiz>() },
            new Category(){ Id = Guid.NewGuid(), Name = "Informatik", Quizzes = new List<Quiz>() },

        };
    }
    #endregion
}