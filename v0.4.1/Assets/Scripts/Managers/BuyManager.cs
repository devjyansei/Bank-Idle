using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyManager : MonoBehaviour
{
    public GameObject ticketWorkerPrefab;
    public Transform ticketWorkerSpawnPoint;
    int boughtTicketWorker;

    public GameObject tellerWorkerPrefab;
    public Transform tellerWorkerSpawnPoint;
    int boughtTellerWorker;
    public void OnBuyTicketWorker()
    {
        if (MoneyBag.Instance.moneyOnPlayer >= WorkerCostData.Instance.currentTicketWorkerCost && boughtTicketWorker < Variables.Instance.ticketWorkerBuyLimit)
        {

            for (int i = 0; i < WorkerCostData.Instance.currentTicketWorkerCost; i++)
            {
                MoneyBag.Instance.DecraseMoney(1);
            }
            
            GameObject tempWorker = Instantiate(ticketWorkerPrefab);
            tempWorker.transform.position = ticketWorkerSpawnPoint.position;
            WorkerCostData.Instance.currentTicketWorkerCost += WorkerCostData.Instance.ticketWorkerCostIncreaseValue;
            boughtTicketWorker++;
            
        }

    }
    public void OnBuyTellerWorker()
    {
        if (MoneyBag.Instance.moneyOnPlayer >= WorkerCostData.Instance.currentTellerWorkerCost && boughtTellerWorker < Variables.Instance.tellerWorkerBuyLimit)
        {
            for (int i = 0; i < WorkerCostData.Instance.currentTellerWorkerCost; i++)
            {
                MoneyBag.Instance.DecraseMoney(1);
            }

            GameObject tempWorker = Instantiate(tellerWorkerPrefab);
            tempWorker.transform.position = tellerWorkerSpawnPoint.position;
            WorkerCostData.Instance.currentTellerWorkerCost += WorkerCostData.Instance.tellerWorkerCostIncreaseValue;
            boughtTellerWorker++;


        }

    }

}
