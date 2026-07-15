using System.Windows;
using MES.Desktop.ViewModels;

namespace MES.Desktop.Views;

public partial class LoginWindow : Window
{
    public LoginWindow(LoginViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
        PwdBox.PasswordChanged += (_, _) => vm.Password = PwdBox.Password;
    }
}
