using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public class RangedFloat
{
    public RangedFloat(float min = float.NegativeInfinity, float max = float.PositiveInfinity)
    {
        this.min = min;
        this.max = max;
    }
    public float min;
    public float max;

    public bool InRange(float value)
    {
        return value >= min && value <= max;
    }
    public float GetRandom()
    {
        return Random.Range(min, max);
    }

    public static bool InRange(float value, float min, float max)
    {
        return value >= min && value <= max;
    }
}

[System.Serializable]
public class RangedInt
{
    public RangedInt(int min = int.MinValue, int max = int.MaxValue)
    {
        this.min = min;
        this.max = max;
    }
    public int min;
    public int max;

    public bool InRange(int value)
    {
        return value >= min && value <= max;
    }
    public int GetRandom()
    {
        return Random.Range(min, max);
    }


    public static bool InRange(int value, int min, int max)
    {
        return value >= min && value <= max;
    }
}

public class BoxValue<T>
{
    public BoxValue(T value) { this.value = value; }
    public T value;
}

public class PolarVector2
{
    public PolarVector2() { }
    public PolarVector2(float angle, float length) { this.angle = angle; this.length = length; }
    public PolarVector2(Vector2 vector) 
    {
        this.angle = Vector2.SignedAngle(Vector2.up, vector);
        this.length = vector.magnitude;
    }
    public float angle;
    public float length;

    public Vector2 GetVector()
    {
        return Quaternion.Euler(0, 0, angle) * Vector2.up * length;
    }
    public static PolarVector2 MoveTowards(PolarVector2 current, PolarVector2 target, 
        float maxDistanceDeltaAngle, float maxDistanceDeltaLenght)
    {
        return new PolarVector2(
            Mathf.MoveTowardsAngle(current.angle, target.angle, maxDistanceDeltaAngle),
            Mathf.MoveTowardsAngle(current.length, target.length, maxDistanceDeltaLenght));
    }

    public static implicit operator Vector2(PolarVector2 vector)
    {
        return vector.GetVector();
    }

}

public static class Extensions
{
    public static PolarVector2 GetPolarVector(this Vector2 vector)
    {
        return new PolarVector2(vector);
    }
    public static Vector2 To2D(this Vector3 vector)
    {
        return new Vector2(vector.x, vector.z);
    }
    public static Vector3 To3D(this Vector2 vector)
    {
        return new Vector3(vector.x, 0, vector.y);
    }
    public static Vector3 ToPlane(this Vector3 vector)
    {
        return new Vector3(vector.x, 0, vector.z);
    }
    public static float Sq(this float f)
    {
        return f * f;
    }
    public static float Sqrt(this float f)
    {
        return Mathf.Sqrt(f);
    }
}