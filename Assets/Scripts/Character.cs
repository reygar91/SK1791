using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    private float oldDelta, delta;
    
    /*
    public Vector3 GetTargetVectorFrom(GameObject TargetObject)
    {
        float x, y, z;
        x = TargetObject.transform.position.x;
        y = transform.position.y;
        z = TargetObject.transform.position.z;
        Vector3 TargetVector = new Vector3(x, y, z);
        return TargetVector;
    }
    */
    public float MoveTo(Vector3 ObjectPosition)
    {
        Vector3 TargetVector = new Vector3(ObjectPosition.x, transform.position.y, ObjectPosition.z);
        float step = 3 * Time.deltaTime;        
        transform.position = Vector3.MoveTowards(transform.position, TargetVector, step);
        if (delta != 0) { oldDelta = delta; }
        delta = transform.position.x - TargetVector.x;      
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

    public bool hasReachedTarget(Vector3 TargetVector)
    {
        bool result_x = (Mathf.Abs(transform.position.x - TargetVector.x) < 0.005f);
        bool result_z = (Mathf.Abs(transform.position.z - TargetVector.z) < 0.005f);
        bool result = result_x && result_z;
        return result;       
    }
}
