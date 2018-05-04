﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustPanelScript : MonoBehaviour {

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
        CustInitialize();
        StartCoroutine("DetailedInfo");
    }

    // Update is called once per frame
    void Update () {     
        transform.position = Camera.main.WorldToScreenPoint(cust.transform.position) + offset;
    }

    private void OnDisable()
    {
        StopCoroutine("DetailedInfo");
    }

    private IEnumerator DetailedInfo()
    {
        while (true)
        {
            patience = cust.getPatience();
            text.text = "Patience: " + patience.ToString();
            yield return new WaitForSeconds(0.25f);
        }
    }

    public void CustInitialize()
    {
        cust = isSelectable.selectedObject.GetComponent<Customer>();
    }
}
