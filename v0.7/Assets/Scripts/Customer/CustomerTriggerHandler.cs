using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerTriggerHandler : MonoBehaviour
{
    NavMeshAgent navmeshagent;
    Customer customer;
    Animator anim;
    private void Awake()
    {
        customer = GetComponent<Customer>();
        navmeshagent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Exit"))
        {
            SpawnManager.Instance.totalCustomerInBank--;
            StopCoroutine(customer.CheckPatience());
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("Chair") && Vector3.Distance(customer.targetPlace.position,other.gameObject.transform.position) < 0.02f)
        {
            navmeshagent.enabled = false;
            PlaySitAnim();
            transform.LookAt(other.gameObject.transform.GetChild(0).transform.position);
        }
    }
 
    public void PlayWaitAnim()
    {
        anim.SetBool("isWaiting", true);
        anim.SetBool("isWalking", false);
        anim.SetBool("isSitting", false);
    }
    public void PlayWalkAnim()
    {
        anim.SetBool("isWaiting", false);
        anim.SetBool("isWalking", true);
        anim.SetBool("isSitting", false);
    }
    public void PlaySitAnim()
    {
        anim.SetBool("isWaiting", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isSitting", true);
    }

}
