using UnityEngine;

public class animControl : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 100f;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();
        HandleAnimations();
        HandleTriggers();
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Move forward/back
        transform.Translate(Vector3.forward * v * speed * Time.deltaTime);

        // Rotate left/right
        transform.Rotate(Vector3.up * h * rotationSpeed * Time.deltaTime);
    }

    void HandleAnimations()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        bool isMoving = Mathf.Abs(h) > 0.01f || Mathf.Abs(v) > 0.01f;
        anim.SetBool("walk", isMoving);
    }

    void HandleTriggers()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            anim.SetTrigger("jump");

        if (Input.GetKeyDown(KeyCode.Alpha1))
            anim.SetTrigger("angry");

        if (Input.GetKeyDown(KeyCode.Alpha2))
            anim.SetTrigger("happy");

        if (Input.GetKeyDown(KeyCode.Alpha3))
            anim.SetTrigger("talking");

        if (Input.GetKeyDown(KeyCode.Alpha4))
            anim.SetTrigger("idle");

        // You can define something for Alpha5 if needed
        // if (Input.GetKeyDown(KeyCode.Alpha5))
        //     anim.SetTrigger("something");
    }
}