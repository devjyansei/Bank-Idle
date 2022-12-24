using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SecurityWorker : MonoBehaviour
{

    NavMeshAgent navmeshagent;

    public Transform target;

    private void Awake()
    {
        navmeshagent = GetComponent<NavMeshAgent>();

        target = FindEmptySecurityDesk().workerStandPoint;

    }
    private void OnEnable()
    {
        GoEmptySecurityDesk();

    }

    public void GoEmptySecurityDesk()
    {
        navmeshagent.SetDestination(target.position);
        Debug.Log("GoEmptySecurityDesk");
    }



    SecurityManager FindEmptySecurityDesk()
    {
        SecurityManager securityDesk = FindObjectOfType<SecurityManager>();

        if (securityDesk.anyWorkerWorks == false)
        {
            securityDesk.anyWorkerWorks = true;
            securityDesk.worker = this.gameObject;
            return securityDesk;
        }

        return null;
       
    }

}
