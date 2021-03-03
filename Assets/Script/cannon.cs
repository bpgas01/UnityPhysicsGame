using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannon : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private Transform towards;
    [SerializeField] private float speed;



    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = ball.GetComponent<Rigidbody>();
    }
    
    
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newa = Instantiate<GameObject>(ball, transform.position, transform.rotation, null);

            newa.transform.LookAt(towards);
            newa.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
        }
    }
}
