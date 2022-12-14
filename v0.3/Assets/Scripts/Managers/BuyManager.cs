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
        if (boughtTicketWorker < Variables.Instance.ticketWorkerBuyLimit)
        {
            GameObject tempWorker = Instantiate(ticketWorkerPrefab);
            tempWorker.transform.position = ticketWorkerSpawnPoint.position;
            boughtTicketWorker++;
        }

    }
    public void OnBuyTellerWorker()
    {
        if (boughtTellerWorker < Variables.Instance.tellerWorkerBuyLimit)
        {
            GameObject tempWorker = Instantiate(tellerWorkerPrefab);
            tempWorker.transform.position = tellerWorkerSpawnPoint.position;
            boughtTellerWorker++;
        }

    }

}
