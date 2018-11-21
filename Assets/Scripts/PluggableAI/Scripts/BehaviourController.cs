using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourController : MonoBehaviour {

    public Animator AnimatorComponent
    {
        get; private set;
    }

    public HumanoidRenderer Renderer
    {
        get; private set;
    }

    public Mover mover
    {
        get; private set;
    }

    public FocusData Focus
    {
        get; private set;
    }

    public BehaviourPattern CountDown_0, behaviour, NextBehaviour;
    public Action[] possibleBehaviours;

    public Character character; //must be defined in Awake, before OnEnable from CharacterReset
    public int ActionID;

    private void Awake()
    {
        AnimatorComponent = GetComponentInChildren<Animator>();
        Renderer = GetComponent<HumanoidRenderer>();
        mover = GetComponent<Mover>();
        Focus = GetComponent<FocusData>();
    }

    private void OnEnable()
    {
        character.OnEnable(this);
    }

    private void OnDisable()
    {
        character.OnDisable(this);
    }

    public void Tick() //Tick
    {
        character.Tick(this, behaviour);
    }

    public void CHARACTER_DEBUG()
    {
        Debug.Log("Name => " + name);
        Debug.Log("Character type => " + character.GetType().ToString());
        //Debug.Log("Target => " + character.Target);
        //Debug.Log("CurrentRoom => " + character.CurrentRoom);
        Debug.Log("State => " + character.state);
    }

}
