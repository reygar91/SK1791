using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {


    public float speed;
    public float HorizontalLimitRight;
    public float HorizontalLimitLeft;
    public float VerticalLimitTop;
    public float VerticalLimitBottom;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        float verticalMovement = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float horizontalMovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        //HorizontalLimitation
        float xUpdated, yUpdated;
        if ((x + horizontalMovement) > HorizontalLimitRight)
        {
            xUpdated = HorizontalLimitRight;
        } else if ((x + horizontalMovement) < HorizontalLimitLeft) {
            xUpdated = HorizontalLimitLeft;
        } else { xUpdated = x + horizontalMovement; }
        //VerticalLimitation
        if ((y + verticalMovement) > VerticalLimitTop)
        {
            yUpdated = VerticalLimitTop;
        }
        else if ((y + verticalMovement) < VerticalLimitBottom)
        {
            yUpdated = VerticalLimitBottom;
        }
        else { yUpdated = y + verticalMovement; }
        //updating camera position
        transform.position = new Vector3(xUpdated, yUpdated, z);
    }
}
