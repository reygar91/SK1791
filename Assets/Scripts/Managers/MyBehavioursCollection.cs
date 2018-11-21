using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBehavioursCollection : MonoBehaviour {

    public static MyBehavioursCollection Instance;

    public BehaviourPattern ChangeRoom, CustEnterReception, PersEnterReception, CustAtBar;

    private void Awake()
    {
        Instance = this;
    }

}
