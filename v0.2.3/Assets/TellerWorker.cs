using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TellerWorker : MonoBehaviour
{
    NavMeshAgent navmeshagent;
    public Transform target;
    private void Awake()
    {
        navmeshagent = GetComponent<NavMeshAgent>();

        target = FindEmptyTellerDesk().workerStandPoint;

    }
    private void OnEnable()
    {
        GoEmptyTellerDesk();
    }

    public void GoEmptyTellerDesk()
    {
        navmeshagent.SetDestination(target.position);

    }



    TellerManager FindEmptyTellerDesk()
    {

        for (int i = 0; i < QueManager.Instance.activatedTellerQues.Count; i++)
        {
            TellerManager tellerDesk = QueManager.Instance.activatedTellerQues[i].GetComponent<TellerManager>();
            if (tellerDesk.anyWorkerWorks == false)
            {
                //thisTicketDesk = ticketDesk;
                tellerDesk.anyWorkerWorks = true;
                tellerDesk.worker = this.gameObject;
                return tellerDesk;
            }

        }
        return null;
    }


}
