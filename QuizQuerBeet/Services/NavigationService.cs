namespace QuizQuerBeet.Services;

public sealed class ShellService
{
    #region Shell Navigation
    public static async Task GoToAsync(string destination)
	{
		await Shell.Current.GoToAsync(destination, true);
	}

	public static async Task GoToCategories(CategorieMode mode)
	{
		await Shell.Current.GoToAsync($"//categories?mode={(int)mode}");
    }

	public static async Task GoToQuizEdit(Guid id = default)
	{
		await Shell.Current.GoToAsync($"quizEditing?id={id.ToString()}");
	}

	public static async Task GoToQuestionEdit(Guid id = default)
	{
        await Shell.Current.GoToAsync($"questionEditing?id={id.ToString()}");
    }
    #endregion

    #region Popups
    public static async Task DisplayAlert(string title, string message)
	{
		await Shell.Current.DisplayAlert(title, message, "Ok");
	}

	public static async Task<string> DisplayActionSheet(string title, string message,params string[] buttons)
	{
		return await Shell.Current.DisplayActionSheet(title, message, null, buttons);
	}
    #endregion

    #region Aditional stuff
    public static async Task<Category> AddCategorie(IUnitOfWork unitOfWork)
	{
        var categorieName = await Shell.Current.DisplayPromptAsync("Neue Categorie anlegen",
                "Wie soll die neue Kategorie heißen?", "Ok", "Abbrechen",
                keyboard: Keyboard.Text);

        if (string.IsNullOrEmpty(categorieName))
            return default;

        var categorie = new Category()
        {
            Id = Guid.NewGuid(),
            Name = categorieName,
            Quizzes = new List<Quiz>()
        };

        await unitOfWork.Categories.AddAsync(categorie);

		return categorie;
    }
    #endregion

}

