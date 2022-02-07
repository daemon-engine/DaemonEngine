using System.Numerics;

namespace DaemonEngine.Maths;

public class Matrix4
{
    Vector4[] _rows = new Vector4[4];

    public Matrix4()
    {
        _rows[0] = new Vector4();
        _rows[1] = new Vector4();
        _rows[2] = new Vector4();
        _rows[3] = new Vector4();
    }

    public Matrix4(float diagonal)
    {
        _rows[0] = new Vector4(diagonal, 0.0f, 0.0f, 0.0f);
        _rows[1] = new Vector4(0.0f, diagonal, 0.0f, 0.0f);
        _rows[2] = new Vector4(0.0f, 0.0f, diagonal, 0.0f);
        _rows[3] = new Vector4(0.0f, 0.0f, 0.0f, diagonal);
    }

    public Matrix4(Vector4 row0, Vector4 row1, Vector4 row2, Vector4 row3)
    {
        _rows[0] = row0;
        _rows[1] = row1;
        _rows[2] = row2;
        _rows[3] = row3;
    }

    public Vector4[] Rows { get { return _rows; } }

    public static Matrix4 Identity
    {
        get
        {
            return new Matrix4(1.0f);
        }
    }

    public static Matrix4 Orthographic(float left, float right, float bottom, float top, float near, float far)
    {
        var matrix = Matrix4x4.CreateOrthographicOffCenter(left, right, bottom, top, near, far);

        return ConvertFrom(matrix);
    }

    public static Matrix4 Perspective(float fieldOfView, float aspectRatio, float near, float far)
    {
        var matrix = Matrix4x4.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, near, far);

        return ConvertFrom(matrix);
    }

    public static Matrix4 LookAt(Vector3 eye, Vector3 target, Vector3 up)
    {
        var cameraPosition = new System.Numerics.Vector3(eye.X, eye.Y, eye.Z);
        var cameraTarget = new System.Numerics.Vector3(target.X, target.Y, target.Z);
        var cameraUpVector = new System.Numerics.Vector3(up.X, up.Y, up.Z);

        var matrix = Matrix4x4.CreateLookAt(cameraPosition, cameraTarget, cameraUpVector);

        return ConvertFrom(matrix);
    }

    public static Matrix4 Translate(Vector3 translation)
    {
        var result = Matrix4.Identity;

        result._rows[0].Z = translation.X;
        result._rows[1].Z = translation.Y;
        result._rows[2].Z = translation.Z;

        return result;
    }

    #region Math methods
    public Matrix4 Multiply(Matrix4 other)
    {
        _rows[0].X *= other._rows[0].X;
        _rows[0].Y *= other._rows[0].Y;
        _rows[0].Z *= other._rows[0].Z;
        _rows[0].W *= other._rows[0].W;

        _rows[1].X *= other._rows[1].X;
        _rows[1].Y *= other._rows[1].Y;
        _rows[1].Z *= other._rows[1].Z;
        _rows[1].W *= other._rows[1].W;

        _rows[2].X *= other._rows[2].X;
        _rows[2].Y *= other._rows[2].Y;
        _rows[2].Z *= other._rows[2].Z;
        _rows[2].W *= other._rows[2].W;

        _rows[3].X *= other._rows[3].X;
        _rows[3].Y *= other._rows[3].Y;
        _rows[3].Z *= other._rows[3].Z;
        _rows[3].W *= other._rows[3].W;

        return this;
    }

    public Vector3 Multiply(Vector3 other)
    {
        return other.Multiply(this);
    }
    #endregion

    #region Operators
    public static Matrix4 operator*(Matrix4 left, Matrix4 right)
    {
        return left.Multiply(right);
    }
    #endregion

    private static Matrix4 ConvertFrom(Matrix4x4 matrix)
    {
        var result = Matrix4.Identity;

        result._rows[0].X = matrix.M11;
        result._rows[0].Y = matrix.M12;
        result._rows[0].Z = matrix.M13;
        result._rows[0].W = matrix.M14;

        result._rows[1].X = matrix.M21;
        result._rows[1].Y = matrix.M22;
        result._rows[1].Z = matrix.M23;
        result._rows[1].W = matrix.M24;

        result._rows[2].X = matrix.M31;
        result._rows[2].Y = matrix.M32;
        result._rows[2].Z = matrix.M33;
        result._rows[2].W = matrix.M34;

        result._rows[3].X = matrix.M41;
        result._rows[3].Y = matrix.M42;
        result._rows[3].Z = matrix.M43;
        result._rows[3].W = matrix.M44;

        return result;
    }
}
