using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TicketManager : MonoBehaviour
{

    public int ticketCooldown;
    public int ticketAutomaticCooldown;

    public int createdMoney;
    public Transform[] moneyTransforms;
    public GameObject[] moneySlot;
    public Transform collectPoint;
    public float stackOffset;
    public int rowCount;

    QueOrder queOrder;

    public GameObject moneyPrefab;
    public GameObject worker = null;

    public Transform workerStandPoint;

    public bool isQueFull;  
    public bool anyWorkerWorks;
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



    }
    
    public IEnumerator StartTicketCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(ticketCooldown);

            if (GetComponent<QueOrder>().customerList[0] != null && QueManager.Instance.emptyTellerQues.Count > 0 && Vector3.Distance(GetComponent<QueOrder>().customerList[0].transform.position, workerStandPoint.position) < 6f)
            {
                TicketCheck();
                CheckQueFill();               
            }

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
            yield return new WaitForSeconds(ticketAutomaticCooldown);

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
    public void AutomateTicket()
    {
        Debug.Log("otomatikleþti");
        isAutomatic = true;
        StopCoroutine(ticketCoroutine);
        StartCoroutine(automaticTicketCoroutine);
    }
    public void GenerateMoney()
    {

        GameObject tempMoney = Instantiate(moneyPrefab, GetComponent<QueOrder>().queTransformsList[0].position, transform.rotation);
        createdMoney++;
        for (int i = 0; i < moneyTransforms.Length; i++)
        {
            if (!moneySlot[i])
            {

                tempMoney.transform.DOJump(moneyTransforms[i].transform.position + new Vector3(0, rowCount / stackOffset, 0), 3f, 1, 1f);
                //tempMoney.transform.position = moneyTransforms[i].transform.position + new Vector3(0, rowCount / stackOffset, 0);
                tempMoney.transform.SetParent(transform.GetChild(transform.childCount - 1));
                if (moneySlot[i] == null)
                {
                    moneySlot[i] = tempMoney;
                }

                if (i == moneyTransforms.Length - 1)
                {
                    rowCount++;
                    for (int a = 0; a < moneySlot.Length; a++)
                    {
                        moneySlot[a] = null;
                    }
                }
                return;
            }

        }

    }
    /*
    public void GenerateMoney()
    {

        GameObject tempMoney = Instantiate(moneyPrefab);
        createdMoney++;
        for (int i = 0; i < moneyTransforms.Length; i++)
        {
            if (!moneySlot[i])
            {
                tempMoney.transform.position = moneyTransforms[i].transform.position + new Vector3(0, rowCount / stackOffset, 0);
                tempMoney.transform.SetParent(transform.GetChild(transform.childCount - 1));
                if (moneySlot[i] == null)
                {
                    moneySlot[i] = tempMoney;
                }

                if (i == moneyTransforms.Length - 1)
                {
                    rowCount++;
                    for (int a = 0; a < moneySlot.Length; a++)
                    {
                        moneySlot[a] = null;
                    }
                }
                return;
            }

        }

    }
    */
}
