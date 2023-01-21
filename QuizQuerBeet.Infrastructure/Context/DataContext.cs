using System.Reflection.Metadata;
using QuizQuerBeet.Infrastructure.Converter;

namespace QuizQuerBeet.Infrastructure.Context;

public class DataContext: DbContext
{
    public DbSet<Category>? Categories { get; set; }

    public DbSet<Quiz>? Quizzes { get; set; }

    public DbSet<Question>? Questions { get; set; }

    public DbSet<Answer>? Answers { get; set; }

    public DbSet<Statistic>? Statistics { get; set; }

    public DataContext(DbContextOptions<DataContext> options)
        :base(options)
	{
        this.Database.EnsureCreated();
	}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Auto-Includes
        modelBuilder.Entity<Category>()
            .Navigation(e => e.Quizzes)
            .AutoInclude();

        modelBuilder.Entity<Quiz>()
            .Navigation(e => e.Category)
            .AutoInclude();

        modelBuilder.Entity<Quiz>()
            .Navigation(e => e.Questions)
            .AutoInclude(); 

        modelBuilder.Entity<Question>()
            .Navigation(e => e.Answers)
            .AutoInclude();
        #endregion

        #region Delete Behavior
        modelBuilder.Entity<Category>()
            .HasMany(e => e.Quizzes)
            .WithOne(e => e.Category)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Quiz>()
            .HasOne(e => e.Category)
            .WithMany(e => e.Quizzes);

        modelBuilder.Entity<Quiz>()
            .HasMany(e => e.Questions)
            .WithOne(e => e.Quiz)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Question>()
            .HasOne(e => e.Quiz)
            .WithMany(e => e.Questions);

        modelBuilder.Entity<Question>()
            .HasMany(e => e.Answers)
            .WithOne(e => e.Question)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Answer>()
            .HasOne(e => e.Question)
            .WithMany(e => e.Answers);

        #endregion
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter>()
            .HaveColumnType("date");

        builder.Properties<DateOnly?>()
            .HaveConversion<NullableDateOnlyConverter>()
            .HaveColumnType("date");
    }
}