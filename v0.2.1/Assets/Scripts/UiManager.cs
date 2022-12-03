using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    public Canvas ticketWorkerCanvas;
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
}
