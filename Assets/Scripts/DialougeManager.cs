using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialougeManager : MonoBehaviour {

    public Text DialogPhrase;

    public TextAsset[] textAssets;
    public GameObject GameUI;
    public Toggle PauseToggle;

    public GameController gameController;

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
        //PauseToggle.isOn = true;
        gameController.JumpButton.Execute();
        gameController.JumpButton = new DialogNext(this);
        GameUI.SetActive(false);
        //Time.timeScale = 0;
    }
    private void OnDisable()
    {
        gameController.JumpButton = new Pause(PauseToggle);
        gameController.JumpButton.Execute();
        GameUI.SetActive(true);
        //PauseToggle.isOn = false;
        //Time.timeScale = 1;
    }

    public void NextPhrase()
    {
        int index = Random.Range(0, 3);
        DialogPhrase.text = textAssets[index].text;
    }

}
