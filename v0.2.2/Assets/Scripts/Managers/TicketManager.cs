using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketManager : MonoBehaviour
{
    public List<GameObject> createdMoneyList = new List<GameObject>();

    QueOrder queOrder;

    public int stackLimit;

    public GameObject moneyPrefab;
    public GameObject worker = null;

    public Transform spawnPoint;
    public Transform workerStandPoint;

    public bool isQueFull;  
    public bool anyWorker;
    public bool isAutomatic;

    public IEnumerator ticketCoroutine;
    public IEnumerator automaticTicketCoroutine;
    private void Awake()
    {
        queOrder = GetComponent<QueOrder>();
        
    }
    
    private void OnEnable()
    {
        ticketCoroutine = StartTicketCheck();
        automaticTicketCoroutine = StartAutomaticTicketCheck();

    }
    
    void TicketCheck()
    {
        var customerList = GetComponent<QueOrder>().customerList;

        //Eger ilk sýrada musteri varsa onu çýkýþa yonlendirir
        
            GenerateMoney();

            Customer firstCustomer = customerList[0].GetComponent<Customer>();
            customerList[0] = null;

            firstCustomer.SetNewTargetForTeller();


            // sýrayý kaydýr

            for (int i = 1; i < customerList.Count; i++)
            {
                if (customerList[i] != null)
                {                   
                    customerList[i].GetComponent<Customer>().AlignQue(this.GetComponent<QueOrder>());
                    customerList[i] = null;                 
                }

            }

        //Debug.Log(QueManager.Instance.activatedWaitingRoomQues[0].GetComponent<QueOrder>().customerList[0].GetComponent<Customer>().name);


    }
    public void AutomateTicket()
    {
        Debug.Log("otomatikleþti");
        isAutomatic = true;
        StopCoroutine(ticketCoroutine);
        StartCoroutine(automaticTicketCoroutine);
    }
    public IEnumerator StartTicketCheck()
    {
        while (true)
        {
            if (GetComponent<QueOrder>().customerList[0] != null && QueManager.Instance.emptyTellerQues.Count > 0 )
            {
                TicketCheck();
                CheckQueFill();
            }
            yield return new WaitForSeconds(1f);

        }

    }
    public IEnumerator StartAutomaticTicketCheck()
    {
        while (true)
        {
            if (GetComponent<QueOrder>().customerList[0] != null && QueManager.Instance.emptyTellerQues.Count > 0 && Vector3.Distance(GetComponent<QueOrder>().customerList[0].transform.position, workerStandPoint.position)< 7f)
            {
                TicketCheck();
                CheckQueFill();
            }
            yield return new WaitForSeconds(2f);

        }
    }



    void CheckQueFill()
     {
        if (queOrder.customerList[queOrder.customerList.Count-1] == null && !QueManager.Instance.activatedQues.Contains(this.gameObject))
        {
            QueManager.Instance.activatedQues.Add(this.gameObject);
        }
        if ((queOrder.customerList[queOrder.customerList.Count - 1] == null && !QueManager.Instance.emptyTicketQues.Contains(this.gameObject)))
        {
            QueManager.Instance.emptyTicketQues.Add(this.gameObject);

        }
    }

    public void GenerateMoney()
    {
        if (GetComponent<QueOrder>().customerList[0] != null) // eger musteri varsa
        {
            float moneyCount = createdMoneyList.Count;
            int rowCount = (int)moneyCount / stackLimit;


            GameObject tempMoney = Instantiate(moneyPrefab);

            tempMoney.transform.position = new Vector3(spawnPoint.position.x + ((float)rowCount / 3), (moneyCount % stackLimit) / 20, spawnPoint.position.z);
            createdMoneyList.Add(tempMoney);

        }

    }
    
}
