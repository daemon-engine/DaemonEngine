﻿using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.OpenGL.Renderer;
using DaemonEngine.Graphics.Renderer;
using Serilog;

namespace DaemonEngine.Graphics.OpenGL.Factories;

internal class OpenGLGraphicsFactory : IGraphicsFactory
{
    public OpenGLGraphicsFactory(ILogger logger)
    {
        Logger = logger;
    }

    protected ILogger Logger { get; }

    public IShader CreateShader()
    {
        return new OpenGLShader();
    }

    public IPipeline CreatePipeline(IShader shader)
    {
        return new OpenGLPipeline(shader);
    }

    public IVertexBuffer CreateVertexBuffer(int size, float[] vertices)
    {
        return new OpenGLVertexBuffer(size, vertices);
    }

    public IIndexBuffer CreateIndexBuffer(int count, uint[] indices)
    {
        return new OpenGLIndexBuffer(count, indices);
    }
}
