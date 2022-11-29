using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerHandler : MonoBehaviour
{
    public static PlayerTriggerHandler Instance;

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

        if (other.gameObject.CompareTag("Money"))
        {

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
    }
   
    

    
}
