namespace DaemonEngine.Maths;

public class Vector3 : IEquatable<Vector3>
{
    public static readonly Vector3 Zero = new(0.0f, 0.0f, 0.0f);
    public static readonly Vector3 One = new(1.0f, 1.0f, 1.0f);

    public static readonly Vector3 XAxis = new(1.0f, 0.0f, 0.0f);
    public static readonly Vector3 YAxis = new(0.0f, 1.0f, 0.0f);
    public static readonly Vector3 ZAxis = new(0.0f, 0.0f, 1.0f);

    public Vector3()
    {
        X = default;
        Y = default;
        Z = default;
    }

    public Vector3(float scalar)
    {
        X = scalar;
        Y = scalar;
        Z = scalar;
    }

    public Vector3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public float Length => Math.Sqrt((X * X) + (Y * Y) + (Z * Z));

    public Vector3 Normalize()
    {
        var scale = 1.0f / Length;

        X *= scale;
        Y *= scale;
        Z *= scale;

        return this;
    }

    #region Static vector math methods
    public static Vector3 Lerp(Vector3 a, Vector3 b, float time)
    {
        a.X = (time * (b.X - a.X)) + a.X;
        a.Y = (time * (b.Y - a.Y)) + a.Y;
        a.Z = (time * (b.Z - a.Z)) + a.Z;

        return a;
    }

    public static Vector3 Cross(Vector3 left, Vector3 right)
    {
        return new Vector3
        {
            X = (left.Y * right.Z) - (left.Z * right.Y),
            Y = (left.Z * right.X) - (left.X * right.Z),
            Z = (left.X * right.Y) - (left.Y * right.X)
        };
    }

    public static float Dot(Vector3 left, Vector3 right)
    {
        return (left.X * right.X) + (left.Y * right.Y) + (left.Z * right.Z);
    }

    public static float Distance(Vector3 left, Vector3 right)
    {
        return Math.Sqrt(((right.X - left.X) * (right.X - left.X)) + ((right.Y - left.Y) * (right.Y - left.Y)) + ((right.Z - left.Z) * (right.Z - left.Z)));
    }
    #endregion

    #region Math methods
    public Vector3 Add(Vector3 other)
    {
        X += other.X;
        Y += other.Y;
        Z += other.Z;

        return this;
    }

    public Vector3 Subtract(Vector3 other)
    {
        X -= other.X;
        Y -= other.Y;
        Z -= other.Z;

        return this;
    }

    public Vector3 Multiply(Vector3 other)
    {
        X *= other.X;
        Y *= other.Y;
        Z *= other.Z;

        return this;
    }

    public Vector3 Divide(Vector3 other)
    {
        X /= other.X;
        Y /= other.Y;
        Z /= other.Z;

        return this;
    }

    public Vector3 Multiply(Matrix4 transform)
    {
        return new Vector3
        {
            X = transform.Rows[0].X * X + transform.Rows[0].Y * Y + transform.Rows[0].Z * Z + transform.Rows[0].W,
            Y = transform.Rows[1].X * X + transform.Rows[1].Y * Y + transform.Rows[1].Z * Z + transform.Rows[1].W,
            Z = transform.Rows[2].X * X + transform.Rows[2].Y * Y + transform.Rows[2].Z * Z + transform.Rows[2].W
        };
    }

    public Vector3 Add(float value)
    {
        X += value;
        Y += value;
        Z += value;

        return this;
    }

    public Vector3 Subtract(float value)
    {
        X -= value;
        Y -= value;
        Z -= value;

        return this;
    }

    public Vector3 Multiply(float value)
    {
        X *= value;
        Y *= value;
        Z *= value;

        return this;
    }

    public Vector3 Divide(float value)
    {
        X /= value;
        Y /= value;
        Z /= value;

        return this;
    }
    #endregion

    #region Operators
    public static Vector3 operator +(Vector3 left, Vector3 right)
    {
        return left.Add(right);
    }

    public static Vector3 operator -(Vector3 left, Vector3 right)
    {
        return left.Subtract(right);
    }

    public static Vector3 operator *(Vector3 left, Vector3 right)
    {
        return left.Multiply(right);
    }

    public static Vector3 operator /(Vector3 left, Vector3 right)
    {
        return left.Divide(right);
    }

    public static Vector3 operator +(Vector3 left, float right)
    {
        return left.Add(right);
    }

    public static Vector3 operator -(Vector3 left, float right)
    {
        return left.Subtract(right);
    }

    public static Vector3 operator *(Vector3 left, float right)
    {
        return left.Multiply(right);
    }

    public static Vector3 operator /(Vector3 left, float right)
    {
        return left.Divide(right);
    }
    #endregion

    public override bool Equals(object? obj)
    {
        return obj is Vector3 vector && Equals(vector);
    }

    public bool Equals(Vector3 other)
    {
        return X == other.X && Y == other.Y && Z == other.Z;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }
}
