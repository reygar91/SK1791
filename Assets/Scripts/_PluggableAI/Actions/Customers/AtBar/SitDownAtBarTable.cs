using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "PluggableAI/Actions/SitDownAtBarTable")]
public class SitDownAtBarTable : Action {
    public override bool Act(myCharacterController Character)
    {
        //Predicate<RoomType> predicate = FindType;
        //RoomType bar = controller.Focus.CurrentRoom.types.Find(predicate) as Bar;
        //Bar room = controller.Focus.CurrentRoom as Bar;
        Character.Focus.Activity.Customers.Add(Character);
        int startIndex = Character.Focus.TargetObj.name.IndexOf("_");
        string orientation = Character.Focus.TargetObj.name.Substring(startIndex + 1);
        switch (orientation)
        {
            case "Right":
                Character.transform.rotation = new Quaternion(0, -1, 0, 0);//facing from right to left
                break;
            case "Left":
                Character.transform.rotation = new Quaternion(0, 0, 0, 1);//facing from left to right
                break;
        }
        return true;
    }

    //private bool FindType(RoomType type)
    //{
    //    return type is Bar;
    //}

}
