using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaemonEngine.Graphics.Renderer;

public interface IUniformBuffer
{
    void SetData(float[] data, int size, int offset = 0);

    void Bind();
    void Unbind();
}

public abstract class UniformBufferBase : IUniformBuffer
{
    public UniformBufferBase()
    {
    }

    public abstract void SetData(float[] data, int size, int offset);

    public abstract void Bind();
    public abstract void Unbind();
}
