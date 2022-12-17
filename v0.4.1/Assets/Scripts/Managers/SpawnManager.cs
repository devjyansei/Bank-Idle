using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    public int totalCustomerInBank;
    public int customerLimit;
    public Transform customerSpawnPosition;

    public GameObject customerPrefab;
    QueOrder queOrder;
    [Range(0,100)]
    public float spawnInterval;

    private void Awake()
    {
        Instance = this;
        queOrder = GameObject.Find("Security").GetComponent<QueOrder>();
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

            GameObject tempCustomer = Instantiate(customerPrefab);
            tempCustomer.transform.position = customerSpawnPosition.position;
            totalCustomerInBank++;
            yield return new WaitForSeconds(spawnInterval);
        }

    }


}
