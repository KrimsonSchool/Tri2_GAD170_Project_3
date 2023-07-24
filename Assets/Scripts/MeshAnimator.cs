using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshAnimator : MonoBehaviour
{
    public MeshFilter meshFilter;
    public GameObject[] meshes;
    float timer;
    public float maxTimePerFrame;
    int no;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        meshFilter.mesh = meshes[no].GetComponentInChildren<MeshFilter>().sharedMesh;
        timer += Time.deltaTime;

        if (timer >= maxTimePerFrame)
        {
            timer = 0;
            if (no < meshes.Length-1)
            {
                no += 1;
            }
        }
    }
}
