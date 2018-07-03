using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialougeManager : MonoBehaviour {

    public Text DialogPhrase;

    public TextAsset[] textAssets;
    public CanvasGroup GameUI;
    public Toggle PauseToggle;

    public GameController gameController;

    string[] Phrases;
    int PhraseCounter;

    private void Awake()
    {
        myEvents.GoldAmmount += DialogEvent;//test event fires wnen some ammount of gold reached
        myEvents.conditions.Add(new GoldCheck(100));//this is adding event condition with 100 gold needed
        gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire2"))
        {
            gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        gameController.JumpButton.Execute();
        gameController.JumpButton = new DialogNext(this);
        GameUI.alpha = 0;//GameUI.interactable = false;
        PhraseCounter = 1;
    }
    private void OnDisable()
    {
        gameController.JumpButton = new Pause(PauseToggle);
        gameController.JumpButton.Execute();
        GameUI.alpha = 1;//GameUI.interactable = true;
    }

    public void NextPhrase()
    {
        //int index = UnityEngine.Random.Range(0, 3);
        if (PhraseCounter < Phrases.Length)
        {
            DialogPhrase.text = Phrases[PhraseCounter];
            PhraseCounter++;
        }
        else gameObject.SetActive(false);
        
    }

    public void DialogEvent(int dialogID)
    {
        gameObject.SetActive(true);
        string[] stringSeparators = new string[] { "[break]", "[choice]" };
        Phrases = textAssets[dialogID].text.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);        
        DialogPhrase.text = Phrases[0];
    }

}
