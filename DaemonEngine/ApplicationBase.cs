using DaemonEngine.Windows;
using Serilog;

namespace DaemonEngine;

public abstract class ApplicationBase : IApplication, IDisposable
{
    private IWindow _window;

    private bool _disposed;

    protected ApplicationBase(ILogger logger, IWindow window)
    {
        Logger = logger;
        _window = window;
    }

    protected ILogger Logger { get; }

    public void Run()
    {
        OnStart();

        while (!_window.IsRunning())
        {
            OnUpdate(0.0f);

            _window.Update();
        }
    }

    public abstract void OnStart();
    public abstract void OnShutdown();
    public abstract void OnUpdate(float deltaTime);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _window.Shutdown();
            OnShutdown();
        }

        _disposed = true;
    }
}
