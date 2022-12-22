using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerCostData : MonoBehaviour
{
    public static WorkerCostData Instance;

    public int ticketWorkerStartCost = 100;
    public int tellerWorkerStartCost = 100;
    public int securityWorkerStartCost = 200;

    public int ticketWorkerCostIncreaseValue = 200;
    public int tellerWorkerCostIncreaseValue = 200;

    public int currentTicketWorkerCost;
    public int currentTellerWorkerCost;
    public int currentSecurityWorkerCost;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
        currentTicketWorkerCost = ticketWorkerStartCost;
        currentTellerWorkerCost = tellerWorkerStartCost;
        currentSecurityWorkerCost = securityWorkerStartCost;
    }
}
