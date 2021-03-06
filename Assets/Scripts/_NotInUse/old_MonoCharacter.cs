﻿//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MonoCharacter : MonoBehaviour {

//    public Animator AnimatorComponent
//    {
//        get; private set;
//    }
//    public HumanoidRenderer Renderer
//    {
//        get; private set;
//    }

//    public Character character;

//    private void Awake()
//    {
//        AnimatorComponent = GetComponentInChildren<Animator>();
//        Renderer = GetComponent<HumanoidRenderer>();
//    }


//    private void OnEnable()
//    {
//        AnimatorComponent.enabled = true;
//        if (character == null)
//        {
//            Debug.Log("character is not defined: disabling");
//            gameObject.SetActive(false);
//        }
//        character.MC = this;
//        character.ApplyCharacteristics();
//        character.ApplyAppearance();
//        Register();
//    }

//    private void Register()
//    {
//        if (CharacterMNGR.Instance.MCPool.Contains(this))
//            CharacterMNGR.Instance.MCPool.Remove(this);
//        CharacterMNGR.Instance.ActiveMC.Add(this);
//    }

//    private void OnDisable()
//    {
//        AnimatorComponent.enabled = false;
//        UnRegister();
//    }

//    private void UnRegister()
//    {
//        CharacterMNGR.Instance.ActiveMC.Remove(this);
//        CharacterMNGR.Instance.MCPool.Add(this);
//    }


//    public void Move()
//    {
//        if (!hasReachedTarget(character.Target))
//        {
//            Vector3 TargetVector = new Vector3(character.Target.x, transform.position.y, character.Target.z);
//            transform.position = Vector3.MoveTowards(transform.position, TargetVector, character.WalkSpeed * Time.deltaTime);
//            MovingDirection(TargetVector);
//            if (AnimatorComponent.GetInteger("AnimationID") != 1)
//            {
//                AnimatorComponent.SetInteger("AnimationID", 1);
//            }
//        } else if (AnimatorComponent.GetInteger("AnimationID") != 0)
//        {
//            AnimatorComponent.SetInteger("AnimationID", 0);
//        }
//    }

//    public bool hasReachedTarget(Vector3 ObjectPosition)
//    {
//        bool result_x = (Mathf.Abs(transform.position.x - ObjectPosition.x) < 0.005f);
//        bool result_z = (Mathf.Abs(transform.position.z - ObjectPosition.z) < 0.005f);
//        bool result = result_x && result_z;
//        return result;
//    }

//    private void MovingDirection(Vector3 TargetVector)
//    {
//        if (TargetVector.x < transform.position.x)
//        {
//            transform.rotation = new Quaternion(0, -1, 0, 0);//facing from right to left
//        }
//        else if (TargetVector.x > transform.position.x)
//        {
//            transform.rotation = new Quaternion(0, 0, 0, 1);//facing from left to right
//        }
//    }


//    private void OnTriggerEnter(Collider other)
//    {
//        character.CurrentRoom = other.GetComponentInParent<Room>(); 
//        character.GetBehaviour();
//        character.Target = transform.position;
//        if (character.Focus.NotSet)
//        {
//            character.SetFocus();
//            character.Focus.NotSet = false;
//        }
//        string charType = character.GetType().ToString();
//        if (charType == "MainCharacter" || charType == "Personnel")
//            character.CurrentRoom.RegisterPersonel(this);
//    }


//    public void Tick()
//    {
//        character.CountDown -= Time.deltaTime * character.CountDownMultiplier;

//        switch (character.state)
//        {
//            case (Character.State.Animation):
//                //Debug.Log(character.AnimationWaitTime);
//                if (character.AnimationWaitTime < 0)
//                {
//                    character.state = Character.State.Move;
//                    character.CountDownMultiplier = 1;
//                }                    
//                else character.AnimationWaitTime -= Time.deltaTime;
//                break;
//            case (Character.State.Move):
//                if (hasReachedTarget(character.Target))
//                {
//                    if (character.CountDown < 0 && character.TargetRoom != Reception.Instance)
//                        character.TargetRoom = Reception.Instance;
//                    else if (character.CurrentRoom != character.TargetRoom)
//                        character.ChangeRoom();
//                    else
//                        character.RoomBehaviour();
//                }
//                Move();
//                break;
//            case (Character.State.Pause):
//                character.CountDown += Time.deltaTime * character.CountDownMultiplier;
//                break;
//        }
//    }

//    public void CHARACTER_DEBUG()
//    {
//        Debug.Log("Name => " + name);
//        Debug.Log("Character type => " + character.GetType().ToString());
//        Debug.Log("Target => " + character.Target);
//        Debug.Log("Current&TargetRoom => " + character.CurrentRoom + " => " + character.TargetRoom);
//        Debug.Log("State => " + character.state);
//    }

//}
