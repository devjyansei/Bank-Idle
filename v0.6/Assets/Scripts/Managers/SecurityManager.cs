using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityManager : MonoBehaviour
{

    public float securityCooldown;
    public float securityAutomaticCooldown;

    public int createdMoney;
    public Transform[] moneyTransforms;
    public GameObject[] moneySlot;

    public float stackOffset;
    public int rowCount;
    public GameObject moneyPrefab;
    public Transform collectPoint;

    public IEnumerator securityCoroutine;
    public IEnumerator automaticSecurityCoroutine;

    QueOrder queOrder;

    public GameObject worker = null;
    public Transform workerStandPoint;
    public bool isQueFull;
    public bool anyWorkerWorks;
    public bool isAutomatic;

    private void Awake()
    {
        securityCoroutine = StartSecurityCheck();
        automaticSecurityCoroutine = StartAutomaticSecurityCheck();
        queOrder = GetComponent<QueOrder>();

    }
    void SecurityCheck()
    {


        //Eger ilk sýrada musteri varsa onu bir ticketa yonlendirir
        if (queOrder.customerList[0] != null && QueManager.Instance.emptyTicketQues.Count > 0)  // ticketa gider   -  && QueManager.Instance.emptyWaitingRoomQues[0].GetComponent<QueOrder>().customerList[0] == null
        {
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
        else if (queOrder.customerList[0] != null && QueManager.Instance.emptyTicketQues.Count == 0 && (QueManager.Instance.emptyWaitingRoomQues.Count > 0 || QueManager.Instance.emptyWaitingAreaQues.Count > 0))
        {
            int random = Random.Range(0, QueManager.Instance.emptyWaitingRoomQues.Count + QueManager.Instance.emptyWaitingAreaQues.Count);//0,1,2 gelebilir

            if (random < QueManager.Instance.emptyWaitingRoomQues.Count)
            {
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
            else if (random >= QueManager.Instance.emptyWaitingRoomQues.Count)
            {
                Customer firstCustomer = queOrder.customerList[0].GetComponent<Customer>();

                queOrder.customerList[0] = null;

                firstCustomer.SetNewTargetForWaitingArea();

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
        }
        else return;


    }

    IEnumerator StartSecurityCheck()
    {
        while (true)
        {

            if (queOrder.customerList[0] != null && Vector3.Distance(queOrder.customerList[0].transform.position, this.transform.position) < 3f)
            {
                SecurityCheck();
            }
            yield return new WaitForSeconds(securityCooldown);

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

    public IEnumerator StartAutomaticSecurityCheck()
    {
        while (true)
        {
            if (GetComponent<QueOrder>().customerList[0] != null && QueManager.Instance.emptyTellerQues.Count > 0 && Vector3.Distance(GetComponent<QueOrder>().customerList[0].transform.position, workerStandPoint.position) < 7f)
            {
                SecurityCheck();
                //CheckQueFill();
            }
            yield return new WaitForSeconds(securityAutomaticCooldown);

        }
    }
    public void AutomateSecurity()
    {
        Debug.Log("otomatikleþti");
        isAutomatic = true;
        StopCoroutine(securityCoroutine);
        StartCoroutine(automaticSecurityCoroutine);
    }


}
