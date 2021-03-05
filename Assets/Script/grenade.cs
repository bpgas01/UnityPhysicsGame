using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade : MonoBehaviour
{

    public GameObject explosionEffect;
    public float delay = 3.0f;
    public float explosionForce;
    public float radius = 20.0f;

    private float timer = 0;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", 3f);
    }

    // Update is called once per frame
    private void Explode()
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius );

        foreach(var near in colliders)
        {
            Rigidbody rigidbody = near.GetComponent<Rigidbody>();
            if(rigidbody != null)
            {
                while (true)
                {
                    timer = timer + 1 * Time.deltaTime;

                    rigidbody.AddExplosionForce(-explosionForce, transform.position, radius, 1f, ForceMode.Impulse);

                    if(timer >= 2)
                    {
                        break;
                    }
                }

            }

            if (near.gameObject.GetComponentInParent<Animator>())
            {
                Debug.Log("DEAD");
                near.gameObject.GetComponentInParent<Animator>().enabled = false;

            }

            if(near.gameObject.CompareTag("Grenade Only"))
            {
                Destroy(near.gameObject);
            }

        }
      

        Instantiate(explosionEffect, transform.position, transform.rotation);
      
        Destroy(gameObject);
      

      
    }

   

}
