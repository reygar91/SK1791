using UnityEngine;

public class Pause : CommandPattern
{
    public override void Execute()
    {
        TimeFlow.isPause = !TimeFlow.isPause;
        foreach (Character item in GameController.CharList)
        {
            if (item.gameObject.activeSelf)
            {
                item.AnimatorComponent.enabled = !item.AnimatorComponent.enabled;
            }
        }
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
    public override void Execute()
    {
        int index = Random.Range(0, 3);
        //DialogPhrase.text = textAssets[index].text;
    }
}