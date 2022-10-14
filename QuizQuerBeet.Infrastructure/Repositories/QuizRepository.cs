namespace QuizQuerBeet.Infrastructure.Repositories;

internal sealed class QuizRepository: GenericRepository<Quiz>, IQuizRepository
{
	public QuizRepository(DataContext context)
		:base(context) { }
}

