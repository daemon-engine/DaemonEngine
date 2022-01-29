using DaemonEngine.Extensions.Glfw;
using DaemonEngine.Extensions.Glfw.Structs;
using DaemonEngine.Windows;
using Serilog;

namespace DaemonEngine.Graphics.Windows
{
    internal class GLWindow : IWindow
    {
        private readonly GlfwWindow _glfwWindow;
        private readonly WindowOptions _windowOptions;

        public GLWindow(ILogger logger, WindowOptions windowOptions)
        {
            Logger = logger;
            _windowOptions = windowOptions;

            if (string.IsNullOrWhiteSpace(_windowOptions.Title))
            {
                Logger.Warning("Title for Windows has not been set!");
                _windowOptions.Title = "Glfw Window";
            }

            if (!Glfw.Init())
            {
                Logger.Fatal("Failed to initialize Glfw");
                return;
            }

            _glfwWindow = Glfw.CreateWindow(_windowOptions.Width, _windowOptions.Height, _windowOptions.Title);
            if (_glfwWindow.Equals(null))
            {
                Glfw.Terminate();
                Logger.Fatal("Failed to create Glfw window");
                return;
            }

            Glfw.MakeContextCurrent(_glfwWindow);
        }

        protected ILogger Logger { get; }

        public bool IsRunning()
        {
            return Glfw.WindowShouldClose(_glfwWindow);
        }

        public void Update()
        {
            if (Glfw.IsKeyPressed(_glfwWindow, GlfwConstants.GLFW_KEY_ESCAPE))
            {
                Glfw.SetWindowShouldClose(_glfwWindow, GlfwConstants.GLFW_TRUE);
            }

            Glfw.SwapBuffers(_glfwWindow);
            Glfw.PollEvents();
        }
    }
}
