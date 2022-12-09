using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityManager : MonoBehaviour
{
    

    public int createdMoney;
    public Transform[] moneyTransforms;
    public GameObject[] moneySlot;

    public float stackOffset;
    public int rowCount;
    public GameObject moneyPrefab;
    public Transform collectPoint;

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
        float moneyCount = createdMoney;

        //int rowCount = (int)moneyCount / stackLimit;
        GameObject tempMoney = Instantiate(moneyPrefab);
        createdMoney++;
        for (int i = 0; i < moneyTransforms.Length; i++)
        {
            if (!moneySlot[i])
            {
                tempMoney.transform.position = moneyTransforms[i].transform.position + new Vector3(0,rowCount/stackOffset,0);
                tempMoney.transform.SetParent(transform.GetChild(transform.childCount - 1));
                if (moneySlot[i] == null)
                {
                    moneySlot[i] = tempMoney;
                }

                if (i == moneyTransforms.Length-1)
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
            //Debug.Log("ticketa gidicek");
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

           // Debug.Log("waitinge gidicek");

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
