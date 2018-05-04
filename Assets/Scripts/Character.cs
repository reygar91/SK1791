using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    

    public Vector3 SetTargetVector(GameObject iTarget)
    {
        float x, y, z;
        x = iTarget.transform.position.x;
        y = transform.position.y;
        z = iTarget.transform.position.z;
        Vector3 Target = new Vector3(x, y, z);
        return Target;
    }

    public void MoveTo(Vector3 iTarget)
    {
        float step = Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, iTarget, 3*step);
    }

    public bool TargetReached(Vector3 iTarget)
    {
        return (transform.position == iTarget);
    }
}
