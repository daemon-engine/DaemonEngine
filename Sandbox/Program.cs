using DaemonEngine.DependencyInjection;
using DaemonEngine.Graphics.Renderer;
using Sandbox;

var builder = new DaemonEngineContainerBuilder();
builder.RegisterApplication<Application>()
    .RegisterRenderer(RendererApi.OpenGL)
    .RegisterWindow()
    .RegisterLogging()
    .BuildAndRun();
