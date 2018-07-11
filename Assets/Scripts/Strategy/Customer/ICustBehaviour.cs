using System.Collections.Generic;
using UnityEngine;

public interface ICustBehaviour
{
    //void RoomBehaviour<T>(Customer customer, T RoomType, Animator AnimatorComponent);
    Vector3 LeaveRoom();
    Vector3 RoomBehaviour();    
    void SwitchRoom();
    int GetStatusID();
    void SetStatusID(int ID);
}