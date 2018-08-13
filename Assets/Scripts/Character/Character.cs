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
        CountDownMultiplier = 1;
    }

    public virtual void EnterTriggerBehaviour(Collider other, MonoCharacter monoCharacter)
    {
    }

    public void Prototype(int prototypeID) //this is called in MonoCharacter onEnable
    {
        //Debug.Log(monoCharacter.name + "=> Prototype called");
        switch (prototypeID)
        {
            case 0:

                int RandomNumber = Random.Range(0, 1000);
                name = "Customer_" + RandomNumber;

                //outfit.SetOutfit();
                Debug.Log("Target => " + Target);
                Debug.Log("TargetRoom => " + TargetRoom);
                break;
            case 1:
                break;
        }
    }

}
