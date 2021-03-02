using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gun : MonoBehaviour
{
    [Header("Object Settings")]
    [SerializeField] Transform BulletSpawn = null;
    [SerializeField] GameObject BulletType;
    [SerializeField] TextMeshProUGUI CrossHairText; 


    [Header("Modifiers")]
    [SerializeField] float fireForce = 1200.0f;




    public void Shoot()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;



        if(Physics.Raycast(ray, out hitInfo, 250) == true)
        {
            
            if (hitInfo.transform.gameObject.tag == "Brick")
            {
                CrossHairText.color = new Vector4(0, 1, 0, 1); 
                 GameObject go = Instantiate(BulletType, BulletSpawn.position, BulletSpawn.rotation);
                 if (go == null)
                     return;

                 Rigidbody rb = go.GetComponent<Rigidbody>();
                 if (rb == null)
                     return;
                
                rb.AddForce(go.transform.forward * fireForce);
            }

            
        }
        else
        {
            CrossHairText.color = new Vector4(1, 0, 0, 1);
        }

        

    }

  
}
