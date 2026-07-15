using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using MES.Desktop.Services;
using MES.Desktop.ViewModels;
using MES.Desktop.Views;

namespace MES.Desktop;

public partial class App : Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        var services = new ServiceCollection();

        // HttpClient
        services.AddHttpClient<MesApiService>(client =>
        {
            client.BaseAddress = new Uri("http://localhost:5000/");
            client.Timeout = TimeSpan.FromSeconds(10);
        });

        // ViewModels
        services.AddSingleton<MainViewModel>();
        services.AddTransient<LoginViewModel>();

        // Views
        services.AddTransient<LoginView>();

        _serviceProvider = services.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        var mainWindow = new MainWindow(mainViewModel);
        mainWindow.Show();

        // 显示登录窗口
        var loginView = _serviceProvider.GetRequiredService<LoginView>();
        if (loginView.ShowDialog() != true)
        {
            mainWindow.Close();
        }
    }
}
