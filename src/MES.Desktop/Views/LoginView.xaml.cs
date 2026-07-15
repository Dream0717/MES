using System.Windows;
using System.Windows.Controls;
using MES.Desktop.ViewModels;

namespace MES.Desktop.Views;

public partial class LoginView : Window
{
    public LoginView(LoginViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        PasswordBox.PasswordChanged += (s, e) => viewModel.Password = PasswordBox.Password;
    }
}
