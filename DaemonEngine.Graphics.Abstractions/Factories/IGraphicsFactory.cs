﻿using DaemonEngine.Graphics.Renderer;

namespace DaemonEngine.Graphics.Factories;

public interface IGraphicsFactory
{
    IShader CreateShader();

    IPipeline CreatePipeline(IShader shader);
    IVertexBuffer CreateVertexBuffer(int size, float[] vertices);
    IIndexBuffer CreateIndexBuffer(int count, uint[] indices);
}
