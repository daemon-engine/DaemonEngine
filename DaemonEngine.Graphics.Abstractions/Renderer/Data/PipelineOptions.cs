using DaemonEngine.Graphics.Renderer.Enums;

namespace DaemonEngine.Graphics.Renderer.Data;

public class PipelineOptions
{
    public IShader Shader { get; set; }
    public IBufferLayout BufferLayout { get; set; }
    public PrimitiveTopology PrimitiveTopology { get; set; } = PrimitiveTopology.Triangles;
    public bool Wireframe { get; set; } = false;
}
