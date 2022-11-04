namespace QuizQuerBeet.Infrastructure.Repositories;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dataContext;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IQuizRepository _quizRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IAnswerRepository _answerRepository;
    private readonly IStatisticRepository _statisticRepository;

    public ICategoryRepository Categories => _categoryRepository;

    public IQuizRepository Quizzes => _quizRepository;

    public IQuestionRepository Questions => _questionRepository;

    public IAnswerRepository Answers => _answerRepository;

    public IStatisticRepository Statistics => _statisticRepository;

    public UnitOfWork(DataContext dataContext, ICategoryRepository category, IQuizRepository quiz, IQuestionRepository question, IAnswerRepository answer, IStatisticRepository statistic)
    {
        _dataContext = dataContext;
        _categoryRepository = category;
        _quizRepository = quiz;
        _questionRepository = question;
        _answerRepository = answer;
        _statisticRepository = statistic;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dataContext.SaveChangesAsync();
    }

    public void Rollback()
    {
        _dataContext.ChangeTracker.Entries()
            .ToList()
            .Where(e => e.State != EntityState.Unchanged)
            .AsParallel()
            .ForAll(entry => SetEntryState(entry));
    }

    private static void SetEntryState(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry)
    {
        switch (entry.State)
        {
            case EntityState.Modified:
                entry.CurrentValues.SetValues(entry.OriginalValues);
                entry.State = EntityState.Unchanged;
                break;
            case EntityState.Added:
                entry.State = EntityState.Detached;
                break;
            case EntityState.Deleted:
                entry.State = EntityState.Unchanged;
                break;
        }
    }


}

