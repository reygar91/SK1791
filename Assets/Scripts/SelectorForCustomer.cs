using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorForCustomer : MonoBehaviour {

    private BehaviourController BC;
    public static BehaviourController SelectedCustomer; //or selected MC
    public static PanelOfCustomer customerPanel;
    private static BehaviourController previousBC;

    private void Awake()
    {
        BC = GetComponent<BehaviourController>();
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
