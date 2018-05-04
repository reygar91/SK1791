using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersPanelScript : MonoBehaviour {

    public GameController Controller;
    private GameObject previousSelectedObject, selectedObject;
    private Text TextComponent;
    private Personel pers;
    private Dropdown DropdownComponent;

    // Use this for initialization

    private void Awake()
    {
        TextComponent = GetComponentInChildren<Text>();
        DropdownComponent = GetComponentInChildren<Dropdown>();
    }
    private void OnEnable()
    {
        selectedObject = Controller.getSelectedObject();
        Initialize();
    }

    private void Initialize()
    {
        pers = selectedObject.GetComponent<Personel>();
        TextComponent.text = selectedObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousSelectedObject = selectedObject;
            selectedObject = Controller.getSelectedObject();
            if (selectedObject.tag == "Personel")
            {
                if (selectedObject != previousSelectedObject)
                {
                    Initialize();
                }                
            }            
        }        
    }

    public void JobSelector()
    {
        //Debug.Log(list.value);
        switch (DropdownComponent.value)
        {
            case 0:
                pers.Job = new Idle();
                break;
            case 1:
                pers.Job = new Waiter();
                break;
        }
    }
}
