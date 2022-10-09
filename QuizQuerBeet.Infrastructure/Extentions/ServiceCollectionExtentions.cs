using System;
using Microsoft.Extensions.DependencyInjection;
using QuizQuerBeet.Infrastructure.Contracts;
using QuizQuerBeet.Infrastructure.Repositories;

namespace QuizQuerBeet.Infrastructure.Extentions;

public static class ServiceCollectionExtentions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITopicRepository, TopicRepository>();
        services.AddScoped<IQuizRepository, QuizRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IAnswerRepository, AnswerRepository>();
        services.AddScoped<IStatisticRepository, StatisticRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}

