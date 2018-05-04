using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour {

    private Text TextComponent;
    private static int Gold = 0;

    private void Awake()
    {
        TextComponent = GetComponent<Text>();
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
