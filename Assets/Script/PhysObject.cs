using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PhysObject : MonoBehaviour
{


    public Material awakeMat = null, sleepingMat = null;

    private Rigidbody _rigidbody = null;

    private bool wasAsleep = false;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (_rigidbody.IsSleeping() && !wasAsleep && sleepingMat != null )
        {
            wasAsleep = true;
            GetComponent<MeshRenderer>().material = sleepingMat;
        }

        if (!_rigidbody.IsSleeping() && wasAsleep && awakeMat != null)
        {

            wasAsleep = false;
            GetComponent<MeshRenderer>().material = awakeMat;


        }
    }
}
