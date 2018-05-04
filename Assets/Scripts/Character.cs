using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    private float oldDelta, delta;
    

    public Vector3 SetTargetVector(GameObject iTarget)
    {
        float x, y, z;
        x = iTarget.transform.position.x;
        y = transform.position.y;
        z = iTarget.transform.position.z;
        Vector3 Target = new Vector3(x, y, z);
        return Target;
    }

    public float MoveTo(Vector3 iTarget)
    {
        float step = Time.deltaTime;
        float oldPositionX = transform.position.x;
        transform.position = Vector3.MoveTowards(transform.position, iTarget, 3*step);
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

    public bool hasReachedTarget(Vector3 iTarget)
    {
        return (transform.position == iTarget);
    }
}
