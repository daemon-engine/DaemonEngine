using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Windows;
using Serilog;

namespace DaemonEngine;

public abstract class ApplicationBase : IApplication, IDisposable
{
    private bool _disposed;

    protected ApplicationBase(ILogger logger, IWindow window, IRenderer renderer, IGraphicsFactory graphicsFactory)
    {
        Logger = logger;
        Window = window;
        Renderer = renderer;
        GraphicsFactory = graphicsFactory;
    }

    protected ILogger Logger { get; }
    protected IWindow Window { get; }
    protected IRenderer Renderer { get; }
    protected IGraphicsFactory GraphicsFactory { get; }

    public void Run()
    {
        OnStart();

        Renderer.Initialize();

        while (!Window.IsRunning())
        {
            OnUpdate(0.0f);

            Window.Update();
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
            Renderer.Shutdown();
            Window.Shutdown();
            OnShutdown();
        }

        _disposed = true;
    }
}
