namespace DaemonEngine.Maths;

public class Vector4
{
    public Vector4()
    {
        X = default;
        Y = default;
        Z = default;
        W = default;
    }

    public Vector4(float scalar)
    {
        X = scalar;
        Y = scalar;
        Z = scalar;
        W = scalar;
    }

    public Vector4(float x, float y, float z, float w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
    public float W { get; set; }

    #region Math methods
    public Vector4 Add(Vector4 other)
    {
        X += other.X;
        Y += other.Y;
        Z += other.Z;
        W += other.W;

        return this;
    }

    public Vector4 Subtract(Vector4 other)
    {
        X -= other.X;
        Y -= other.Y;
        Z -= other.Z;
        W -= other.W;

        return this;
    }

    public Vector4 Multiply(Vector4 other)
    {
        X *= other.X;
        Y *= other.Y;
        Z *= other.Z;
        W *= other.W;

        return this;
    }

    public Vector4 Divide(Vector4 other)
    {
        X /= other.X;
        Y /= other.Y;
        Z /= other.Z;
        W /= other.W;

        return this;
    }
    #endregion

    #region Operators
    public static Vector4 operator +(Vector4 left, Vector4 right)
    {
        return left.Add(right);
    }

    public static Vector4 operator -(Vector4 left, Vector4 right)
    {
        return left.Subtract(right);
    }

    public static Vector4 operator *(Vector4 left, Vector4 right)
    {
        return left.Multiply(right);
    }

    public static Vector4 operator /(Vector4 left, Vector4 right)
    {
        return left.Divide(right);
    }
    #endregion
}
