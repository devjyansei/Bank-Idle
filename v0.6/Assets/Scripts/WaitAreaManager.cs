using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAreaManager : MonoBehaviour
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
    void WaitingAreaCheck()
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
            yield return new WaitForSeconds(1f);

            if (QueManager.Instance.emptyTicketQues.Count > 0)
            {
                WaitingAreaCheck();
                CheckQueFill();
            }

        }

    }

    void CheckQueFill()
    {
        if (queOrder.customerList[queOrder.customerList.Count - 1] == null && !QueManager.Instance.emptyWaitingAreaQues.Contains(this.gameObject))
        {
            QueManager.Instance.emptyWaitingAreaQues.Add(this.gameObject);
        }
    }

}
