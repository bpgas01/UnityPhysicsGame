using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

[RequireComponent(typeof(NavMeshAgent))]
public class pathfind : MonoBehaviour
{
    private bool hit = false;
    private Animator animator = null;
    private NavMeshAgent agent = null;
    public Transform Target;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        animator.SetFloat("Speed", agent.velocity.magnitude);
        if (Target != null)
        {
            agent.SetDestination(Target.position);
        }
        
        if(gameObject.GetComponent<Animator>().enabled == false)
        {
            transform.position = transform.up * 5 * Time.deltaTime;
            Target = null;
        }


          
    }

   


    
}
