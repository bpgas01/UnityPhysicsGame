using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Vector3 = System.Numerics.Vector3;
using System;
using UnityEditor;
using UnityEngine.AI;
using UnityEngine.Analytics;

public class gun : MonoBehaviour
{
    [Header("Object Settings")]
    [SerializeField] Transform BulletSpawn = null;
    [SerializeField] Transform muzzleFlashPos;
    [SerializeField] GameObject BulletType;
    [SerializeField] GameObject MuzzleFlash;
    [SerializeField] GameObject ImpactShot;
    private TextMeshProUGUI CrossHairText; 


    [Header("Modifiers")]
    [SerializeField] float fireForce = 1200.0f;


    [Header("RagDoll Settings")]
    [SerializeField] public List<GameObject> bodyParts = new List<GameObject>();

    public void RayCastShoot(Transform playerTransform)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 300))
        {
            if (hitInfo.transform.GetComponentInParent<Animator>())
            {
                Instantiate(MuzzleFlash, muzzleFlashPos.position, muzzleFlashPos.rotation);
                Instantiate(ImpactShot, hitInfo.transform.position, hitInfo.transform.rotation);

                hitInfo.transform.GetComponentInParent<Animator>().enabled = false;
                foreach (var limb in bodyParts)
                {
                    if (hitInfo.transform.gameObject.name == limb.name)
                    {
                        hitInfo.transform.gameObject.transform.SetParent(null);
                        try
                        {
                            if (limb.name == "Head" && hitInfo.transform.gameObject.name == "Head")
                            {
                                hitInfo.transform.gameObject.GetComponent<NavMeshAgent>().enabled = false;
                            }



                            Destroy(hitInfo.transform.gameObject.GetComponent<CharacterJoint>());
                        }
                        catch
                        {
                            continue;

                        }
                    }
                }
                Rigidbody as1s = hitInfo.transform.gameObject.GetComponent<Rigidbody>();
                if (as1s == null)
                {
                    return;
                }


                as1s.AddForce(transform.forward * fireForce * Time.deltaTime, ForceMode.Impulse);

            }

            Instantiate(MuzzleFlash, muzzleFlashPos.position, muzzleFlashPos.rotation);
            
            Instantiate(ImpactShot, hitInfo.transform.position, hitInfo.transform.rotation);


            Collider[] colliders = Physics.OverlapSphere(hitInfo.transform.position, 5);
            foreach (var near in colliders)
            {
                Rigidbody rigidbody = near.GetComponent<Rigidbody>();
                if (rigidbody != null)
                {
                    Debug.Log(rigidbody.gameObject.name);
                    rigidbody.AddExplosionForce(10, hitInfo.transform.position, 5, 0, ForceMode.Impulse);

                }
            }



            // For non NPC objects
        }
    }

    private UnityEngine.Vector3 Vector3Mulitply(UnityEngine.Vector3 up, UnityEngine.Vector3 forward)
    {

        return new UnityEngine.Vector3(up.x + forward.x, up.y + forward.y, up.z + forward.z);


        
    }

    public void Shoot()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;



        if (Physics.Raycast(ray, out hitInfo, 250) == true)
        {



            GameObject go = Instantiate(BulletType, BulletSpawn.position, BulletSpawn.rotation);
            if (go == null)
                return;

            Rigidbody rb = go.GetComponent<Rigidbody>();
            if (rb == null)
                return;


            rb.AddForce(transform.forward * fireForce * Time.fixedDeltaTime, ForceMode.Impulse);



        }
      

        

    }

  
}
