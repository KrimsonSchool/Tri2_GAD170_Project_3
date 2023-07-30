using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position + transform.up * 1.5f, transform.TransformDirection(Vector3.forward), out hit, 50))
        {
            Debug.DrawRay(transform.position + transform.up * 1.5f, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            if(hit.collider.gameObject.tag == "Player")
            {
                agent.destination = hit.collider.gameObject.transform.position - hit.collider.gameObject.transform.forward * 2;
            }
        }
    }
}
