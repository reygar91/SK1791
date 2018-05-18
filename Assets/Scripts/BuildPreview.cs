using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPreview : MonoBehaviour {

    //public GameObject Doors;
    public GameObject[] Rooms;
    /*
     * 0 - Bar;
    */
    public GameObject RoomContainer;
    private bool allowedToBuild = true;
    private BoxCollider myCollider;
    private GameObject child;

    private void Awake()
    {
        myCollider = GetComponent<BoxCollider>();
    }
    private void OnEnable()
    {
        child = gameObject.transform.GetChild(0).gameObject;
        BoxCollider ChildCollider = child.GetComponentInChildren<BoxCollider>();
        myCollider.center = new Vector3(ChildCollider.center.x, myCollider.center.y, myCollider.center.z);
        myCollider.size = new Vector3(ChildCollider.size.x, myCollider.size.y, myCollider.size.z);       

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Room")
        {
            allowedToBuild = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Room")
        {
            allowedToBuild = true; //Debug.Log("Colliding with other Room");
        }
    }

    private void SelectBuildPosition()
    {
        Vector3 newPosition = UI_helper.MoveWithMouse(gameObject, 3, new Vector3(0, 0, 0));
        if (Input.GetMouseButtonDown(0) && !UI_helper.isPointerOverUI())
        {
            if (allowedToBuild == true)
            {
                GameObject newRoom = Instantiate(child, RoomContainer.transform);
                newRoom.transform.position = newPosition;
                resetPreview();
            }
            else
            {
                Debug.Log("Not allowed to build here");
            }
        }
    }

    void LateUpdate()
    {
        SelectBuildPosition();
        if (Input.GetMouseButtonDown(1))
        {
            resetPreview();
        }
    }

    private void resetPreview()
    {
        transform.position = new Vector3();
        child.transform.SetParent(transform.parent);
        //child.transform.position = new Vector3();        
        child.SetActive(false);        
        gameObject.SetActive(false);
    }


}
