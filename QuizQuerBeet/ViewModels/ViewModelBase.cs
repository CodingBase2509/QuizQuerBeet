namespace QuizQuerBeet.ViewModels;

public abstract partial class ViewModelBase: CommunityToolkit.Mvvm.ComponentModel.ObservableObject
{
    /// <summary>
    /// Gives information if the page is currently working
    /// </summary>
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    [ObservableProperty]
    bool isBusy;

    /// <summary>
    /// Gives information if the page is currently working
    /// </summary>
    public bool IsNotBusy => !IsBusy;

    /// <summary>
    /// The Titel of the page
    /// </summary>
    public string Title { get; set; }

    public ViewModelBase()
    { }

}
