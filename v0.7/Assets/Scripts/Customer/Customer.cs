using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{

    NavMeshAgent navmeshagent;
    EmojiControl emojiControl;
    CustomerTriggerHandler customerTriggerHandler;
    public Transform targetPlace;
    public Transform exitPoint;
    public bool inWaitingRoom;
    public bool inWaitingArea;
    public float currentPatience;
    public float patienceLimit;
    
    private void Awake()
    {
        navmeshagent = GetComponent<NavMeshAgent>();
        targetPlace = this.transform;
        emojiControl = GetComponent<EmojiControl>();
        customerTriggerHandler = GetComponent<CustomerTriggerHandler>();
    }

    private void Start()
    {
        StartCoroutine(TargetDistanceCheck());
        Variables.Instance.priority++;
        navmeshagent.avoidancePriority = Variables.Instance.priority;

        patienceLimit = (float)Random.Range(20, 50);

        AlignQue(FindObjectOfType<SecurityManager>().GetComponent<QueOrder>());

    }

    private void Update()
    {
        currentPatience += Time.deltaTime;
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



                navmeshagent.enabled = true;
                navmeshagent.SetDestination(targetPlace.position);
                customerTriggerHandler.PlayWalkAnim();


                ResetPatience();
                
                StartCoroutine(CheckPatience());
                return;

            }
        }
    }

   /* public void AlignQue(QueOrder queOrder)
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
    }*/
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

                CheckRandomHappyEmoji();
                ResetPatience();

                navmeshagent.enabled = true;
                customerTriggerHandler.PlayWalkAnim();
                
                navmeshagent.SetDestination(targetPlace.position);


                    //eðer son sýra doluysa son sýrayý boþalt
                    if (targetQue.customerList[targetQue.customerList.Count - 1] != null)
                    {
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

                CheckRandomHappyEmoji();
                ResetPatience();

                customerTriggerHandler.PlayWalkAnim();

                navmeshagent.SetDestination(targetPlace.position);



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


                ResetPatience();

                customerTriggerHandler.PlayWalkAnim();

                navmeshagent.SetDestination(targetPlace.position);


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
        inWaitingArea = true;

        int activeWaitingRoomCount = QueManager.Instance.emptyWaitingAreaQues.Count;

        int randomTarget = Random.Range(0, activeWaitingRoomCount);

        QueOrder targetQue = QueManager.Instance.emptyWaitingAreaQues[randomTarget].GetComponent<QueOrder>();


        for (int i = 0; i < targetQue.queTransformsList.Count; i++)
        {
            if (targetQue.customerList[i] == null)
            {
                targetQue.customerList[i] = this.gameObject;

                Vector3 randomPos = new Vector3(Random.Range(-20,5), 1, Random.Range(-15, 15));

                targetQue.customerList[i] = this.gameObject;
                targetQue.queTransformsList[i].position = randomPos;
                targetPlace = targetQue.queTransformsList[i];

                ResetPatience();

                customerTriggerHandler.PlayWalkAnim();

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
        targetPlace = exitPoint;
        navmeshagent.SetDestination(targetPlace.position);
        customerTriggerHandler.PlayWalkAnim();



    }


    // waiting room funcs
    public void StartCheckForEmptyTicket()
    {
        StartCoroutine(CheckForEmptyTicket());
    }


    public IEnumerator CheckForEmptyTicket()
    {
        
            
            if (QueManager.Instance.emptyTicketQues.Count > 0 && inWaitingRoom || QueManager.Instance.emptyTicketQues.Count > 0 && inWaitingArea)
            {
                SetNewTargetForTicket();
            }
            float random = Random.Range(1f, 2f);

            yield return new WaitForSeconds(random);
        

    }
   

    public IEnumerator TargetDistanceCheck()
    {
        
            if (Vector3.Distance(this.transform.position, targetPlace.transform.position) < 1f && targetPlace.transform.GetComponentInParent<WaitingRoomManager>() == null) // koltuk deðilse ve yaklasmýssa
            {
                customerTriggerHandler.PlayWaitAnim();

            }
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(TargetDistanceCheck());
        
    }






    // emoji funcs

    void ResetPatience()
    {
        currentPatience = 0f;

    }
    public IEnumerator CheckPatience()
    {
        
        if (currentPatience >= patienceLimit)
        {
            
            emojiControl.emojiMad.gameObject.SetActive(true);
            ResetPatience();
        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(CheckPatience());

    }
    void CheckRandomHappyEmoji()
    {
        int random = Random.Range(0, 10);
        if (random <= 1 ) // 0 - 1 - 2 
        {
            
            emojiControl.emojiJoy.gameObject.SetActive(true);
        }
    }
}


