using System.Collections.Generic;
using UnityEngine;

public interface ICharBehaviour
{
    Vector3 RoomBehaviour();
    Vector3 ChangeRoom(Room targetRoom);
    BehaviourData BehaviourData { get; }
}