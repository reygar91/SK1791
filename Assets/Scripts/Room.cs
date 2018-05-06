using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    //public int RoomSize;
    //public InteriorPreview Preview;
    //private GameController Controller;
    public GameObject[] Interior;
    /*
     * 0 - Bar;
    */
    public bool allowedToBuild;

    private void Awake()
    {
        //Controller = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Room")
        {
            allowedToBuild = false;
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Room")
        {
            allowedToBuild = true; //Debug.Log("Colliding with other Room");
        }
    }

    public void BuildInterior(int i)
    {
        GameObject newInterior = Instantiate(Interior[i],transform);
        newInterior.SetActive(true);
        switch (i)
        {
            case 0:
                name = name + "_(Bar)";
                break;
            case 1:
                break;
        }
        //Controller.UpdateSeats(newInterior);
    }
}
