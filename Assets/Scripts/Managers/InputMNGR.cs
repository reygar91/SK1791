using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputMNGR : MonoBehaviour {

    public static InputMNGR Instance;

    public int Reputation = 1;//at 0 there will be only 1 cust spawned initially, at 7 - cust spawns every 5 sec

    private InputCommands CancelButton, JumpButton;
    //public GameObject EscMenu;
    public GameObject[] Panels;
    public CanvasGroup GameUI, DialogUI;

    private Text CustPanelText;
    public Button Serve;
    private MonoCharacter MC;
    public Vector3 CustPanelOffset;    


    protected InputMNGR() { }

    void Awake () {
        Instance = this; //Debug.Log("InputMNGR" + Instance);
        CustPanelText = Panels[3].GetComponentInChildren<Text>();
        Serve = Panels[3].GetComponentInChildren<Button>();
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

    public void CustomerClicked(MonoCharacter monoCharacter)
    {
        string charType = monoCharacter.character.GetType().ToString();
        switch (charType)
        {
            case "Customer":
                if (!Panels[3].activeSelf)
                {
                    Panels[3].SetActive(true);
                    MC = monoCharacter; //needed for couritine, to avoid null reference
                    StartCoroutine("DetailedInfo");
                    StartCoroutine("CustPanelPosition");
                }
                else if (MC == monoCharacter)
                    Panels[3].SetActive(false);
                Customer customer = monoCharacter.character as Customer;
                if (customer.HasBeenServed)
                    Serve.gameObject.SetActive(false);
                else
                {
                    Serve.gameObject.SetActive(true);
                    Serve.onClick.RemoveAllListeners();
                    Serve.onClick.AddListener(customer.GetServed);
                }
                break;
        }        
        MC = monoCharacter;
    }

    private IEnumerator DetailedInfo()
    {
        while (Panels[3].activeSelf)
        {
            int patience = Mathf.RoundToInt(MC.character.CountDown);
            CustPanelText.text = "Patience: " + patience.ToString();
            yield return new WaitForSeconds(0.25f);
        }
    }

    private IEnumerator CustPanelPosition()
    {
        while (Panels[3].activeSelf)
        {
            Panels[3].transform.position = Camera.main.WorldToScreenPoint(MC.transform.position + CustPanelOffset) ;
            yield return null;
        }
    }


}
