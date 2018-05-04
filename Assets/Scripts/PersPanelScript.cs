using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersPanelScript : MonoBehaviour {

    //public GameController Controller;
    //public MyRaycaster Raycaster2D;
    //private GameObject previousSelectedObject;
    //public GameObject selectedObject;
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
        //selectedObject = Raycaster2D.getSelectedObject();
        Initialize();
    }

    public void Initialize()
    {
        pers = isSelectable.selectedObject.GetComponent<Personel>();
        TextComponent.text = isSelectable.selectedObject.name;
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
