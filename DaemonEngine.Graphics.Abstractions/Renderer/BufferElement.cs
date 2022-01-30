using DaemonEngine.Graphics.Helpers;

namespace DaemonEngine.Graphics.Renderer;

public enum ShaderDataType
{
    None = 0,
    Bool,
    Mat3, Mat4,
    Float, Float2, Float3, Float4,
    Int, Int2, Int3, Int4,
}

public class BufferElement
{
    public BufferElement(string name, ShaderDataType type, bool normalized = false)
    {
        Name = name;
        Type = type;
        Offset = 0;
        Size = ShaderHelper.ShaderDataTypeSize(Type);
        Normalized = normalized;
    }

    public string? Name { get; set; }
    public ShaderDataType Type { get; set; }
    public int Offset { get; set; }
    public int Size { get; set; }
    public bool Normalized { get; set; }

    public int GetComponentCount()
    {
        return Type switch
        {
            ShaderDataType.Float => 1,
            ShaderDataType.Float2 => 2,
            ShaderDataType.Float3 => 3,
            ShaderDataType.Float4 => 4,
            ShaderDataType.Int => 1,
            ShaderDataType.Int2 => 2,
            ShaderDataType.Int3 => 3,
            ShaderDataType.Int4 => 4,
            ShaderDataType.Mat3 => 3 * 3,
            ShaderDataType.Mat4 => 4 * 4,
            ShaderDataType.Bool => 1,
            _ => 0,
        };
    }
}
