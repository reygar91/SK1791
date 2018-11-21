using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    private Vector3 Target, FinalTarget;
    public float MovementSpeed;
    private BehaviourController behaviourController;

    private Animator AnimatorComponent;

    private void Awake()
    {        
        behaviourController = GetComponent<BehaviourController>();
        AnimatorComponent = GetComponentInChildren<Animator>();
    }

    public void StartMovement(Vector3 target, float movementLineZ)
    {
        StopAllCoroutines();
        FinalTarget = target;
        MovementAnimation(1);
        behaviourController.character.state = Character.State.Move;

        StartCoroutine(BracketsMovement(movementLineZ));
    }

    public void StartMovement(Vector3 target)
    {
        StopAllCoroutines();
        FinalTarget = target;
        MovementAnimation(1);
        behaviourController.character.state = Character.State.Move;

        StartCoroutine(StraightMovement());
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
        behaviourController.character.state = Character.State.BehaviourUpdate;
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

}
