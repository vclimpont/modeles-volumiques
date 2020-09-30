using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImplicitSurfaces : MonoBehaviour
{
    [SerializeField] private int nbCubesPerRows;
    [SerializeField] private int threshold;
    [SerializeField] private Vector3[] centers;
    [SerializeField] private float[] radius;
    [SerializeField] private float[] potentialOnCenters;

    private List<Cube> cubes;
    private Sphere[] spheres;
    private float offset = 0.5f;

    private bool rdy = false;

    // Start is called before the first frame update
    void Start()
    {
        CreateCubes();
        CreateSpheres();
        rdy = true;
    }

    // Update is called once per frame
    void Update()
    {
        InputSettings();
    }

    void InputSettings()
    {
        if (Input.GetKeyDown(KeyCode.A)) 
        {
            DrawCubes(true);
        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            DrawCubes(false);
        }
    }

    void AddPotentialToCube(Cube c, int p)
    {
        c.SetPotential(c.GetPotential() + p);
    }

    void RemovePotentialToCube(Cube c, int p)
    {
        c.SetPotential(c.GetPotential() - p);
    }

    void DrawCubes(bool add)
    {
        foreach(Cube c in cubes)
        {
            int k = 0;
            while(!c.GetDraw() && k < spheres.Length)
            {
                if(add)
                {
                    AddPotentialToCube(c, spheres[k].GetPotentialToAdd(c.GetCenter()));
                }
                else
                {
                    RemovePotentialToCube(c, spheres[k].GetPotentialToAdd(c.GetCenter()));
                }
                //Debug.Log(c.GetPotential());
                if (c.GetPotential() > threshold)
                {
                    c.SetDraw(true);
                }
                k++;
            }
        }
    }

    void CreateCubes()
    {
        cubes = new List<Cube>(); 

        for(int i = 0; i < nbCubesPerRows; i++)
        {
            for(int j = 0; j < nbCubesPerRows; j++)
            {
                for(int k = 0; k < nbCubesPerRows; k++)
                {
                    cubes.Add(new Cube(new Vector3(i, j, k) + new Vector3(offset, offset, offset)));
                }
            }
        }

    }

    void CreateSpheres()
    {
        UnityEngine.Assertions.Assert.IsTrue(centers.Length == radius.Length && centers.Length == potentialOnCenters.Length);
        spheres = new Sphere[centers.Length];

        for (int i = 0; i < spheres.Length; i++)
        {
            spheres[i] = new Sphere(centers[i], radius[i], potentialOnCenters[i]);
        }
    }


    void OnDrawGizmos()
    {
        if(rdy)
        {
            foreach(Cube c in cubes)
            {
                if (c.GetDraw())
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawCube(c.GetCenter(), new Vector3(1, 1, 1));
                }
            }
        }
    }
}
