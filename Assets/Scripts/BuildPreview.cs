using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPreview : MonoBehaviour {

    //public GameObject Doors;
    public GameObject[] InteriorOption;
    /*
     * 0 - Bar;
    */
    public GameObject Interior, InteriorContainer, RoomContainer;
    private bool allowedToBuild = true;

    private void OnEnable()
    {
        Interior.transform.SetParent(transform);
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
        Vector3 newPosition = UI_helper.MoveWithMouse(gameObject, 3, new Vector3(0, -0.1f, 0.9f));
        if (Input.GetMouseButtonDown(0) && !UI_helper.isPointerOverUI())
        {
            if (allowedToBuild == true)
            {
                GameObject newRoom = Instantiate(gameObject, RoomContainer.transform);
                newRoom.transform.position = newPosition;
                Destroy(newRoom.GetComponent<BuildPreview>());
                Interior.transform.SetParent(InteriorContainer.transform);
                newRoom.name = Interior.name;
                gameObject.SetActive(false);
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
            gameObject.SetActive(false);
        }
    }

}
