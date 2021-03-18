using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//[RequireComponent(typeof(Animator))]

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    CharacterController controller = null;
    gun gun = null;

    [Header("General Settings")]
    public float speed = 80;
    public float pushPower = 2.0f;
    public float Cooldown = 3;
    public bool playerLocked = false;
    public int numOfGrenades = 7;
    [Tooltip("Timer in seconds")]public float LengthTimer = 60;
    public GameObject targetTrack;

    [Header("Mouse Settings")]
    [SerializeField] [Range(100f, 1000f)] float MouseSpeed = 1f;
    [SerializeField] [Tooltip("Angle given for limiting mouse")] float lookXLimit = 45.0f;

    [Header("Camera Settings")]
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] Camera camera;
    
 

    private float rotationX = 0;
    private float rotationY = 0;
    private float gunCooldown;
    // Start is called before the first frame update
    void Start()
    { 
        controller = GetComponent<CharacterController>();
        gun = GetComponent<gun>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gunCooldown = 0;
        targetTrack.SetActive(false);

        timerText.text = "< " + FormatTime(LengthTimer) + " >";
       // animator = GetComponent<Animator>();
    }


    public string FormatTime(float timeAmount)
    {
        int min = (int) timeAmount / 60;
        int sec = (int)timeAmount - 60 * min;

        return string.Format("{0:00}:{1:00}", min, sec);
    }
    
    void Update()
    {
        LengthTimer -= 1 * Time.deltaTime;
        timerText.text = "< " + FormatTime(LengthTimer) + " >";

        if(LengthTimer <= 0)
        {
            timerText.text = "Game over";
            playerLocked = true;
        }

        if (!playerLocked)
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


            rotationX += -Input.GetAxis("Mouse Y") * Time.fixedDeltaTime * MouseSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            camera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * MouseSpeed * Time.fixedDeltaTime, 0);

            gunCooldown = 0;

          

            if (Input.GetButton("Fire1") && Input.GetKey(KeyCode.Tab))
            {
                gun.RayCastShoot(this.transform);

            }
            if (Input.GetMouseButton(1))
            {
                gun.RemoveLimbs();
                gun.FusionLight.intensity += 1;
            }
            if (Input.GetMouseButtonUp(1))
            {
                gun.FusionLight.intensity = 2;

            }


            if (numOfGrenades != 0)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    numOfGrenades -= 1;
                    gun.Shoot(); ;
                }
            }

            if (gunCooldown <= 0)
            {
                // Grenade throw

                if (Input.GetButtonDown("Fire1"))
                {
                    
                    gun.RayCastShoot(this.transform);
                    gunCooldown = Cooldown;

                }


                
            }
                
          
                
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            numOfGrenades += 2;
            LengthTimer += 5;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Start Nav"))
        {
            targetTrack.SetActive(true);
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
