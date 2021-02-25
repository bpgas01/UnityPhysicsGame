using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightcontroller : MonoBehaviour
{
    private bool state = false;
    public List<Light> lights;
    // Start is called before the first frame update
    void Start()
    {
         foreach (Light a in lights)
        {
            a.gameObject.SetActive(state);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
            if (Input.GetKeyUp(KeyCode.L))
            {
                ToggleLights();
                foreach (Light a in lights)
                {
                    a.gameObject.SetActive(state);
                }
            }
       
    }


    void ToggleLights()
    {
        if(state == true)
        {
            state = false;
            return;
        }
        if(state == false)
        {
            state = true;
            return;
        }
    }
}
