using EnglishQuizTelegramBot.Services.Classes;
using EnglishQuizTelegramBot.Services.Classes.DAL;
using EnglishQuizTelegramBot.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishQuizTelegramBot.Services
{
    public static class ServiceProviderExtensions
    {
        public static void AddConnectionStrings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));
        }

        public static void AddTelegramBotConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TelegramBotConfig>(configuration.GetSection("TelegramBotConfig"));
        }

        public static void AddDbEnglishQuiz(this IServiceCollection services)
        {
            services.AddSingleton<IDBEnqlishQuiz, DBEnglishQuiz>();
        }

        public static void AddDbEnglishQuizRepository(this IServiceCollection services)
        {
            services.AddSingleton<IMemberRepository, MemberRepository>();
            services.AddSingleton<IWordRepository, WordRepository>();
        }

        public static void AddTelegramBot(this IServiceCollection services)
        {
            services.AddSingleton<TelegramBot>();
        }

        public static void AddTelegramService(this IServiceCollection services)
        {
            services.AddSingleton<TelegramService>();
        }
    }
}