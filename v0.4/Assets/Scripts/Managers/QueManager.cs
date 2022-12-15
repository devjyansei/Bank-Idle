using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueManager : MonoBehaviour
{
    public static QueManager Instance;


    [Header("LISTS (QUES)")]

    public List<GameObject> activatedQues = new List<GameObject>();

    public List<GameObject> emptyTellerQues = new List<GameObject>();
    public List<GameObject> emptyTicketQues = new List<GameObject>();
    public List<GameObject> emptyWaitingRoomQues = new List<GameObject>();

    // satin alindiginda ve oyunun basinda ekleme islemi yapilacak
    public List<GameObject> activatedTellerQues = new List<GameObject>();
    public List<GameObject> activatedTicketQues = new List<GameObject>();
    public List<GameObject> activatedWaitingRoomQues = new List<GameObject>();


    public List<GameObject> emptyWaitingAreaQues = new List<GameObject>();


    [Header("LISTS (WORKERS)")]

    public List<GameObject> activatedTicketWorkers = new List<GameObject>();


    
    [Header("OBJECTS")]

    public GameObject ticketOrj;
    public GameObject ticket1;
    public GameObject ticket2;
    public GameObject ticket3;
    public GameObject ticket4;
    public GameObject ticket5;

    public GameObject atm1;
    public GameObject atm2;

    public GameObject tellerOrj;
    public GameObject teller1;
    public GameObject teller2;
    public GameObject teller3;

    public GameObject waitingRoomOrj;
    public GameObject waitingRoom1;
    public GameObject waitingRoom2;


    public GameObject waitingArea1;


    private void Awake()
    {
        Instance = this;

        activatedQues.Add(ticketOrj);
        activatedQues.Add(tellerOrj);
        activatedQues.Add(waitingRoomOrj);


        activatedTicketQues.Add(ticketOrj);
        activatedTellerQues.Add(tellerOrj);
        activatedWaitingRoomQues.Add(waitingRoomOrj);
       
        emptyTellerQues.Add(tellerOrj);
        emptyTicketQues.Add(ticketOrj);

        emptyWaitingRoomQues.Add(waitingRoomOrj);

        emptyWaitingAreaQues.Add(waitingArea1);
        

        
    }
    
        

}
