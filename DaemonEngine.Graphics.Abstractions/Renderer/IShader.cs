using System.Numerics;

namespace DaemonEngine.Graphics.Renderer;

public interface IShader
{
    void Bind();
    void Unbind();

    void SetInt(string name, int value);
    void SetMat4(string name, Matrix4x4 matrix, bool transpose = false);
}
