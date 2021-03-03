using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade : MonoBehaviour
{

    public GameObject explosionEffect;
    public float delay = 3.0f;
    public float explosionForce;
    public float radius = 20.0f;



    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", delay);
    }

    // Update is called once per frame
    private void Explode()
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach(var near in colliders)
        {
            Rigidbody rigidbody = near.GetComponent<Rigidbody>();
            if(rigidbody != null)
            {
                rigidbody.AddExplosionForce(explosionForce, transform.position, radius, 1f, ForceMode.Impulse);

            }
        }

        Instantiate(explosionEffect, transform.position, transform.rotation);
        explosionEffect.GetComponent<ParticleSystem>().loop = false;
        Destroy(gameObject);

      
    }
}
