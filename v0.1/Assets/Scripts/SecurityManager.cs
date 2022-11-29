using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityManager : MonoBehaviour
{
    

    public List<GameObject> createdMoneyList = new List<GameObject>();

    public int stackLimit;

    public GameObject moneyPrefab;
    public Transform spawnPoint;

    public IEnumerator securityCoroutine;

    public bool isQueFull;
    private void Awake()
    {
        securityCoroutine = StartSecurityCheck();
    }
    public void GenerateMoney()
    {
        float moneyCount = createdMoneyList.Count;
        int rowCount = (int)moneyCount / stackLimit;


        GameObject tempMoney = Instantiate(moneyPrefab);
       
        tempMoney.transform.position = new Vector3(spawnPoint.position.x + ((float)rowCount / 3), (moneyCount % stackLimit) / 20, spawnPoint.position.z);
        createdMoneyList.Add(tempMoney);
    }


    IEnumerator StartSecurityCheck()
    {
        while (true)
        {

            SecurityCheck();
            yield return new WaitForSeconds(1);

        }

    }
    void SecurityCheck() // SECURITY SIRASINI KONTROL EDER  VE SIRAYI KAYDIRIR
    {
        
        var customerList =  GetComponent<QueOrder>().customerList;

        if (customerList[0] != null)
        {

            Customer firstCustomer = customerList[0].GetComponent<Customer>();

            customerList[0] = null;
            firstCustomer.SetNewTargetForTicket();
            GenerateMoney();

            //sýrayý kaydýr
            for (int i = 1; i < customerList.Count; i++)
            {
                if (customerList[i] != null)
                {
                    customerList[i].GetComponent<Customer>().AlignQue(this.GetComponent<QueOrder>());
                    customerList[i] = null;
                }
            }
        }
    }
}
