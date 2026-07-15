using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MES.Desktop.Data;
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
        s.AddDbContext<MesDbContext>(o => o.UseMySql(
            "Server=localhost;Port=3306;Database=minimes;User=root;Password=Dream;",
            ServerVersion.AutoDetect("Server=localhost;Port=3306;Database=minimes;User=root;Password=Dream;")));
        s.AddSingleton<AuthService>();
        s.AddSingleton<MainViewModel>();
        s.AddTransient<LoginViewModel>();
        _sp = s.BuildServiceProvider();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        ShutdownMode = ShutdownMode.OnExplicitShutdown;

        // 初始化数据库
        try
        {
            using var scope = _sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<MesDbContext>();
            await db.Database.MigrateAsync();
            await DbInitializer.InitializeAsync(db);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"数据库连接失败: {ex.Message}\n请确认 MySQL 服务已启动。", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            Shutdown();
            return;
        }

        // 登录
        var login = new LoginWindow(_sp.GetRequiredService<LoginViewModel>());
        if (login.ShowDialog() != true) { Shutdown(); return; }

        // 主窗口
        var mainVm = _sp.GetRequiredService<MainViewModel>();
        mainVm.UserName = (string)Properties["UserName"];
        mainVm.Status = "就绪";

        ShutdownMode = ShutdownMode.OnMainWindowClose;
        MainWindow = new MainWindow(mainVm);
        MainWindow.Show();
    }
}
