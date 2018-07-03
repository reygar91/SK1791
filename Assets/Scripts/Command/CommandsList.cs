using UnityEngine;
using UnityEngine.UI;

public class Pause : CommandPattern
{
    private Toggle pauseToggle;

    public Pause(Toggle toggle)
    {
        pauseToggle = toggle;
    }

    public override void Execute()
    {
        pauseToggle.isOn = !pauseToggle.isOn;
    }
}

public class DoNothing : CommandPattern
{
    public override void Execute()
    {
    }
}

public class DialogNext : CommandPattern
{
    private DialougeManager dialougeManager;

    public DialogNext(DialougeManager Manager)
    {
        dialougeManager = Manager;
    }

    public override void Execute()
    {
        dialougeManager.NextPhrase();
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
        } else
        {
            do
            {
                DisableWithEsc.Panels[0].gameObject.SetActive(false);
            } while (DisableWithEsc.Panels.Count != 0);
        }        
    }
}