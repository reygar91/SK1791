using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldMNGR : Singleton<GoldMNGR> {

    private Text TextComponent;
    public int Gold
    {
        get;    private set;        
    } 
    
    protected GoldMNGR() { }

private void Awake()
    {
        TextComponent = GetComponent<Text>();
        //Gold = 0;
    }
	
	// Update is called once per frame
	void Update () {
        TextComponent.text = "Gold: " + Gold;
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
