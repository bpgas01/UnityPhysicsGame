using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    [Header("Rocket Launcher")]

    [SerializeField] GameObject RocketTrail;
    [SerializeField] GameObject RocketExplosion;


    [Header("Modifiers")]
    [SerializeField] float explosionForce;
    [SerializeField] float radius;



    private void Start()
    {

       // Instantiate(RocketTrail, transform.position, transform.rotation);
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        rigidbody.AddForce(Vector3Mulitply(transform.up, transform.forward) * 10, ForceMode.Impulse);

    }

    private UnityEngine.Vector3 Vector3Mulitply(UnityEngine.Vector3 up, UnityEngine.Vector3 forward)
    {
        return new UnityEngine.Vector3(up.x + forward.x, up.y + forward.y, up.z + forward.z);     
    }


    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var near in colliders)
        {
            Rigidbody rigidbody = near.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(explosionForce, transform.position, radius, 1f, ForceMode.Impulse);

            }
        }

        Instantiate(RocketExplosion, transform.position, transform.rotation);
        Destroy(gameObject);

    }




    private void OnCollisionEnter(Collision collision)
    {

        Explode();

    }

}
