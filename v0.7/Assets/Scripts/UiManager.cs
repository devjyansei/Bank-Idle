using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    public Canvas ticketWorkerCanvas;
    public Canvas tellerWorkerCanvas;
    public Canvas securityWorkerCanvas;
    private void Awake()
    {
        Instance = this;
    }
    public void OpenTicketWorkerCanvas()
    {
        ticketWorkerCanvas.gameObject.SetActive(true);
    }
    public void CloseTicketWorkerCanvas()
    {
        ticketWorkerCanvas.gameObject.SetActive(false);
    }
    public void OpenTellerWorkerCanvas()
    {
        tellerWorkerCanvas.gameObject.SetActive(true);
    }
    public void CloseTellerWorkerCanvas()
    {
        tellerWorkerCanvas.gameObject.SetActive(false);
    }
    public void OpenSecurityWorkerCanvas()
    {
        securityWorkerCanvas.gameObject.SetActive(true);
    }
    public void CloseSecurityWorkerCanvas()
    {
        securityWorkerCanvas.gameObject.SetActive(false);
    }
}
