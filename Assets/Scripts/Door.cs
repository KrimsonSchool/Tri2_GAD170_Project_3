using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool open;
    public float startPos;
    public float endPos;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (open && transform.position.y < endPos)
        {
            transform.position += transform.up * Time.deltaTime * speed;
        }
        if (!open && transform.position.y > startPos)
        {
            transform.position += -transform.up * Time.deltaTime * speed;
        }
    }
}
