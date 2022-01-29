using DaemonEngine.DependencyInjection;

using Sandbox;

var builder = new DaemonEngineContainerBuilder();
builder.RegisterApplication<Application>()
    .RegisterLogging()
    .BuildAndRun();
