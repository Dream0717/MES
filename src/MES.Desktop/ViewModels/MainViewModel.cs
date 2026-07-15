using System.Collections.ObjectModel;
using System.Windows.Input;
using MES.Desktop.Services;

namespace MES.Desktop.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly MesApiService _api;

    public MainViewModel(MesApiService api)
    {
        _api = api;
    }

    // ── 属性 ──

    private bool _isLoggedIn;
    public bool IsLoggedIn
    {
        get => _isLoggedIn;
        set
        {
            SetProperty(ref _isLoggedIn, value);
            OnPropertyChanged(nameof(IsLoggedOut));
        }
    }

    public bool IsLoggedOut => !IsLoggedIn;

    private string _userName = "";
    public string UserName
    {
        get => _userName;
        set => SetProperty(ref _userName, value);
    }

    private string _statusText = "就绪";
    public string StatusText
    {
        get => _statusText;
        set => SetProperty(ref _statusText, value);
    }

    private string _webViewUrl = "about:blank";
    public string WebViewUrl
    {
        get => _webViewUrl;
        set => SetProperty(ref _webViewUrl, value);
    }

    // ── 导航命令 ──

    public ICommand NavigateToDashboardCommand => new RelayCommand(_ =>
    {
        WebViewUrl = $"{_api.GetApiBaseUrl()}";
    });

    public ICommand NavigateToWorkOrdersCommand => new RelayCommand(_ =>
    {
        WebViewUrl = $"{_api.GetApiBaseUrl()}workorders";
    });

    public ICommand NavigateToProductsCommand => new RelayCommand(_ =>
    {
        WebViewUrl = $"{_api.GetApiBaseUrl()}products";
    });
}
