using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoCharacter : MonoBehaviour {

    private Animator AnimatorComponent
    {
        get; set;
    }

    public float WalkSpeed;


    public CharacterVer2 characterVer2;

    private void Awake()
    {
        AnimatorComponent = GetComponent<Animator>();
        Reception reception = FindObjectOfType<Reception>();
        characterVer2 = new CustomerVer2
        {
            reception = reception
        };
        Debug.Log("Character is Awake");
    }


    public void MoveTo()
    {
        if (!hasReachedTarget(characterVer2.Target))
        {
            Vector3 TargetVector = new Vector3(characterVer2.Target.x, transform.position.y, characterVer2.Target.z);
            transform.position = Vector3.MoveTowards(transform.position, TargetVector, WalkSpeed * Time.deltaTime);
            MovingDirection(TargetVector);
            if (AnimatorComponent.GetInteger("AnimationID") != 1)
            {
                AnimatorComponent.SetInteger("AnimationID", 1);
            }
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
        characterVer2.EnterTriggerBehaviour(other, transform);
    }

    //public void Initialize(CharacterVer2 newChar)
    //{
    //    switch (newChar.GetType().ToString())
    //    {
    //        case "CustomerVer2":
    //            //characterVer2 = newChar as newChar.GetType();
    //            break;
    //    }
    //}
}
