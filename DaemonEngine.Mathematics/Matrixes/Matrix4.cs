namespace DaemonEngine.Mathematics;

public class Matrix4 : IEquatable<Matrix4>
{
    public Matrix4(Vector4 row0, Vector4 row1, Vector4 row2, Vector4 row3)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
        Row3 = row3;
    }

    public Matrix4(Matrix3 matrix)
    {
        Row0 = new Vector4(matrix.Row0.X, matrix.Row0.Y, matrix.Row0.Z, 0.0f);
        Row1 = new Vector4(matrix.Row1.X, matrix.Row1.Y, matrix.Row1.Z, 0.0f);
        Row2 = new Vector4(matrix.Row2.X, matrix.Row2.Y, matrix.Row2.Z, 0.0f);
        Row3 = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
    }

    public Vector4 Row0 { get; private set; }
    public Vector4 Row1 { get; private set; }
    public Vector4 Row2 { get; private set; }
    public Vector4 Row3 { get; private set; }

    public static Matrix4 Zero => new(Vector4.Zero, Vector4.Zero, Vector4.Zero, Vector4.Zero);
    public static Matrix4 Identity => new(Vector4.XAxis, Vector4.YAxis, Vector4.ZAxis, Vector4.WAxis);

    #region Static matrix4 math methods
    public static Matrix4 Translate(Vector3 translation)
    {
        var result = Identity;

        result.Row3.X = translation.X;
        result.Row3.Y = translation.Y;
        result.Row3.Z = translation.Z;

        return result;
    }

    public static Matrix4 Rotate(Vector3 eulerAngle)
    {
        var result = Identity;

        result *= RotateX(eulerAngle.X);
        result *= RotateY(eulerAngle.Y);
        result *= RotateZ(eulerAngle.Z);

        return result;
    }

    public static Matrix4 RotateX(float angle)
    {
        var cos = Maths.Cos(angle);
        var sin = Maths.Sin(angle);

        var result = Identity;
        result.Row1.Y = cos;
        result.Row1.Z = sin;
        result.Row2.Y = -sin;
        result.Row2.Z = cos;

        return result;
    }
    public static Matrix4 RotateY(float angle)
    {
        var cos = Maths.Cos(angle);
        var sin = Maths.Sin(angle);

        var result = Identity;
        result.Row0.X = cos;
        result.Row0.Z = -sin;
        result.Row2.X = sin;
        result.Row2.Z = cos;

        return result;
    }
    public static Matrix4 RotateZ(float angle)
    {
        var cos = Maths.Cos(angle);
        var sin = Maths.Sin(angle);

        var result = Identity;
        result.Row0.X = cos;
        result.Row0.Y = sin;
        result.Row1.X = -sin;
        result.Row1.Y = cos;

        return result;
    }

    public static Matrix4 Scale(Vector3 scale)
    {
        var result = Identity;

        result.Row0.X = scale.X;
        result.Row1.Y = scale.Y;
        result.Row2.Z = scale.Z;

        return result;
    }

    public static Matrix4 Scale(float scale)
    {
        var result = Identity;

        result.Row0.X = scale;
        result.Row1.Y = scale;
        result.Row2.Z = scale;

        return result;
    }

    public static Matrix4 Perspective(float fieldOfView, float aspectRatio, float near, float far)
    {
        var result = Identity;

        var maxY = near * Maths.Tan(0.5f * fieldOfView);
        var minY = -maxY;
        var minX = minY * aspectRatio;
        var maxX = maxY * aspectRatio;

        var x = 2.0f * near / (maxX - minX);
        var y = 2.0f * near / (maxY - minY);
        var a = (maxX + minX) / (maxX - minX);
        var b = (maxY + minY) / (maxY - minY);
        var c = -(far + near) / (far - near);
        var d = -(2.0f * far * near) / (far - near);

        result.Row0.X = x;
        result.Row0.Y = 0.0f;
        result.Row0.Z = 0.0f;
        result.Row0.W = 0.0f;
        result.Row1.X = 0.0f;
        result.Row1.Y = y;
        result.Row1.Z = 0.0f;
        result.Row1.W = 0.0f;
        result.Row2.X = a;
        result.Row2.Y = b;
        result.Row2.Z = c;
        result.Row2.W = -1.0f;
        result.Row3.X = 0.0f;
        result.Row3.Y = 0.0f;
        result.Row3.Z = d;
        result.Row3.W = 0.0f;

        return result;
    }

    public static Matrix4 Orthographic(float left, float right, float bottom, float top, float near, float far)
    {
        var result = Identity;

        var inverseRightLeft = 1.0f / (right - left);
        var inverseTopBottom = 1.0f / (top - bottom);
        var inverseFarNear = 1.0f / (far - near);

        result.Row0.X = 2 * inverseRightLeft;
        result.Row0.Y = 2 * inverseTopBottom;
        result.Row0.Z = 2 * inverseFarNear;

        result.Row3.X = -(right - left) * inverseRightLeft;
        result.Row3.Y = -(top - bottom) * inverseTopBottom;
        result.Row3.Z = -(far - near) * inverseFarNear;

        return result;
    }

    public static Matrix4 LookAt(Vector3 eye, Vector3 target, Vector3 up)
    {
        var z = Vector3.Normalize(eye - target);
        var x = Vector3.Normalize(Vector3.Cross(up, z));
        var y = Vector3.Normalize(Vector3.Cross(z, x));

        var result = Identity;

        result.Row0.X = x.X;
        result.Row0.Y = y.X;
        result.Row0.Z = z.X;
        result.Row0.W = 0.0f;
        result.Row1.X = x.Y;
        result.Row1.Y = y.Y;
        result.Row1.Z = z.Y;
        result.Row1.W = 0.0f;
        result.Row2.X = x.Z;
        result.Row2.Y = y.Z;
        result.Row2.Z = z.Z;
        result.Row2.W = 0.0f;
        result.Row3.X = -((x.X * eye.X) + (x.Y * eye.Y) + (x.Z * eye.Z));
        result.Row3.Y = -((y.X * eye.X) + (y.Y * eye.Y) + (y.Z * eye.Z));
        result.Row3.Z = -((z.X * eye.X) + (z.Y * eye.Y) + (z.Z * eye.Z));
        result.Row3.W = 1.0f;

        return result;
    }

    public static Matrix4 CreateFromAxisAngle(Vector3 axis, float angle)
    {
        var cos = Maths.Cos(angle);
        var sin = Maths.Sin(angle);
        var t = 1.0f / cos;

        axis.Normalize();

        var row0 = new Vector4(t * axis.X * axis.X + cos, t * axis.X * axis.Y - sin * axis.Z, t * axis.X * axis.Z + sin * axis.Y, 0.0f);
        var row1 = new Vector4(t * axis.X * axis.Y + sin * axis.Z, t * axis.Y * axis.Y + cos, t * axis.Y * axis.Z - sin * axis.X, 0.0f);
        var row2 = new Vector4(t * axis.X * axis.Z - sin * axis.Y, t * axis.Y * axis.Z + sin * axis.X, t * axis.Z * axis.Z + cos, 0.0f);
        var row3 = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);

        var result = new Matrix4(row0, row1, row2, row3);
        return result;
    }
    #endregion

    #region Math methods
    public Matrix4 Add(Matrix4 other)
    {
        Row0 += other.Row0;
        Row1 += other.Row1;
        Row2 += other.Row2;
        Row3 += other.Row3;

        return this;
    }

    public Matrix4 Subtract(Matrix4 other)
    {
        Row0 -= other.Row0;
        Row1 -= other.Row1;
        Row2 -= other.Row2;
        Row3 -= other.Row3;

        return this;
    }

    public Matrix4 Multiply(Matrix4 other)
    {
        float M11 = Row0.X;
        float M12 = Row0.Y;
        float M13 = Row0.Z;
        float M14 = Row0.W;
        float M21 = Row1.X;
        float M22 = Row1.Y;
        float M23 = Row1.Z;
        float M24 = Row1.W;
        float M31 = Row2.X;
        float M32 = Row2.Y;
        float M33 = Row2.Z;
        float M34 = Row2.W;
        float M41 = Row3.X;
        float M42 = Row3.Y;
        float M43 = Row3.Z;
        float M44 = Row3.W;
        float otherM11 = other.Row0.X;
        float otherM12 = other.Row0.Y;
        float otherM13 = other.Row0.Z;
        float otherM14 = other.Row0.W;
        float otherM21 = other.Row1.X;
        float otherM22 = other.Row1.Y;
        float otherM23 = other.Row1.Z;
        float otherM24 = other.Row1.W;
        float otherM31 = other.Row2.X;
        float otherM32 = other.Row2.Y;
        float otherM33 = other.Row2.Z;
        float otherM34 = other.Row2.W;
        float otherM41 = other.Row3.X;
        float otherM42 = other.Row3.Y;
        float otherM43 = other.Row3.Z;
        float otherM44 = other.Row3.W;

        Row0.X = (M11 * otherM11) + (M12 * otherM21) + (M13 * otherM31) + (M14 * otherM41);
        Row0.Y = (M11 * otherM12) + (M12 * otherM22) + (M13 * otherM32) + (M14 * otherM42);
        Row0.Z = (M11 * otherM13) + (M12 * otherM23) + (M13 * otherM33) + (M14 * otherM43);
        Row0.W = (M11 * otherM14) + (M12 * otherM24) + (M13 * otherM34) + (M14 * otherM44);
        Row1.X = (M21 * otherM11) + (M22 * otherM21) + (M23 * otherM31) + (M24 * otherM41);
        Row1.Y = (M21 * otherM12) + (M22 * otherM22) + (M23 * otherM32) + (M24 * otherM42);
        Row1.Z = (M21 * otherM13) + (M22 * otherM23) + (M23 * otherM33) + (M24 * otherM43);
        Row1.W = (M21 * otherM14) + (M22 * otherM24) + (M23 * otherM34) + (M24 * otherM44);
        Row2.X = (M31 * otherM11) + (M32 * otherM21) + (M33 * otherM31) + (M34 * otherM41);
        Row2.Y = (M31 * otherM12) + (M32 * otherM22) + (M33 * otherM32) + (M34 * otherM42);
        Row2.Z = (M31 * otherM13) + (M32 * otherM23) + (M33 * otherM33) + (M34 * otherM43);
        Row2.W = (M31 * otherM14) + (M32 * otherM24) + (M33 * otherM34) + (M34 * otherM44);
        Row3.X = (M41 * otherM11) + (M42 * otherM21) + (M43 * otherM31) + (M44 * otherM41);
        Row3.Y = (M41 * otherM12) + (M42 * otherM22) + (M43 * otherM32) + (M44 * otherM42);
        Row3.Z = (M41 * otherM13) + (M42 * otherM23) + (M43 * otherM33) + (M44 * otherM43);
        Row3.W = (M41 * otherM14) + (M42 * otherM24) + (M43 * otherM34) + (M44 * otherM44);

        return this;
    }
    #endregion

    #region Operators
    public static Matrix4 operator +(Matrix4 left, Matrix4 right)
    {
        return left.Add(right);
    }

    public static Matrix4 operator -(Matrix4 left, Matrix4 right)
    {
        return left.Subtract(right);
    }

    public static Matrix4 operator *(Matrix4 left, Matrix4 right)
    {
        return left.Multiply(right);
    }

    public static bool operator ==(Matrix4 left, Matrix4 right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Matrix4 left, Matrix4 right)
    {
        return !(left == right);
    }
    #endregion

    #region Conversion operators
    public static implicit operator System.Numerics.Matrix4x4(Matrix4 matrix)
    {
        return new System.Numerics.Matrix4x4(
            matrix.Row0.X, matrix.Row0.Y, matrix.Row0.Z, matrix.Row0.W,
            matrix.Row1.X, matrix.Row1.Y, matrix.Row1.Z, matrix.Row1.W,
            matrix.Row2.X, matrix.Row2.Y, matrix.Row2.Z, matrix.Row2.W,
            matrix.Row3.X, matrix.Row3.Y, matrix.Row3.Z, matrix.Row3.W
            );
    }

    public static explicit operator Matrix4(System.Numerics.Matrix4x4 matrix)
    {
        return new Matrix4(
            new Vector4(matrix.M11, matrix.M12, matrix.M13, matrix.M14),
            new Vector4(matrix.M21, matrix.M22, matrix.M23, matrix.M24),
            new Vector4(matrix.M31, matrix.M32, matrix.M33, matrix.M34),
            new Vector4(matrix.M41, matrix.M42, matrix.M43, matrix.M44)
            );
    }
    #endregion

    public override bool Equals(object? obj)
    {
        return obj is Matrix4 matrix && Equals(matrix);
    }

    public bool Equals(Matrix4 other)
    {
        return Row0 == other.Row0 && Row1 == other.Row1 && Row2 == other.Row2 && Row3 == other.Row3;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Row0, Row1, Row2, Row3);
    }
}
