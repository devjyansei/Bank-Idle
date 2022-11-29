using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmManager : MonoBehaviour
{
    public List<GameObject> createdMoneyList = new List<GameObject>();

    public int stackLimit;

    public GameObject moneyPrefab;
    public Transform spawnPoint;
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
            if (queOrder.customerList[0] != null && Vector3.Distance(queOrder.customerList[0].transform.position, this.transform.position) < 1.5f) //atmnýn çalýsmasý ýcýn mesafenýn kýsa olmasý lazým
            {
                AtmCheck();
                CheckQueFill();
            }
            yield return new WaitForSeconds(3f);

        }

    }
    void AtmCheck()
    {
   
            if (queOrder.customerList[0] != null)
            {
                Customer firstCustomer = queOrder.customerList[0].GetComponent<Customer>();
            queOrder.customerList[0] = null;

                firstCustomer.GotoExit(); // degisecek ve baska bir yere gidecek.

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
        
        //Eger ilk sýrada musteri varsa onu çýkýþa yonlendirir
        
    }

    public void GenerateMoney()
    {
        float moneyCount = createdMoneyList.Count;
        int rowCount = (int)moneyCount / stackLimit;


        GameObject tempMoney = Instantiate(moneyPrefab);

        tempMoney.transform.position = new Vector3(spawnPoint.position.x + ((float)rowCount / 3), (moneyCount % stackLimit) / 20, spawnPoint.position.z);
        createdMoneyList.Add(tempMoney);
    }
    void CheckQueFill()
    {
        if (queOrder.customerList[queOrder.customerList.Count - 1] == null && !QueManager.Instance.activatedQues.Contains(this.gameObject))
        {
            QueManager.Instance.activatedQues.Add(this.gameObject);
        }
    }
}
