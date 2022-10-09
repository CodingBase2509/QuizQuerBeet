using QuizQuerBeet.Domain.Interfaces;

namespace QuizQuerBeet.Domain.Models;

public class Topic: IIdentifiable
{
    /// <summary>
    /// The Id of the <see cref="Topic"/>
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The Name of the <see cref="Topic"/>
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// An <see cref="IEnumerable{Quiz}"/> with all Quizzes of the <see cref="Topic"/>
    /// </summary>
    public IEnumerable<Quiz>? Quizzes { get; set; }
}

