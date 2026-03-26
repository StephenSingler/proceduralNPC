using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class AnimControl : MonoBehaviour
{

    public float speed = 5f;
    public float jumpForce = 5f;
    public Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
            Animator anim = GetComponent<Animator>();
            anim.SetBool("Walk", true);
            if (vertical < 0)
            {
                anim.SetFloat("AnimSpeed", -1f);
            }
            else
            {
                anim.SetFloat("AnimSpeed", 1f);
            }   
            transform.Translate(new Vector3(0, 0, vertical * Time.deltaTime * speed));
            transform.Rotate(new Vector3(0, horizontal* Time.deltaTime * 100 * -speed, 0));
        }
        else
        {
            GetComponent<Animator>().SetBool("Walk", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Animator>().SetTrigger("Jump");
            rb.AddForce(Vector3.up * 100f * jumpForce);
            
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetComponent<Animator>().SetTrigger("Angry");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GetComponent<Animator>().SetTrigger("Happy");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GetComponent<Animator>().SetTrigger("Idle");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GetComponent<Animator>().SetTrigger("Talk");
        }
    }
}
