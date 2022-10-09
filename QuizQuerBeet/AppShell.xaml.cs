namespace QuizQuerBeet;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		CurrentItem = StartTab;

        ShellExtentions.RegisterRoutes();
	}
}
