using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueManager : MonoBehaviour
{
    public static QueManager Instance;


    [Header("LISTS (QUES)")]

    public List<GameObject> activatedQues = new List<GameObject>();
    public List<GameObject> activatedTellerQues = new List<GameObject>();
    public List<GameObject> activatedTicketQues = new List<GameObject>();


    [Header("LISTS (WORKERS)")]

    public List<GameObject> activatedTicketWorkers = new List<GameObject>();



    [Header("OBJECTS")]

    public GameObject ticket1;
    public GameObject ticket2;
    public GameObject atm1;
    public GameObject atm2;
    public GameObject teller1;
    public GameObject teller2;

    private void Awake()
    {
        Instance = this;

        activatedQues.Add(ticket1);
        activatedQues.Add(ticket2);
        activatedQues.Add(atm1);
        //activatedQues.Add(atm2);
        activatedTellerQues.Add(teller1);
        //activatedTellerQues.Add(teller2);
        activatedTicketQues.Add(ticket1);
        activatedTicketQues.Add(ticket2);
    }
    
        

}
