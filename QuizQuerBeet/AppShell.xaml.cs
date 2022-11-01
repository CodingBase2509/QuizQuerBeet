namespace QuizQuerBeet;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        ShellExtentions.RegisterRoutes();

		CurrentItem = StartTab;

	}
}
