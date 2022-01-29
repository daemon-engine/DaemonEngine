﻿using DaemonEngine.Graphics.Renderer;

namespace DaemonEngine.Graphics.Factories;

public interface IGLFactory
{
    Pipeline CreatePipeline();
    VertexBuffer CreateVertexBuffer(int size, float[] vertices);
    IndexBuffer CreateIndexBuffer(int count, uint[] indices);
}
