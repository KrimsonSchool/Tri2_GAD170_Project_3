using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("obj" + transform.position, 0); --> enable to reset permament progression

        if (PlayerPrefs.GetInt("First") != 1)
        {
            PlayerPrefs.SetInt("obj" + transform.position, 0);
        }
        if (PlayerPrefs.GetInt("obj"+transform.position) == 1)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
