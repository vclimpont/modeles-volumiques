using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere
{
    private float radius;
    private Vector3 center;

    public Sphere(Vector3 center, float radius)
    {
        this.center = center;
        this.radius = radius;
    }

    public Vector3 GetCenter()
    {
        return center;
    }

    public float GetRadius()
    {
        return radius;
    }
}
