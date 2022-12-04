using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TellerManager : MonoBehaviour
{

    public List<GameObject> createdMoneyList = new List<GameObject>();

    QueOrder queOrder;


    public int stackLimit;

    public GameObject moneyPrefab;
    public GameObject worker = null;

    public Transform spawnPoint;
    public Transform workerStandPoint;

    public bool isQueFull;
    public bool anyWorkerWorks;
    public bool isAutomatic;

    public IEnumerator tellerCoroutine;
    public IEnumerator automaticTellerCoroutine;


    private void Awake()
    {
        queOrder = GetComponent<QueOrder>();
    }
    private void OnEnable()
    {
        tellerCoroutine = StartTellerCheck();
        automaticTellerCoroutine = StartAutomaticTellerCheck();
    }

    void TellerCheck()
    {
        var customerList = GetComponent<QueOrder>().customerList;

        //Eger ilk s�rada musteri varsa onu ��k��a yonlendirir
        if (customerList[0] != null)
        {
            GenerateMoney();

            Customer firstCustomer = customerList[0].GetComponent<Customer>();
            customerList[0] = null;

            firstCustomer.GotoExit();

            // s�ray� kayd�r

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
        while (GetComponent<QueOrder>().customerList[0] != null)
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
        if (queOrder.customerList[queOrder.customerList.Count - 1] == null && !QueManager.Instance.emptyTellerQues.Contains(this.gameObject))
        {
            QueManager.Instance.emptyTellerQues.Add(this.gameObject);            
        }
    }

    
    IEnumerator StartAutomaticTellerCheck()
    {
        while (true)
        {
            if (GetComponent<QueOrder>().customerList[0] != null && Vector3.Distance(GetComponent<QueOrder>().customerList[0].transform.position, workerStandPoint.position) < 7f)
            {
                TellerCheck();
                CheckQueFill();
            }
            yield return new WaitForSeconds(2f);

        }
    }
    public void AutomateTeller()
    {
        Debug.Log("otomatikle�ti");
        isAutomatic = true;
        StopCoroutine(tellerCoroutine);
        StartCoroutine(automaticTellerCoroutine);
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
