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
    [Header("Grenade Settings")]
    [InspectorName("Grenade Object")][SerializeField] GameObject BulletType;
    [SerializeField] GameObject rocket;

    [Header("Gun Settings")]
    [SerializeField] Transform BulletSpawn = null;
    [SerializeField] Transform muzzleFlashPos;
    [SerializeField] public Light FusionLight;
    [SerializeField] GameObject MuzzleFlash;
    [SerializeField] GameObject ImpactShot;
    private TextMeshProUGUI CrossHairText; 


    [Header("Modifiers")]
    [SerializeField] float fireForce = 1200.0f;
    [SerializeField] float ExplosionForce = 10.0f;
    [SerializeField] float rocketLaunchForce;


    [Header("RagDoll Settings")]
    [SerializeField] public List<GameObject> bodyParts = new List<GameObject>();

    private void Start()
    {
     
    }


    public void RemoveLimbs()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 50))
        {
            GameObject moveForward = Instantiate(MuzzleFlash, muzzleFlashPos.position, muzzleFlashPos.rotation);

            moveForward.AddComponent<Rigidbody>().useGravity = false;
            moveForward.GetComponent<Rigidbody>().AddForce(muzzleFlashPos.transform.forward * Time.deltaTime * 500, ForceMode.Impulse);
           
            Instantiate(MuzzleFlash, hitInfo.point, hitInfo.transform.rotation);
            foreach (var limb in bodyParts)
            {
                
                if (hitInfo.transform.gameObject.name == limb.name)
                {
                    hitInfo.transform.gameObject.transform.SetParent(null);

                    Destroy(hitInfo.transform.gameObject.GetComponent<CharacterJoint>());

                    if (hitInfo.transform.gameObject.GetComponent<Rigidbody>())
                    {
                        hitInfo.transform.gameObject.GetComponent<Rigidbody>().AddForce(hitInfo.transform.forward, ForceMode.Impulse);
                    }



                }
            }
     
            

        }
    }

    public void RayCastShoot(Transform playerTransform)
    {
   
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 300))
        {
            if (hitInfo.transform.GetComponentInParent<Animator>())
            {
                GameObject gameObject = Instantiate(MuzzleFlash, muzzleFlashPos.position, muzzleFlashPos.rotation);

                gameObject.AddComponent<Rigidbody>().useGravity = false;
                gameObject.GetComponent<Rigidbody>().AddForce(muzzleFlashPos.transform.forward * Time.deltaTime * 500, ForceMode.Impulse);
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


                as1s.AddForce(hitInfo.transform.forward * fireForce * Time.deltaTime, ForceMode.Impulse);

            }


            


            Collider[] colliders = Physics.OverlapSphere(hitInfo.transform.position, 5);
            foreach (var near in colliders)
            {
                GameObject moveForward = Instantiate(MuzzleFlash, muzzleFlashPos.position, muzzleFlashPos.rotation);

                moveForward.AddComponent<Rigidbody>().useGravity = false;
                
                moveForward.GetComponent<Rigidbody>().AddForce(muzzleFlashPos.transform.forward *Time.fixedDeltaTime *10, ForceMode.Impulse);
                if (near.gameObject.tag != "Grenade Only")
                {
                    Rigidbody rigidbody = near.GetComponent<Rigidbody>();
                    if (rigidbody != null)
                    {
                        Debug.Log(rigidbody.gameObject.name);
                        rigidbody.AddExplosionForce(15, hitInfo.point, 5, 1, ForceMode.Impulse);
                        rigidbody.AddForce(hitInfo.transform.forward * Time.deltaTime * 1200, ForceMode.Impulse);


                    }
                }
            }

            Instantiate(ImpactShot, hitInfo.point, hitInfo.transform.rotation);


           
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

            rb.AddForce(BulletSpawn.transform.forward * (fireForce * 5) * Time.fixedDeltaTime, ForceMode.Impulse);
        }    
    }

    public void ShootRocket()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;



        if (Physics.Raycast(ray, out hitInfo, 250) == true)
        {
            GameObject go = Instantiate(rocket, BulletSpawn.position, BulletSpawn.rotation);        
        }

    }

}
