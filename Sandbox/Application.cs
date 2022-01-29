﻿using DaemonEngine;
using DaemonEngine.Windows;
using DaemonEngine.Graphics.Renderer;
using Serilog;

// Temp
using DaemonEngine.Extensions.OpenGL;
using DaemonEngine.Extensions.OpenGL.Enums;
using DaemonEngine.Graphics.Factories;

namespace Sandbox;

public class Application : ApplicationBase
{
    Pipeline? _pipeline;
    IndexBuffer? _ibo;

    public Application(ILogger logger, IWindow window, IGLFactory factory)
        : base(logger, window)
    {
        GLFactory = factory;
    }

    protected IGLFactory GLFactory { get; }

    public override void OnStart()
    {
        Logger.Information("OnStart");

        float[] vertices = new float[4 * 3] {
             0.5f,  0.5f, 0.0f,  // top right
             0.5f, -0.5f, 0.0f,  // bottom right
            -0.5f, -0.5f, 0.0f,  // bottom left
            -0.5f,  0.5f, 0.0f   // top left 
        };
        uint[] indices = new uint[2 * 3] {  // note that we start from 0!
            0, 1, 3,   // first triangle
            1, 2, 3    // second triangle
        };

        _pipeline = GLFactory.CreatePipeline();
        _ = GLFactory.CreateVertexBuffer(12 * sizeof(float), vertices);

        // Must happend after VBO is bound
        GL.VertexAttribPointer(0, 3, GLConstants.GL_FLOAT, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        _ibo = GLFactory.CreateIndexBuffer(6, indices);
    }

    public override void OnShutdown()
    {
        Logger.Information("OnShutdown");
    }

    public override void OnUpdate(float deltaTime)
    {
        GL.ClearColor(0.8f, 0.5f, 0.3f, 1.0f);
        GL.Clear(GLClearMask.ColorBufferBit);

        _pipeline?.Bind();
        _ibo?.Bind();
        GL.DrawElements(GLConstants.GL_TRIANGLES, _ibo.Count, GLConstants.GL_UNSIGNED_INT);
    }
}
