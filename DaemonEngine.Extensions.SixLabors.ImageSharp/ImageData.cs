namespace DaemonEngine.Extensions.SixLabors.ImageSharp;

public struct ImageData
{
    public byte[] Pixels { get; set; }
    public int BitPerPixel { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}
