using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBag : MonoBehaviour
{
    public static MoneyBag Instance;

    public GameObject moneyHolder;
    public int moneyOnPlayer;
    public List<GameObject> moneyList = new List<GameObject>();
    public List<Transform> moneyPositions = new List<Transform>();
    public GameObject moneyPrefab;
    private void Awake()
    {
        Instance = this;
        moneyOnPlayer = Mathf.Clamp(MoneyBag.Instance.moneyOnPlayer, 0, 99999);
        
    }
    public void CollectMoney(int collectedmoney)
    {
        moneyOnPlayer += collectedmoney;


        int tempMoneyOnPlayer = moneyList.Count;
        for (int i = tempMoneyOnPlayer; i < tempMoneyOnPlayer+collectedmoney; i++)
        {
            GameObject tempMoney = Instantiate(moneyPrefab);
            moneyList.Add(tempMoney);
            tempMoney.transform.SetParent(transform.GetChild(transform.childCount - 1));
            tempMoney.GetComponent<Money>().Follower();

            // StartCoroutine(TrackIndexPosition(tempMoney, moneyPositions[i]));

            //StartCoroutine(tempMoney.GetComponent<Money>().FollowIndex(moneyPositions[i]));  // topladgýmýz para indexi takip edicek.

        }
    }    

    // paralar ait oldugu indexi takip eder.

    public IEnumerator TrackIndexPosition(GameObject money, Transform followPos)
    {    
            while (true)
            {
                money.transform.position = followPos.position;
                yield return null;
            }
                
    }
    public void IncreaseMoney(float amount)
    {
       moneyOnPlayer += (int)amount;
    }
    public void DecraseMoney(float amount)
    {
        //Transform moneyWillRemove = moneyHolder.transform.GetChild(moneyHolder.transform.childCount - 1);//son sýradaki para
        Destroy(moneyHolder.transform.GetChild(moneyOnPlayer-1).gameObject);
        moneyOnPlayer -= (int)amount;

        moneyList.Remove(moneyList[moneyList.Count - 1]);



        // parayý hata almadan yoketmenýn býr yolunu bul

    }

}


