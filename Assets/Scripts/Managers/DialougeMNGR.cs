using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialougeMNGR : Singleton<DialougeMNGR> {

    private Text DialogPhrase;

    private TextAsset[] textAssets;
    private CanvasGroup GameUI, DialogUI;

    private string[] Phrases;
    private int PhraseCounter;

    protected DialougeMNGR() { }

    private void Awake()
    {
        GameUI = MainSceneReferences.Instance.GameUI;
        DialogUI = MainSceneReferences.Instance.DialogUI;
        Button button = DialogUI.gameObject.GetComponent<Button>();
        button.onClick.AddListener(NextPhrase);

        DialogPhrase = MainSceneReferences.Instance.DialogPhrase;
        textAssets = MainSceneReferences.Instance.textAssets;
    }

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
        InputMNGR.Instance.JumpButton.Execute();
        InputMNGR.Instance.JumpButton = new DialogNext();
        GameUI.alpha = 0;//GameUI.interactable = false;//as long as DialogUI is over GameUI this is not needed
        PhraseCounter = 1;
    }

    private void DisableDialogUI()
    {
        DialogUI.gameObject.SetActive(false);
        InputMNGR.Instance.JumpButton = new Pause();
        InputMNGR.Instance.JumpButton.Execute();
        GameUI.alpha = 1;//GameUI.interactable = true;
    }

}
