using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmManager : MonoBehaviour
{
    public int atmCooldown;
    public List<GameObject> createdMoneyList = new List<GameObject>();

    public int createdMoney;
    public Transform[] moneyTransforms;
    public GameObject[] moneySlot;
    public Transform collectPoint;
    public float stackOffset;
    public int rowCount;

    public int stackLimit;

    public GameObject moneyPrefab;
    QueOrder queOrder;
    private void Awake()
    {
         queOrder = GetComponent<QueOrder>();
    }
    private void OnEnable()
    {
        StartCoroutine(StartAtmCheck());

    }
    IEnumerator StartAtmCheck()
    {
        

        while (true)
        {
            if (queOrder.customerList[0] != null && Vector3.Distance(queOrder.customerList[0].transform.position, this.transform.position) < 2f) //atmnýn çalýsmasý ýcýn mesafenýn kýsa olmasý lazým
            {
                AtmCheck();
                CheckQueFill();
            }
            yield return new WaitForSeconds(atmCooldown);

        }

    }
    void AtmCheck()
    {
   
            if (queOrder.customerList[0] != null)
            {
                Customer firstCustomer = queOrder.customerList[0].GetComponent<Customer>();
                queOrder.customerList[0] = null;

                firstCustomer.GotoExit(); 

                GenerateMoney();

                // sýrayý kaydýr

                for (int i = 1; i < queOrder.customerList.Count; i++)
                {
                    if (queOrder.customerList[i] != null)
                    {
                    queOrder.customerList[i].GetComponent<Customer>().AlignQue(this.GetComponent<QueOrder>());
                    queOrder.customerList[i] = null;
                    }

                }
            }
        
    
        
    }

    public void GenerateMoney()
    {
        GameObject tempMoney = Instantiate(moneyPrefab);
        createdMoney++;
        for (int i = 0; i < moneyTransforms.Length; i++)
        {
            if (!moneySlot[i])
            {
                tempMoney.transform.position = moneyTransforms[i].transform.position + new Vector3(0, rowCount / stackOffset, 0);
                tempMoney.transform.SetParent(transform.GetChild(transform.childCount - 1));
                if (moneySlot[i] == null)
                {
                    moneySlot[i] = tempMoney;
                }

                if (i == moneyTransforms.Length - 1)
                {
                    rowCount++;
                    for (int a = 0; a < moneySlot.Length; a++)
                    {
                        moneySlot[a] = null;
                    }
                }
                return;
            }

        }
    }
    void CheckQueFill()
    {
        if (queOrder.customerList[queOrder.customerList.Count - 1] == null && !QueManager.Instance.activatedQues.Contains(this.gameObject))
        {
            QueManager.Instance.activatedQues.Add(this.gameObject);
        }
    }
}
