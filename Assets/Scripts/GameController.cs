using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject MenuPanel;
    public Transform[] WayPoint;
    /*
     * 0 - SpawnPoint
     * 1 - BarEntrance
     */
    public GameObject CustomerOriginal, CustomerContainer; //IMPORTANT CustOrig must be not active, or in coroutine it lead to endless Instantiations
    //private Customer[] CustomerList;
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
        StartCoroutine("SpawnCustomer");        
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Cancel") && !DisableWithEsc.ActivePanel())
        {
            MenuPanel.SetActive(true);
        }               
    }

    private IEnumerator SpawnCustomer()
    {
        GameObject[] CustomerList = new GameObject[1];
        CustomerList[0] = Instantiate(CustomerOriginal, CustomerContainer.transform);
        while (true)
        {
            for (int i=0; i < CustomerList.Length; i++)
            {
                if (CustomerList[i].gameObject.activeSelf == false)
                {
                    CustomerList[i].gameObject.SetActive(true);
                    yield return new WaitForSeconds(5);
                }
                else if (i == (CustomerList.Length - 1))
                {
                    Array.Resize(ref CustomerList, i + 2);
                    CustomerList[i + 1] = Instantiate(CustomerOriginal, CustomerContainer.transform);
                    //yield return new WaitForSeconds(5);
                }
            }
        }
    }
    /*
    public void UpdateSeats(GameObject item)
    {
        int seat = item.transform.childCount; Debug.Log(seat);
        //seat.position = new Vector3(0,0,0);
    }
    */
}
