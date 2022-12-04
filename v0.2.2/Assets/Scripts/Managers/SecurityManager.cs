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
    QueOrder queOrder;
    private void Awake()
    {
        securityCoroutine = StartSecurityCheck();
        queOrder = GetComponent<QueOrder>();
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
            if (queOrder.customerList[0] != null && Vector3.Distance(queOrder.customerList[0].transform.position, this.transform.position) < 3f)
            {
                SecurityCheck();

            }
            yield return new WaitForSeconds(1f);

        }

    }
    void SecurityCheck() // SECURITY SIRASINI KONTROL EDER  VE SIRAYI KAYDIRIR
    {



        //Eger ilk sýrada musteri varsa onu bir ticketa yonlendirir
        if (queOrder.customerList[0] != null && QueManager.Instance.emptyTicketQues.Count > 0 && QueManager.Instance.emptyWaitingRoomQues[0].GetComponent<QueOrder>().customerList[0] == null)  // ticketa gider
        {
            Debug.Log("ticketa gidicek");
            Customer firstCustomer = queOrder.customerList[0].GetComponent<Customer>();

            queOrder.customerList[0] = null;
            firstCustomer.SetNewTargetForTicket();
            GenerateMoney();

            //sýrayý kaydýr
            for (int i = 1; i < queOrder.customerList.Count; i++)
            {
                if (queOrder.customerList[i] != null)
                {
                    queOrder.customerList[i].GetComponent<Customer>().AlignQue(this.GetComponent<QueOrder>());
                    queOrder.customerList[i] = null;
                }
            }
        }
        else if (queOrder.customerList[0] != null && QueManager.Instance.emptyTicketQues.Count == 0 && QueManager.Instance.emptyWaitingRoomQues.Count > 0) // waiting rooma gider
        {

            Debug.Log("waitinge gidicek");

            Customer firstCustomer = queOrder.customerList[0].GetComponent<Customer>();

            queOrder.customerList[0] = null;
            
            firstCustomer.SetNewTargetForWaitingRoom();
            
            GenerateMoney();


            //sýrayý kaydýr
            for (int i = 1; i < queOrder.customerList.Count; i++)
            {
                if (queOrder.customerList[i] != null)
                {
                    queOrder.customerList[i].GetComponent<Customer>().AlignQue(this.GetComponent<QueOrder>());
                    queOrder.customerList[i] = null;
                }
            }
        }
        else return;
    }
    
}
