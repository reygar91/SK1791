using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public int RoomSize;
    //public InteriorPreview Preview;
    private GameController Controller;
    public GameObject[] Interior;
    /*
     * 0 - Bar;
    */

    private void Awake()
    {
        Controller = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Personel")
        {
            Debug.Log("Its Personel");
        }
    }

    public void BuildInterior(int i)
    {
        GameObject newInterior = Instantiate(Interior[i],transform);
        newInterior.SetActive(true);
        Controller.UpdateSeats(newInterior);
    }
}
