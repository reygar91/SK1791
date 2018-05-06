using System.Collections.Generic;
using UnityEngine;

public interface ICustBehaviour
{
    void RoomBehaviour<T>(Customer customer, T RoomType, Animator AnimatorComponent);
    GameObject RoomBehaviour();
    void SwitchRoom();
}