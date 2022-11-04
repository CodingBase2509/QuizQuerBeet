namespace QuizQuerBeet.Infrastructure.Repositories;

internal sealed class CategoryRepository: GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(DataContext context)
        : base(context) { }
}

