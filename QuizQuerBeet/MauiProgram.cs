using QuizQuerBeet.Infrastructure.Extentions;
using QuizQuerBeet.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

namespace QuizQuerBeet;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddDbContext<DataContext>(options =>
		{
            var appPath = Path.Combine(FileSystem.AppDataDirectory, "ExamTrainer");

            if (!Directory.Exists(appPath))
                Directory.CreateDirectory(appPath);

            var DbPath = Path.Combine(appPath, "ExamTrainer.db");
            options.UseSqlite($"Data Source={DbPath}");
        });

		builder.Services.AddRepositories();

		builder.Services.AddViewModels();
		builder.Services.AddViews();

		return builder.Build();
	}
}
