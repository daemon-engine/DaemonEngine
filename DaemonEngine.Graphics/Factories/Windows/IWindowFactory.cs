using DaemonEngine.Windows;

namespace DaemonEngine.Graphics.Factories.Windows;

public interface IWindowFactory<TWindow>
    where TWindow : class, IWindow
{
    TWindow CreateWindow(WindowOptions windowOptions);
}
