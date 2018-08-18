using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour {

    public static MainMenuButtons Instance;

    public Button NewGame, Save, Load, Options, ToMainMenu, Quit;

    private void Awake()
    {
        Instance = this;
    }

}



