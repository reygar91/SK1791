using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Build : MonoBehaviour {

    public Toggle[] Option;
    public GameObject[] RoomComponent;
    /*Room component legend:
     * 0 - Room Container - emptyGameObject (Transform (0, 0.1f, 0.9f))
     * 1 - Wall+Door (Transform (0, 0, 0) // (5.9f, 0, 0))
     * 2 - Wall
     * 3 - Section (Transform (3*i, -0.1f, 0.1f) // (3, -0.1f, 0.1f))
     * 
     * */
    GameObject RoomContainer;
    bool valueChanged;

    public void SelectRoom()
    {
        for (int i = 0; i < Option.Length; i++)
        {
            if (Option[i].isOn)
            {
                RoomContainer = Instantiate(RoomComponent[0], new Vector3(0, 0.1f, 0.9f), Quaternion.identity);
                GameObject DoorLeft = Instantiate(RoomComponent[1], new Vector3(0, 0, 0), Quaternion.identity);
                DoorLeft.transform.parent = RoomContainer.transform;
                GameObject[] Section = new GameObject[i + 2];
                for (int j = 0; j < (i + 2); j++)
                {
                    Section[j] = Instantiate(RoomComponent[3], new Vector3(j*3, -0.1f, 0.1f), Quaternion.identity);
                    Section[j].transform.parent = RoomContainer.transform;
                }
                GameObject DoorRight = Instantiate(RoomComponent[1], new Vector3(((i+2)*3)-0.1f, 0, 0), Quaternion.identity);
                DoorRight.transform.parent = RoomContainer.transform;
                RoomContainer.name = "RoomContainer " + (i+2);
            }
        }
    }

    private void SelectBuildPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 p = ray.origin + (ray.direction * 10.0f);
        //Debug.Log("Input: " + Input.mousePosition);        
        Debug.Log("ScreenToWorld: " + p);        
        RoomContainer.transform.position = new Vector3(p.x, p.y, 0.0f);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {        
        if (RoomContainer != null)
        {
            SelectBuildPosition();
            if (Input.GetMouseButtonDown(0) && valueChanged == true)
            {
                RoomContainer = null; Debug.Log("Nullified");
                valueChanged = false;
            }
            valueChanged = true;
        }        
    }
    
}
