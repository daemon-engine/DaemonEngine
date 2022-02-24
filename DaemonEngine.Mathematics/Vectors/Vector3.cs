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
    public static Vector3 ToEulerAngles(Quaternion quaternion)
    {
        var result = Zero;

        // roll (x-axis rotation)
        var sinr_cosp = 2.0f * (quaternion.W * quaternion.X + quaternion.Y * quaternion.Z);
        var cosr_cosp = 1.0f - 2.0f * (quaternion.X * quaternion.X + quaternion.Y * quaternion.Y);
        result.X = Maths.Atan2(sinr_cosp, cosr_cosp);

        // pitch (y-axis rotation)
        var sinp = 2.0f * (quaternion.W * quaternion.Y - quaternion.Z * quaternion.X);
        if (Maths.Abs(sinp) >= 1)
        {

            result.Y = Maths.CopySign(Maths.PI / 2, sinp); // use 90 degrees if out of range
        }
        else
        {
            result.Y = Maths.Asin(sinp);
        }

        // yaw (z-axis rotation)
        var siny_cosp = 2.0f * (quaternion.W * quaternion.Z + quaternion.X * quaternion.Y);
        var cosy_cosp = 1.0f - 2.0f * (quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z);
        result.Z = Maths.Atan2(siny_cosp, cosy_cosp);

        return result;
    }

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
        var t0 = 2.0f * (q.W * q.X + q.Y * q.Z);
        var t1 = 1.0f - 2.0f * (q.X * q.X + q.Y * q.Y);
        var roll = Maths.Atan2(t0, t1);

        var t2 = 2.0f * (q.W * q.Y - q.Z * q.X);
        t2 = ((t2 > 1.0f) ? 1.0f : t2);
        t2 = ((t2 < -1.0f) ? -1.0f : t2);
        var pitch = Maths.Asin(t2);

        var t3 = 2.0f * (q.W * q.Y - q.Z * q.X);
        var t4 = 1.0f - 2.0f * (q.Y * q.Y + q.Z * q.Z);
        var yaw = Maths.Atan2(t3, t4);

        return new Vector3(roll, pitch, yaw);
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

    public bool Equals(Vector3? other)
    {
        return other is not null && X == other.X && Y == other.Y && Z == other.Z;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }
}
