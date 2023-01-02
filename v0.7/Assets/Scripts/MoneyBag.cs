using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MoneyBag : MonoBehaviour
{
    public static MoneyBag Instance;

    
    public GameObject moneyHolder;
    public int moneyOnPlayer;
    public List<GameObject> moneyList = new List<GameObject>();
    //public List<Transform> moneyPositions = new List<Transform>();
    public GameObject moneyPrefab;
    private void Awake()
    {
        Instance = this;
        moneyOnPlayer = Mathf.Clamp(MoneyBag.Instance.moneyOnPlayer, 0, 99999);
        //moneyLimit = moneyPositions.Count;
    }
    
    public void CollectMoney(int collectedmoney)
    {
        moneyOnPlayer += collectedmoney;

        int tempMoneyOnPlayer = moneyList.Count;
        for (int i = tempMoneyOnPlayer; i < tempMoneyOnPlayer+collectedmoney; i++)
        {
            //toplanan her para için
            GameObject tempMoney = Instantiate(moneyPrefab);
            moneyList.Add(tempMoney);
            tempMoney.transform.SetParent(transform.GetChild(transform.childCount - 1));
            tempMoney.GetComponent<Money>().Follower();

        }
    }    
    
    public void DecraseMoney(float amount,BuyArea buyarea)
    {
        TransferMoney(buyarea);
        RemoveOnListLastMoney();
      //  DestroyLastMoney(buyarea);
        moneyOnPlayer -= (int)amount;
    }
    public void DecraseMoney(float amount)
    {
        RemoveOnListLastMoney();

        DestroyLastMoney();
        moneyOnPlayer -= (int)amount;
    }

    /*  public void DecraseMoney(float amount)
      {
          RemoveOnListLastMoney();
          DestroyLastMoney();
          moneyOnPlayer -= (int)amount;
      }*/
    /*
    public void TransferMoney(BuyArea buyarea)
    {
        Debug.Log("transferred");
        moneyHolder.transform.GetChild(moneyOnPlayer - 1).transform.parent = 

       //moneyHolder.transform.GetChild(moneyOnPlayer - 1).transform.DOMove(buyarea.transform.position, .5f);
       //moneyHolder.transform.GetChild(moneyOnPlayer - 1).transform.position = Vector3.Lerp(moneyHolder.transform.GetChild(moneyOnPlayer - 1).transform.position, buyarea.transform.position, .3f);

    }
    public void RemoveOnListLastMoney()
    {
        moneyList.Remove(moneyList[moneyList.Count - 1]);
    }
    public void DestroyLastMoney()
    {

        Destroy(moneyHolder.transform.GetChild(moneyOnPlayer - 1).gameObject);

    }*/
    public void TransferMoney(BuyArea buyarea)
    {
        GameObject tempmoney = moneyHolder.transform.GetChild(moneyOnPlayer - 1).gameObject;
        moneyHolder.transform.GetChild(moneyOnPlayer - 1).transform.parent = buyarea.moneyTarget;
        tempmoney.transform.DOJump(buyarea.moneyTarget.position, 2f, 1, 1f).OnComplete(() => Destroy(tempmoney));
    }
    public void RemoveOnListLastMoney()
    {     
        moneyList.Remove(moneyList[moneyList.Count - 1]);
    }
    public void DestroyLastMoney(float amount)
    {
        Debug.Log("destroyed");
        for (int i = 0; i < (int)amount; i++)
        {
            Destroy(moneyHolder.transform.GetChild(moneyHolder.transform.childCount - 1).gameObject);
        }

    }
    public void DestroyLastMoney()
    {

        Destroy(moneyHolder.transform.GetChild(moneyOnPlayer - 1).gameObject);

    }
    /*
    public void DestroyLastMoney()
    {
        Debug.Log("destroyed");
        Destroy(moneyHolder.transform.GetChild(0).gameObject);

    }*/
}


