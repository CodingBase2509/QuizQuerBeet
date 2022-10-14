namespace QuizQuerBeet.Views;

public partial class CategorieView : ContentPage
{
	public CategorieView(CategorieViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
