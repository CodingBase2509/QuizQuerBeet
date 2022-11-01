using QuizQuerBeet.Views;

namespace QuizQuerBeet.Extentions;

public static class ServiceCollectionExtentions
{
    public static IServiceCollection AddViews(this IServiceCollection services)
    {
        services.AddSingleton<MainPage>();
        services.AddSingleton<Statistics>();
        services.AddSingleton<CategorieView>();

        services.AddTransient<QuizEditing>();
        services.AddTransient<QuestionEditing>();

        return services;
    }

    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        services.AddSingleton<MainPageViewModel>();
        services.AddSingleton<StatisticsViewModel>();
        services.AddSingleton<CategorieViewModel>();

        services.AddTransient<QuizEditingViewModel>();
        services.AddTransient<QuestionEditingViewModel>();

        return services;
    }
}
