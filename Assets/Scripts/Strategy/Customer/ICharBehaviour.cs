using System.Collections.Generic;
using UnityEngine;

public interface ICharBehaviour
{
    //void RoomBehaviour<T>(Customer customer, T RoomType, Animator AnimatorComponent);
    Vector3 LeaveRoom();
    Vector3 RoomBehaviour();
    Vector3 ChangeRoom(Room targetRoom);
    void SwitchRoom();
    int GetStatusID();
    void SetStatusID(int ID);
}