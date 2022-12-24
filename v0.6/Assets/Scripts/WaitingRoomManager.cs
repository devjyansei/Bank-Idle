using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingRoomManager : MonoBehaviour
{
    QueOrder queOrder;
    private void Awake()
    {
        queOrder = GetComponent<QueOrder>();

    }
    private void OnEnable()
    {
        StartCoroutine(CheckForEmptyTicketQues());
    }

   /* void WaitingRoomCheck() // old
    {
        var customerList = GetComponent<QueOrder>().customerList;
       
        Customer firstCustomer = customerList[0].GetComponent<Customer>();
        customerList[0] = null;

        firstCustomer.StartCheckForEmptyTicket(); // ticketa git


        // s�ray� kayd�r

        for (int i = 1; i < customerList.Count; i++)
        {
            if (customerList[i] != null)
            {
                customerList[i].GetComponent<Customer>().AlignQue(this.GetComponent<QueOrder>());
                customerList[i] = null;
            }

        }
    }*/
    void WaitingRoomCheck() 
    {
        var customerList = GetComponent<QueOrder>().customerList;
        var random = Random.Range(0, customerList.Count);

        if (customerList[random] != null)
        {
            GameObject tempCustomer = customerList[random];

            tempCustomer.GetComponent<Customer>().StartCheckForEmptyTicket(); // ticketa git

            for (int i = 0; i < customerList.Count - 1; i++)
            {
                customerList[random] = null;
            }
        }
    }
    IEnumerator CheckForEmptyTicketQues()
    {
        while (true)
        {
            if (GetComponent<QueOrder>().customerList[0] != null && QueManager.Instance.emptyTicketQues.Count > 0) 
            {
                WaitingRoomCheck();
                CheckQueFill();
            }
            float random = Random.Range(1, 2.5f);
            yield return new WaitForSeconds(random);

        }

    }

    void CheckQueFill()
    {
        if (queOrder.customerList[queOrder.customerList.Count - 1] == null && !QueManager.Instance.emptyWaitingRoomQues.Contains(this.gameObject))
        {
            QueManager.Instance.emptyWaitingRoomQues.Add(this.gameObject);
        }    
    }



}
