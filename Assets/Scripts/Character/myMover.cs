using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myMover : MonoBehaviour {

    private Vector3 Target, FinalTarget;
    public float MovementSpeed;
    private myCharacterController Character;

    private Animator AnimatorComponent;
    private Room Room;

    private void Awake()
    {        
        Character = GetComponent<myCharacterController>();
        AnimatorComponent = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Room = collision.GetComponentInParent<Room>();
    }

    public void StartMovement(Vector3 target, string MovemetTypeParameter)
    {
        StopAllCoroutines();
        FinalTarget = target;
        
        Character.Stats.state = myCharacterStats.State.Move;

        switch (MovemetTypeParameter)
        {
            case "Direct":
                StartCoroutine(StraightMovement());
                break;
            case "Exit":
                FinalTarget = Room.Doors.position;
                StartCoroutine(BracketsMovement(Room.MiddleOfTheRoom.position.z));
                break;
            default:
                StartCoroutine(BracketsMovement(Room.MiddleOfTheRoom.position.z));
                break;
        }

        MovementAnimation(1);
    }

    private void MovementAnimation(int value) //1 to start animation, 0 - to stop
    {
        AnimatorComponent.SetInteger("AnimationID", value);
        MovingDirection();
    }

    private void MovingDirection()
    {
        if (FinalTarget.x < transform.position.x)
        {
            transform.rotation = new Quaternion(0, -1, 0, 0);//facing from right to left
        }
        else if (FinalTarget.x > transform.position.x)
        {
            transform.rotation = new Quaternion(0, 0, 0, 1);//facing from left to right
        }
    }

    private IEnumerator StraightMovement()
    {
        Target =  new Vector3(FinalTarget.x, transform.position.y, FinalTarget.z);  
        while (true)
        {
            yield return StartCoroutine(Movement());
            break;
        }
        StopMovement();
    }

    private IEnumerator BracketsMovement(float movementLineZ)
    {
        Target = transform.position;
        while (true)
        {
            yield return StartCoroutine(Midwards(movementLineZ));
            yield return StartCoroutine(AlongX());
            yield return StartCoroutine(OutMid());
            break;
        }
        StopMovement();
    }

    private void StopMovement()
    {
        StopAllCoroutines();
        MovementAnimation(0);
        Character.Stats.state = myCharacterStats.State.BehaviourUpdate;
    }

    private IEnumerator Movement()
    {
        while (!HasReachedTarget())
        {
            transform.position = Vector3.MoveTowards(transform.position, Target, MovementSpeed * Time.deltaTime);
            yield return null;
        }
    }    

    private IEnumerator Midwards(float movementLineZ)
    {
        Target.z = movementLineZ;
        while (true)
        {
            yield return StartCoroutine(Movement());
            break;
        }
    }

    private IEnumerator AlongX()
    {
        Target.x = FinalTarget.x;
        while (true)
        {
            yield return StartCoroutine(Movement());
            break;
        }
    }

    private IEnumerator OutMid()
    {
        Target.z = FinalTarget.z;
        while (true)
        {            
            yield return StartCoroutine(Movement());
            break; 
        }
    }

    private bool HasReachedTarget()
    {
        bool result_x = (Mathf.Abs(transform.position.x - Target.x) < 0.005f);
        bool result_z = (Mathf.Abs(transform.position.z - Target.z) < 0.005f);
        bool result = result_x && result_z;
        return result;
    }

    public bool IsInActivityRoom()
    {
        return (Character.Focus.Activity.Room == Room);
    }

}
