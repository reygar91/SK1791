using UnityEngine;

public class InputMNGR : Singleton<InputMNGR> {

    public int Reputation = 1;//at 0 there will be only 1 cust spawned initially, at 7 - cust spawns every 5 sec

    public CommandPattern CancelButton, JumpButton;


    protected InputMNGR() { }

    void Awake () {
        CancelButton = new DisablePanel(MainSceneReferences.Instance.Windows.EscMenu.gameObject);
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
