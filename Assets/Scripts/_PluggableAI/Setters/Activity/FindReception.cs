using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindReception : ActivitySetter {
    public override Activity Set(myCharacterController Character)
    {
        //System.Predicate<Activity> predicate = FindType;
        //Character.Focus.Activity = room.Activities.Find(predicate) as Reception;
        return Reception.Instance;
    }

    private bool FindType(Activity type)
    {
        return type is Reception;
    }
}
