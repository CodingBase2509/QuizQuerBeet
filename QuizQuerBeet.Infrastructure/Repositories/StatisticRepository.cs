namespace QuizQuerBeet.Infrastructure.Repositories;

internal sealed class StatisticRepository: GenericRepository<Statistic>, IStatisticRepository
{
	public StatisticRepository(DataContext context)
		:base(context) { }
}

