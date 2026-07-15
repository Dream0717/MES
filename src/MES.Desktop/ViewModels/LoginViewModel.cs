using System.Windows;
using System.Windows.Input;
using MES.Desktop.Services;

namespace MES.Desktop.ViewModels;

public class LoginViewModel : ViewModelBase
{
    private readonly MesApiService _api;
    private readonly MainViewModel _mainViewModel;

    public LoginViewModel(MesApiService api, MainViewModel mainViewModel)
    {
        _api = api;
        _mainViewModel = mainViewModel;
    }

    private string _username = "admin";
    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }

    private string _password = "admin123";
    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    private string _errorMessage = "";
    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    public ICommand LoginCommand => new AsyncRelayCommand(async () =>
    {
        ErrorMessage = "";
        IsBusy = true;

        try
        {
            var result = await _api.LoginAsync(new LoginRequest
            {
                Username = Username,
                Password = Password
            });

            if (result.Success && result.Data != null)
            {
                _api.Token = result.Data.Token;
                _mainViewModel.IsLoggedIn = true;
                _mainViewModel.UserName = result.Data.RealName;
                _mainViewModel.StatusText = "已登录";
                _mainViewModel.WebViewUrl = _api.GetApiBaseUrl();
            }
            else
            {
                ErrorMessage = result.Message ?? "登录失败";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"连接失败: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    });
}
