using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyArea : MonoBehaviour
{



    public GameObject purchasedItem;
    public Image progressImage;
    public float cost;
    public float progress;
    public float storedMoney;
    public IEnumerator buyCoroutine;
    private void OnEnable()
    {
       // buyCoroutine = BuyProgress(1f);
    }
    /*
    public void BuyProgress(float amount)
    {
        if (MoneyBag.Instance.moneyOnPlayer >= 1f)
        {

            storedMoney += amount;
            progress = storedMoney / cost;
            progressImage.fillAmount = progress;

            MoneyBag.Instance.DecraseMoney(amount);
            

            if (progress >= 1)
            {
                purchasedItem.SetActive(true);

                //ticket list islemleri
                if (!QueManager.Instance.activatedQues.Contains(this.purchasedItem) && purchasedItem.GetComponent<TicketManager>() != null) //
                {
                    QueManager.Instance.activatedQues.Add(this.purchasedItem);
                    QueManager.Instance.emptyTicketQues.Add(this.purchasedItem);
                    QueManager.Instance.activatedTicketQues.Add(this.purchasedItem);

                    Variables.Instance.ticketWorkerBuyLimit++;
                }
                //teller list islemleri
                else if (!QueManager.Instance.activatedQues.Contains(this.purchasedItem) && purchasedItem.GetComponent<TellerManager>() != null)
                {
                    QueManager.Instance.activatedQues.Add(this.purchasedItem);
                    QueManager.Instance.emptyTellerQues.Add(this.purchasedItem);
                    QueManager.Instance.activatedTellerQues.Add(this.purchasedItem);

                    Variables.Instance.tellerWorkerBuyLimit++;

                }

                this.gameObject.SetActive(false);
                this.enabled = false;
            }


        }
        
    
    }
    */
    public IEnumerator BuyProgress(float amount)
    {
        while (true)
        {
            if (MoneyBag.Instance.moneyOnPlayer >= 1f)
            {
                MoneyBag.Instance.DecraseMoney(amount);

                storedMoney += amount;
                progress = storedMoney / cost;
                progressImage.fillAmount = progress;



                if (progress >= 1)
                {
                    purchasedItem.SetActive(true);

                    //ticket list islemleri
                    if (!QueManager.Instance.activatedQues.Contains(this.purchasedItem) && purchasedItem.GetComponent<TicketManager>() != null) //
                    {
                        QueManager.Instance.activatedQues.Add(this.purchasedItem);
                        QueManager.Instance.emptyTicketQues.Add(this.purchasedItem);
                        QueManager.Instance.activatedTicketQues.Add(this.purchasedItem);

                        Variables.Instance.ticketWorkerBuyLimit++;
                    }
                    //teller list islemleri
                    else if (!QueManager.Instance.activatedQues.Contains(this.purchasedItem) && purchasedItem.GetComponent<TellerManager>() != null)
                    {
                        QueManager.Instance.activatedQues.Add(this.purchasedItem);
                        QueManager.Instance.emptyTellerQues.Add(this.purchasedItem);
                        QueManager.Instance.activatedTellerQues.Add(this.purchasedItem);

                        Variables.Instance.tellerWorkerBuyLimit++;

                    }

                    this.gameObject.SetActive(false);
                    this.enabled = false;
                }

            }
            yield return new WaitForSeconds(0.2f);

        }

    }
}
