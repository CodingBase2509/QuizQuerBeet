namespace QuizQuerBeet.Infrastructure.Repositories;

internal sealed class AnswerRepository: GenericRepository<Answer>, IAnswerRepository
{
	public AnswerRepository(DataContext context)
		:base(context) { }
}

