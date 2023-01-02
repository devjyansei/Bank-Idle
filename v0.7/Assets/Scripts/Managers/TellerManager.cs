using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TellerManager : MonoBehaviour
{
    public float tellerCooldown;
    public float tellerAutomaticCooldown;

    public int createdMoney;
    public Transform[] moneyTransforms;
    public GameObject[] moneySlot;
    public Transform collectPoint;
    public float stackOffset;
    public int rowCount;

    QueOrder queOrder;

    public GameObject moneyPrefab;
    public GameObject worker = null;

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
            yield return new WaitForSeconds(tellerCooldown);

            if (GetComponent<QueOrder>().customerList[0] != null && Vector3.Distance(GetComponent<QueOrder>().customerList[0].transform.position, workerStandPoint.position) < 5f)
            {
                TellerCheck();
                CheckQueFill();
            }
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
            yield return new WaitForSeconds(tellerAutomaticCooldown);

        }
    }
    public void AutomateTeller()
    {
        Debug.Log("otomatikleþti");
        isAutomatic = true;
        StopCoroutine(tellerCoroutine);
        StartCoroutine(automaticTellerCoroutine);
    }
    public void GenerateMoney()
    {

        GameObject tempMoney = Instantiate(moneyPrefab, GetComponent<QueOrder>().queTransformsList[0].position, transform.rotation);
        createdMoney++;
        for (int i = 0; i < moneyTransforms.Length; i++)
        {
            if (!moneySlot[i])
            {
                tempMoney.transform.DOJump(moneyTransforms[i].transform.position + new Vector3(0, rowCount / stackOffset, 0), 3f, 1, 1f);
                //tempMoney.transform.position = moneyTransforms[i].transform.position + new Vector3(0, rowCount / stackOffset, 0);
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
}
