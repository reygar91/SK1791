using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    private float oldDelta, delta;
    

    private Vector3 SetTargetVector(GameObject TargetObject)
    {
        float x, y, z;
        x = TargetObject.transform.position.x;
        y = transform.position.y;
        z = TargetObject.transform.position.z;
        Vector3 TargetVector = new Vector3(x, y, z);
        return TargetVector;
    }

    public float MoveTo(GameObject TargetObject)
    {
        float oldPositionX = transform.position.x;
        if (TargetObject != null)
        {
            Vector3 TargetVector = SetTargetVector(TargetObject);
            float step = Time.deltaTime;            
            transform.position = Vector3.MoveTowards(transform.position, TargetVector, 3 * step);
        }        
        if (delta != 0) { oldDelta = delta; }        
        delta = oldPositionX - transform.position.x;
        if (Mathf.Sign(oldDelta) != Mathf.Sign(delta))
        {
            if (delta > 0)
            {
                transform.rotation = new Quaternion(0, -1, 0, 0);//facing from right to left
            }
            else if (delta < 0)
            {
                transform.rotation = new Quaternion(0, 0, 0, 1);//facing from left to right
            }
        }        
        return delta;
    }

    public bool hasReachedTarget(GameObject TargetObject)
    {
        if (TargetObject != null)
        {
            Vector3 TargetVector = SetTargetVector(TargetObject);
            return (transform.position == TargetVector);
        }
        else return false; //maybe should return true?         
    }
}
