using QuizQuerBeet.Infrastructure.Contracts;
namespace QuizQuerBeet.Infrastructure.Repositories;

internal class QuestionRepository: GenericRepository<Question>, IQuestionRepository
{
	public QuestionRepository(DataContext context)
		: base(context) { }
}

