using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustPanelScript : MonoBehaviour {

    public GameController Controller;
    private GameObject previousSelectedObject, selectedObject;
    Text text;
    Customer cust;
    int patience;
    public Vector3 offset; 

    // Use this for initialization

    private void Awake()
    {
        text = gameObject.GetComponentInChildren<Text>();
    }
    private void OnEnable()
    {
        selectedObject = Controller.getSelectedObject();
        cust = selectedObject.GetComponent<Customer>();
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            previousSelectedObject = selectedObject;
            selectedObject = Controller.getSelectedObject();
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
