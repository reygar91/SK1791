using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/ActivityOptions/AlcoBar")]
public class AlcoBarOption : PossibleActivity {

    public override bool CheckAvailabilityOverAll(myCharacterController Character)
    {
        bool result = false;
        if (BarMNGR.Instance.AvailableSeats.Count != 0)
        {
            Character.behaviour.ResetFocus.Act(Character);

            int RandomSeat = Random.Range(0, BarMNGR.Instance.AvailableSeats.Count);
            Character.Focus.TargetObj = BarMNGR.Instance.AvailableSeats[RandomSeat];
            BarMNGR.Instance.AvailableSeats.Remove(Character.Focus.TargetObj);
            
            Character.NextBehaviour = MyBehavioursCollection.Instance.Customer.AtBar;

            Room room = Character.Focus.TargetObj.GetComponentInParent<Room>();
            System.Predicate<Activity> predicate = delegate (Activity item) { return item is Bar; };
            Character.Focus.Activity = room.Activities.Find(predicate) as Bar;

            Character.Focus.Target.ObjectIndex = Character.Focus.Activity.FindIndexOfInteractionObj(Character.Focus.TargetObj);
            result = true;
        }
        return result;
    }

    //not in use currently
    public override ActivityContainer CheckAvailabilityWithinRoom(myCharacterController Character)
    {
        ActivityContainer result = new ActivityContainer();
        System.Predicate<Activity> predicate = delegate (Activity item) { return item is Bar; };
        result.activity = Character.Focus.CurrentRoom.Activities.Find(predicate) as Bar;
        if (result.activity)
            result.Obj = result.activity.CheckActivityWithinRoom();
        return result;//what if return is struct combined of activity and Transform?
    }

}


