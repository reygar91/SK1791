using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour {

    public GameObject MenuPanel;
    public Transform[] WayPoint;
    /*
     * 0 - SpawnPoint
     * 1 - BarEntrance
     */
    public Customer[] CustomerList;
    public GameObject[] Panels;
    /* Panels:
     * 0 - CustPanel
     * 1 - PersonelPanel
     * */
    public List<GameObject> unOccupiedSeats;

    private void Awake()
    {
        unOccupiedSeats = new List<GameObject>();
        GameObject[] Seats = GameObject.FindGameObjectsWithTag("Seat");
        foreach (GameObject item in Seats)
        {
            unOccupiedSeats.Add(item);
        }
    }

    // Use this for initialization
    void Start () {
        StartCoroutine("ActivateCustomer");        
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Cancel") && !DisableWithEsc.ActivePanel())
        {
            MenuPanel.SetActive(true);
        }               
    }

    private IEnumerator ActivateCustomer()
    {
        while (true)
        {
            for (int i=0; i < CustomerList.Length; i++)
            {
                if (CustomerList[i].gameObject.activeSelf == false)
                {
                    CustomerList[i].gameObject.SetActive(true);
                    yield return new WaitForSeconds(2);
                }
                else if (i == (CustomerList.Length - 1))
                {
                    yield return new WaitForSeconds(5);
                }
            }            
        }
    }  

    
}
