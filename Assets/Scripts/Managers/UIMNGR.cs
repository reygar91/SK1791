using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMNGR : MonoBehaviour {

    //public static UIMNGR Instance;

    public PanelOfPersonnel personnelPanel;
    public PanelOfCustomer customerPanel;

    private void Awake()
    {
        //Instance = this;
        SelectorForCustomer.customerPanel = customerPanel;
        SelectorForPersonnel.personnelPanel = personnelPanel;
    }
}
