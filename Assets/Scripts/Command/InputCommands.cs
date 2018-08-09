using UnityEngine;

public abstract class InputCommands
{
    public abstract void Execute();
}

public class DoNothing : InputCommands
{
    public override void Execute()
    {
    }
}

public class Pause : InputCommands
{
    public override void Execute()
    {
        TimeMNGR.Instance.pauseToggle.isOn = !TimeMNGR.Instance.pauseToggle.isOn;
    }
}

public class DialogNext : InputCommands
{
    public override void Execute()
    {
        DialougeMNGR.Instance.NextPhrase();
    }
}

public class DisablePanel : InputCommands
{
    private GameObject[] Panel;
    public DisablePanel(GameObject[] panels)
    {
        Panel = panels;
    }

    public override void Execute()
    {
        bool noActivePanels = true;
        foreach (GameObject item in Panel)
        {
            if (item.activeSelf)
            {
                noActivePanels = false;
                item.SetActive(false);
            }
        }
        if (noActivePanels)
            Panel[0].SetActive(true);// 0 index reserved for EscMenu
    }
}