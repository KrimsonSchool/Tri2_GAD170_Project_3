using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class global : MonoBehaviour
{
    public float gSecond;
    public float gTimer;

    public bool killTiles;
    // Start is called before the first frame update
    void Start()
    {
        killTiles = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        killTiles = false;
        gSecond += Time.deltaTime;
        gTimer += Time.deltaTime;

        if (gSecond >= 1)
        {
            gSecond = 0;
        }
    }
}
