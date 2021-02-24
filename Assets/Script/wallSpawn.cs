using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallSpawn : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private int xAmount;
    [SerializeField] private int yAmount;
    [SerializeField] private int zAmount;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            for (int x = 0; x < xAmount; x++)
            {
                for (int y = 0; y < yAmount; y++)
                {
                    for (int z = 0; z < zAmount; z++)
                    {
                        GameObject newa = Instantiate<GameObject>(cube);
                        newa.transform.position = new Vector3(x, y + 2, z + 2);
                    }
                }
            }
            
        }        
    }
}
