using QuizQuerBeet.Views;

namespace QuizQuerBeet.Extentions;

public static class ServiceCollectionExtentions
{
    public static IServiceCollection AddViews(this IServiceCollection services)
    {
        services.AddSingleton<MainPage>();
        services.AddSingleton<QuizOverview>();
        services.AddSingleton<Statistics>();

        services.AddTransient<QuizEditing>();
        services.AddTransient<QuestionEditing>();
        services.AddTransient<CategorieView>();

        return services;
    }

    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        services.AddSingleton<MainPageViewModel>();
        services.AddSingleton<QuizOverviewViewModel>();
        services.AddSingleton<StatisticsViewModel>();

        services.AddTransient<QuizEditingViewModel>();
        services.AddTransient<QuestionEditingViewModel>();
        services.AddTransient<CategorieViewModel>();

        return services;
    }
}
