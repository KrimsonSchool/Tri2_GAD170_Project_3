using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    public Vector3 targetPos;
    public GameObject target;

    public float wanderRange;

    bool seenPlayer;

    float timer;
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
            targetPos = hit.point;
            target = hit.collider.gameObject;

            Debug.DrawRay(transform.position + transform.up * 1.5f, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            if(target.gameObject.tag == "Player")
            {
                agent.destination = hit.collider.gameObject.transform.position - hit.collider.gameObject.transform.forward * 2;
                seenPlayer = true;
            }
            else
            {
                if (Vector3.Distance(agent.destination, transform.position) > 5)
                {

                }
                else
                {
                    Wander();
                }
            }
        }

        if(agent.isStopped)
        {
            Wander();
        }

        if (seenPlayer)
        {
            timer += Time.deltaTime;

            if(timer >= 5)
            {
                seenPlayer = false;
            }
        }
    }

    public void Wander()
    {
        if(seenPlayer)
        {
            agent.destination = target.gameObject.transform.position - target.gameObject.transform.forward * 2;
        }
        else
        {
            agent.destination = new Vector3(transform.position.x + Random.Range(-wanderRange, wanderRange), transform.position.y, transform.position.z + Random.Range(-wanderRange, wanderRange));
        }
    }
}
