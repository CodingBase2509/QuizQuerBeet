using QuizQuerBeet.Views;

namespace QuizQuerBeet.Extentions;

public static class ShellExtentions
{
    public static void RegisterRoutes()
    {
        Routing.RegisterRoute("quizEditing", typeof(QuizEditing));
        Routing.RegisterRoute("questionEditing", typeof(QuestionEditing));
    }
}
