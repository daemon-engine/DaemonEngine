using DaemonEngine.Extensions.OpenGL;
using Serilog;

namespace DaemonEngine.Graphics.Renderer;

public class Pipeline : GLBase
{
    private readonly uint _id;

    public Pipeline(ILogger logger)
        : base(logger)
    {

        uint[] ids = new uint[1];
        GL.GenVertexArrays(1, ref ids);
        _id = ids[0];
        GL.BindVertexArray(_id);
    }

    public override void Bind()
    {
        GL.BindVertexArray(_id);
    }

    public override void Unbind()
    {
        GL.BindVertexArray(0);
    }
}
