using QuizQuerBeet.Views;

namespace QuizQuerBeet.Extentions;

public static class ShellExtentions
{
    public static void RegisterRoutes()
    {
        Routing.RegisterRoute("mainPage", typeof(MainPage));
        Routing.RegisterRoute("statistics", typeof(Statistics));
        Routing.RegisterRoute("categories", typeof(CategorieView));

        Routing.RegisterRoute("quizEditing", typeof(QuizEditing));
        Routing.RegisterRoute("questionEditing", typeof(QuestionEditing));
    }
}
