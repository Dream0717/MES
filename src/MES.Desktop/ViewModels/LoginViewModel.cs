using System.Windows;
using System.Windows.Input;
using MES.Desktop.Services;

namespace MES.Desktop.ViewModels;

public class LoginViewModel : ViewModelBase
{
    readonly MesApiService _api;

    public LoginViewModel(MesApiService api) => _api = api;

    public string Username { get; set; } = "admin";
    public string Password { get; set; } = "admin123";

    string _err = "";
    public string Error { get => _err; set => Set(ref _err, value); }

    bool _busy;
    public bool IsBusy { get => _busy; set => Set(ref _busy, value); }

    public ICommand LoginCmd => new AsyncRelayCommand(async () =>
    {
        Error = ""; IsBusy = true;
        try
        {
            var r = await _api.LoginAsync(new() { Username = Username, Password = Password });
            if (r.Success && r.Data != null)
            {
                _api.Token = r.Data.Token;
                // 通知主窗口登录成功，关闭登录窗口
                Application.Current.Properties["LoggedIn"] = true;
                Application.Current.Properties["UserName"] = r.Data.RealName;
                foreach (Window w in Application.Current.Windows)
                    if (w is Views.LoginWindow) { w.DialogResult = true; w.Close(); }
            }
            else Error = r.Message ?? "登录失败";
        }
        catch (Exception ex) { Error = $"连接失败: {ex.Message}"; }
        finally { IsBusy = false; }
    });
}
