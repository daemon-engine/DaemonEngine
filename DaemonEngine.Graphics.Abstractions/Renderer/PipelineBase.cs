﻿using DaemonEngine.Graphics.Renderer.Data;
using Serilog;

namespace DaemonEngine.Graphics.Renderer;

public interface IPipeline
{
    void Bind();
    void Unbind();
}

public abstract class PipelineBase : IPipeline
{
    public PipelineBase(ILogger logger, PipelineOptions pipelineOptions)
    {
        Logger = logger;
        Options = pipelineOptions;
    }

    protected ILogger Logger { get; }
    protected PipelineOptions Options { get; }

    public abstract void Bind();
    public abstract void Unbind();
}
