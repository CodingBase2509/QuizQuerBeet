using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace QuizQuerBeet.Views.Templates.MainPage;

public partial class NavigationButton : ContentView
{
    static readonly BindableProperty TextProperty = BindableProperty
        .Create(nameof(Text), typeof(string), typeof(NavigationButton), string.Empty);

    public NavigationButton()
    {
        InitializeComponent();
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}
