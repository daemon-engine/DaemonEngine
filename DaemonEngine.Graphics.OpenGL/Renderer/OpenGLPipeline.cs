﻿using DaemonEngine.Extensions.Runtime;
using DaemonEngine.Graphics.Helpers;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Graphics.Renderer.Data;
using DaemonEngine.OpenGL.DllImport;
using Serilog;

namespace DaemonEngine.Graphics.OpenGL.Renderer;

internal class OpenGLPipeline : PipelineBase, IPipeline
{
    private readonly uint _id;

    public OpenGLPipeline(ILogger logger, PipelineOptions pipelineOptions)
        : base(logger, pipelineOptions)
    {
        uint[] ids = new uint[1];
        GL.GenVertexArrays(1, ref ids);
        _id = ids[0];
        GL.BindVertexArray(_id);
    }

    public override void Bind()
    {
        Options.Shader.Bind();
        GL.BindVertexArray(_id);

        SetupBufferLayout();
    }

    public override void Unbind()
    {
        GL.BindVertexArray(0);
    }

    private void SetupBufferLayout()
    {
        uint index = 0;
        var layout = Options.BufferLayout;
        foreach (var element in layout.Elements)
        {
            var componentCount = element.GetComponentCount();
            var glType = ShaderHelper.ShaderDataTypeToOpenGLBaseType(element.Type);

            GL.EnableVertexAttribArray(index);
            GL.VertexAttribPointer(index, componentCount, glType, element.Normalized, layout.Stride, element.Offset);

            index++;
        }
    }
}
