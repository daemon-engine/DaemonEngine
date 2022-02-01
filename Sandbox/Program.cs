using DaemonEngine.DependencyInjection;
using DaemonEngine.Graphics.DependencyInjection;
using Sandbox;

var builder = new DaemonEngineContainerBuilder();
builder.RegisterApplication<Application>()
    .RegisterWindow()
    .RegisterGraphicsModule(DaemonEngine.Graphics.Renderer.RendererApi.OpenGL)
    .RegisterLogging()
    .BuildAndRun();

/*
builder.RegisterApplication<Application>()
    .RegisterGraphicsModule(RendererApi)
    .RegisterLogging()
    .BuildAndRun();
 */
