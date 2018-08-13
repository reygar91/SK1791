﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoCharacter : MonoBehaviour {

    public Animator AnimatorComponent
    {
        get; private set;
    }    

    public Character character;    

    private void Awake()
    {
        AnimatorComponent = GetComponentInChildren<Animator>();        
    }

    private void OnEnable()
    {
        AnimatorComponent.enabled = true;
        if (character == null)
        {
            Debug.Log("character is not defined: disabling");
            gameObject.SetActive(false);
        }
        character.Prototype(character.prototypeID);
        name = character.name;        
    }
    private void OnDisable()
    {
        AnimatorComponent.enabled = false;
    }


    public void Move()
    {
        if (!hasReachedTarget(character.Target))
        {
            Vector3 TargetVector = new Vector3(character.Target.x, transform.position.y, character.Target.z);
            transform.position = Vector3.MoveTowards(transform.position, TargetVector, character.WalkSpeed * Time.deltaTime);
            MovingDirection(TargetVector);
            if (AnimatorComponent.GetInteger("AnimationID") != 1)
            {
                AnimatorComponent.SetInteger("AnimationID", 1);
            }
        } else if (AnimatorComponent.GetInteger("AnimationID") != 0)
        {
            AnimatorComponent.SetInteger("AnimationID", 0);
        }
    }

    public bool hasReachedTarget(Vector3 ObjectPosition)
    {
        bool result_x = (Mathf.Abs(transform.position.x - ObjectPosition.x) < 0.005f);
        bool result_z = (Mathf.Abs(transform.position.z - ObjectPosition.z) < 0.005f);
        bool result = result_x && result_z;
        return result;
    }

    private void MovingDirection(Vector3 TargetVector)
    {
        if (TargetVector.x < transform.position.x)
        {
            transform.rotation = new Quaternion(0, -1, 0, 0);//facing from right to left
        }
        else if (TargetVector.x > transform.position.x)
        {
            transform.rotation = new Quaternion(0, 0, 0, 1);//facing from left to right
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(name + " OnTriggerEnter " + other.gameObject.name);
        character.EnterTriggerBehaviour(other, this);
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(name + " OnTriggerExit");
    }

    public void Tick()
    {
        if (gameObject.activeSelf)
        {
            character.CountDown -= Time.deltaTime * character.CountDownMultiplier;

            switch (character.state)
            {
                case (Character.State.Animation):
                    //Debug.Log(character.AnimationWaitTime);
                    if (character.AnimationWaitTime < 0)
                        character.state = Character.State.Move;
                    else character.AnimationWaitTime -= Time.deltaTime;
                    break;
                case (Character.State.Move):
                    if (character.Behaviour != null)
                    {
                        if (hasReachedTarget(character.Target))
                        {
                            //Debug.Log(name + " " + CurrentRoom + "=>" + TargetRoom + "=>" + character.CountDown);
                            if (character.CountDown < 0 && character.TargetRoom != Reception.Instance)
                                character.TargetRoom = Reception.Instance;
                            else if (character.CurrentRoom != character.TargetRoom)
                            {
                                //Debug.Log(name + "_room_" + TargetRoom.name);
                                character.Target = character.Behaviour.ChangeRoom(character.TargetRoom);
                            }
                                
                            else
                                character.Target = character.Behaviour.RoomBehaviour();
                        }                        
                    }
                    Move();
                    break;
                case (Character.State.Pause):
                    break;
            }
        }        
    }
}
