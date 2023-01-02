using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BuyArea : MonoBehaviour
{
    //Camera Target Change
    CameraFollow cameraFollow;
    Transform orjTarget;
    public BuyAreaUi buyAreaUi;


    
    public List<GameObject> ticketBuyAreas = new List<GameObject>();
    public List<GameObject> tellerBuyAreas = new List<GameObject>();
    public List<GameObject> waitingRoomBuyAreas = new List<GameObject>();
    public List<GameObject> atmBuyAreas = new List<GameObject>();
    public GameObject ticketOrj;
    public GameObject tellerOrj;
    public GameObject waitingRoomOrj;


    public GameObject purchasedItem;
    public Image progressImage;
    public float cost;
    public float progress;
    public float storedMoney;
    public Transform moneyTarget;


    private void Awake()
    {
        cameraFollow = FindObjectOfType<CameraFollow>();
    }
    private void Update()
    {
      //  if (isOrjTicketOpened)        {            TemporaryCameraSwitch(orjTarget);              }
    }
    public void BuyProgress(float amount,BuyArea buyarea)
    {
        

        if (MoneyBag.Instance.moneyOnPlayer >= 1f)
        {
            MoneyBag.Instance.DecraseMoney(amount,buyarea);

            storedMoney += amount;
            progress = storedMoney / cost;
            progressImage.fillAmount = progress;

            //para degerýný degýstýr UI
            buyAreaUi.costText.text = (buyarea.cost-buyarea.storedMoney).ToString();

            if (progress >= 1)
            {
                Vector3 originalScale = purchasedItem.transform.localScale;
                purchasedItem.transform.localScale = Vector3.zero;

                purchasedItem.SetActive(true);

                purchasedItem.transform.DOScale(originalScale*1.5f,.2f).OnComplete(()=>
                purchasedItem.transform.DOScale(originalScale,.15f));

                //ticket list islemleri
                if (!QueManager.Instance.activatedQues.Contains(this.purchasedItem) && purchasedItem.GetComponent<TicketManager>() != null) //
                {
                    if (purchasedItem.GetInstanceID() == ticketOrj.GetInstanceID())
                    {
                        foreach (GameObject item in ticketBuyAreas)
                        {
                            item.SetActive(true);
                        }
                        tellerBuyAreas[0].SetActive(true);

                        cameraFollow.TemporaryCameraSwitch(tellerBuyAreas[0].transform);
                        //ok iþareti
                    }
                    
                    QueManager.Instance.activatedQues.Add(this.purchasedItem);
                    QueManager.Instance.emptyTicketQues.Add(this.purchasedItem);
                    QueManager.Instance.activatedTicketQues.Add(this.purchasedItem);

                    Variables.Instance.ticketWorkerBuyLimit++;                  
                }
                //teller list islemleri
                else if (!QueManager.Instance.activatedQues.Contains(this.purchasedItem) && purchasedItem.GetComponent<TellerManager>() != null)
                {
                    if (purchasedItem.GetInstanceID() == tellerOrj.GetInstanceID())
                    {
                        foreach (GameObject item in tellerBuyAreas)
                        {
                            item.SetActive(true);
                        }

                        waitingRoomBuyAreas[0].SetActive(true);
                        cameraFollow.TemporaryCameraSwitch(waitingRoomBuyAreas[0].transform);
                        //ok iþareti

                    }
                        QueManager.Instance.activatedQues.Add(this.purchasedItem);
                        QueManager.Instance.emptyTellerQues.Add(this.purchasedItem);
                        QueManager.Instance.activatedTellerQues.Add(this.purchasedItem);

                        Variables.Instance.tellerWorkerBuyLimit++;

                }


                else if (!QueManager.Instance.activatedQues.Contains(this.purchasedItem) && purchasedItem.GetComponent<WaitingRoomManager>() != null)
                {
                    if (purchasedItem.GetInstanceID() == waitingRoomOrj.GetInstanceID())
                    {
                        foreach (GameObject item in waitingRoomBuyAreas)
                        {
                            item.SetActive(true);
                        }
                        foreach (GameObject item in atmBuyAreas)
                        {
                            item.SetActive(true);
                        }
                        cameraFollow.TemporaryCameraSwitch(atmBuyAreas[0].transform);
                        //ok iþareti

                    }
                    QueManager.Instance.activatedQues.Add(this.purchasedItem);
                    QueManager.Instance.emptyWaitingRoomQues.Add(this.purchasedItem);
                    QueManager.Instance.activatedWaitingRoomQues.Add(this.purchasedItem);


                }
                else if (!QueManager.Instance.activatedQues.Contains(this.purchasedItem) && purchasedItem.GetComponent<AtmManager>() != null)
                {
                    QueManager.Instance.activatedQues.Add(this.purchasedItem);
                    QueManager.Instance.emptyTicketQues.Add(this.purchasedItem);
                    QueManager.Instance.activatedTicketQues.Add(this.purchasedItem);

                }

                this.gameObject.SetActive(false);
                this.enabled = false;
            }

        }
        
    }


}
