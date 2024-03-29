﻿using QuizQuerBeet.Infrastructure.Converter;

namespace QuizQuerBeet.Infrastructure.Context;

public class DataContext: DbContext
{
    public DbSet<Topic>? Topics { get; set; }

    public DbSet<Quiz>? Quizzes { get; set; }

    public DbSet<Question>? Questions { get; set; }

    public DbSet<Answer>? Answers { get; set; }

    public DbSet<Statistic>? Statistics { get; set; }

    public DataContext(DbContextOptions<DataContext> options)
        :base(options)
	{
        this.Database.Migrate();
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

