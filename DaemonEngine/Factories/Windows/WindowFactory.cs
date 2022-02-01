using DaemonEngine.Windows;
using DaemonEngine.Windows.GraphicsLibraryFramework;
using Serilog;

namespace DaemonEngine.Factories.Windows;

public interface IWindowFactory
{
    IWindow CreateWindow(WindowOptions windowOptions);
}

internal class WindowFactory : IWindowFactory
{
    private readonly ILogger _logger;

    public WindowFactory(ILogger logger)
    {
        _logger = logger;
    }

    public IWindow CreateWindow(WindowOptions windowOptions)
    {
        return new GlfwWindow(_logger, windowOptions);
    }
}
