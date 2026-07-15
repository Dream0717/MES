using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using MES.Desktop.Services;
using MES.Desktop.ViewModels;
using MES.Desktop.Views;

namespace MES.Desktop;

public partial class App : Application
{
    readonly ServiceProvider _sp;

    public App()
    {
        var s = new ServiceCollection();
        s.AddHttpClient<MesApiService>(c => { c.BaseAddress = new("http://localhost:5000/"); c.Timeout = TimeSpan.FromSeconds(10); });
        s.AddSingleton<MainViewModel>();
        s.AddTransient<LoginViewModel>();
        _sp = s.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        ShutdownMode = ShutdownMode.OnExplicitShutdown;

        var login = new LoginWindow(_sp.GetRequiredService<LoginViewModel>());
        if (login.ShowDialog() != true)
        {
            Shutdown();
            return;
        }

        var mainVm = _sp.GetRequiredService<MainViewModel>();
        mainVm.UserName = (string)Properties["UserName"];
        mainVm.Status = "已连接";

        ShutdownMode = ShutdownMode.OnMainWindowClose;
        MainWindow = new MainWindow(mainVm);
        MainWindow.Show();
    }
}
