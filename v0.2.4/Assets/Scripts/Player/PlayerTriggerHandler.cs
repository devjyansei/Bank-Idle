using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerHandler : MonoBehaviour
{
    public static PlayerTriggerHandler Instance { get; private set; }

    public bool securityIsWorking;


    private void Awake()
    {
        Instance = this;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Security"))
        {
            securityIsWorking = true;
            StartCoroutine(other.GetComponentInParent<SecurityManager>().securityCoroutine);                       
        }
        if (other.gameObject.CompareTag("Ticket"))
        {
           StartCoroutine(other.GetComponentInParent<TicketManager>().ticketCoroutine);
        }
        if (other.gameObject.CompareTag("Teller"))
        {
            StartCoroutine(other.GetComponentInParent<TellerManager>().tellerCoroutine);
        }
        if (other.gameObject.CompareTag("MoneyCollectPoint"))
        {
            SecurityManager tempManager = other.gameObject.GetComponentInParent<SecurityManager>();
            MoneyBag moneyBag = GetComponent<MoneyBag>();
            GameObject moneyHolder = tempManager.transform.GetChild(tempManager.transform.childCount - 1).gameObject;

            tempManager.rowCount = 0;       

            moneyBag.CollectMoney(tempManager.createdMoney);

            // para uretilmisse hepsini yokeder
            if (tempManager.createdMoney > 0) 
            {
                for (int i = 0; i < moneyHolder.transform.childCount; i++)
                {
                    moneyHolder.transform.GetChild(i).GetComponent<Money>().SelfDestroy();
                    
                    
                    for (int b = 0; b < tempManager.moneySlot.Length; b++)
                    {
                        tempManager.moneySlot[b] = null;
                    }
                }
                tempManager.createdMoney = 0;
            }
        }
        if (other.gameObject.CompareTag("TicketWorkerShop"))
        {
            UiManager.Instance.OpenTicketWorkerCanvas();
        }
        if (other.gameObject.CompareTag("TellerWorkerShop"))
        {
            UiManager.Instance.OpenTellerWorkerCanvas();
        }

        if (other.gameObject.CompareTag("BuyArea"))
        {
            StartCoroutine(other.GetComponent<BuyArea>().BuyProgress(1f));
        }

    }
    private void OnTriggerStay(Collider other)
    {
        /*
        if (other.gameObject.CompareTag("BuyArea"))
        {
            other.GetComponent<BuyArea>().BuyProgress(1f);           
        }
        */
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Security"))
        {
            securityIsWorking = false;
            StopCoroutine(other.GetComponentInParent<SecurityManager>().securityCoroutine);            
        }

        if (other.gameObject.CompareTag("Ticket"))
        {
            StopCoroutine(other.GetComponentInParent<TicketManager>().ticketCoroutine);
        }
        if (other.gameObject.CompareTag("Teller"))
        {
            StopCoroutine(other.GetComponentInParent<TellerManager>().tellerCoroutine);
        }
       /* if (other.gameObject.CompareTag("BuyArea"))
        {
            StopCoroutine(other.GetComponent<BuyArea>().buyCoroutine);
        }*/
    }
   
    

    
}
