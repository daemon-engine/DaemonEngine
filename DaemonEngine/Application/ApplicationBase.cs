using DaemonEngine.Core.Inputs;
using DaemonEngine.Core.Layer;
using DaemonEngine.EventSystem;
using DaemonEngine.EventSystem.Events.Window;
using DaemonEngine.Factories;
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

    protected ApplicationBase(IServiceProvider serviceProvider)
    {
        Logger = serviceProvider.GetRequiredService<ILogger>();
        Window = serviceProvider.GetRequiredService<IWindow>();

        LayerFactory = serviceProvider.GetRequiredService<ILayerFactory>();
        Renderer = serviceProvider.GetRequiredService<IRenderer>();

        _layerStack = new LayerStack();

        var input = serviceProvider.GetRequiredService<IInput>();
        new Input(input);
    }

    protected ILogger Logger { get; }
    protected IWindow Window { get; }

    private IRenderer Renderer { get; }
    private ILayerFactory LayerFactory { get; }

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
            }

            Window.Update();
        }
    }

    public void Stop()
    {
        _isRunning = false;
    }

    public TLayer AddLayer<TLayer>(string name)
        where TLayer : class, ILayer
    {
        var layer = LayerFactory.CreateLayer<TLayer>(name);
        _layerStack.AddLayer(layer);
        layer.OnStart();
        return layer;
    }

    public abstract void OnStart();
    public abstract void OnShutdown();

    public virtual void OnEvent(IEvent e)
    {
        EventDispatcher dispatcher = new(e);
        dispatcher.Dispatch<WindowResizeEvent>(OnWindowResizeEvent);
        dispatcher.Dispatch<WindowFocusEvent>(OnWindowFocusEvent);
        dispatcher.Dispatch<WindowCloseEvent>(OnWindowCloseEvent);

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

    private bool OnWindowCloseEvent(WindowCloseEvent e)
    {
        _isRunning = false;
        return true;
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
            foreach (var layer in _layerStack.Layers)
            {
                layer.OnShutdown();
            }

            OnShutdown();

            Renderer.Shutdown();
            Window.Shutdown();
        }

        _disposed = true;
    }
}
