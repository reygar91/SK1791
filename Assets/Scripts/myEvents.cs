﻿public abstract class myEvents
{
    public abstract bool Condition();
    public abstract void Result();
    public IntegerData extraData = new IntegerData();
}

public class IntegerData
{
    public bool exists = false;
    public int value = -1;

    public void SetDataValue(int incomingValue)
    {
        exists = true;
        value = incomingValue;
    }
}

public class GoldCheck : myEvents
{
    private int Gold;

    public GoldCheck(int value)
    {
        Gold = value;
        extraData.SetDataValue(Gold);
    }

    public override bool Condition()
    {
        return (GoldMNGR.Instance.Gold > Gold);
    }

    public override void Result()
    {
        DialougeMNGR.Instance.DialogEvent(1);
    }
}

public class StartGameTutorial : myEvents
{
    public override bool Condition()
    {
        return myEventMNGR.Instance.ActiveEvents.Contains(this);
    }

    public override void Result()
    {
        DialougeMNGR.Instance.DialogEvent(0);
        myEventMNGR.Instance.ActiveEvents.Add(new GoldCheck(100));//this is adds new event: 100 gold needed
    }
}