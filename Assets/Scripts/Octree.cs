using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Octree : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject cube = null;
    [SerializeField] private Vector3[] centers;
    [SerializeField] private float[] radius;
    [SerializeField] private int nbBox = 0;
    [SerializeField] private bool union;

    private List<Cube> cubes;
    private Sphere[] spheres;

    private bool rdy = false;

    void Start()
    {
        cubes = new List<Cube>();
        CreateSpheres();

        CreateBox(spheres, nbBox);

        rdy = true;
    }

    void CreateSpheres()
    {
        Assert.IsTrue(centers.Length == radius.Length);
        spheres = new Sphere[centers.Length];

        for (int i = 0; i < spheres.Length; i++)
        {
            spheres[i] = new Sphere(centers[i], radius[i]);
        }
    }

    void CreateBox(Sphere[] spheres, int nbBox)
    {
        float x = Mathf.Infinity;
        float y = Mathf.Infinity;
        float z = Mathf.Infinity;
        float xMax = Mathf.NegativeInfinity;
        float yMax = Mathf.NegativeInfinity;
        float zMax = Mathf.NegativeInfinity;

        for (int w = 0; w < spheres.Length; w++)
        {
            float r = spheres[w].GetRadius();
            float[] coords = GetCoordsFromVector(spheres[w].GetCenter());

            if (coords[0] - r < x) x = coords[0] - r;
            if (coords[0] + r > xMax) xMax = coords[0] + r;

            if (coords[1] - r < y) y = coords[1] - r;
            if (coords[1] + r > yMax) yMax = coords[1] + r;

            if (coords[2] - r < z) z = coords[2] - r;
            if (coords[2] + r > zMax) zMax = coords[2] + r;
        }

        float step = GetLengthFrom(new Vector3(x, y, z), new Vector3(xMax, yMax, zMax)) / nbBox; // size of cube
        float offset = step / 2; // offset to get center of cube

        Debug.Log(new Vector3(x, y, z));
        Debug.Log(new Vector3(xMax, yMax, zMax));

        x += offset;
        y += offset;
        z += offset;
        xMax -= offset;
        yMax -= offset;
        zMax -= offset;

        for (float i = x; i <= xMax; i += step)
        {
            for (float j = y; j <= yMax; j += step)
            {
                for (float k = z; k <= zMax; k += step)
                {
                    Cube c = new Cube(new Vector3(i, j, k));
                    cubes.Add(c);
                }
            }
        }

        if (union)
        {
            DrawCubesUnion(step);
        }
        else
        {
            DrawCubesIntersect(step);
        }

    }

    float GetLengthFrom(Vector3 min, Vector3 max)
    {
        float length = Mathf.Max((max.x - min.x), (max.y - min.y), (max.z - min.z));
        return length;
    }

    float[] GetCoordsFromVector(Vector3 v)
    {
        float[] coords = new float[3];
        coords[0] = v.x;
        coords[1] = v.y;
        coords[2] = v.z;
        return coords;
    }

    void DrawCubesUnion(float step)
    {
        foreach (Cube c in cubes)
        {
            if (c.UnionWithSpheres(centers, radius))
            {
                GameObject go = Instantiate(cube, c.GetCenter(), Quaternion.identity);
                go.transform.localScale = new Vector3(step, step, step);
                go.transform.SetParent(transform);
            }
        }
    }
    void DrawCubesIntersect(float step)
    {
        foreach (Cube c in cubes)
        {
            if (c.IntersectWithSpheres(centers, radius))
            {
                GameObject go = Instantiate(cube, c.GetCenter(), Quaternion.identity);
                go.transform.localScale = new Vector3(step, step, step);
                go.transform.SetParent(transform);
            }
        }
    }

    //void OnDrawGizmos()
    //{
    //    if(rdy)
    //    {
    //        foreach(Cube c in cubes)
    //        {
    //            Gizmos.DrawSphere(c.GetCenter(), 0.05f);
    //        }
    //    }
    //}
}
