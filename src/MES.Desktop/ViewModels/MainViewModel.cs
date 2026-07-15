using System.Windows.Input;

namespace MES.Desktop.ViewModels;

public class MainViewModel : ViewModelBase
{
    string _status = "就绪";
    public string Status { get => _status; set => Set(ref _status, value); }

    string _userName = "";
    public string UserName { get => _userName; set => Set(ref _userName, value); }

    object? _currentPage;
    public object? CurrentPage { get => _currentPage; set => Set(ref _currentPage, value); }

    // 导航命令
    public ICommand NavDashboardCmd => new RelayCommand(_ => Status = "生产看板");
    public ICommand NavOrdersCmd => new RelayCommand(_ => Status = "工单管理");
    public ICommand NavProductsCmd => new RelayCommand(_ => Status = "产品管理");
    public ICommand NavWorkstationsCmd => new RelayCommand(_ => Status = "工位管理");
    public ICommand NavReportsCmd => new RelayCommand(_ => Status = "报工查询");
    public ICommand NavUsersCmd => new RelayCommand(_ => Status = "用户管理");
    public ICommand NavMonitorCmd => new RelayCommand(_ => Status = "设备监控");

    // 打开浏览器用于查询操作
    public ICommand OpenBrowserCmd => new RelayCommand(_ =>
    {
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = "http://localhost:5000/swagger",
            UseShellExecute = true
        });
        Status = "已打开浏览器";
    });
}
