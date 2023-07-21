using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;

    public MeshRenderer meshRenderer;
    public Material defaultMat;
    public Material altMat;

    public float speed;

    bool moving;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moving && transform.position != endPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, Time.deltaTime * speed);
        }
        else if(Vector3.Distance(transform.position, endPos) < 1)
        {
            moving = false;
        }

        if(!moving && transform.position != startPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, Time.deltaTime * speed);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        moving = true;
    }
}
