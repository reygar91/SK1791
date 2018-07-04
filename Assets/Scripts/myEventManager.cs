using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myEventManager : Singleton<myEventManager> {

    //public delegate void MethodContainer(int dialogID);
    //public static event MethodContainer GoldAmmount, GameTutorial;

    public List<myEvents> ActiveEvents = new List<myEvents>();
    public List<myEvents> InActiveEvents = new List<myEvents>();
    public List<myEvents> CompletedEvents = new List<myEvents>();

    protected myEventManager() { }

    // Use this for initialization
    void Start () {
        ActiveEvents.Add(new StartGameTutorial());
        //GoldAmmount += DialougeManager.Instance.DialogEvent;//test event fires wnen some ammount of gold reached
        StartCoroutine("ConditionsChecks");
    }
	
	// Update is called once per frame
	void Update () {
	}

    private IEnumerator ConditionsChecks()
    {
        while (true)
        {
            if (ActiveEvents.Count > 0)
            {
                int i = 0;
                do
                {
                    if (ActiveEvents[i].Condition())
                    {
                        ActiveEvents[i].Result();
                        CompletedEvents.Add(ActiveEvents[i]);
                        ActiveEvents.Remove(ActiveEvents[i]);                        
                    }
                    i++;
                } while (ActiveEvents.Count > i);
            }
            yield return new WaitForSeconds(5);
        }
    }

    //private void RunEvent(myEvents myEvent)
    //{
    //    //switch (condition.GetType().ToString())
    //    //{
    //    //    case "GoldCheck":
    //    //        GoldAmmount(0);
    //    //        break;
    //    //    case "StartGameTutorial":
    //    //        GameTutorial(1);
    //    //        break;
    //    //    default:
    //    //        Debug.Log("Unidentified Event Type");
    //    //        break;
    //    //}
    //    ActiveEvents.Remove(myEvent);
    //    CompletedEvents.Add(myEvent);
    //}

}
