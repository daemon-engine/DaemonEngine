namespace DaemonEngine.Mathematics;

public static class Matrix4Extensions
{
    public static float[] ToFloatArray(this Matrix4 matrix)
    {
        var result = new float[4 * 4]
        {
            matrix.Row0.X, matrix.Row0.Y, matrix.Row0.Z, matrix.Row0.W,
            matrix.Row1.X, matrix.Row1.Y, matrix.Row1.Z, matrix.Row1.W,
            matrix.Row2.X, matrix.Row2.Y, matrix.Row2.Z, matrix.Row2.W,
            matrix.Row3.X, matrix.Row3.Y, matrix.Row3.Z, matrix.Row3.W
        };
        return result;
    }
}
