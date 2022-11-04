using QuizQuerBeet.Domain.Interfaces;

namespace QuizQuerBeet.Domain.Models;

public class Category: IIdentifiable
{
    /// <summary>
    /// The Id of the <see cref="Category"/>
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The Displayname of the <see cref="Category"/>
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// A <see cref="List{Quiz}"/> with Quizzes, wich belongs to the <see cref="Category"/>
    /// </summary>
    public IEnumerable<Quiz> Quizzes { get; set; }
}

