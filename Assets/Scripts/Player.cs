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
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();

        if(PlayerPrefs.GetInt("First") != 1)
        {
            PlayerPrefs.SetInt("Gems", 0);
            PlayerPrefs.SetInt("Xero", 0);
            PlayerPrefs.SetInt("First", 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime + transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        transform.Rotate(0, Input.GetAxis("Mouse X") * mouseSpeed, 0);

        if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            GetComponent<Animator>().SetBool("Running", true);
        }
        if(Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            GetComponent<Animator>().SetBool("Running", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            jumping = true;
        }

        if (Input.GetKey(KeyCode.Space) && jumping)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            transform.position += transform.up * jumpSpeed * Time.deltaTime;
        }

        if (jumping)
        {
            GetComponent<Animator>().SetBool("Jumping", true);
            jumpTimer += Time.deltaTime;

            if (jumpTimer >= 0.25f)
            {
                jumpTimer = 0;
                jumping = false;

                canJump = false;
            }
        }
        else
        {
            GetComponent<Animator>().SetBool("Jumping", false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.SetInt("First", 0);
        }

        if(transform.position.y <= -5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
        {
            canJump = true;
        }

        if(other.tag == "Death")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if(other.tag == "Gem")
        {

            PlayerPrefs.SetInt("Gems", PlayerPrefs.GetInt("Gems") + 1);
            PlayerPrefs.SetInt("obj" + other.gameObject.transform.position, 1);
            Destroy(other.gameObject);
        }

        if(other.tag == "Xero")
        {
            PlayerPrefs.SetInt("Xero", PlayerPrefs.GetInt("Xero") + 1);
            PlayerPrefs.SetInt("obj" + other.gameObject.transform.position, 1);
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Tutor")
        {
            Destroy(collision.gameObject);
            FindObjectOfType<TutorialManager>().tutorial += 1;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Door")
        {
            if (Input.GetKey(KeyCode.E))
            {
                other.gameObject.transform.position += other.gameObject.transform.up * 10;
            }
        }
    }
}
