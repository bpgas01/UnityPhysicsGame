using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

[RequireComponent(typeof(NavMeshAgent))]
public class pathfind : MonoBehaviour
{
    public TextMeshProUGUI PlayButton;
    private bool hit = false;
    private Animator animator = null;
    private NavMeshAgent agent = null;
    public Transform Target;
    // Start is called before the first frame update
    void Start()
    {
        PlayButton.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        animator.SetFloat("Speed", agent.velocity.magnitude);
        agent.SetDestination(Target.position);
            
        if(agent.velocity.magnitude == 0)
        {
            PlayButton.gameObject.SetActive(true);

        }

    }





    
}
