
namespace QuizApp
{

    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp() => MauiApp
            .CreateBuilder()
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitMediaElement()
            .UseShinyFramework(
                new DryIocContainerExtension(),
                prism => prism.CreateWindow("NavigationPage/MainPage"),
                new(
#if DEBUG
                    ErrorAlertType.FullError
#else
                    ErrorAlertType.NoLocalize
#endif
                )
            )
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .RegisterInfrastructure()
            .RegisterAppServices()
            .RegisterViews()
            .Build();


        static MauiAppBuilder RegisterAppServices(this MauiAppBuilder builder)
        {
            // register your own services here!
            return builder;
        }


        static MauiAppBuilder RegisterInfrastructure(this MauiAppBuilder builder)
        {
            builder.Configuration.AddJsonPlatformBundle();
            builder.Logging.AddSqlite(Path.Combine(FileSystem.AppDataDirectory, "logging.db"));
#if DEBUG
            builder.Logging.SetMinimumLevel(LogLevel.Trace);
            builder.Logging.AddDebug();
#endif
            var s = builder.Services;
            s.AddSingleton<QuizApp.Services.MySqliteConnection>();
            s.AddSingleton(CommunityToolkit.Maui.Media.SpeechToText.Default);
            s.AddDataAnnotationValidation();
            return builder;
        }


        static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
            var s = builder.Services;


            s.RegisterForNavigation<MainPage, MainViewModel>();
            return builder;
        }
    }
}