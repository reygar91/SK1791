using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonnelFactory : MonoBehaviour {

    protected PersonnelFactory() { }
    [SerializeField] private BehaviourController PersonnelPrefab;
    [SerializeField] private GameObject PersonnelContainer;
    public List<BehaviourController> PersonnelPool = new List<BehaviourController>();

    public static PersonnelFactory Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnPersonnel(Personnel personnel)
    {
        BehaviourController BC = GetController();
        BC.character = personnel;
        BC.gameObject.SetActive(true);
        //return BC;
    }

    private BehaviourController GetController()
    {
        BehaviourController BC = null;
        if (PersonnelPool.Count == 0)
            BC = Instantiate(PersonnelPrefab, PersonnelContainer.transform);
        else
            BC = PersonnelPool[0];
        return BC;
    }
}
