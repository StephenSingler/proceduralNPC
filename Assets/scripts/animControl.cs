using UnityEngine;

public class animControl : MonoBehaviour
{
    public float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
            GetComponent<Animator>().SetBool("walk", true);
            transform.Translate(new Vector3(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * speed));
            transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * Time.deltaTime * 100 * speed, 0));
        } else {
            GetComponent<Animator>().SetBool("walk", false);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            GetComponent<Animator>().SetTrigger("jump");
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            GetComponent<Animator>().SetTrigger("angry");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            GetComponent<Animator>().SetTrigger("happy");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            GetComponent<Animator>().SetTrigger("talking");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            GetComponent<Animator>().SetTrigger("idle");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            GetComponent<Animator>();
        }
    }
}
