using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public bool started;
    public Animator animator;
    int stage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            animator.SetInteger("Stage", stage);

            if(stage == 0 )
            {
                stage = 1;
            }
        }
    }

    //public void Spawn
    public void StageUp()
    {
        stage += 1;
    }
}
