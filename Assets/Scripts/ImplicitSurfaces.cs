﻿using System.Collections;
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

        DrawCubes();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void DrawCubes()
    {
        foreach(Cube c in cubes)
        {
            int k = 0;
            while(!c.GetDraw() && k < spheres.Length)
            {
                if (c.IsInSphere(spheres[k].GetCenter(), spheres[k].GetRadius()))
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

        for(int i = 1; i <= nbCubesPerRows; i++)
        {
            for(int j = 1; j <= nbCubesPerRows; j++)
            {
                for(int k = 1; k <= nbCubesPerRows; k++)
                {
                    cubes.Add(new Cube(new Vector3(i, j, k) * offset));
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
                if(c.GetDraw())
                {
                    Gizmos.DrawSphere(c.GetCenter(), 0.05f);
                }
            }
        }
    }
}
