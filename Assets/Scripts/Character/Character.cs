using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character {
    
    public ICharBehaviour Behaviour;
    public MonoCharacter monoCharacter;
    public float WalkSpeed;
    public BehaviourData behaviourData;

    //CountDown will represent patience for customers and energy for personel, Multiplier is used to determine difficulty of performed activity
    public float AnimationWaitTime, CountDown, CountDownMultiplier;
    public int prototypeID;

    public enum State
    {
        Animation, Move, Pause
    }

    public State state, prevState;

    public Character(MonoCharacter instance)
    {
        monoCharacter = instance;
        state = State.Animation;
        CountDownMultiplier = 1;
        //Prototype(prototypeID);
        //Target = Reception.instance
    }

    public virtual void EnterTriggerBehaviour(Collider other, Transform transform)
    {
    }

    public void Prototype(int prototypeID) //this is called in MonoCharacter onEnable
    {
        //Debug.Log(monoCharacter.name + "=> Prototype called");
        switch (prototypeID)
        {
            case 0:
                monoCharacter.Target = Reception.Instance.EntrancePoint.transform.position;
                Behaviour = null;
                CountDown = 25;
                //Wait = false;
                int RandomNumber = UnityEngine.Random.Range(0, 1000);
                monoCharacter.name = "Customer_" + RandomNumber;
                monoCharacter.AnimatorComponent.enabled = true; //Debug.Log(behaviourData);
                if (Time.timeScale != 0) //temporary solution, timescale = 0 only on pause; 
                    monoCharacter.TargetRoom = Reception.Instance;
                //outfit.SetOutfit();
                break;
            case 1:
                break;
        }
    }

}
