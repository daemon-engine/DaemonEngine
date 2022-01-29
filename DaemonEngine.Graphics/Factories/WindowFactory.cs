using DaemonEngine.Windows;
using Serilog;

namespace DaemonEngine.Graphics.Factories;

internal class WindowFactory<TWindow> : IWindowFactory<TWindow>
    where TWindow : class, IWindow
{
    private ILogger _logger;

    public WindowFactory(ILogger logger)
    {
        _logger = logger;
    }

    public TWindow CreateWindow(WindowOptions windowOptions)
    {
        var type = typeof(TWindow);

        var instance = Activator.CreateInstance(type, _logger, windowOptions);
        if (instance == null)
        {
            throw new Exception($"Failed to create instance of {type.Name}");
        }

        ((TWindow)instance).Initialize();
        return (TWindow)instance;
    }
}
