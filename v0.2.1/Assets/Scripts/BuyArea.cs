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
    public void BuyProgress(float amount)
    {
        if (GoldManager.Instance.goldAmount >= 1f)
        {

            storedMoney += amount;
            progress = storedMoney / cost;
            progressImage.fillAmount = progress;
            GoldManager.Instance.DecraseGold(amount);

            if (progress >= 1)
            {
                purchasedItem.SetActive(true);

                //eger ticket açtýysak
                if (!QueManager.Instance.activatedQues.Contains(this.purchasedItem) && purchasedItem.GetComponent<TicketManager>() != null) //
                {
                    QueManager.Instance.activatedQues.Add(this.purchasedItem);
                    QueManager.Instance.activatedTicketQues.Add(this.purchasedItem);
                    Variables.Instance.ticketWorkerBuyLimit++;
                }
                //eger teller açtýysak
                else if (!QueManager.Instance.activatedQues.Contains(this.purchasedItem) && purchasedItem.GetComponent<TellerManager>() != null)
                {
                    QueManager.Instance.activatedQues.Add(this.purchasedItem);
                    QueManager.Instance.activatedTellerQues.Add(this.purchasedItem);
                    Variables.Instance.tellerWorkerBuyLimit++;

                }

                this.gameObject.SetActive(false);
                this.enabled = false;
            }


        }
        

    }
}
