using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    NavMeshAgent navmeshagent;

    public Transform targetPlace;
    public Transform exitPoint;
    //public Transform waitingRoom;
    private void Awake()
    {
        navmeshagent = GetComponent<NavMeshAgent>();
        targetPlace = this.transform;
        
    }

    private void Start()
    {
        Variables.Instance.priority++;
        navmeshagent.avoidancePriority = Variables.Instance.priority;
        
        AlignQue(GameObject.Find("Security").GetComponent<QueOrder>());
    }
    
    public void AlignQue(QueOrder queOrder)
    {
        
        for (int i = 0; i < queOrder.queTransformsList.Count; i++)
        {
            var tempCustomerList = queOrder.customerList;
            if (queOrder.customerList[i] == null)
            {
                queOrder.customerList[i] = this.gameObject;

                targetPlace = queOrder.queTransformsList[i];
                navmeshagent.SetDestination(targetPlace.position);
                //Debug.Log(this.gameObject.name + i + " numaralý sýraya girdi");               
                
                return;

            }
        }
    }
    

    public void SetNewTargetForTicket() // randomize system
    {


        int activeListCount = QueManager.Instance.activatedQues.Count;

        int randomTarget = Random.Range(0, activeListCount);

        var targetQue =  QueManager.Instance.activatedQues[randomTarget].GetComponent<QueOrder>();
        
        for (int i = 0; i < targetQue.queTransformsList.Count; i++)
        {
            if (targetQue.customerList[i] == null)
            {
                targetQue.customerList[i] = this.gameObject;

                targetPlace = targetQue.queTransformsList[i];
                
                navmeshagent.SetDestination(targetPlace.position);
                Debug.Log(this.gameObject.name + i + " numaralý sýraya girdi");

                //eðer son sýra doluysa son sýrayý boþalt
                if (targetQue.customerList[targetQue.customerList.Count - 1] != null) 
                {
                    QueManager.Instance.activatedQues.Remove(targetQue.gameObject);
                }

                return;

            }
        }
               
    }
    
    
    public void SetNewTargetForTeller()
    {
        int activeTellerCount = QueManager.Instance.activatedTellerQues.Count;

        int randomTarget = Random.Range(0, activeTellerCount);

        var targetQue = QueManager.Instance.activatedTellerQues[randomTarget].GetComponent<QueOrder>();

        for (int i = 0; i < targetQue.queTransformsList.Count; i++)
        {
            if (targetQue.customerList[i] == null)
            {
                targetQue.customerList[i] = this.gameObject;

                targetPlace = targetQue.queTransformsList[i];

                navmeshagent.SetDestination(targetPlace.position);
                Debug.Log(this.gameObject.name + i + " numaralý sýraya girdi");

                //eðer son sýra doluysa son sýrayý boþalt

                if (targetQue.customerList[targetQue.customerList.Count - 1] != null) 
                {
                    QueManager.Instance.activatedTellerQues.Remove(targetQue.gameObject);
                }

                return;

            }
        }
    }
    public void GotoExit()
    {
        navmeshagent.SetDestination(exitPoint.position);
    }

    // SON DURUM : MUSTERILER BOS OLAN YERLERE GIDIYOR. EGER TUM YERLER DOLARSA VE HALA SECURITYDEN GECIRMEYE CALISIRSAK HATA ALIYORUZ. 
    // YAPILMASI GEREKEN :  EGER HIC AKTIF SIRA YOKSA MUSTERILER SECURITYDEN GECEMEMESI LAZIM
}


