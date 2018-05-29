using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour {

    public GameObject Doors, MiddleOfTheRoom;
    public List<GameObject> Seats;
    public List<Customer> custAtBar;

    private void OnMouseDown()
    {
        Debug.Log("Room Clicked!");
    }
}
