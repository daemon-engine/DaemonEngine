using DaemonEngine.Extensions.Glfw;
using DaemonEngine.Extensions.Glfw.Structs;
using DaemonEngine.Windows;
using Serilog;

namespace DaemonEngine.Graphics.Windows
{
    internal class GLWindow : WindowBase
    {
        private GlfwWindow _glfwWindow;

        public GLWindow(ILogger logger, WindowOptions windowOptions)
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

            _glfwWindow = Glfw.CreateWindow(WindowOptions.Width, WindowOptions.Height, WindowOptions.Title);
            if (_glfwWindow.Equals(null))
            {
                Glfw.Terminate();
                Logger.Fatal("Failed to create Glfw window");
                return;
            }

            Glfw.MakeContextCurrent(_glfwWindow);
        }

        public override bool IsRunning()
        {
            return Glfw.WindowShouldClose(_glfwWindow);
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
            if (Glfw.IsKeyPressed(_glfwWindow, GlfwConstants.GLFW_KEY_ESCAPE))
            {
                Glfw.SetWindowShouldClose(_glfwWindow, GlfwConstants.GLFW_TRUE);
            }

            Glfw.SwapBuffers(_glfwWindow);
            Glfw.PollEvents();
        }
    }
}
