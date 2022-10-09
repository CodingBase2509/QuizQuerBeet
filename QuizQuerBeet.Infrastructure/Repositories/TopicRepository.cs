namespace QuizQuerBeet.Infrastructure.Repositories;

internal class TopicRepository: GenericRepository<Topic>, ITopicRepository
{
    public TopicRepository(DataContext context)
        :base(context) { }
}

