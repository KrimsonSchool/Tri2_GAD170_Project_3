using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public GameObject toFollow;
    public float speed;
    public float rotSpeed;
    public float rotSpeedUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, toFollow.transform.position, speed * Time.deltaTime);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, toFollow.transform.rotation, rotSpeed * Time.deltaTime);

        rotSpeed += Time.deltaTime * rotSpeedUp;
    }
}
