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
    void WaitingRoomCheck() 
    {
        var customerList = GetComponent<QueOrder>().customerList;
       
        Customer firstCustomer = customerList[0].GetComponent<Customer>();
        customerList[0] = null;

        firstCustomer.StartCheckForEmptyTicket(); // ticketa git


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

    IEnumerator CheckForEmptyTicketQues()
    {
        while (true)
        {
            if (GetComponent<QueOrder>().customerList[0] != null && QueManager.Instance.emptyTicketQues.Count > 0) 
            {
                WaitingRoomCheck();
                CheckQueFill();
            }
            yield return new WaitForSeconds(1.33f);

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
