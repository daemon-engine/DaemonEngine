﻿using DaemonEngine.Graphics.Renderer;

namespace DaemonEngine.Graphics.Factories;

public interface IGraphicsFactory
{
    IShader CreateShader(string filepath);

    IPipeline CreatePipeline(IShader shader, IBufferLayout bufferLayout);
    IVertexBuffer CreateVertexBuffer(int size, float[] vertices);
    IIndexBuffer CreateIndexBuffer(int count, uint[] indices);
}
