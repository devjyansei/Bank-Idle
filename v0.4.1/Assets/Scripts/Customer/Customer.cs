using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    NavMeshAgent navmeshagent;

    public Transform targetPlace;
    public Transform exitPoint;
    bool inWaitingRoom;
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
        
        inWaitingRoom = false;

        int activeListCount = QueManager.Instance.emptyTicketQues.Count;

            int randomTarget = Random.Range(0, activeListCount);

            var targetQue = QueManager.Instance.emptyTicketQues[randomTarget].GetComponent<QueOrder>();



            for (int i = 0; i < targetQue.queTransformsList.Count; i++)
            {
                if (targetQue.customerList[i] == null)
                {
                    targetQue.customerList[i] = this.gameObject;

                    targetPlace = targetQue.queTransformsList[i];

                    navmeshagent.SetDestination(targetPlace.position);
                    //Debug.Log(this.gameObject.name + i + " numaralý sýraya girdi");

                    //eðer son sýra doluysa son sýrayý boþalt
                    if (targetQue.customerList[targetQue.customerList.Count - 1] != null)
                    {
                        //QueManager.Instance.activatedQues.Remove(targetQue.gameObject);
                        QueManager.Instance.emptyTicketQues.Remove(targetQue.gameObject);
                    }

                    return;

                }
            }


 

    }

    public void SetNewTargetForTeller()
    {
        inWaitingRoom = false;
        int activeTellerCount = QueManager.Instance.emptyTellerQues.Count;

        int randomTarget = Random.Range(0, activeTellerCount);

        var targetQue = QueManager.Instance.emptyTellerQues[randomTarget].GetComponent<QueOrder>();

        for (int i = 0; i < targetQue.queTransformsList.Count; i++)
        {
            if (targetQue.customerList[i] == null)
            {
                targetQue.customerList[i] = this.gameObject;

                targetPlace = targetQue.queTransformsList[i];

                navmeshagent.SetDestination(targetPlace.position);
                //Debug.Log(this.gameObject.name + i + " numaralý sýraya girdi");

                //eðer son sýra doluysa son sýrayý boþalt

                if (targetQue.customerList[targetQue.customerList.Count - 1] != null) 
                {
                    QueManager.Instance.emptyTellerQues.Remove(targetQue.gameObject);
                    
                }
                return;

            }
        }
    }
    public void SetNewTargetForWaitingRoom()
    {

            inWaitingRoom = true;
            int activeWaitingRoomCount = QueManager.Instance.emptyWaitingRoomQues.Count;

            int randomTarget = Random.Range(0, activeWaitingRoomCount);

            QueOrder targetQue = QueManager.Instance.emptyWaitingRoomQues[randomTarget].GetComponent<QueOrder>();


            for (int i = 0; i < targetQue.queTransformsList.Count; i++)
            {
                if (targetQue.customerList[i] == null)
                {
                    targetQue.customerList[i] = this.gameObject;

                    targetPlace = targetQue.queTransformsList[i];

                    navmeshagent.SetDestination(targetPlace.position);
                    //Debug.Log(this.gameObject.name + i + " numaralý sýraya girdi");


                    //eðer son sýra doluysa son sýrayý boþalt
                    if (targetQue.customerList[targetQue.customerList.Count - 1] != null)
                    {
                        QueManager.Instance.emptyWaitingRoomQues.Remove(targetQue.gameObject);
                    }

                    return;

                }
            }
        
                
        
    }
    public void SetNewTargetForWaitingArea()
    {
        inWaitingRoom = true;

        int activeWaitingRoomCount = QueManager.Instance.emptyWaitingAreaQues.Count;

        int randomTarget = Random.Range(0, activeWaitingRoomCount);

        QueOrder targetQue = QueManager.Instance.emptyWaitingAreaQues[randomTarget].GetComponent<QueOrder>();


        for (int i = 0; i < targetQue.queTransformsList.Count; i++)
        {
            if (targetQue.customerList[i] == null)
            {
                targetQue.customerList[i] = this.gameObject;

                Vector3 randomPos = new Vector3(Random.Range(-20,20), 1, Random.Range(-5, 25));

                targetQue.customerList[i] = this.gameObject;
                targetQue.queTransformsList[i].position = randomPos;
                targetPlace = targetQue.queTransformsList[i];

                navmeshagent.SetDestination(targetPlace.position);


                //eðer son sýra doluysa son sýrayý boþalt
                if (targetQue.customerList[targetQue.customerList.Count - 1] != null)
                {
                    QueManager.Instance.emptyWaitingAreaQues.Remove(targetQue.gameObject);
                }

                return;

            }
        }
    }

    public void GotoExit()
    {
        navmeshagent.SetDestination(exitPoint.position);
    }


    // waiting room funcs
    public void StartCheckForEmptyTicket()
    {
        StartCoroutine(CheckForEmptyTicket());
    }
    public IEnumerator CheckForEmptyTicket()
    {
        if (QueManager.Instance.emptyTicketQues.Count > 0 && inWaitingRoom)
        {
            SetNewTargetForTicket();          
        }
        yield return new  WaitForSeconds(1f);
    }
   



    
}


