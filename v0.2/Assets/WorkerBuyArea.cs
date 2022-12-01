using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkerBuyArea : MonoBehaviour
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

                this.gameObject.SetActive(false);
                this.enabled = false;
            }


        }


    }
}
