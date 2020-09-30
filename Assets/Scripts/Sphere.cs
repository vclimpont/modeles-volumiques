using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    private float radius;
    private Vector3 center;
    private float potentialOnCenter;

    public Sphere(Vector3 center, float radius)
    {
        this.center = center;
        this.radius = radius;
    }

    public Sphere(Vector3 center, float radius, float potentialOnCenter)
    {
        this.center = center;
        this.radius = radius;
        this.potentialOnCenter = potentialOnCenter;
    }

    public Vector3 GetCenter()
    {
        return center;
    }

    public float GetRadius()
    {
        return radius;
    }

    public float GetPotentialOnCenter()
    {
        return potentialOnCenter;
    }
}
