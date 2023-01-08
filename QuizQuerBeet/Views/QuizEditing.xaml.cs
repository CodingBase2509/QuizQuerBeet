namespace QuizQuerBeet.Views;

public partial class QuizEditing : ContentPage
{
	public QuizEditing(QuizEditingViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}