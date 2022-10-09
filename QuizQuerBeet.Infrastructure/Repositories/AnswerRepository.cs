using QuizQuerBeet.Infrastructure.Contracts;
namespace QuizQuerBeet.Infrastructure.Repositories;

internal class AnswerRepository: GenericRepository<Answer>, IAnswerRepository
{
	public AnswerRepository(DataContext context)
		:base(context) { }
}

