namespace QuizQuerBeet.Infrastructure.Repositories;

internal sealed class QuestionRepository: GenericRepository<Question>, IQuestionRepository
{
	public QuestionRepository(DataContext context)
		: base(context) { }
}

