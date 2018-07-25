using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialougeMNGR : Singleton<DialougeMNGR> {

    public Text DialogPhrase;

    public TextAsset[] textAssets;
    public CanvasGroup GameUI, DialogUI;

    //public GameController gameController;

    string[] Phrases;
    int PhraseCounter;

    protected DialougeMNGR() { }

    //private void OnEnable()
    //{

    //}
    //private void OnDisable()
    //{

    //}

    public void NextPhrase() //this is assigned to JumpButton with this line: gameController.JumpButton = new DialogNext(this);
    {
        if (PhraseCounter < Phrases.Length)
        {
            DialogPhrase.text = Phrases[PhraseCounter];
            PhraseCounter++;
        }
        else DisableDialogUI();        
    }

    public void DialogEvent(int dialogID)
    {
        if (!DialogUI.gameObject.activeSelf)
        {
            EnableDialogUI();
        }
        string[] stringSeparators = new string[] { "[break]", "[choice]" };
        Phrases = textAssets[dialogID].text.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);        
        DialogPhrase.text = Phrases[0];
    }

    private void EnableDialogUI()
    {
        DialogUI.gameObject.SetActive(true);
        GameMNGR.Instance.JumpButton.Execute();
        GameMNGR.Instance.JumpButton = new DialogNext();
        GameUI.alpha = 0;//GameUI.interactable = false;//as long as DialogUI is over GameUI this is not needed
        PhraseCounter = 1;
    }

    private void DisableDialogUI()
    {
        DialogUI.gameObject.SetActive(false);
        GameMNGR.Instance.JumpButton = new Pause();
        GameMNGR.Instance.JumpButton.Execute();
        GameUI.alpha = 1;//GameUI.interactable = true;
    }

}
