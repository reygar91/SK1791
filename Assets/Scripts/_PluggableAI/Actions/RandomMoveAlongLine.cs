using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/RandomMoveAlongLine")]
public class RandomMoveAlongLine : Action {

    public override bool Act(myCharacterController Character)
    {
        Vector3 Middle = Character.Focus.Activity.Room.MiddleOfTheRoom.position;
        float delta = Character.Focus.Activity.Room.Doors.position.x - Middle.x;//distance from center of the room to the doors, considering doors located right to the center. doors.x > center.x
        float minX = Middle.x - delta;
        float maxX = Middle.x + delta;
        Middle.x = Random.Range(minX, maxX);
        Character.Mover.StartMovement(Middle, "");
        return true;
    }
}
