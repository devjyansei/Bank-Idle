using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBag : MonoBehaviour
{
    public static MoneyBag Instance;

    public int moneyLimit;
    public GameObject moneyHolder;
    public int moneyOnPlayer;
    public List<GameObject> moneyList = new List<GameObject>();
    public List<Transform> moneyPositions = new List<Transform>();
    public GameObject moneyPrefab;
    private void Awake()
    {
        Instance = this;
        moneyOnPlayer = Mathf.Clamp(MoneyBag.Instance.moneyOnPlayer, 0, 99999);
        moneyLimit = moneyPositions.Count;
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
    public void CollectMoney2(int collectedmoney)
    {
        moneyOnPlayer += collectedmoney;
        int tempMoneyOnPlayer = moneyList.Count;
        for (int i = tempMoneyOnPlayer; i < tempMoneyOnPlayer + collectedmoney; i++)
        {
            //toplanan her para için
            GameObject tempMoney = Instantiate(moneyPrefab);
            moneyList.Add(tempMoney);
            tempMoney.transform.SetParent(transform.GetChild(transform.childCount - 1));
            tempMoney.GetComponent<Money>().Follower();

        }
    }
    public void DecraseMoney(float amount)
    {
        RemoveOnListLastMoney();
        DestroyLastMoney();
        moneyOnPlayer -= (int)amount;
    }


    public void RemoveOnListLastMoney()
    {
        moneyList.Remove(moneyList[moneyList.Count - 1]);
    }
    public void DestroyLastMoney()
    {
        Destroy(moneyHolder.transform.GetChild(moneyOnPlayer - 1).gameObject);

    }
}


