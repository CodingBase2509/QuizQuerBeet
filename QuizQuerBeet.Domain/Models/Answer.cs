using System.ComponentModel.DataAnnotations.Schema;
using QuizQuerBeet.Domain.Interfaces;

namespace QuizQuerBeet.Domain.Models;

public class Answer : IIdentifiable
{
    /// <summary>
    /// The Id of the <see cref="Answer"/>
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// <see langword="true"/> if the <see cref="Answer"/> was selected in a quiz or while creating a quiz, otherwise <see langword="false"/>
    /// </summary>
    [NotMapped]
    public bool IsSelected { get; set; }

    /// <summary>
    /// <see langword="true"/> if the <see cref="Answer"/> is correct, otherwise <see langword="false"/>
    /// </summary>
    public bool IsCorrectAnswer { get; set; }

    /// <summary>
    /// The text of the <see cref="Answer"/> as a string
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    /// The <see cref="QuizQuerBeet.Domain.Models.Question"/> where the <see cref="Answer"/> belongs to
    /// </summary>
    public Question Question { get; set; }
}
