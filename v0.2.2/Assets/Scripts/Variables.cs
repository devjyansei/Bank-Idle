using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{
    public static Variables Instance { get; private set; }

    public int priority;
    public int ticketWorkerBuyLimit;
    public int tellerWorkerBuyLimit;
    [Range(0, 100)]
    public float moneyValue = 10f;
   
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        ticketWorkerBuyLimit = QueManager.Instance.emptyTicketQues.Count;
        tellerWorkerBuyLimit = QueManager.Instance.emptyTellerQues.Count;
    }
}
