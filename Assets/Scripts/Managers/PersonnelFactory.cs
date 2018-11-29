using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonnelFactory : MonoBehaviour {

    protected PersonnelFactory() { }
    [SerializeField] private myCharacterController PersonnelPrefab;
    [SerializeField] private GameObject PersonnelContainer;
    public List<myCharacterController> PersonnelPool = new List<myCharacterController>();

    public static PersonnelFactory Instance;

    private void Awake()
    {
        Instance = this;
    }

    public myCharacterController SpawnPersonnel(Personnel personnel)
    {
        myCharacterController CC = GetController();
        CC.Stats = personnel;
        CC.gameObject.SetActive(true);
        return CC;
    }

    private myCharacterController GetController()
    {
        myCharacterController CC = null;
        if (PersonnelPool.Count == 0)
            CC = Instantiate(PersonnelPrefab, PersonnelContainer.transform);
        else
            CC = PersonnelPool[0];
        return CC;
    }
}
