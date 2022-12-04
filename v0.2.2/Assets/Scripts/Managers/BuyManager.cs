using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyManager : MonoBehaviour
{
    public GameObject ticketWorkerPrefab;
    public Transform ticketWorkerSpawnPoint;
    int boughtTicketWorker;
    
    public void OnBuyTicketWorker()
    {
        if (boughtTicketWorker < Variables.Instance.ticketWorkerBuyLimit)
        {
            GameObject tempWorker = Instantiate(ticketWorkerPrefab);
            tempWorker.transform.position = ticketWorkerSpawnPoint.position;
            boughtTicketWorker++;
        }



    }

}
