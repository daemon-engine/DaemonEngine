using DaemonEngine.Extensions.OpenGL;
using DaemonEngine.Extensions.Runtime;
using DaemonEngine.Graphics.Renderer;

namespace DaemonEngine.Graphics.OpenGL.Renderer;

internal class OpenGLPipeline : IPipeline
{
    private readonly uint _id;

    public OpenGLPipeline(IShader shader)
    {
        Throw.IfNull(shader, nameof(shader));
        Shader = shader;

        uint[] ids = new uint[1];
        GL.GenVertexArrays(1, ref ids);
        _id = ids[0];
        GL.BindVertexArray(_id);
    }

    protected IShader Shader { get; }

    public void Bind()
    {
        Shader.Bind();
        GL.BindVertexArray(_id);
    }

    public void Unbind()
    {
        GL.BindVertexArray(0);
    }
}
