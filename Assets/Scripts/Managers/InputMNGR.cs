using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputMNGR : MonoBehaviour {

    public static InputMNGR Instance;

    public int Reputation = 1;//at 0 there will be only 1 cust spawned initially, at 7 - cust spawns every 5 sec

    private InputCommands CancelButton, JumpButton;
    public GameObject[] Panels;
    public CanvasGroup GameUI, DialogUI;
 


    protected InputMNGR() { }

    void Awake () {
        Instance = this; 
        CancelButton = new DisablePanel(Panels);
        JumpButton = new Pause();
    }

    void Update () {
        if (Input.GetButtonDown("Cancel"))
        {
            CancelButton.Execute();
        }
        if (Input.GetButtonDown("Jump"))
        {
            JumpButton.Execute();
        }
    }

    public void EnableDialogUI()
    {
        if (!DialogUI.gameObject.activeSelf)
        {
            DialogUI.gameObject.SetActive(true);
            JumpButton.Execute();
            JumpButton = new DialogNext();
            GameUI.alpha = 0;//GameUI.interactable = false;//as long as DialogUI is over GameUI this is not needed
            DialougeMNGR.Instance.ResetPhraseCounter();
        }        
    }

    public void DisableDialogUI()
    {
        DialogUI.gameObject.SetActive(false);
        JumpButton = new Pause();
        JumpButton.Execute();
        GameUI.alpha = 1;//GameUI.interactable = true;
    }

    public void Pause()
    {
        JumpButton.Execute();
    }


}
