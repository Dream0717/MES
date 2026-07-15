using System.Windows;
using System.Windows.Input;
using MES.Desktop.Services;

namespace MES.Desktop.ViewModels;

public class LoginViewModel : ViewModelBase
{
    readonly AuthService _auth;
    public LoginViewModel(AuthService auth) => _auth = auth;

    public string Username { get; set; } = "admin";

    string _password = "admin123";
    public string Password { get => _password; set => _password = value; }

    string _err = "";
    public string Error { get => _err; set => Set(ref _err, value); }

    bool _busy;
    public bool IsBusy { get => _busy; set => Set(ref _busy, value); }

    public ICommand LoginCmd => new AsyncRelayCommand(async () =>
    {
        Error = ""; IsBusy = true;
        await Task.Delay(50); // let UI breathe
        try
        {
            if (_auth.Login(Username, Password))
            {
                Application.Current.Properties["LoggedIn"] = true;
                Application.Current.Properties["UserName"] = _auth.CurrentRealName;
                foreach (Window w in Application.Current.Windows)
                    if (w is Views.LoginWindow) { w.DialogResult = true; w.Close(); }
            }
            else Error = "用户名或密码错误";
        }
        catch (Exception ex) { Error = $"错误: {ex.Message}"; }
        finally { IsBusy = false; }
    });
}
