using DaemonEngine.DependencyInjection;
using DaemonEngine.Graphics.DependencyInjection;
using Sandbox;

var builder = new DaemonEngineContainerBuilder();
builder.RegisterApplication<Application>()
    .RegisterGraphicsModule(DaemonEngine.Graphics.Renderer.RendererApi.OpenGL)
    .RegisterLogging()
    .BuildAndRun();
