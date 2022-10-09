namespace QuizQuerBeet.Views;

public partial class QuizOverview : ContentPage
{
	public QuizOverview(QuizOverviewViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}