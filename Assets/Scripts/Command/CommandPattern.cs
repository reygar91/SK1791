using UnityEngine;

public abstract class CommandPattern
{
    public abstract void Execute();
}

public class DoNothing : CommandPattern
{
    public override void Execute()
    {
    }
}

public class Pause : CommandPattern
{
    public override void Execute()
    {
        TimeMNGR.Instance.pauseToggle.isOn = !TimeMNGR.Instance.pauseToggle.isOn;
    }
}

public class DialogNext : CommandPattern
{
    public override void Execute()
    {
        DialougeMNGR.Instance.NextPhrase();
    }
}

public class DisablePanel : CommandPattern
{
    private GameObject panel;
    public DisablePanel(GameObject MenuPanel)
    {
        panel = MenuPanel;
    }

    public override void Execute()
    {
        if (DisableWithEsc.Panels.Count == 0)
        {
            panel.SetActive(true);
        }
        else
        {
            do
            {
                DisableWithEsc.Panels[0].gameObject.SetActive(false);
            } while (DisableWithEsc.Panels.Count != 0);
        }
    }
}