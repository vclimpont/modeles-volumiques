using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImplicitSurfaces : MonoBehaviour
{
    [SerializeField] GameObject cubeObject = null;
    [SerializeField] private int nbCubesPerRows;
    [SerializeField] private int threshold;
    [SerializeField] private Vector3[] centers;
    [SerializeField] private float[] radius;

    private List<Cube> cubes;
    private Dictionary<Vector3, GameObject> cubeObjectsAtPosition;
    private Sphere[] spheres;
    private float offset = 0.5f;

    private bool rdy = false;
    private bool add = false;
    private bool remove = false;

    // Start is called before the first frame update
    void Start()
    {
        cubeObjectsAtPosition = new Dictionary<Vector3, GameObject>();
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
            add = true;
        }
        else
        {
            add = false;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            remove = true;
        }
        else
        {
            remove = false;
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
            while(k < spheres.Length)
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
                if (c.GetPotential() >= threshold && !c.GetDraw())
                {
                    c.SetDraw(true);
                    GameObject cube = Instantiate(cubeObject, c.GetCenter(), Quaternion.identity);
                    cubeObjectsAtPosition.Add(c.GetCenter(), cube);
                }
                else if(c.GetPotential() < threshold && c.GetDraw())
                {
                    c.SetDraw(false);
                    GameObject cube;
                    if(cubeObjectsAtPosition.TryGetValue(c.GetCenter(), out cube))
                    {
                        cubeObjectsAtPosition.Remove(c.GetCenter());
                        Destroy(cube);
                    }
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
        UnityEngine.Assertions.Assert.IsTrue(centers.Length == radius.Length);
        spheres = new Sphere[centers.Length];

        for (int i = 0; i < spheres.Length; i++)
        {
            spheres[i] = new Sphere(centers[i], radius[i]);
        }
    }


    void OnDrawGizmos()
    {
        if(rdy)
        {
           if(add)
            {
                DrawCubes(true);
            }

           if(remove)
            {
                DrawCubes(false);
            }
        }
    }
}
