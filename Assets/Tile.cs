using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int chance;
    public bool faller;

    public Material fallMat;
    // Start is called before the first frame update
    void Start()
    {
        if(Random.Range(1, 101) <= chance)
        {
            faller = true;
            GetComponent<MeshRenderer>().material = fallMat;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (FindAnyObjectByType<global>().killTiles)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (faller)
        {
            Destroy(gameObject);
        }
    }
}
