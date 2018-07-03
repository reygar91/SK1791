using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myEvents : MonoBehaviour {

    public delegate void MethodContainer(int someID);
    public static event MethodContainer GoldAmmount;
    //public DialougeManager dialougeManager;
    public static List<myEventConditions> conditions = new List<myEventConditions>();

	// Use this for initialization
	void Start () {
        //GoldAmmount += dialougeManager.TestEvent;
        //conditions.Add(new GoldCheck(100));
        StartCoroutine("ConditionsChecks");
    }
	
	// Update is called once per frame
	void Update () {
	}

    private IEnumerator ConditionsChecks()
    {
        while (true)
        {
            if (conditions.Count > 0)
            {
                int i = 0;
                do
                {
                    if (conditions[i].Condition())
                    {
                        RunEvent(conditions[i]);
                    }
                    i++;
                } while (conditions.Count > i);
            }
            yield return new WaitForSeconds(5);
        }
    }

    private void RunEvent(myEventConditions condition)
    {
        switch (condition.GetType().ToString())
        {
            case "GoldCheck":
                GoldAmmount(0);
                break;
            default:
                Debug.Log("Unidentified Event Type");
                break;
        }
        conditions.Remove(condition);
    }

}


public abstract class myEventConditions
{
    public abstract bool Condition();
    public abstract bool EventName();
}

public class GoldCheck : myEventConditions
{
    private int Gold;
    //private myEvents my = new myEvents();

    public GoldCheck(int value)
    {
        Gold = value;
    }

    public override bool Condition()
    {
        return (GoldManager.Gold > Gold);
    }

    public override bool EventName()
    {
        throw new System.NotImplementedException();
    }
}
