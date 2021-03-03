using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Animator))]

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    CharacterController controller = null;
    gun gun = null;

    [Header("General Settings")]
    public float speed = 80;
    public float pushPower = 2.0f;
    public bool playerLocked = false;

    [Header("Mouse Settings")]
    [SerializeField] [Range(100f, 1000f)] float MouseSpeed = 1f;
    [SerializeField] [Tooltip("Angle given for limiting mouse")] float lookXLimit = 45.0f;

    [Header("Camera Settings")]
    [SerializeField] Camera camera;
    
 

    private float rotationX = 0;
    private float rotationY = 0;

    // Start is called before the first frame update
    void Start()
    { 
        controller = GetComponent<CharacterController>();
        gun = GetComponent<gun>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
       // animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        if(!playerLocked)
        {
            float vertical = Input.GetAxis("Vertical");
            controller.SimpleMove(transform.forward * vertical * speed * Time.fixedDeltaTime);
            
            
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * Time.fixedDeltaTime, Space.Self); // Left
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * Time.fixedDeltaTime, Space.Self); // right
            }


            rotationX += -Input.GetAxis("Mouse Y") * Time.fixedDeltaTime *  MouseSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            camera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * MouseSpeed * Time.fixedDeltaTime, 0);

            // Grenade throw
            if(Input.GetKeyDown(KeyCode.Space))
            {
                gun.Shoot();
            }

            if (Input.GetButtonDown("Fire1"))
            {
                gun.RayCastShoot(this.transform);
            }

            if (Input.GetButton("Fire1") && Input.GetKey(KeyCode.Tab))
            {
                gun.RayCastShoot(this.transform);
            }
        }
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
