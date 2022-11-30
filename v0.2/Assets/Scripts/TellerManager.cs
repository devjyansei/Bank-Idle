using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TellerManager : MonoBehaviour
{

    public List<GameObject> createdMoneyList = new List<GameObject>();

    public int stackLimit;
    public GameObject moneyPrefab;
    public Transform spawnPoint;

    public IEnumerator tellerCoroutine;

    public bool isQueFull;

    QueOrder queOrder;
    private void Awake()
    {
        queOrder = GetComponent<QueOrder>();
    }
    private void OnEnable()
    {
        tellerCoroutine = StartTellerCheck();
    }

    void TellerCheck()
    {
        var customerList = GetComponent<QueOrder>().customerList;

        //Eger ilk sýrada musteri varsa onu çýkýþa yonlendirir
        if (customerList[0] != null)
        {
            GenerateMoney();

            Customer firstCustomer = customerList[0].GetComponent<Customer>();
            customerList[0] = null;

            firstCustomer.GotoExit();


            // sýrayý kaydýr

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
    public IEnumerator StartTellerCheck()
    {
        while (true)
        {
            TellerCheck();
            CheckQueFill();
            yield return new WaitForSeconds(2f);
        }
    }
    void CheckQueFill()
    {
        if (queOrder.customerList[queOrder.customerList.Count - 1] == null && !QueManager.Instance.activatedQues.Contains(this.gameObject))
        {
            QueManager.Instance.activatedQues.Add(this.gameObject);
        }
    }

    public void GenerateMoney()
    {
        if (GetComponent<QueOrder>().customerList[0] != null) // eger musteri varsa
        {
            float moneyCount = createdMoneyList.Count;
            int rowCount = (int)moneyCount / stackLimit;


            GameObject tempMoney = Instantiate(moneyPrefab);

            tempMoney.transform.position = new Vector3(spawnPoint.position.x + ((float)rowCount / 3), (moneyCount % stackLimit) / 20, spawnPoint.position.z);
            createdMoneyList.Add(tempMoney);

        }

    }
}
