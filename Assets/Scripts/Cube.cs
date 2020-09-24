using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private Vector3 center;
    private Vector3[] vertices;
    bool hasCube;

    public Cube(Vector3 center, float offset)
    {
        this.center = center;
        hasCube = false;
        vertices = new Vector3[8];
        CreateVertices();
    }

    void CreateVertices()
    {

    }

    public bool IsInSphere(float radius)
    {
        float x = center.x;
        float y = center.y;
        float z = center.z;

        return (x * x + y * y + z * z) - (radius * radius) < 0;
    }

    public Vector3 GetCenter()
    {
        return center;
    }
}
