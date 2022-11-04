using static System.Net.Mime.MediaTypeNames;

namespace QuizQuerBeet.Views.Templates.MainPage;

public partial class Header : ContentView
{
    public static readonly BindableProperty NameProperty = BindableProperty
        .Create(nameof(Name), typeof(string), typeof(Header), string.Empty);

    public Header()
	{
		InitializeComponent();
	}

    public string Name
    {
        get => GetValue(NameProperty) as string;
        set => SetValue(NameProperty, value);
    }

}
