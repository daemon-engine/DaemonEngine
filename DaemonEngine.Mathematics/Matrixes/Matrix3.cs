namespace DaemonEngine.Mathematics;

public class Matrix3 : IEquatable<Matrix3>
{
    public Matrix3(Vector3 row0, Vector3 row1, Vector3 row2)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
    }

    public Matrix3(Matrix4 matrix)
    {
        Row0 = new Vector3(matrix.Row0.X, matrix.Row0.Y, matrix.Row0.Z);
        Row1 = new Vector3(matrix.Row1.X, matrix.Row1.Y, matrix.Row1.Z);
        Row2 = new Vector3(matrix.Row2.X, matrix.Row2.Y, matrix.Row2.Z);
    }

    public Vector3 Row0 { get; private set; }
    public Vector3 Row1 { get; private set; }
    public Vector3 Row2 { get; private set; }

    public static Matrix3 Zero => new(Vector3.Zero, Vector3.Zero, Vector3.Zero);
    public static Matrix3 Identity => new(Vector3.XAxis, Vector3.YAxis, Vector3.ZAxis);

    public override bool Equals(object? obj)
    {
        return obj is Matrix4 matrix && Equals(matrix);
    }

    public bool Equals(Matrix3 other)
    {
        return Row0 == other.Row0 && Row1 == other.Row1 && Row2 == other.Row2;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Row0, Row1, Row2);
    }
}