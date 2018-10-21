using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character {
    
    //public ICharBehaviour Behaviour;
    public MonoCharacter MC;
    public float WalkSpeed;
    //public BehaviourData behaviourData;
    public Vector3 Target;
    public Room CurrentRoom, TargetRoom;
    public string Name;
    public CharacterAppearance Appearance;

    public int BehaviourStatusID;
    //public GameObject FocusGO;

    public delegate void BehaviourDelegate();
    public BehaviourDelegate RoomBehaviour, ResetFocus, GetFocus, DoAction;

    public FocusData Focus = new FocusData();


    //CountDown will represent patience for customers and energy for personel, Multiplier is used to determine difficulty of performed activity
    public float AnimationWaitTime, CountDown, CountDownMultiplier;

    public enum State
    {
        Animation, Move, Pause, Idle
    }

    public State state, prevState;

    public Character()
    {
        Target = Reception.Instance.EntrancePoint.transform.position;
        state = State.Animation;
        CountDownMultiplier = 1; //multiplier shows how much energy (countDown) is drained per sec in some action
    }


    protected virtual void SetCharacteristics()
    {
    }

    public virtual void ApplyCharacteristics()
    {
    }

    protected virtual void SetAppearance()
    {
    }

    public virtual void ApplyAppearance()
    {
    }
    /// <summary>
    /// Behaviours Block
    /// </summary>

    public void GetBehaviour()
    {
        string RoomType = CurrentRoom.GetType().ToString();
        switch (RoomType)
        {
            case "Reception":
                RoomBehaviour = AtReception; //Debug.Log(RoomBehaviour.Method.ToString());
                break;
            case "Bar":
                RoomBehaviour = AtBar;
                break;
            default:
                RoomBehaviour = null;
                break;
        }
    }

    protected virtual void AtReception()
    {
    }
    protected virtual void AtBar()
    {
    }


    public void ChangeRoom()
    {
        Transform Doors, Middle, Char;
        Doors = CurrentRoom.Doors.transform;
        Middle = CurrentRoom.MiddleOfTheRoom.transform;
        Char = MC.transform;
        switch (BehaviourStatusID)
        {
            case 1://align with doors
                Target.x = Doors.position.x;
                BehaviourStatusID = 2;
                break;
            case 2://move to the doors
                Target.z = Doors.position.z;
                BehaviourStatusID = 3;
                break;
            case 3:
                Char.position = TargetRoom.Doors.transform.position;
                Target = TargetRoom.Doors.transform.position;
                Target.z -= 1.5f;
                BehaviourStatusID = 4;
                break;
            case 4:
                //empty, so to do nothing untill he enters to a new room
                break;
            default://move to the center of the room
                Target.z = Middle.position.z;
                BehaviourStatusID = 1;
                ResetFocus();
                break;
        }
    }

    public void ReachFocusMC()
    {
        Transform Middle, FocusTR;
        Middle = CurrentRoom.MiddleOfTheRoom.transform; //Debug.Log(Focus.MC);
        FocusTR = Focus.MC.transform;
        switch (BehaviourStatusID)
        {
            case 5://align with Focus target
                Target.x = FocusTR.position.x;
                BehaviourStatusID = 6;
                break;
            case 6://move to the Focus
                Target.z = FocusTR.position.z;
                BehaviourStatusID = 7;
                break;
            case 7:
                DoAction();
                BehaviourStatusID = 8;                
                break;
            case 8:
                GetBehaviour();
                break;
            default://move to the center of the room
                Target.z = Middle.position.z;
                BehaviourStatusID = 5;
                break;
        }
    }

    //public virtual void DoAction()
    //{

    //}

    public virtual void SetFocus()
    {
        
    }


}
