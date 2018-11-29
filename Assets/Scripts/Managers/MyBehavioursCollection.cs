using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBehavioursCollection : MonoBehaviour {

    public static MyBehavioursCollection Instance;

    public CustomersBehaviours Customer; 
    public PersonnelBehaviours Personnel;

    public BehaviourPattern ChangeRoom;

    private void Awake()
    {
        Instance = this;
    }

}
[System.Serializable]
public class CustomersBehaviours
{
    public BehaviourPattern CountDown_0, EnterReception, AtBar;
}
[System.Serializable]
public class PersonnelBehaviours
{
    public BehaviourPattern CountDown_0, EnterReception, AtBar;
}
