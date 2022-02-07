namespace DaemonEngine.Maths;

public class Vector3
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

    #region Vector3 math
    public Vector3 Cross(Vector3 other)
    {
        return new Vector3(Y * other.Z - Z * other.Y, Z * other.X - X * other.Z, X * other.Y - Y * other.X);
    }

    public Vector3 Normalize()
    {
        float length = Magnitude();
        return new Vector3(X / length, Y / length, Z / length);
    }

    public float Magnitude()
    {
        return Math.Sqrt(X * X + Y * Y + Z * Z);
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
}
