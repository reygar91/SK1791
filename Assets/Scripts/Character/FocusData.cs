using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusData : MonoBehaviour
{
    public FocusTarget Target = new FocusTarget();
    public myCharacterController CC;
    public Transform TargetObj;
    public Activity Activity;//TargetRoom
    public Room CurrentRoom;

    public FocusTarget Save()
    {
        if (TargetObj)
        {
            Target.Signature = System.String.Concat("Object"); //Debug.Log("Insert");
        }
        if (CC)
        {
            Target.Signature = System.String.Concat("Character");
            Target.CharacterIndex = CharacterMNGR.Instance.ActiveCharacters.IndexOf(CC);
            Debug.Log(CC.name + " with index " + Target.CharacterIndex + " saved");
        }
        if (TargetObj && CC)
        {
            Debug.Log("Both TargetObj & CC are Focused.");
        }

        Target.CurrentRoomIndex = BuildMNGR.Instance.roomsList.IndexOf(CurrentRoom);
        Target.ActivityRoomIndex = BuildMNGR.Instance.roomsList.IndexOf(Activity.Room);
        Target.ActivitySignature = Activity.GetType().ToString();
        return Target;
    }

    public void Load(FocusTarget data)
    {
        Target = data;
        Activity = Target.LoadActivity(data.ActivitySignature);

        if (Target.CurrentRoomIndex == -1)
            CurrentRoom = Reception.Instance.Room;
        else
            CurrentRoom = BuildMNGR.Instance.roomsList[Target.CurrentRoomIndex];

        if (Target.Signature.Contains("Object"))
        {
            TargetObj = Activity.FindInteractionObjByIndex(Target.ObjectIndex);
        } 
        if (Target.Signature.Contains("Character"))
        {
            CC = CharacterMNGR.Instance.ActiveCharacters[Target.CharacterIndex];
        }
        Activity.Load(Target.ObjectIndex, CC);
    }

    private void OnDisable()
    {
        CC = null;
        TargetObj = null;
        Target = new FocusTarget();
    }
}

[System.Serializable]
public class FocusTarget
{
    public int ObjectIndex, CharacterIndex, ActivityRoomIndex, CurrentRoomIndex;
    public string Signature, ActivitySignature;

    public Activity LoadActivity(string signature)
    {
        Activity activity;
        System.Predicate<Activity> predicate;
        switch (signature)
        {
            case "Reception":
                predicate = FindReception;
                break;
            case "Queue":
                predicate = FindQueue;                
                break;
            case "Bar":
                predicate = FindBar;
                break;
            default:
                predicate = null;
                break;
        }
        Room room;

        //if (ActivityRoomIndex == -1)
        //    room = Reception.Instance.Room;
        //else
            room = BuildMNGR.Instance.roomsList[ActivityRoomIndex];

        activity = room.Activities.Find(predicate);
        return activity;
    }

    private bool FindReception(Activity type)
    {
        return type is Reception;
    }
    private bool FindQueue(Activity type)
    {
        return type is Queue;
    }
    private bool FindBar(Activity type)
    {
        return type is Bar;
    }
}
