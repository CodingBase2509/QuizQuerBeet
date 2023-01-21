using System.Windows.Input;

namespace QuizQuerBeet.Views.Templates.Categories;

public partial class CategorieCard : ContentView
{
    public static readonly BindableProperty CategoryProperty = BindableProperty
        .Create(nameof(Category), typeof(Category), typeof(CategorieCard));

    public static readonly BindableProperty CommandProperty = BindableProperty
		.Create(nameof(Command), typeof(ICommand), typeof(CategorieCard));

	public static readonly BindableProperty CommandParameterProperty = BindableProperty
		.Create(nameof(CommandParameter), typeof(object), typeof(CategorieCard));

    public CategorieCard()
	{
		InitializeComponent();
	}

    public Category Category
    {
        get => GetValue(CategoryProperty) as Category;
        set => SetValue(CategoryProperty, value);
    }

    public ICommand Command
    {
        get => GetValue(CommandProperty) as ICommand;
        set => SetValue(CommandProperty, value);
    }

	public object CommandParameter
    {
		get => GetValue(CommandParameterProperty) as object;
		set => SetValue(CommandParameterProperty, value);
    }

    //public void OnSelectionChanged(object sender, SelectedCategoryChangedEventArgs args)
    //{
    //    var colors = Application.Current.Resources.MergedDictionaries
    //        .Where(rd => rd.Source.ToString().Contains("CustomColors"))
    //        .FirstOrDefault();

    //    if (Equals(this.Category.Id, args.NewSelectedCategory.Id))
    //        this.BackgroundRectangle.Fill = colors["SecondaryVariant"] as Color;
    //    else
    //        this.BackgroundRectangle.Fill = colors["WhiteVariant"] as Color;
    //}
}
