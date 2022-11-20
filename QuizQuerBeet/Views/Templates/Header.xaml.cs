namespace QuizQuerBeet.Views.Templates;

public partial class Header : ContentView
{
	public static readonly BindableProperty TextProperty = BindableProperty
		.Create(nameof(Text), typeof(string), typeof(Header), string.Empty);

	public Header()
	{
		InitializeComponent();
	}

	public string Text
    {
		get => GetValue(TextProperty) as string;
		set => SetValue(TextProperty, value);
    }
}
