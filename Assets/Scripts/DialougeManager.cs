using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialougeManager : MonoBehaviour {

    public Text DialogPhrase;

    public TextAsset[] textAssets;
    public GameObject GameUI;
    public Toggle PauseToggle;

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
        GameUI.SetActive(false);
        Time.timeScale = 0;
    }
    private void OnDisable()
    {
        GameUI.SetActive(true);
        //PauseToggle.isOn = false;
        Time.timeScale = 1;
    }

    public void NextPhrase()
    {
        int index = Random.Range(0, 3);
        DialogPhrase.text = textAssets[index].text;
    }

    public DialougeManager GetDialogMNGR()
    {
        return this;
    }
}
