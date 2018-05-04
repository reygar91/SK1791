using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustPanelScript : MonoBehaviour {

    //public GameController Controller;
    public MyRaycaster Raycaster2D;
    private GameObject previousSelectedObject, selectedObject;
    private Text text;
    private Customer cust;
    private int patience;
    public Vector3 offset; 

    // Use this for initialization

    private void Awake()
    {
        text = gameObject.GetComponentInChildren<Text>();
    }
    private void OnEnable()
    {
        selectedObject = Raycaster2D.getSelectedObject();
        cust = selectedObject.GetComponent<Customer>();
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            previousSelectedObject = selectedObject;
            selectedObject = Raycaster2D.getSelectedObject();
            if (selectedObject.tag == "Customer")
            {
                //gameObject.SetActive(true);
                if (selectedObject != previousSelectedObject)
                {
                    cust = selectedObject.GetComponent<Customer>();                    
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        patience = cust.getPatience();
        text.text = "Patience: " + patience.ToString();
        transform.position = Camera.main.WorldToScreenPoint(cust.transform.position) + offset;
    }
}
