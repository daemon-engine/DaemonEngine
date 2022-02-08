namespace DaemonEngine.Maths;

public class Vector4 : IEquatable<Vector4>
{
    public static readonly Vector4 Zero = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
    public static readonly Vector4 One = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);

    public static readonly Vector4 XAxis = new(1.0f, 0.0f, 0.0f, 0.0f);
    public static readonly Vector4 YAxis = new(0.0f, 1.0f, 0.0f, 0.0f);
    public static readonly Vector4 ZAxis = new(0.0f, 0.0f, 1.0f, 0.0f);
    public static readonly Vector4 WAxis = new(0.0f, 0.0f, 0.0f, 1.0f);

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

    public float Length => Math.Sqrt((X * X) + (Y * Y) + (Z * Z) + (W * W));

    public Vector4 Normalize()
    {
        var scale = 1.0f / Length;

        X *= scale;
        Y *= scale;
        Z *= scale;
        W *= scale;

        return this;
    }

    #region Static vector math methods
    public static Vector4 Lerp(Vector4 a, Vector4 b, float time)
    {
        a.X = (time * (b.X - a.X)) + a.X;
        a.Y = (time * (b.Y - a.Y)) + a.Y;
        a.Z = (time * (b.Z - a.Z)) + a.Z;
        a.W = (time * (b.W - a.W)) + a.W;

        return a;
    }

    public static float Dot(Vector4 left, Vector4 right)
    {
        return (left.X * right.X) + (left.Y * right.Y) + (left.Z * right.Z) + (left.W * right.W);
    }
    #endregion

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

    public override bool Equals(object? obj)
    {
        return obj is Vector4 vector && Equals(vector);
    }

    public bool Equals(Vector4 other)
    {
        return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z, W);
    }
}
