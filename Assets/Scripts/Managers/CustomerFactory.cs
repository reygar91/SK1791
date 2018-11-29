using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerFactory : MonoBehaviour {

    protected CustomerFactory() { }
    [SerializeField] private myCharacterController CustomerPrefab;
    [SerializeField] private GameObject CustomersContainer;
    public List<myCharacterController> CustomersPool = new List<myCharacterController>();

    public static CustomerFactory Instance;

    private void Awake()
    {
        Instance = this;
    }

    public myCharacterController SpawnCustomer(Customer customer)
    {
        myCharacterController CC = GetController();
        CC.Stats = customer;
        CC.gameObject.SetActive(true);
        return CC;
    }

    private myCharacterController GetController()
    {
        myCharacterController CC = null;
        if (CustomersPool.Count == 0)
            CC = Instantiate(CustomerPrefab, CustomersContainer.transform);
        else
            CC = CustomersPool[0];
        return CC;
    }

}
