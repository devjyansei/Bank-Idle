using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TicketWorker : MonoBehaviour
{
    NavMeshAgent navmeshagent;

    public Transform target;
   // public TicketManager thisTicketDesk;
    private void Awake()
    {
        navmeshagent = GetComponent<NavMeshAgent>();

        target = FindEmptyTicketDesk().workerStandPoint;

    }
    private void OnEnable()
    {
        GoEmptyTicketDesk();
        
    }

    public void GoEmptyTicketDesk()
    {
        navmeshagent.SetDestination(target.position);
        
    }



    TicketManager FindEmptyTicketDesk()
    {

        for (int i = 0; i < QueManager.Instance.activatedTicketQues.Count; i++)
        {
            TicketManager ticketDesk = QueManager.Instance.activatedTicketQues[i].GetComponent<TicketManager>();
            if (ticketDesk.anyWorkerWorks == false)
            {
                //thisTicketDesk = ticketDesk;
                ticketDesk.anyWorkerWorks = true;
                ticketDesk.worker = this.gameObject;
                return ticketDesk;
            }
            
        }
        return null;
    }

    
}


    

