using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[ExecuteInEditMode]
public class tileinator : MonoBehaviour
{
    public GameObject tileA;
    public GameObject tileB;

    public GameObject[] tiles;
    public int no;
    public float spacing;
    int at;
    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        if (FindAnyObjectByType<global>().gTimer == 0)
        {
            Generate();
        }
    }

    public void Generate()
    {
        for (int i = 0; i < tiles.Length - 1; i++)
        {
            DestroyImmediate(tiles[i].gameObject);
        }
        at = 0;
        for (int x = 0; x < no; x++)
        {
            for (int z = 0; z < no; z++)
            {
                if (at % 2 == 0)
                {
                    GameObject tile = Instantiate(tileA, new Vector3(transform.position.x + x * spacing, transform.position.y, transform.position.z + z * spacing), Quaternion.identity);
                    tiles[at] = tile;
                    tile.transform.parent = transform;
                }
                else
                {
                    GameObject tile = Instantiate(tileB, new Vector3(transform.position.x + x * spacing, transform.position.y, transform.position.z + z * spacing), Quaternion.identity);
                    tiles[at] = tile;
                    tile.transform.parent = transform;
                }
                at += 1;
            }
        }
    }
}
