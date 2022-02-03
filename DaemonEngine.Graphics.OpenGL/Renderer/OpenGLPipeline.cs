using DaemonEngine.Extensions.Runtime;
using DaemonEngine.Graphics.Helpers;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.OpenGL.DllImport;

namespace DaemonEngine.Graphics.OpenGL.Renderer;

internal class OpenGLPipeline : IPipeline
{
    private readonly uint _id;

    public OpenGLPipeline(IShader shader, IBufferLayout bufferLayout)
    {
        Throw.IfNull(shader, nameof(shader));
        Throw.IfNull(bufferLayout, nameof(bufferLayout));

        Shader = shader;
        BufferLayout = bufferLayout;

        uint[] ids = new uint[1];
        GL.GenVertexArrays(1, ref ids);
        _id = ids[0];
        GL.BindVertexArray(_id);
    }

    protected IShader Shader { get; }
    protected IBufferLayout BufferLayout { get; }

    public void Bind()
    {
        Shader.Bind();
        GL.BindVertexArray(_id);

        uint index = 0;
        foreach (var element in BufferLayout.Elements)
        {
            var componentSize = element.GetComponentCount();
            var glType = ShaderHelper.ShaderDataTypeToOpenGLBaseType(element.Type);

            GL.EnableVertexAttribArray(index);
            GL.VertexAttribPointer(index, componentSize, glType, element.Normalized, BufferLayout.Stride, element.Offset);

            index++;
        }
    }

    public void Unbind()
    {
        GL.BindVertexArray(0);
    }
}
