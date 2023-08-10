using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public bool started;
    public Animator animator;
    public int stage;
    int lstage;

    public GameObject orbWideSpawn;
    public GameObject orbSingleSpawn;

    public GameObject orb;

    public int health;

    public GameObject win;

    public TMPro.TextMeshPro healthText;
    // Start is called before the first frame update
    void Start()
    {
        health = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = health + "/1000 Hp";

        if (started)
        {
            animator.SetInteger("Stage", stage);

            if(stage == 0 )
            {
                stage = 1;
            }
        }

        if(health <= 0)
        {
            win.SetActive(true);
            Destroy(gameObject);
        }
    }

    //public void Spawn
    public void StageUp()
    {
        stage += 1;
    }

    public void StageDown()
    {
        stage -= 1;
    }

    public void RandomStage()
    {
        lstage = stage;
        stage = Random.Range(2, 5);

        if(stage == lstage)
        {
            RandomStage();
        }
    }

    public void OrbsWide()
    {
        Instantiate(orb, orbWideSpawn.transform.position, transform.rotation).transform.Rotate(0, -30, 0);
        Instantiate(orb, orbWideSpawn.transform.position, transform.rotation).transform.Rotate(0, -25, 0);
        Instantiate(orb, orbWideSpawn.transform.position, transform.rotation).transform.Rotate(0, -20, 0);
        Instantiate(orb, orbWideSpawn.transform.position, transform.rotation).transform.Rotate(0, -15, 0);
        Instantiate(orb, orbWideSpawn.transform.position, transform.rotation).transform.Rotate(0, -10, 0);
        Instantiate(orb, orbWideSpawn.transform.position, transform.rotation).transform.Rotate(0, -5, 0);
        Instantiate(orb, orbWideSpawn.transform.position, transform.rotation).transform.Rotate(0, -4, 0);
        Instantiate(orb, orbWideSpawn.transform.position, transform.rotation).transform.Rotate(0, -3, 0);
        Instantiate(orb, orbWideSpawn.transform.position, transform.rotation).transform.Rotate(0, -2, 0);
        Instantiate(orb, orbWideSpawn.transform.position, transform.rotation).transform.Rotate(0, -1, 0);
        Instantiate(orb, orbWideSpawn.transform.position, transform.rotation).transform.Rotate(0, 0, 0);
        Instantiate(orb, orbWideSpawn.transform.position, transform.rotation).transform.Rotate(0, 1, 0);
        Instantiate(orb, orbWideSpawn.transform.position, transform.rotation).transform.Rotate(0, 2, 0);
        Instantiate(orb, orbWideSpawn.transform.position, transform.rotation).transform.Rotate(0, 3, 0);
        Instantiate(orb, orbWideSpawn.transform.position, transform.rotation).transform.Rotate(0, 4, 0);
        Instantiate(orb, orbWideSpawn.transform.position, transform.rotation).transform.Rotate(0, 5, 0);
        Instantiate(orb, orbWideSpawn.transform.position, transform.rotation).transform.Rotate(0, 10, 0);
        Instantiate(orb, orbWideSpawn.transform.position, transform.rotation).transform.Rotate(0, 15, 0);
        Instantiate(orb, orbWideSpawn.transform.position, transform.rotation).transform.Rotate(0, 20, 0);
        Instantiate(orb, orbWideSpawn.transform.position, transform.rotation).transform.Rotate(0, 25, 0);
        Instantiate(orb, orbWideSpawn.transform.position, transform.rotation).transform.Rotate(0, 30, 0);
    }

    public void OrbsSingle()
    {
        Instantiate(orb, orbSingleSpawn.transform.position, transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            Destroy(other.gameObject);
            health -= 1;
        }
    }
}
