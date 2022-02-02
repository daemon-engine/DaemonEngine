using System.Numerics;

namespace DaemonEngine.Graphics.Renderer;

public interface IShader
{
    void Bind();
    void Unbind();

    void SetInt(string name, int value);
    void SetFloat(string name, float value);
    void SetFloat2(string name, float v0, float v1);
    void SetFloat3(string name, float v0, float v1, float v2);
    void SetMat4(string name, Matrix4x4 matrix, bool transpose = false);
}
