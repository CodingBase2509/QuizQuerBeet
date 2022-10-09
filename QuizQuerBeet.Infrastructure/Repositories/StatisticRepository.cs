using QuizQuerBeet.Infrastructure.Contracts;
namespace QuizQuerBeet.Infrastructure.Repositories;

internal class StatisticRepository: GenericRepository<Statistic>, IStatisticRepository
{
	public StatisticRepository(DataContext context)
		:base(context) { }
}

