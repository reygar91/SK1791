using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOfCustomer : MonoBehaviour {

    //public static CustomerPanel Instance;
    public Vector3 Offset;
    private myCharacterController SelectedCustomer;
    public Button ServeBTN;
    private Text CustPanelText;

    private void Awake()
    {
        //Instance = this; //Debug.Log("awake");
        CustPanelText = GetComponentInChildren<Text>();
        ServeBTN = GetComponentInChildren<Button>();
    }

    private void OnEnable()
    {
        StartCoroutine("DetailedInfo");
        StartCoroutine("CustPanelPosition");
    }

    //public void ToggleServeBTN()
    //{
    //    UpdateMC();
    //    Customer customer = BC.character as Customer;
    //    if (customer.HasBeenServed)
    //        ServeBTN.gameObject.SetActive(false);
    //    else
    //    {
    //        ServeBTN.gameObject.SetActive(true);
    //        ServeBTN.onClick.RemoveAllListeners();
    //        ServeBTN.onClick.AddListener(customer.GetServed);
    //    }
    //}

    public void UpdateSelectedCustomer()
    {
        SelectedCustomer = SelectorForCustomer.SelectedCustomer;
    }


    private IEnumerator DetailedInfo()
    {
        while (gameObject.activeSelf)
        {
            int patience = Mathf.RoundToInt(SelectedCustomer.Stats.CountDown);
            CustPanelText.text = "Patience: " + patience.ToString();
            yield return new WaitForSeconds(0.25f);
        }
    }

    private IEnumerator CustPanelPosition()
    {
        while (gameObject.activeSelf)
        {
            gameObject.transform.position = Camera.main.WorldToScreenPoint(SelectedCustomer.transform.position + Offset);
            yield return null;
        }
    }
}
