using QuizQuerBeet.Domain.Interfaces;

namespace QuizQuerBeet.Domain.Models;

public class Question: IIdentifiable
{
    /// <summary>
    /// The Id of the <see cref="Question"/>
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The text of the <see cref="Question"/> as a string
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    /// An <see cref="Array"/> of <see cref="byte"/>s representing an image,
    /// that can be append to the <see cref="Question"/> to illustrate the question
    /// </summary>
    public byte[]? Image { get; set; }

    /// <summary>
    /// An <see cref="IEnumerable{Answer}"/> with all possible <see cref="Answer"/>s
    /// </summary>
    public IEnumerable<Answer>? Answers { get; set; }

    /// <summary>
    /// The <see cref="QuizQuerBeet.Domain.Models.Quiz"/> where the Question belongs to
    /// </summary>
    public Quiz Quiz { get; set; }
}
