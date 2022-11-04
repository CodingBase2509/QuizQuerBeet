using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace QuizQuerBeet.Views.Templates.MainPage;

public partial class NavigationButton : ContentView
{
    public static readonly BindableProperty TextProperty = BindableProperty
        .Create(nameof(Text), typeof(string), typeof(NavigationButton), string.Empty);

    public static readonly BindableProperty CommandProperty = BindableProperty
        .Create(nameof(Command), typeof(ICommand), typeof(NavigationButton), null);

    public static readonly BindableProperty CommandParameterProperty = BindableProperty
        .Create(nameof(CommandParameter), typeof(object), typeof(NavigationButton));

    public NavigationButton()
    {
        InitializeComponent();
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public ICommand Command
    {
        get => GetValue(CommandProperty) as ICommand;
        set => SetValue(CommandProperty, value);
    }

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty) as object;
        set => SetValue(CommandParameterProperty, value);
    }
}
