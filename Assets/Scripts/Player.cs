using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public float mouseSpeed;
    Rigidbody rb;

    bool canJump;
    bool jumping;
    float jumpTimer;

    public GameObject gem;
    public GameObject splode;
    public AudioSource audioSourcePop;
    public AudioSource audioSourceJump;
    public AudioSource audioSourceExplode;

    bool canShoot;
    public GameObject bullet;

    public GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
        //sets the cursor to not be visible
        Cursor.visible = false;
        //sets the cursor to be locked to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;

        //allocates the variable rb to be the character rigidbody
        rb = GetComponent<Rigidbody>();

        //if this is the first time the player is playing then
        if(PlayerPrefs.GetInt("First") != 1)
        {
            //set the variable gems to 0
            PlayerPrefs.SetInt("Gems", 0);
            //set the variable xero to 0
            PlayerPrefs.SetInt("Xero", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //the players position is added to by the players forward vector times by the Vertical axis value times by the players speed value added to the players right vector times by the Horizontal axis value times by the players speed value
        transform.position += transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime + transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        //the player is rotated around their x axis by the Mouse X value
        transform.Rotate(0, Input.GetAxis("Mouse X") * mouseSpeed, 0);

        //if the player is not still then
        if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            //set the animators running value to true
            GetComponent<Animator>().SetBool("Running", true);
        }
        //if the player is still then
        if(Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            //set the animators running value to false
            GetComponent<Animator>().SetBool("Running", false);
        }

        //if the player presses the space key and the canJump value is true then
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            //the boolean jumping is set to true
            jumping = true;
            //the jump sound plays
            audioSourceJump.Play();
        }

        //if the space key is held and the jumping boolean is true then
        if (Input.GetKey(KeyCode.Space) && jumping)
        {
            //set the players y velocity to 0
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            //the player is moved on the y axis by the jumpSpeed value
            transform.position += transform.up * jumpSpeed * Time.deltaTime;
        }

        //if the player is jumping then
        if (jumping)
        {
            //set the animators jumping value to true
            GetComponent<Animator>().SetBool("Jumping", true);
            //start the jumptimer timer
            jumpTimer += Time.deltaTime;

            //if the jumptimer's value is greater than 0.25 then
            if (jumpTimer >= 0.25f)
            {
                //set jumptimer to be 0
                jumpTimer = 0;
                //set jumping's value to 0
                jumping = false;

                //set canjump to false
                canJump = false;
            }
        }
        //if not then
        else
        {
            //set the animator's jumping value to false
            GetComponent<Animator>().SetBool("Jumping", false);
        }

        //DEBUG SCRIPTS, IGNORE//\

        //resets saves and respawns
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    PlayerPrefs.SetInt("First", 0);
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //}

        //teleports the player to the boss fight
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    transform.position = new Vector3(90, 71, 22.5f);
        //}

        //DEBUG SCRIPTS, IGNORE



        //if the players y value is less than -5 then
        if (transform.position.y <= -5)
        {
            //reload the scene (the player has died)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //if canShoot then
        if (canShoot)
        {
            //if the left mouse button is down then
            if (Input.GetMouseButton(0))
            {
                //spawn a bullet at the players position and rotation
                Instantiate(bullet, transform.position, transform.rotation);
            }
        }
    }

    //called when the player enters a trigger
    private void OnTriggerEnter(Collider other)
    {
        //if the trigger's tag is ground then
        if(other.tag == "Ground")
        {
            //set canJump to true
            canJump = true;
        }

        //if the trigger's tag is death then
        if(other.tag == "Death")
        {
            //reload the scene (the player has died)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //if the trigger's tag is gem then
        if(other.tag == "Gem")
        {
            //set first to 1
            PlayerPrefs.SetInt("First", 1);
            //add 1 to the gems variable
            PlayerPrefs.SetInt("Gems", PlayerPrefs.GetInt("Gems") + 1);
            //save that that gem has been collected
            PlayerPrefs.SetInt("obj" + other.transform.position, 1);
            //delete the object
            Destroy(other.gameObject);
            //play the pop sound effect
            audioSourcePop.Play();
            //print that the player has gained a gem
            print("You gained a gem!!!");
        }

        //if the trigger's tag is xero then
        if(other.tag == "Xero")
        {
            //set first to 1
            PlayerPrefs.SetInt("First", 1);
            //add 1 to the xero variable
            PlayerPrefs.SetInt("Xero", PlayerPrefs.GetInt("Xero") + 1);
            //save that the xero has been collected
            PlayerPrefs.SetInt("obj" + other.transform.position, 1);
            //delete the object
            Destroy(other.gameObject);
            //play the pop sound effect
            audioSourcePop.Play();
            //print that the player has gained a xero
            print("You gained a xero!!!");
        }

        //if the trigger's tag is weak then
        if(other.tag == "Weak")
        {
            //save that the enemy has been killed
            PlayerPrefs.SetInt("obj" + other.gameObject.GetComponentInParent<Enemy>().ogPos, 1);
            //spawn the enemy death explosion particle effect
            Instantiate(splode, other.gameObject.transform.position + other.gameObject.transform.up * 1.5f, Quaternion.identity);
            //spawn a gem at the enemies location
            Instantiate(gem, other.gameObject.transform.position, Quaternion.identity);
            //delete the enemy
            Destroy(other.gameObject.GetComponentInParent<Enemy>().gameObject);
            //set the player's y velocity to 0
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            //move the player up on the y axis 5 times their jump speed variable
            transform.position += transform.up * jumpSpeed * Time.deltaTime * 5;
            //play the explosion sound effect
            audioSourceExplode.Play();
            //print that the player has killed an enemy
            print("You kill an enemy!");
        }

        //if the trigger's tag is BossStart then
        if(other.tag == "BossStart")
        {
            //set the boss's started variable to true
            FindAnyObjectByType<Boss>().started = true;
            //print that the boss fight has started
            print("The boss fight has started!!!");
        }

        //if the trigger's tag is PowerUp then
        if(other.tag == "PowerUp")
        {
            //set the players speed to be 1.5 times its amount
            jumpSpeed *= 1.5f;
            //delete the object
            Destroy(other.gameObject);
            //play the explosion sound effect
            audioSourceExplode.Play();
            //set the canshoot variable to true
            canShoot = true;
            //destroy the wall blocking the boss fight
            Destroy(wall.gameObject);
            //print that the player can press the left mouse button to shoot
            print("Press the Left mouse button to shoot");
        }
    }

    //called when the player collides with an object
    private void OnCollisionEnter(Collision collision)
    {
        //if the collided objects tag is tutor then
        if(collision.gameObject.tag == "Tutor")
        {
            //delete the object
            Destroy(collision.gameObject);
            //add 1 to the tutorial variable
            FindObjectOfType<TutorialManager>().tutorial += 1;
        }
    }

    //called when the player stays within a trigger
    private void OnTriggerStay(Collider other)
    {
        //if the trigger's tag is door then
        if(other.tag == "Door")
        {
            //if the player presses the E key then
            if (Input.GetKeyDown(KeyCode.E))
            {
                //set the doors open status to the opposite of its current value (true -> false | false -> true)
                other.gameObject.GetComponent<Door>().open = !other.gameObject.GetComponent<Door>().open;
            }
        }
    }
}
