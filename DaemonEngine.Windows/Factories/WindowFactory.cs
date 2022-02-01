using Serilog;

namespace DaemonEngine.Windows.Factories;

public interface IWindowFactory
{
    IWindow CreateWindow(WindowOptions windowOptions);
}

internal class WindowFactory : IWindowFactory
{
    private readonly ILogger _logger;
    private readonly WindowApi _windowApi;

    public WindowFactory(ILogger logger, WindowApi windowApi)
    {
        _logger = logger;
        _windowApi = windowApi;
    }

    public IWindow CreateWindow(WindowOptions windowOptions)
    {
        switch (_windowApi)
        {
            case WindowApi.Glfw:    return new GlfwWindow();
            case WindowApi.None:
            case WindowApi.Win32:
            default: break;
        }
    }
}
