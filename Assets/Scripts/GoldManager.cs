using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour {

    private Text TextComponent;
    public static int Gold
    {
        get;    private set;        
    }    

private void Awake()
    {
        TextComponent = GetComponent<Text>();
        //Gold = 0;
    }
	
	// Update is called once per frame
	void Update () {
        TextComponent.text = "Gold: " + Gold;
    }

    public static void AddGold(int AddedValue)
    {
        Gold = Gold + AddedValue;
    }

    public static void SubtractGold(int SubtractedValue)
    {
        Gold = Gold - SubtractedValue;
    }

}
