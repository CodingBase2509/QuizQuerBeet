namespace QuizQuerBeet.Views;

public partial class QuestionEditing : ContentPage
{
	public QuestionEditing(QuestionEditingViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}