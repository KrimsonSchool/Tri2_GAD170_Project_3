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
            audioSourceJump.Play();
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (transform.position.y <= -5)
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
            PlayerPrefs.SetInt("First", 1);
            PlayerPrefs.SetInt("Gems", PlayerPrefs.GetInt("Gems") + 1);
            PlayerPrefs.SetInt("obj" + other.transform.position, 1);
            Destroy(other.gameObject);
            audioSourcePop.Play();
        }

        if(other.tag == "Xero")
        {
            PlayerPrefs.SetInt("First", 1);
            PlayerPrefs.SetInt("Xero", PlayerPrefs.GetInt("Xero") + 1);
            PlayerPrefs.SetInt("obj" + other.transform.position, 1);
            Destroy(other.gameObject);
        }

        if(other.tag == "Weak")
        {
            PlayerPrefs.SetInt("obj" + other.gameObject.GetComponentInParent<Enemy>().ogPos, 1);
            Instantiate(splode, other.gameObject.transform.position + other.gameObject.transform.up * 1.5f, Quaternion.identity);
            Instantiate(gem, other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject.GetComponentInParent<Enemy>().gameObject);
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            transform.position += transform.up * jumpSpeed * Time.deltaTime * 5;
            audioSourceExplode.Play();
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
                other.gameObject.GetComponent<Door>().open = !other.gameObject.GetComponent<Door>().open;
            }
        }
    }
}
