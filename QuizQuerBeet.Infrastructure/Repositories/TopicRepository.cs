namespace QuizQuerBeet.Infrastructure.Repositories;

internal sealed class TopicRepository: GenericRepository<Topic>, ITopicRepository
{
    public TopicRepository(DataContext context)
        :base(context) { }
}

