namespace QuizQuerBeet.Views;

public partial class Statistics : ContentPage
{
	public Statistics(StatisticsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}