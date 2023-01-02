using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    public int totalCustomerInBank;
    public int customerLimit;
    public Transform customerSpawnPosition;

    
    QueOrder queOrder;

    public List<GameObject> customersPrefabsList = new List<GameObject>();
    public float spawnInterval =1f;

    private void Awake()
    {
        Instance = this;
        queOrder = FindObjectOfType<SecurityManager>().GetComponent<QueOrder>();
    }
    private void Start()
    {
        StartSpawner();
    }
    void StartSpawner()
    {
        StartCoroutine(Spawner());
    }
    IEnumerator Spawner()
    {
        while (true)
        {
            yield return new WaitUntil(() => queOrder.customerList[queOrder.customerList.Count - 1] == null);
            yield return new WaitUntil(() => totalCustomerInBank < customerLimit);

            GameObject tempCustomer = customersPrefabsList[Random.Range(0, customersPrefabsList.Count - 1)];
            Instantiate(tempCustomer, customerSpawnPosition.position,transform.rotation);
            totalCustomerInBank++;
            yield return new WaitForSeconds(spawnInterval);
        }

    }


}
