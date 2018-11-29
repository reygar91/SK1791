using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/SelectSeatAtBar")]
public class SelectSeatAtBar : Action
{
    public override bool Act(myCharacterController Character)
    {
        bool result = false;
        if (BarMNGR.Instance.AvailableSeats.Count != 0)
        {
            Character.behaviour.ResetFocus.Act(Character);

            int RandomSeat = Random.Range(0, BarMNGR.Instance.AvailableSeats.Count);            
            Character.Focus.TargetObj = BarMNGR.Instance.AvailableSeats[RandomSeat];
            BarMNGR.Instance.AvailableSeats.Remove(Character.Focus.TargetObj);
            //controller.character.Focus.Index = room.SeatIndex(Focus.Object);

            Character.NextBehaviour = MyBehavioursCollection.Instance.Customer.AtBar;

            Room room = Character.Focus.TargetObj.GetComponentInParent<Room>();
            System.Predicate<Activity> predicate = FindType;
            Character.Focus.Activity = room.Activities.Find(predicate) as Bar;
            //Activity AlcoBar = Character.Focus.Room.Activities.Find(predicate) as Bar;

            result = true;
        }
        return result;
    }
    
    private bool FindType(Activity type)
    {
        return type is Bar;
    }
}