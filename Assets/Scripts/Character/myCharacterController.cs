using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myCharacterController : MonoBehaviour {

    public Animator AnimatorComponent
    {
        get; private set;
    }

    public HumanoidRenderer Renderer
    {
        get; private set;
    }

    public myMover Mover
    {
        get; private set;
    }

    public FocusData Focus
    {
        get; private set;
    }

    public int ActionID;
    public BehaviourPattern behaviour, NextBehaviour;
    public Action[] possibleBehaviours;

    public myCharacterStats Stats; //must be defined in Awake, before OnEnable from CharacterReset    

    private void Awake()
    {
        AnimatorComponent = GetComponentInChildren<Animator>();
        Renderer = GetComponent<HumanoidRenderer>();
        Mover = GetComponent<myMover>();
        Focus = GetComponent<FocusData>();
    }

    private void OnEnable()
    {
        Stats.OnEnable(this);
    }

    private void OnDisable()
    {
        Stats.OnDisable(this);
    }

    public void Tick() //Tick
    {
        Stats.Tick(this, behaviour);
    }

    public void CHARACTER_DEBUG()
    {
        Debug.Log("Name => " + name);
        Debug.Log("Character type => " + Stats.GetType().ToString());
        //Debug.Log("Target => " + character.Target);
        //Debug.Log("CurrentRoom => " + character.CurrentRoom);
        Debug.Log("State => " + Stats.state);
    }

}
