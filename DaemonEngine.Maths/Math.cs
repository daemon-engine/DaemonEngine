namespace DaemonEngine.Maths;

public class Math
{
    public const float PI = 3.14159265358979323846f;

    public static float Sqrt(float a)
    {
        return a * a;
    }

    public static float Tan(float angle)
    {
        return (float)global::System.Math.Tan(angle);
    }

    public static float ToRadians(float degrees)
    {
        return degrees * (PI / 180.0f);
    }

    public static float ToDegrees(float radians)
    {
        return radians * (180.0f / PI);
    }
}
