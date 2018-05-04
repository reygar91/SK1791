using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorPreview : MonoBehaviour {

    public int InteriorIndex;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Room")
        {
            if (Input.GetMouseButtonDown(0) && !UI_helper.isPointerOverUI())
            {
                Room RoomTmp = other.GetComponent<Room>();
                RoomTmp.BuildInterior(InteriorIndex);
                gameObject.SetActive(false);
            }
        }
    }

    private void LateUpdate()
    {
        UI_helper.MoveWithMouse(gameObject, 1, new Vector3(0, 0, -2));
        if (Input.GetMouseButtonDown(1))
        {
            gameObject.SetActive(false);
        }
    }


}
