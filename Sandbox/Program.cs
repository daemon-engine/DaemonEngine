using DaemonEngine.DependencyInjection;
using DaemonEngine.Graphics.DependencyInjection;
using Sandbox;
using Sandbox.Layers;

var builder = new DaemonEngineContainerBuilder();
builder.RegisterApplication<Application>()
    .RegisterGraphicsModule(DaemonEngine.Graphics.Renderer.RendererApi.OpenGL)
    .RegisterLogging()
    .BuildAndRun();

/*
builder.RegisterApplication<Application>()
    .RegisterGraphicsModule(RendererApi)
    .RegisterLogging()
    .BuildAndRun();
 */
