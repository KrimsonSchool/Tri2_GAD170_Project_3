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

    Vector3 pos;
    float chekT;

    public Vector3 ogPos;
    // Start is called before the first frame update
    void Start()
    {
        //assignes the agent variable as the enemies navmeshagent component
        agent = GetComponent<NavMeshAgent>();
        //sets pos as the enemies current location
        pos = transform.position;
        //sets ogPos as the enemies current location
        ogPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //starts the timer chekT to count up every second
        chekT += Time.deltaTime;

        //casts ray
        RaycastHit hit;
        // Does the ray intersect any objects then
        if (Physics.Raycast(transform.position + transform.up * 1.5f, transform.TransformDirection(Vector3.forward), out hit, 50))
        {
            //set targetPos to be the position that was hit
            targetPos = hit.point;
            //set target to be the object that was hit
            target = hit.collider.gameObject;

            //draw a line that follows the path of the ray
            Debug.DrawRay(transform.position + transform.up * 1.5f, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            //if the hit object is the player then
            if(target.gameObject.tag == "Player")
            {
                //set the enemies destination location to be just behind the player (so its easier for the player to jump on)
                agent.destination = hit.collider.gameObject.transform.position - hit.collider.gameObject.transform.forward * 2;
                //set that the player has been seen
                seenPlayer = true;
            }
            //if not then
            else
            {
                //if the distance to the target is greater that 5 then
                if (Vector3.Distance(agent.destination, transform.position) > 5)
                {
                    //do nothin
                }
                //if not then
                else
                {
                    //call the wander function
                    Wander();
                }
            }
        }

        //if(!CanReachPosition(agent.destination))
        //{
        //    Wander();
        //}

        //if the player has been seen then
        if (seenPlayer)
        {
            //start a timer called timer
            timer += Time.deltaTime;

            //if the timer is greater than 15 then
            if(timer >= 15)
            {
                //set that the player hasnt been seen
                seenPlayer = false;
                //reset the timer
                timer = 0;
            }
        }
        
        //(this function checks if the enemy hasnt moved in a while (cant reach destination) and if so picks another destination)\\
        //if chekt is greater than 0.1 then
        if (chekT >= 0.1f)
        {
            //if the enemies position is equal to pos then
            if(transform.position == pos)
            {
                //call the wonder function
                Wander();
            }
            //set pos to be the enemies current position
            pos = transform.position;
            //reset chekt
            chekT = 0;
        }
    }

    //the wander function
    public void Wander()
    {
        //if the player has been seen then
        if(seenPlayer)
        {
            //set the enemies destination to be behind the player
            agent.destination = target.gameObject.transform.position - target.gameObject.transform.forward * 2;
        }
        //if not
        else
        {
            //set the enemies destination to be somewhere random around the enemy within a certain range
            agent.destination = new Vector3(transform.position.x + UnityEngine.Random.Range(-wanderRange, wanderRange), transform.position.y, transform.position.z + UnityEngine.Random.Range(-wanderRange, wanderRange));
        }
    }

    //public bool CanReachPosition(Vector3 position)
    //{
    //    NavMeshPath path = new NavMeshPath();
    //    agent.CalculatePath(position, path);
    //    return path.status == NavMeshPathStatus.PathComplete;
    //}
}
