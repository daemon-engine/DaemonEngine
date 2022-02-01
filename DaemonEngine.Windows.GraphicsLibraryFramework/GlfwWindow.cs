using DaemonEngine.EventSystem.Events.Key;
using DaemonEngine.EventSystem.Events.Mouse;
using DaemonEngine.EventSystem.Events.Window;
using DaemonEngine.Extensions.Glfw;
using Serilog;

namespace DaemonEngine.Windows.GraphicsLibraryFramework;

internal class GlfwWindow : WindowBase
{
    private Extensions.Glfw.Structs.GlfwWindow _glfwWindow;

    public GlfwWindow(ILogger logger, WindowOptions windowOptions)
        : base(logger, windowOptions)
    {
        Initialize();
    }

    public override void Initialize()
    {
        if (string.IsNullOrWhiteSpace(WindowOptions.Title))
        {
            Logger.Warning("Title for Windows has not been set!");
            WindowOptions.Title = "Glfw Window";
        }

        if (!Glfw.Init())
        {
            Logger.Fatal("Failed to initialize Glfw");
            return;
        }

        _glfwWindow = Glfw.CreateWindow(WindowOptions.Width, WindowOptions.Height, WindowOptions.Title);
        if (_glfwWindow.Equals(null))
        {
            Glfw.Terminate();
            Logger.Fatal("Failed to create Glfw window");
            return;
        }
        GC.KeepAlive(_glfwWindow);

        Glfw.MakeContextCurrent(_glfwWindow);

        Glfw.SetWindowCloseCallback(ref _glfwWindow, WindowCloseEventFn);
        Glfw.SetWindowFocusCallback(ref _glfwWindow, WindowFocusEventFn);
        Glfw.SetWindowResizeCallback(ref _glfwWindow, WindowResizeEventFn);

        Glfw.SetKeyCallback(ref _glfwWindow, KeyEventFn);
        Glfw.SetCharCallback(ref _glfwWindow, KeyTypedFn);

        Glfw.SetCursorPosCallback(ref _glfwWindow, MouseMovedEventFn);
        Glfw.SetMouseButtonCallback(ref _glfwWindow, MouseButtonEventFn);
        Glfw.SetScrollCallback(ref _glfwWindow, MouseScrollEventFn);
    }

    private void MouseButtonEventFn(IntPtr window, int button, int action, int mods)
    {
        switch (action)
        {
            case GlfwConstants.GLFW_PRESS:
                {
                    MouseButtonPressedEvent e = new(button);
                    EventCallback(e);
                }
                break;
            case GlfwConstants.GLFW_RELEASE:
                {
                    MouseButtonReleasedEvent e = new(button);
                    EventCallback(e);
                }
                break;
        }
    }

    private void MouseScrollEventFn(IntPtr window, double xOffset, double yOffset)
    {
        MouseScrolledEvent e = new((float)xOffset, (float)yOffset);
        EventCallback?.Invoke(e);
    }

    private void MouseMovedEventFn(IntPtr window, double mouseX, double mouseY)
    {
        MouseMovedEvent e = new((float)mouseX, (float)mouseY);
        EventCallback?.Invoke(e);
    }

    private void KeyTypedFn(IntPtr windowHandle, uint keycode)
    {
        KeyTypedEvent e = new((int)keycode);
        EventCallback(e);
    }

    private void KeyEventFn(IntPtr windowHandle, int key, int scancode, int action, int mods)
    {
        switch (action)
        {
            case GlfwConstants.GLFW_PRESS:
                {
                    KeyPressedEvent e = new(key, 0);
                    EventCallback(e);
                }
                break;
            case GlfwConstants.GLFW_RELEASE:
                {
                    KeyReleasedEvent e = new(key);
                    EventCallback(e);
                }
                break;
            case GlfwConstants.GLFW_REPEAT:
                {
                    KeyPressedEvent e = new(key, 1);
                    EventCallback(e);
                }
                break;
        }
    }

    private void WindowResizeEventFn(IntPtr windowHandle, int width, int height)
    {
        WindowResizeEvent e = new(width, height);
        EventCallback(e);
    }

    private void WindowFocusEventFn(IntPtr windowHandle, int focused)
    {
        WindowFocusEvent e = new(focused == GlfwConstants.GLFW_TRUE);
        EventCallback?.Invoke(e);
    }

    private void WindowCloseEventFn(IntPtr windowHandle)
    {
        WindowCloseEvent e = new();
        EventCallback?.Invoke(e);
    }

    public override double GetTime()
    {
        return Glfw.GetTime();
    }

    public override void Shutdown()
    {
        Glfw.DestroyWindow(_glfwWindow);
        Glfw.Terminate();
    }

    public override void Update()
    {
        Glfw.SwapBuffers(_glfwWindow);
        Glfw.PollEvents();
    }

    public override object GetNativeWindowHandle()
    {
        return _glfwWindow;
    }
}
