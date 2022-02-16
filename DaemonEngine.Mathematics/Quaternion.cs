namespace DaemonEngine.Mathematics;

public class Quaternion : IEquatable<Quaternion>
{
    public Quaternion()
    {
        X = default;
        Y = default;
        Z = default;
        W = default;
    }

    public Quaternion(float x, float y, float z, float w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public Quaternion(Vector3 vector, float w)
    {
        X = vector.X;
        Y = vector.Y;
        Z = vector.Z;
        W = w;
    }

    public float X { get { return Xyz.X; } set { Xyz.X = value; } }
    public float Y { get { return Xyz.Y; } set { Xyz.Y = value; } }
    public float Z { get { return Xyz.Z; } set { Xyz.Z = value; } }
    public float W { get; set; }

    public Vector3 Xyz { get; set; }

    public static Quaternion Identity => new(Vector3.Zero, 1.0f);

    public float Length => Maths.Sqrt((X * X) + (Y * Y) + (Z * Z) + (W * W));
    public float LengthSquared => (X * X) + (Y * Y) + (Z * Z) + (W * W);

    #region Static quaternion methods
    public static Quaternion Conjugate(Quaternion quaternion)
    {
        var vector = new Vector3(quaternion.X, quaternion.Y, quaternion.Z);
        return new Quaternion(-vector, quaternion.W);
    }

    public static Quaternion Invert(Quaternion quaternion)
    {
        var lengthSq = quaternion.X * quaternion.X + quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z + quaternion.W * quaternion.W;
        if (lengthSq != 0.0f)
        {
            var i = 1.0f / lengthSq;

            var vector = new Vector3(quaternion.X, quaternion.Y, quaternion.Z) * -1;

            return new Quaternion(vector, quaternion.W * i);
        }

        return quaternion;
    }

    public static Quaternion Normalize(Quaternion quaternion)
    {
        var scale = 1.0f / quaternion.Length;

        var vector = new Vector3(quaternion.X, quaternion.Y, quaternion.Z);

        var result = new Quaternion(vector * scale, quaternion.W * scale);

        return result;
    }

    public static Quaternion FromAxisAngle(Vector3 axis, float angle)
    {
        if (axis.LengthSquared == 0.0f)
        {
            return Identity;
        }

        Quaternion result = Identity;

        angle *= 0.5f;
        axis.Normalize();
        var resultVector = axis * Maths.Sin(angle);

        result.X = resultVector.X;
        result.Y = resultVector.Y;
        result.Z = resultVector.Z;
        result.W = Maths.Cos(angle);

        return Normalize(result);
    }

    public static Quaternion Slerp(Quaternion q1, Quaternion q2, float blend)
    {
        if(q1.LengthSquared == 0.0f)
        {
            if(q2.LengthSquared == 0.0f)
            {
                return Identity;
            }

            return q2;
        }
        else if(q2.LengthSquared == 0.0f)
        {
            return q1;
        }

        float cosHalfAngle = q1.W * q2.W + Vector3.Dot(q1.Xyz, q2.Xyz);

        if (cosHalfAngle >= 1.0f || cosHalfAngle <= -1.0f)
        {
            // angle = 0.0f, so just return one input.
            return q1;
        }
        else if (cosHalfAngle < 0.0f)
        {
            q2.Xyz = -q2.Xyz;
            q2.W = -q2.W;
            cosHalfAngle = -cosHalfAngle;
        }

        float blendA;
        float blendB;
        if (cosHalfAngle < 0.99f)
        {
            // do proper slerp for big angles
            float halfAngle = Maths.Acos(cosHalfAngle);
            float sinHalfAngle = Maths.Sin(halfAngle);
            float oneOverSinHalfAngle = 1.0f / sinHalfAngle;
            blendA = Maths.Sin(halfAngle * (1.0f - blend)) * oneOverSinHalfAngle;
            blendB = Maths.Sin(halfAngle * blend) * oneOverSinHalfAngle;
        }
        else
        {
            // do lerp if angle is really small.
            blendA = 1.0f - blend;
            blendB = blend;
        }

        var q1Vector = new Vector3(blendA * q1.X, blendA * q1.Y, blendA * q1.Z);
        var q2Vector = new Vector3(blendB * q2.X, blendB * q2.Y, blendB * q2.Z);

        var result = new Quaternion(q1Vector * q2Vector, blendA * q1.W + blendB * q2.W);
        if(result.LengthSquared > 0.0f)
        {
            return result;
        }

        return Identity;
    }
    #endregion

    #region Operators
    public static Quaternion operator +(Quaternion left, Quaternion right)
    {
        var result = new Quaternion
        {
            X = left.X + right.X,
            Y = left.Y + right.Y,
            Z = left.Z + right.Z,
            W = left.W + right.W
        };

        return result;
    }

    public static Quaternion operator -(Quaternion left, Quaternion right)
    {
        var result = new Quaternion
        {
            X = left.X - right.X,
            Y = left.Y - right.Y,
            Z = left.Z - right.Z,
            W = left.W - right.W
        };

        return result;
    }

    public static Quaternion operator *(Quaternion left, Quaternion right)
    {
        var leftVector = new Vector3(left.X, left.Y, left.Z);
        var rightVector = new Vector3(right.X, right.Y, right.Z);

        var resultXyz = right.W * leftVector + left.W * rightVector + Vector3.Cross(leftVector, rightVector);
        var resultW = left.W * right.W - Vector3.Dot(leftVector, rightVector);

        var result = new Quaternion(resultXyz, resultW);

        return result;
    }
    #endregion

    public override bool Equals(object? obj)
    {
        return obj is Quaternion quat && Equals(quat);
    }

    public bool Equals(Quaternion? other)
    {
        return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z, W);
    }
}
