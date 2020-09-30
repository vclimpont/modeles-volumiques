using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube
{
    private Vector3 center;
    private int potential;

    public Cube(Vector3 center)
    {
        this.center = center;
        potential = 0;
    }

    public Cube(Vector3 center, int potential)
    {
        this.center = center;
        this.potential = potential;
    }

    public bool IsInSphere(Vector3 sCenter, float radius)
    {
        float x = center.x;
        float y = center.y;
        float z = center.z;

        return Mathf.Pow(x - sCenter.x, 2) + Mathf.Pow(y - sCenter.y, 2) + Mathf.Pow(z - sCenter.z, 2) - (radius * radius) < 0;
    }

    public bool UnionWithSpheres(Vector3[] centers, float[] radius)
    {
        for(int i = 0; i < radius.Length; i++)
        {
            if(IsInSphere(centers[i], radius[i]))
            {
                return true;
            }
        }

        return false;
    }

    public bool IntersectWithSpheres(Vector3[] centers, float[] radius)
    {
        for (int i = 0; i < radius.Length; i++)
        {
            if (!IsInSphere(centers[i], radius[i]))
            {
                return false;
            }
        }

        return true;
    }

    public Vector3 GetCenter()
    {
        return center;
    }

    public void SetPotential(int _potential)
    {
        potential = _potential;
    }

    public int GetPotential()
    {
        return potential;
    }
}
