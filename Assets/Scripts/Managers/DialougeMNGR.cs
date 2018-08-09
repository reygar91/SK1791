using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialougeMNGR : MonoBehaviour {

    public static DialougeMNGR Instance;

    public Text DialogPhrase;
    public TextAsset[] textAssets;    

    private string[] Phrases;
    private int PhraseCounter;

    protected DialougeMNGR() { }

    private void Awake()
    {
        Instance = this;        
    }

    private void Start()
    {
        Button button = InputMNGR.Instance.DialogUI.gameObject.GetComponent<Button>();
        button.onClick.AddListener(NextPhrase);
    }

    public void NextPhrase() //used to switch to next phrase with jumpButton or mouseClick
    {
        if (PhraseCounter < Phrases.Length)
        {
            DialogPhrase.text = Phrases[PhraseCounter];
            PhraseCounter++;
        }
        else InputMNGR.Instance.DisableDialogUI();        
    }

    public void DialogEvent(int dialogID) // used to start DialogEvents in myEvents script
    {
        InputMNGR.Instance.EnableDialogUI();        
        string[] stringSeparators = new string[] { "[break]", "[choice]" };
        Phrases = textAssets[dialogID].text.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);        
        DialogPhrase.text = Phrases[0];
    }

    public void ResetPhraseCounter()
    {
        PhraseCounter = 1;
    }

}
