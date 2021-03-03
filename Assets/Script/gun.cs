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
    [SerializeField] GameObject BulletType;
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
                Rigidbody ass = hitInfo.transform.gameObject.GetComponent<Rigidbody>();
                if (ass == null)
                {
                    return;
                }
                // Apply force to ragdoll object based on raycast direction and the player transform
                UnityEngine.Vector3 force = Vector3Mulitply(ray.direction, playerTransform.forward);
                Debug.Log("Hit: " + hitInfo.transform.gameObject.name);
                ass.AddForce(force * 10, ForceMode.Impulse);
            }
        
            else
            {
                Rigidbody ass = hitInfo.transform.gameObject.GetComponent<Rigidbody>();
                if (ass == null)
                {
                    return;
                }
                UnityEngine.Vector3 force = Vector3Mulitply(ray.direction, playerTransform.forward);
                Debug.Log("Hit: " + hitInfo.transform.gameObject.name);
                ass.AddForce(force * 10, ForceMode.Impulse);
                Debug.DrawLine(playerTransform.position, hitInfo.transform.gameObject.transform.position, Color.white,
                    10);
           

                Debug.Log(force.x + " " + force.y);
            } // For non NPC objects
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

            rb.AddForce(go.transform.forward * fireForce);



        }
      

        

    }

  
}
