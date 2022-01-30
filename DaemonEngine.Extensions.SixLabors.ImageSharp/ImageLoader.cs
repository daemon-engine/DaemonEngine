using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace DaemonEngine.Extensions.SixLabors.ImageSharp;

public class ImageLoader
{
    public static ImageData Load(string filepath, bool flipVertical = false)
    {
        Image<Rgba32> image = Image.Load<Rgba32>(filepath);
        image.Mutate(x => x.Flip(flipVertical ? FlipMode.Vertical : FlipMode.Horizontal));

        List<byte> pixels = new List<byte>(image.Width * image.Height);
        for (int y = 0; y < image.Height; y++)
        {
            var row = image.GetPixelRowSpan(y);
            for (int x = 0; x < image.Width; x++)
            {
                pixels.Add(row[x].R);
                pixels.Add(row[x].G);
                pixels.Add(row[x].B);
                pixels.Add(row[x].A);
            }
        }

        image.Dispose();

        return new ImageData
        {
            Pixels = pixels.ToArray(),
            BitPerPixel = image.PixelType.BitsPerPixel,
            Height = image.Height,
            Width = image.Width,
        };
    }
}
