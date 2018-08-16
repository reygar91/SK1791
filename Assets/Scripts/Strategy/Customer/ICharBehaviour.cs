using System.Collections.Generic;
using UnityEngine;

public interface ICharBehaviour
{
    Vector3 RoomBehaviour();
    BehaviourData BehaviourData { get; }
}