using System.Windows;
using MES.Desktop.ViewModels;

namespace MES.Desktop;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
