using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HirePersonel : MonoBehaviour {

    public GameObject personel;
    
    public void Hire()
    {
        personel.SetActive(true);
    }
}
