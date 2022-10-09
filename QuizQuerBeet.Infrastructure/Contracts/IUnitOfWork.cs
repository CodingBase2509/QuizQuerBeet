namespace QuizQuerBeet.Infrastructure.Contracts;

public interface IUnitOfWork
{
    ITopicRepository Topics { get; }
    IQuizRepository Quizzes { get; }
    IQuestionRepository Questions { get; }
    IAnswerRepository Answers { get; }
    IStatisticRepository Statistics { get; }

    Task<int> SaveChangesAsync();

    void Rollback();
}

