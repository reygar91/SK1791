using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character {
    
    public ICharBehaviour Behaviour;
    //public MonoCharacter monoCharacter;
    public float WalkSpeed;
    public BehaviourData behaviourData;
    public Vector3 Target;
    public Room CurrentRoom, TargetRoom;
    public string name;
    public CharacterAppearance appearance;

    //CountDown will represent patience for customers and energy for personel, Multiplier is used to determine difficulty of performed activity
    public float AnimationWaitTime, CountDown, CountDownMultiplier;
    public int prototypeID;

    public enum State
    {
        Animation, Move, Pause
    }

    public State state, prevState;

    public Character()
    {
        Target = Reception.Instance.EntrancePoint.transform.position;
        state = State.Animation;
        CountDownMultiplier = 1; //multiplier shows how much energy (countDown) is drained per sec in some action
    }

    public virtual void EnterTriggerBehaviour(Collider other, MonoCharacter monoCharacter)
    {
    }

    protected virtual void SetCharacteristics()
    {
    }

    public virtual void ApplyCharacteristics(MonoCharacter monoCharacter)
    {
    }

    protected virtual void SetAppearance()
    {
    }

    public virtual void ApplyAppearance(MonoCharacter monoCharacter)
    {
    }

}
