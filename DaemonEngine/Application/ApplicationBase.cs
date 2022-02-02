using DaemonEngine.Core.Layer;
using DaemonEngine.EventSystem;
using DaemonEngine.EventSystem.Events.Window;
using DaemonEngine.Factories;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Windows;
using DaemonEngine.Windows.Inputs;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DaemonEngine.Application;

public abstract class ApplicationBase : IApplication, IDisposable
{
    private bool _disposed;
    private float _lastFrameTime = 0.0f;
    private bool _isRunning = true;

    private bool _isMinimized = false;
    private bool _isFocused = true;

    private LayerStack _layerStack;

    private ILayerFactory _layerFactory;

    protected ApplicationBase(ILogger logger, IWindow window, IInput input, IRenderer renderer, IGraphicsFactory graphicsFactory, IServiceProvider serviceProvider)
    {
        Logger = logger;
        Window = window;
        Input = input;
        Renderer = renderer;
        GraphicsFactory = graphicsFactory;

        _layerFactory = serviceProvider.GetRequiredService<ILayerFactory>();

        _layerStack = new LayerStack();
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
            if (_isFocused && !_isMinimized)
            {
                float time = (float)Window.GetTime();
                float deltaTime = time - _lastFrameTime;
                _lastFrameTime = time;

                foreach (var layer in _layerStack.Layers)
                {
                    layer.OnUpdate(deltaTime);
                }
                //OnUpdate(deltaTime);
            }

            Window.Update();
        }
    }

    public TLayer AddLayer<TLayer>(string name)
        where TLayer : class, ILayer
    {
        var layer = _layerFactory.CreateLayer<TLayer>(name);
        _layerStack.AddLayer(layer);
        layer.OnStart();
        return layer;
    }

    public abstract void OnStart();
    public abstract void OnShutdown();
    public abstract void OnUpdate(float deltaTime);

    public virtual void OnEvent(IEvent e)
    {
        EventDispatcher dispatcher = new(e);
        dispatcher.Dispatch<WindowResizeEvent>(OnWindowResizeEvent);
        dispatcher.Dispatch<WindowFocusEvent>(OnWindowFocusEvent);

        foreach (var layer in _layerStack.Layers)
        {
            layer.OnEvent(e);
        }
    }

    private bool OnWindowFocusEvent(WindowFocusEvent e)
    {
        _isFocused = e.Focused;
        return true;
    }

    private bool OnWindowResizeEvent(WindowResizeEvent e)
    {
        if(e.Width <= 0 || e.Height <= 0)
        {
            _isMinimized = true;
            return true;
        }

        Renderer.SetViewport(0, 0, e.Width, e.Height);
        return true;
    }

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
