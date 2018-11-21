using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public float WalkSpeed;
    
    public string Name;
    public CharacterAppearance Appearance;

    public float X, Y, Z;

    //public FocusData Focus = new FocusData();

    //CountDown will represent patience for customers and energy for personel, Multiplier is used to determine difficulty of performed activity
    public float AnimationWaitTime, CountDown, CountDownMultiplier;

    public enum State
    {
        Animation, Move, BehaviourUpdate, Pause, Idle
    }

    public State state, prevState;

    public Character()
    {
        state = State.Animation;
        CountDownMultiplier = 1; //multiplier shows how much energy (countDown) is drained per sec in some action
    }

    protected virtual void SetCharacteristics() //possibly this may be changed to be private to Cust & Pers class
    {
    }

    protected virtual void SetAppearance()
    {
    }

    public void Tick(BehaviourController controller, BehaviourPattern behaviour)
    {
        CountDown -= Time.deltaTime * CountDownMultiplier;
        switch (state)
        {
            case (State.Animation):
                if (AnimationWaitTime < 0)
                {
                    state = State.BehaviourUpdate;
                    CountDownMultiplier = 1;
                }
                else AnimationWaitTime -= Time.deltaTime;
                break;
            case (State.Move):
                break;
            case (State.BehaviourUpdate):
                behaviour.UpdateBehaviour(controller);
                break;
            case (State.Pause):
                CountDown += Time.deltaTime * CountDownMultiplier;
                break;
        }
    }

    public virtual void Spawn() { }
    //public virtual void ApplyRoomBehaviour(BehaviourController MC) { }
    public virtual void OnEnable(BehaviourController MC) { }
    public virtual void OnDisable(BehaviourController MC) { }

    //public void ReachFocusMC()
    //{
    //    Transform Middle, FocusTR;
    //    Middle = CurrentRoom.MiddleOfTheRoom.transform; //Debug.Log(Focus.MC);
    //    FocusTR = Focus.MC.transform;
    //    switch (BehaviourStatusID)
    //    {
    //        case 5://align with Focus target
    //            Target.x = FocusTR.position.x;
    //            BehaviourStatusID = 6;
    //            break;
    //        case 6://move to the Focus
    //            Target.z = FocusTR.position.z;
    //            BehaviourStatusID = 7;
    //            break;
    //        case 7:
    //            DoAction();
    //            BehaviourStatusID = 8;                
    //            break;
    //        case 8:
    //            GetBehaviour();
    //            break;
    //        default://move to the center of the room
    //            Target.z = Middle.position.z;
    //            BehaviourStatusID = 5;
    //            break;
    //    }
    //}



}
