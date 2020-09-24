using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelShapes : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Vector3 center = Vector3.zero;
    [SerializeField] private float radius = 0f;
    [SerializeField] private int nbBox = 0;
    [SerializeField] private GameObject cube = null;

    private Vector3[] vertices;
    private bool rdy = false;
    private List<Cube> cubes;

    void Start()
    {
        cubes = new List<Cube>();

        CreateBox(radius * 2, nbBox);
    }

    void CreateBox(float length, int nbBox)
    {
        float step = length / nbBox; // size of cube
        float offset = step / 2; // offset to get center of cube

        int vLength = (int)Mathf.Pow((nbBox + 1), 3);
        Debug.Log(vLength);
        vertices = new Vector3[vLength];

        float x, xMax, y, yMax, z, zMax;
        x = center.x - radius + offset;
        y = center.y - radius + offset;
        z = center.z - radius + offset;
        xMax = center.x + radius - offset;
        yMax = center.y + radius - offset;
        zMax = center.z + radius - offset;

        int a = 0;
        for(float i = x; i <= xMax; i += step)
        {
            for(float j = y; j <= yMax; j += step)
            {
                for(float k = z; k <= zMax; k += step)
                {
                    vertices[a] = new Vector3(i, j, k);
                    Cube c = new Cube(vertices[a], offset);
                    cubes.Add(c);
                    //GameObject go = Instantiate(cube, vertices[a], Quaternion.identity);
                    //go.transform.localScale = new Vector3(step, step, step);
                    //Debug.Log(a + " " + vertices[a]);
                    a++;
                }
            }
        }

        DrawCubes(step);

        rdy = true;

    }

    void DrawCubes(float step)
    {
        foreach(Cube c in cubes)
        {
            if(c.IsInSphere(radius))
            {
                GameObject go = Instantiate(cube, c.GetCenter(), Quaternion.identity);
                go.transform.localScale = new Vector3(step, step, step);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(rdy)
        {
            for(int i = 0; i < vertices.Length; i++)
            {
                Gizmos.DrawSphere(vertices[i], 0.2f);
            }
        }
    }
}
