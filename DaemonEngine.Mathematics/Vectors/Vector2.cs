namespace DaemonEngine.Mathematics;

public class Vector2 : IEquatable<Vector2>
{
    public Vector2()
    {
        X = default;
        Y = default;
    }

    public Vector2(float scalar)
    {
        X = scalar;
        Y = scalar;
    }

    public Vector2(float x, float y)
    {
        X = x;
        Y = y;
    }

    public float X { get; set; }
    public float Y { get; set; }

    public static Vector2 Zero => new(0, 0);
    public static Vector2 One => new(1, 1);

    public float Length => Maths.Sqrt((X * X) + (Y * Y));

    public Vector2 Normalize()
    {
        var scale = 1.0f / Length;

        X *= scale;
        Y *= scale;

        return this;
    }

    #region Static vector math methods
    public static Vector2 Lerp(Vector2 a, Vector2 b, float time)
    {
        a.X = (time * (b.X - a.X)) + a.X;
        a.Y = (time * (b.Y - a.Y)) + a.Y;

        return a;
    }

    public static float Dot(Vector2 left, Vector2 right)
    {
        return (left.X * right.X) + (left.Y * right.Y);
    }

    public static float Distance(Vector2 vec1, Vector2 vec2)
    {
        return Maths.Sqrt((vec2.X - vec1.X) * (vec2.X - vec1.X) + (vec2.Y - vec1.Y) * (vec2.Y - vec1.Y));
    }
    #endregion

    #region Operators
    public static Vector2 operator *(Vector2 left, float right)
    {
        var result = new Vector2
        {
            X = left.X * right,
            Y = left.Y * right
        };

        return result;
    }

    public static Vector2 operator *(float right, Vector2 left)
    {
        var result = new Vector2
        {
            X = left.X * right,
            Y = left.Y * right
        };

        return result;
    }

    public static Vector2 operator +(Vector2 left, Vector2 right)
    {
        var result = new Vector2
        {
            X = left.X + right.X,
            Y = left.Y + right.Y
        };

        return result;
    }

    public static Vector2 operator -(Vector2 left, Vector2 right)
    {
        var result = new Vector2
        {
            X = left.X - right.X,
            Y = left.Y - right.Y
        };

        return result;
    }

    public static Vector2 operator *(Vector2 left, Vector2 right)
    {
        var result = new Vector2
        {
            X = left.X * right.X,
            Y = left.Y * right.Y
        };

        return result;
    }

    public static Vector2 operator /(Vector2 left, Vector2 right)
    {
        var result = new Vector2
        {
            X = left.X / right.X,
            Y = left.Y / right.Y
        };

        return result;
    }

    public static Vector2 operator -(Vector2 vector)
    {
        vector.X = -vector.X;
        vector.Y = -vector.Y;
        return vector;
    }

    public static bool operator ==(Vector2 left, Vector2 right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Vector2 left, Vector2 right)
    {
        return !(left == right);
    }
    #endregion

    public override bool Equals(object? obj)
    {
        return obj is Vector2 vector && Equals(vector);
    }

    public bool Equals(Vector2 other)
    {
        return X == other.X && Y == other.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}
