using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerFactory : MonoBehaviour {

    protected CustomerFactory() { }
    [SerializeField] private BehaviourController CustomerPrefab;
    [SerializeField] private GameObject CustomersContainer;
    public List<BehaviourController> CustomersPool = new List<BehaviourController>();

    public static CustomerFactory Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnCustomer(Customer customer)
    {
        BehaviourController BC = GetController();
        BC.character = customer;
        BC.gameObject.SetActive(true);
        //return BC;
    }

    private BehaviourController GetController()
    {
        BehaviourController BC = null;
        if (CustomersPool.Count == 0)
            BC = Instantiate(CustomerPrefab, CustomersContainer.transform);
        else
            BC = CustomersPool[0];
        return BC;
    }

}
