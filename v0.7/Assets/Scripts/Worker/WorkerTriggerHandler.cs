using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerTriggerHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ticket"))
        {
            other.GetComponentInParent<TicketManager>().AutomateTicket();
        }
        if (other.gameObject.CompareTag("Teller"))
        {
            other.GetComponentInParent<TellerManager>().AutomateTeller();
        }
        if (other.gameObject.CompareTag("Security"))
        {
            other.GetComponentInParent<SecurityManager>().AutomateSecurity();
        }
    }
}
