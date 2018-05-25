using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public Animator AnimatorComponent
    {
        get; set;
    }


    public void MoveTo(Vector3 ObjectPosition)
    {
        if (!hasReachedTarget(ObjectPosition))
        {
            Vector3 TargetVector = new Vector3(ObjectPosition.x, transform.position.y, ObjectPosition.z);
            float step = 3 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, TargetVector, step);
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
}
