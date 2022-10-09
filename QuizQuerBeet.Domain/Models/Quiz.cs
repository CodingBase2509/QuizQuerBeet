using QuizQuerBeet.Domain.Interfaces;

namespace QuizQuerBeet.Domain.Models;

public class Quiz: IIdentifiable
{
    /// <summary>
    /// The Id of the <see cref="Quiz"/>
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the <see cref="Quiz"/>
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// An <see cref="IEnumerable{Question}"/> with all <see cref="Question"/>s of the <see cref="Quiz"/>
    /// </summary>
    public IEnumerable<Question>? Questions { get; set; }

    /// <summary>
    /// The <see cref="QuizQuerBeet.Domain.Models.Topic"/> where the <see cref="Quiz"/> belongs to
    /// </summary>
    public Topic Topic { get; set; }

}
