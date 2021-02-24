using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    CharacterController controller = null;
    Animator animator = null;

    public float speed = 80;
    public float pushPower = 2.0f;


    // Start is called before the first frame update
    void Start()
    { 
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        //controller.SimpleMove(transform.forward * vertical * speed * Time.fixedDeltaTime);

        controller.SimpleMove(transform.up * Time.fixedDeltaTime);

        transform.Rotate(transform.up * horizontal * speed * Time.fixedDeltaTime);

        animator.SetFloat("Speed", vertical * speed * Time.fixedDeltaTime);

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic)
        {
            return;
        }

        if (hit.moveDirection.y < -0.3f)
        {
            return;
        }

        Vector3 pushDirectiom = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDirectiom * pushPower;



    }
}
