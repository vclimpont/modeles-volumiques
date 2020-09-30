using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImplicitSurfaces : MonoBehaviour
{
    [SerializeField] private int nbCubesPerRows;

    private List<Cube> cubes;
    private float offset = 0.5f;

    private bool rdy = false;

    // Start is called before the first frame update
    void Start()
    {
        CreateCubes();
        rdy = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateCubes()
    {
        cubes = new List<Cube>();
        float x = offset;
        float y = offset;
        float z = offset;

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


    void OnDrawGizmos()
    {
        if(rdy)
        {
            foreach(Cube c in cubes)
            {
                Gizmos.DrawSphere(c.GetCenter(), 0.05f);
            }
        }
    }
}
