using UnityEngine;

public class InputMNGR : MonoBehaviour {

    public static InputMNGR Instance;

    public int Reputation = 1;//at 0 there will be only 1 cust spawned initially, at 7 - cust spawns every 5 sec

    public CommandPattern CancelButton, JumpButton;
    public GameObject EscMenu;


    protected InputMNGR() { }

    void Awake () {
        Instance = this; //Debug.Log("InputMNGR" + Instance);
        CancelButton = new DisablePanel(EscMenu);
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


   

}
