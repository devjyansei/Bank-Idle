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
        if (other.gameObject.CompareTag("Money"))
        {
            GoldManager.Instance.IncreaseGold(Variables.Instance.moneyValue);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("TicketWorkerShop"))
        {
            UiManager.Instance.OpenTicketWorkerCanvas();
        }




    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("BuyArea"))
        {
            other.GetComponent<BuyArea>().BuyProgress(1f);
        }
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
    }
   
    

    
}
