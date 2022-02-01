using DaemonEngine.DependencyInjection;
using DaemonEngine.Windows;
using DaemonEngine.Windows.GraphicsLibraryFramework;
using Serilog;

namespace DaemonEngine.Factories.Windows;

public interface IWindowFactory
{
    IWindow CreateWindow(WindowApi windowApi, WindowOptions windowOptions);
}

internal class WindowFactory : IWindowFactory
{
    private readonly ILogger _logger;

    public WindowFactory(ILogger logger)
    {
        _logger = logger;
    }

    public IWindow CreateWindow(WindowApi windowApi, WindowOptions windowOptions)
    {
        switch (windowApi)
        {
            case WindowApi.Glfw:    return new GlfwWindow(_logger, windowOptions);
            case WindowApi.Win32:
            case WindowApi.None: 
            default: break;
        }
        _logger.Error("Unknown WindowApi!");
        return default;
    }
}
