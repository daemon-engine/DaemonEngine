using DaemonEngine.Mathematics;

namespace DaemonEngine.Graphics.Renderer;

public interface IShader
{
    void Bind();
    void Unbind();

    void SetInt(string name, int value);
    void SetFloat(string name, float value);
    void SetFloat2(string name, Vector2 values);
    void SetFloat2(string name, float v0, float v1);
    void SetFloat3(string name, Vector3 values);
    void SetFloat3(string name, float v0, float v1, float v2);
    void SetFloat4(string name, Vector4 values);
    void SetFloat4(string name, float v0, float v1, float v2, float v3);
    void SetMat4(string name, Matrix4 matrix, bool transpose = false);
}
