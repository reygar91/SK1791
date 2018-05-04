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
    //public SeatManager[] SeatsReference;
    public List<GameObject> unOccupiedSeats;
    private GameObject selectedObject;

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
        RaycastHit();        
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

    private void RaycastHit()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse is down");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (!IsPointerOverUIObject())
                {
                    selectedObject = hit.transform.gameObject;
                    Debug.Log("Hit " + selectedObject.name);
                    if (selectedObject.tag == "Customer")
                    {
                        Panels[0].SetActive(true);
                    }
                    if (selectedObject.tag == "Personel")
                    {
                        Panels[1].SetActive(true);
                    }
                }                
            }
            else
            {
                Debug.Log("No hit");
            }
            //Debug.Log("Mouse is down");
        }
    }

    //When Touching UI check
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public GameObject getSelectedObject()
    {
        return selectedObject;
    }

    
}
