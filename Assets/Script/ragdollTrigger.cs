using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragdollTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ragdoll ragdoll = other.gameObject.GetComponentInParent<ragdoll>();
        if(ragdoll != null)
        {
            ragdoll.RagdollOn = true;
        }
    }
}
