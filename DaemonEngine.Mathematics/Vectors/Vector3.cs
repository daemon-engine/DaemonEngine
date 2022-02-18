namespace DaemonEngine.Mathematics;

public class Vector3 : IEquatable<Vector3>
{
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

    public static Vector3 Zero => new(0, 0, 0);
    public static Vector3 One => new(1, 1, 1);

    public static Vector3 XAxis => new(1, 0, 0);
    public static Vector3 YAxis => new(0, 1, 0);
    public static Vector3 ZAxis => new(0, 0, 1);

    public float Length => MathF.Sqrt((X * X) + (Y * Y) + (Z * Z));
    public float LengthSquared => (X * X) + (Y * Y) + (Z * Z);

    public Vector3 Normalize()
    {
        var scale = 1.0f / Length;

        X *= scale;
        Y *= scale;
        Z *= scale;

        return this;
    }

    #region Static vector math methods
    public static Vector3 Normalize(Vector3 vector)
    {
        var scale = 1.0f / vector.Length;

        vector.X *= scale;
        vector.Y *= scale;
        vector.Z *= scale;

        return vector;
    }

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
        return Maths.Sqrt(((right.X - left.X) * (right.X - left.X)) + ((right.Y - left.Y) * (right.Y - left.Y)) + ((right.Z - left.Z) * (right.Z - left.Z)));
    }
    #endregion

    #region Operators
    public static Vector3 operator +(Vector3 left, Vector3 right)
    {
        var result = new Vector3
        {
            X = left.X + right.X,
            Y = left.Y + right.Y,
            Z = left.Z + right.Z
        };

        return result;
    }

    public static Vector3 operator -(Vector3 left, Vector3 right)
    {
        var result = new Vector3
        {
            X = left.X - right.X,
            Y = left.Y - right.Y,
            Z = left.Z - right.Z
        };

        return result;
    }

    public static Vector3 operator *(Vector3 left, Vector3 right)
    {
        var result = new Vector3
        {
            X = left.X * right.X,
            Y = left.Y * right.Y,
            Z = left.Z * right.Z
        };

        return result;
    }

    public static Vector3 operator /(Vector3 left, Vector3 right)
    {
        var result = new Vector3
        {
            X = left.X / right.X,
            Y = left.Y / right.Y,
            Z = left.Z / right.Z
        };

        return result;
    }

    public static Vector3 operator +(Vector3 left, float right)
    {
        var result = new Vector3
        {
            X = left.X + right,
            Y = left.Y + right,
            Z = left.Z + right
        };

        return result;
    }

    public static Vector3 operator -(Vector3 left, float right)
    {
        var result = new Vector3
        {
            X = left.X - right,
            Y = left.Y - right,
            Z = left.Z - right
        };

        return result;
    }

    public static Vector3 operator *(Vector3 left, float right)
    {
        var result = new Vector3
        {
            X = left.X * right,
            Y = left.Y * right,
            Z = left.Z * right
        };

        return result;
    }

    public static Vector3 operator *(float left, Vector3 right)
    {
        var result = new Vector3
        {
            X = left * right.X,
            Y = left * right.Y,
            Z = left * right.Z
        };

        return result;
    }

    public static Vector3 operator /(Vector3 left, float right)
    {
        var result = new Vector3
        {
            X = left.X / right,
            Y = left.Y / right,
            Z = left.Z / right
        };

        return result;
    }

    public static Vector3 operator -(Vector3 vector)
    {
        var result = new Vector3
        {
            X = -vector.X,
            Y = -vector.Y,
            Z = -vector.Z
        };

        return result;
    }
    #endregion

    #region Conversion operators
    public static implicit operator Vector3(System.Numerics.Quaternion q)
    {
        var result = Zero;

        var test = q.X * q.Y + q.Z * q.W;
        var sqx = q.X * q.X;
        var sqy = q.Y * q.Y;
        var sqz = q.Z * q.Z;

        result.X = Maths.Atan2(2 * q.Y * q.W - 2 * q.X * q.Z, 1 - 2 * sqy - 2 * sqz);
        result.Y = Maths.Asin(2 * test);
        result.Z = Maths.Atan2(2 * q.X * q.W - 2 * q.Y * q.Z, 1 - 2 * sqx - 2 * sqz);

        return result;
    }

    public static implicit operator Vector3(System.Numerics.Vector3 v)
    {
        return new Vector3(v.X, v.Y, v.Z);
    }

    public static implicit operator System.Numerics.Vector3(Vector3 v)
    {
        return new System.Numerics.Vector3(v.X, v.Y, v.Z);
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
