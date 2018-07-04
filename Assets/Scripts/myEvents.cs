public abstract class myEvents
{
    public abstract bool Condition();
    public abstract void Result();
}

public class GoldCheck : myEvents
{
    private int Gold;

    public GoldCheck(int value)
    {
        Gold = value;
    }

    public override bool Condition()
    {
        return (GoldManager.Instance.Gold > Gold);
    }

    public override void Result()
    {
        DialougeManager.Instance.DialogEvent(1);
    }
}

public class StartGameTutorial : myEvents
{
    public override bool Condition()
    {
        return myEventManager.Instance.ActiveEvents.Contains(this);
    }

    public override void Result()
    {
        DialougeManager.Instance.DialogEvent(0);
        myEventManager.Instance.ActiveEvents.Add(new GoldCheck(100));//this is adds new event: 100 gold needed
    }
}