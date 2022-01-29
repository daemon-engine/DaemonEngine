using DaemonEngine.Windows;

namespace DaemonEngine.Graphics.Factories;

public interface IWindowFactory<TWindow>
    where TWindow : class, IWindow
{
    TWindow CreateWindow(WindowOptions windowOptions);
}
