namespace DaemonEngine.Maths;

public class Vector2
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

    #region Math methods
    public Vector2 Add(Vector2 other)
    {
        X += other.X;
        Y += other.Y;

        return this;
    }

    public Vector2 Subtract(Vector2 other)
    {
        X -= other.X;
        Y -= other.Y;

        return this;
    }

    public Vector2 Multiply(Vector2 other)
    {
        X *= other.X;
        Y *= other.Y;

        return this;
    }

    public Vector2 Divide(Vector2 other)
    {
        X /= other.X;
        Y /= other.Y;

        return this;
    }
    #endregion

    #region Operators
    public static Vector2 operator +(Vector2 left, Vector2 right)
    {
        return left.Add(right);
    }

    public static Vector2 operator -(Vector2 left, Vector2 right)
    {
        return left.Subtract(right);
    }

    public static Vector2 operator *(Vector2 left, Vector2 right)
    {
        return left.Multiply(right);
    }

    public static Vector2 operator /(Vector2 left, Vector2 right)
    {
        return left.Divide(right);
    }
    #endregion
}
