using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class myEventMNGR : Singleton<myEventMNGR> {

    //public delegate void MethodContainer(int dialogID);
    //public static event MethodContainer GoldAmmount, GameTutorial;

    public List<myEvents> ActiveEvents = new List<myEvents>();
    public List<myEvents> InActiveEvents = new List<myEvents>();
    public List<myEvents> CompletedEvents = new List<myEvents>();

    protected myEventMNGR() { }

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

    public List<string> GetActiveEventsSignatures()
    {
        List<string> eventSignature = new List<string>();
        foreach (myEvents item in ActiveEvents)
        {
            string signature = item.GetType().ToString();
            if (item.extraData.exists)
            {
                int data = item.extraData.value;
                signature = signature + "_" + data;
            }
            eventSignature.Add(signature);
        }
        return eventSignature;
    }

    public void LoadEventsFromSignatures(List<string> eventSignature)
    {
        ActiveEvents = new List<myEvents>();
        foreach (string item in eventSignature)
        {
            string signature = item;
            if (signature.Contains("_"))
            {
                string[] separator = new string[] { "_" };
                string[] signComponent = signature.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                signature = signComponent[0];
                int intData = Int32.Parse(signComponent[1]);
                Type decypheredEvent = Type.GetType(signature);
                Type[] types = new Type[1];
                types[0] = typeof(int);
                ConstructorInfo constructor = decypheredEvent.GetConstructor(types);
                System.Object[] parameters = new System.Object[1];
                parameters[0] = intData;
                var temp = constructor.Invoke(parameters);
                ActiveEvents.Add(temp as myEvents);
            } else
            {
                Type decypheredEvent = Type.GetType(signature);
                Type[] types = new Type[0];//new Type[0] for constructors without arguments
                ConstructorInfo constructor = decypheredEvent.GetConstructor(types);
                System.Object[] parameters = new System.Object[0];//new System.Object[0] for constructors without arguments
                var temp = constructor.Invoke(parameters); 
                ActiveEvents.Add(temp as myEvents);
            }
        }
        //Debug.Log("EventsLoaded");
    }

    public void TestEventsSaveLoad()
    {
        List<string> eventSignature = new List<string>();
        eventSignature = GetActiveEventsSignatures();
        foreach (string item in eventSignature)
        {
            Debug.Log(item + " saved");
        }
        LoadEventsFromSignatures(eventSignature);
        foreach (myEvents item in ActiveEvents)
        {
            if (item.extraData.exists)
            {
                int data = item.extraData.value;
                Debug.Log(item.GetType().ToString() + "_" + data + " loaded");
            } else
                Debug.Log(item.GetType().ToString() + " loaded");
        }
    }


}
