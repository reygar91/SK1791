﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character {
    
    public ICustBehaviour Behaviour;
    public MonoCharacter monoCharacter;
    public float WalkSpeed;

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

    public void Prototype(int prototypeID)
    {
        switch (prototypeID)
        {
            case 0:
                monoCharacter.Target = Reception.instance.EntrancePoint.transform.position;
                Behaviour = null;
                CountDown = 25;
                //Wait = false;
                int RandomNumber = UnityEngine.Random.Range(0, 1000);
                monoCharacter.name = "Customer_" + RandomNumber;
                monoCharacter.AnimatorComponent.enabled = true;

                //outfit.SetOutfit();
                break;
            case 1:
                break;
        }
    }

}