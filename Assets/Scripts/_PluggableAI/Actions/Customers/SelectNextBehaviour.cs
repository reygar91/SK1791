using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/SelectNextBehaviour")]
public class SelectNextBehaviour : Action {
    public override bool Act(myCharacterController Character)
    {
        bool result = false;

        if (Character.possibleBehaviours.Length != 0)
        {
            int index = Random.Range(0, Character.possibleBehaviours.Length);
            result = Character.possibleBehaviours[index].CheckAvailabilityOverAll(Character);
        }
        else
        {
            Character.Stats.CountDownReached_0(Character);
            result = true;
        }
        
        if (result)
        {
            //controller.Focus.RoomToBeLeft = controller.Focus.RoomType.Room;
            //controller.Focus.CurrentRoom = controller.Focus.Object.GetComponentInParent<Room>();
            Character.behaviour = MyBehavioursCollection.Instance.ChangeRoom;
            Character.ActionID = -1;
            //because after exiting Act ActionID will be increased by 1, nextBehaviour will start with step 0,
        }
        return result;
    }
}
