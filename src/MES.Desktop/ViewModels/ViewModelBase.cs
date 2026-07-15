using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MES.Desktop.ViewModels;

public class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? n = null) => PropertyChanged?.Invoke(this, new(n));
    protected bool Set<T>(ref T f, T v, [CallerMemberName] string? n = null) { if (EqualityComparer<T>.Default.Equals(f, v)) return false; f = v; OnPropertyChanged(n); return true; }
}

public class RelayCommand : ICommand
{
    readonly Action<object?> _e; readonly Func<object?, bool>? _c;
    public RelayCommand(Action<object?> e, Func<object?, bool>? c = null) { _e = e; _c = c; }
    public RelayCommand(Action e, Func<bool>? c = null) : this(_ => e(), c is null ? null : _ => c()) { }
    public event EventHandler? CanExecuteChanged { add => CommandManager.RequerySuggested += value; remove => CommandManager.RequerySuggested -= value; }
    public bool CanExecute(object? p) => _c?.Invoke(p) ?? true;
    public void Execute(object? p) => _e(p);
}

public class AsyncRelayCommand : ICommand
{
    readonly Func<object?, Task> _e; readonly Func<object?, bool>? _c; bool _running;
    public AsyncRelayCommand(Func<object?, Task> e, Func<object?, bool>? c = null) { _e = e; _c = c; }
    public AsyncRelayCommand(Func<Task> e, Func<bool>? c = null) : this(_ => e(), c is null ? null : _ => c()) { }
    public event EventHandler? CanExecuteChanged { add => CommandManager.RequerySuggested += value; remove => CommandManager.RequerySuggested -= value; }
    public bool CanExecute(object? p) => !_running && (_c?.Invoke(p) ?? true);
    public async void Execute(object? p) { if (_running) return; _running = true; CommandManager.InvalidateRequerySuggested(); try { await _e(p); } finally { _running = false; CommandManager.InvalidateRequerySuggested(); } }
}
