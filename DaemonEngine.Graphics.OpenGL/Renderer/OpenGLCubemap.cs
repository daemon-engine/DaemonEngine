using DaemonEngine.Extensions.SixLabors.ImageSharp;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Graphics.Renderer.Data;
using DaemonEngine.OpenGL.DllImport;
using Serilog;

namespace DaemonEngine.Graphics.OpenGL.Renderer;

internal class OpenGLCubemap : CubemapBase
{
    private uint _id;

    public OpenGLCubemap(ILogger logger, CubemapOptions cubemapOptions)
        : base(logger, cubemapOptions)
    {
        CreateCubemap();
    }

    private void CreateCubemap()
    {
        uint[] ids = new uint[1];
        GL.GenTextures(1, ref ids);
        _id = ids[0];

        GL.BindTexture(GLConstants.GL_TEXTURE_CUBE_MAP, _id);

        uint index = 0;
        foreach (var path in Options.Paths)
        {
            var imageData = ImageLoader.Load(path);

            var internalFormat = GLConstants.GL_RGBA8;
            var format = GLConstants.GL_RGBA;
            var type = GLConstants.GL_UNSIGNED_BYTE;
            GL.TexImage2D(GLConstants.GL_TEXTURE_CUBE_MAP_POSITIVE_X + index, 0, internalFormat, imageData.Width, imageData.Height, 0, format, type, imageData.Pixels);
            index++;
        }

        GL.TexParameteri(GLConstants.GL_TEXTURE_CUBE_MAP, GLConstants.GL_TEXTURE_MIN_FILTER, GLConstants.GL_LINEAR);
        GL.TexParameteri(GLConstants.GL_TEXTURE_CUBE_MAP, GLConstants.GL_TEXTURE_MAG_FILTER, GLConstants.GL_LINEAR);
        GL.TexParameteri(GLConstants.GL_TEXTURE_CUBE_MAP, GLConstants.GL_TEXTURE_WRAP_R, GLConstants.GL_CLAMP_TO_EDGE);
        GL.TexParameteri(GLConstants.GL_TEXTURE_CUBE_MAP, GLConstants.GL_TEXTURE_WRAP_S, GLConstants.GL_CLAMP_TO_EDGE);
        GL.TexParameteri(GLConstants.GL_TEXTURE_CUBE_MAP, GLConstants.GL_TEXTURE_WRAP_T, GLConstants.GL_CLAMP_TO_EDGE);
    }

    public override void Bind()
    {
        GL.DepthFunc(GLConstants.GL_LEQUAL);
        //GL.DepthMask(false);
        GL.ActiveTexture(GLConstants.GL_TEXTURE0);
        GL.BindTexture(GLConstants.GL_TEXTURE_CUBE_MAP, _id);
    }

    public override void Unbind()
    {
        GL.BindTexture(GLConstants.GL_TEXTURE_CUBE_MAP, 0);
        //GL.DepthMask(true);
        GL.DepthFunc(GLConstants.GL_LESS);
    }
}
