using QuizQuerBeet.Infrastructure.Contracts;
namespace QuizQuerBeet.Infrastructure.Repositories;

internal class QuizRepository: GenericRepository<Quiz>, IQuizRepository
{
	public QuizRepository(DataContext context)
		:base(context) { }
}

