using DaemonEngine.EventSystem.Events.Key;
using DaemonEngine.EventSystem.Events.Mouse;
using DaemonEngine.EventSystem.Events.Window;
using DaemonEngine.Graphics.OpenGL.Renderer;
using DaemonEngine.GraphicsLibraryFramework.DllImport;
using DaemonEngine.GraphicsLibraryFramework.DllImport.Structs;
using Serilog;

namespace DaemonEngine.Windows.GraphicsLibraryFramework;

internal class GlfwWindow : WindowBase
{
    private GlfwNativeWindowHandle _glfwNativeWindowHandle;

    public GlfwWindow(ILogger logger, WindowOptions windowOptions)
        : base(logger, windowOptions)
    {
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

        Glfw.SetErrorCallback(ref _glfwNativeWindowHandle, OnGlfwErrorFn);

        Glfw.WindowHint(GlfwConstants.GLFW_CONTEXT_VERSION_MAJOR, 3);
        Glfw.WindowHint(GlfwConstants.GLFW_CONTEXT_VERSION_MINOR, 3);
        Glfw.WindowHint(GlfwConstants.GLFW_OPENGL_PROFILE, GlfwConstants.GLFW_OPENGL_CORE_PROFILE);

        _glfwNativeWindowHandle = Glfw.CreateWindow(WindowOptions.Width, WindowOptions.Height, WindowOptions.Title);
        if (_glfwNativeWindowHandle.Equals(null))
        {
            Glfw.Terminate();
            Logger.Fatal("Failed to create Glfw window");
            return;
        }

        GraphicsContext = new OpenGLContext(Logger, _glfwNativeWindowHandle);
        GraphicsContext.Initialize();

        Glfw.SetWindowCloseCallback(ref _glfwNativeWindowHandle, WindowCloseEventFn);
        Glfw.SetWindowFocusCallback(ref _glfwNativeWindowHandle, WindowFocusEventFn);
        Glfw.SetWindowResizeCallback(ref _glfwNativeWindowHandle, WindowResizeEventFn);

        Glfw.SetKeyCallback(ref _glfwNativeWindowHandle, KeyEventFn);
        Glfw.SetCharCallback(ref _glfwNativeWindowHandle, KeyTypedFn);

        Glfw.SetCursorPosCallback(ref _glfwNativeWindowHandle, MouseMovedEventFn);
        Glfw.SetMouseButtonCallback(ref _glfwNativeWindowHandle, MouseButtonEventFn);
        Glfw.SetScrollCallback(ref _glfwNativeWindowHandle, MouseScrollEventFn);
    }

    private void OnGlfwErrorFn(int errorCode, string description)
    {
        Logger.Error($"(Glfw) [{errorCode}] {description}");
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

    public override void Maximize()
    {
        Glfw.MaximizeWindow(_glfwNativeWindowHandle);
    }

    public override void Restore()
    {
        Glfw.RestoreWindow(_glfwNativeWindowHandle);
    }

    public override double GetTime()
    {
        return Glfw.GetTime();
    }

    public override void Shutdown()
    {
        Glfw.DestroyWindow(_glfwNativeWindowHandle);
        Glfw.Terminate();
    }

    public override void Update()
    {
        Glfw.SwapBuffers(_glfwNativeWindowHandle);
        Glfw.PollEvents();
    }

    public override object GetNativeWindowHandle()
    {
        return _glfwNativeWindowHandle;
    }
}
