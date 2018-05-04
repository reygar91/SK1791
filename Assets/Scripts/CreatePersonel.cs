using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePersonel : MonoBehaviour {

    public GameObject personel;
    public HirePersonel HireBTN;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AssignPersonel()
    {
        HireBTN.personel = personel;
    }
}
