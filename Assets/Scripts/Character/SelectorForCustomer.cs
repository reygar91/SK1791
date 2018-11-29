using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorForCustomer : MonoBehaviour {

    private myCharacterController BC;
    public static myCharacterController SelectedCustomer; //or selected MC
    public static PanelOfCustomer customerPanel;
    private static myCharacterController previousBC;

    private void Awake()
    {
        BC = GetComponent<myCharacterController>();
    }

    private void OnMouseUp()
    {
        SelectedCustomer = BC;
        customerPanel.UpdateSelectedCustomer();
        GameObject Panel = customerPanel.gameObject;
        if (!Panel.activeSelf)
        {
            Panel.SetActive(true);
        }
        else if (BC == previousBC)
            Panel.SetActive(false);
        //customerPanel.ToggleServeBTN();
        previousBC = BC;
    }

}
