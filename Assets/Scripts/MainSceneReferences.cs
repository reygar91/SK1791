using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneReferences : Singleton<MainSceneReferences> {

    public MyWindows Windows = new MyWindows();
    public MyContainers Containers = new MyContainers();
    public BuildPreview buildPreview;
    public MonoCharacter HumanoidPrefab;
    public CanvasGroup GameUI, DialogUI;
    public Text DialogPhrase;
    public TextAsset[] textAssets;

}

[System.Serializable]
public class MyWindows
{    
    public GameObject BuildPanel, MarketPanel, CustPanel, PersonelPanel, TimePanel;
    public EscMenu EscMenu;
}

[System.Serializable]
public class MyContainers
{
    public GameObject Characters, Rooms;
}


