using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldMNGR : MonoBehaviour {

    public static GoldMNGR Instance;

    public InputField inputField;
    public int Gold
    {
        get;    private set;        
    } 
    
    protected GoldMNGR() { }

private void Awake()
    {
        Instance = this;
        inputField = GetComponentInChildren<InputField>();
        //Gold = 0;
    }
	
	// Update is called once per frame
	void Update () {
        inputField.text = "Gold: " + Gold;
    }

    public void AddGold(int AddedValue)
    {
        Gold = Gold + AddedValue;
    }

    public void SubtractGold(int SubtractedValue)
    {
        Gold = Gold - SubtractedValue;
    }

}
