using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerCostData : MonoBehaviour
{
    static public readonly Dictionary<string, int> TicketWorkerCost = new Dictionary<string, int>()
    {
        {"Gold",50}
    };
    static public readonly Dictionary<string, int> TellerWorkerCost = new Dictionary<string, int>()
    {
        {"Gold",100}
    };

}
