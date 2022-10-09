using QuizQuerBeet.Domain.Interfaces;

namespace QuizQuerBeet.Domain.Models;

public class Statistic: IIdentifiable
{
    /// <summary>
    /// The Id of the <see cref="Statistic"/>
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The total count of <see cref="Question"/>s of the Quiz
    /// </summary>
    public int QuestionsOfTheQuizCount { get; set; }

    /// <summary>
    /// The count of <see cref="Question"/>s that where answered right
    /// in the attemp
    /// </summary>
    public int QuestionsRight { get; set; }

    /// <summary>
    /// The count of <see cref="Question"/>s that where answered wrong
    /// in the attemp
    /// </summary>
    public int QuestionsWrong { get; set; }

    /// <summary>
    /// The Day, where the statistic was created
    /// </summary>
    public DateOnly DayOfTry { get; set; }

    /// <summary>
    /// The Id of the Quiz, where the <see cref="Statistic"/> belongs to
    /// </summary>
    public Guid QuizId { get; set; }
}
