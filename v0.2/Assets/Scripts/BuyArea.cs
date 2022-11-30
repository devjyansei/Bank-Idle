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

                if (!QueManager.Instance.activatedQues.Contains(this.purchasedItem) && purchasedItem.GetComponent<TellerManager>() == null)
                {
                    QueManager.Instance.activatedQues.Add(this.purchasedItem);
                }
                this.gameObject.SetActive(false);
                this.enabled = false;
            }


        }
        

    }
}
