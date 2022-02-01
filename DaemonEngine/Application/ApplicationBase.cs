using DaemonEngine.EventSystem;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Windows;
using DaemonEngine.Windows.Inputs;
using Serilog;

namespace DaemonEngine.Application;

public abstract class ApplicationBase : IApplication, IDisposable
{
    private bool _disposed;
    private float _lastFrameTime = 0.0f;
    private bool _isRunning = true;

    protected ApplicationBase(ILogger logger, IWindow window, IInput input, IRenderer renderer, IGraphicsFactory graphicsFactory)
    {
        Logger = logger;
        Window = window;
        Input = input;
        Renderer = renderer;
        GraphicsFactory = graphicsFactory;
    }

    protected ILogger Logger { get; }
    protected IWindow Window { get; }
    protected IInput Input { get; }
    protected IRenderer Renderer { get; }
    protected IGraphicsFactory GraphicsFactory { get; }

    public void Run()
    {
        Window.Initialize();
        Window.SetEventCallback(OnEvent);

        Renderer.Initialize();

        OnStart();

        while (_isRunning)
        {
            float time = (float)Window.GetTime();
            float deltaTime = time - _lastFrameTime;
            _lastFrameTime = time;

            OnUpdate(deltaTime);

            Window.Update();
        }
    }

    public abstract void OnStart();
    public abstract void OnShutdown();
    public abstract void OnUpdate(float deltaTime);
    public abstract void OnEvent(IEvent e);

    protected void Close()
    {
        _isRunning = false;
    }

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
            OnShutdown();

            Renderer.Shutdown();
            Window.Shutdown();
        }

        _disposed = true;
    }
}
