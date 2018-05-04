using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Build : MonoBehaviour {

    public Toggle[] RoomOption, InteriorOption;
    public GameObject[] RoomOriginals;
    /*Room Originals:
     * 0 - RoomSize 6
     * 1 - RoomSize 9
     * 2 - RoomSize 12
     */    
    public GameObject RoomContainer, InteriorPrewiewGO;
    private GameObject RoomGO;
    InteriorPreview IntPreview;

    private void Awake()
    {
        RoomGO = RoomOriginals[0];
        IntPreview = InteriorPrewiewGO.GetComponent<InteriorPreview>();
    }

    public void SelectRoom(int i)
    {
        if (RoomOption[i].isOn)
        {
            RoomGO.SetActive(false);
            RoomGO = RoomOriginals[i];
            RoomGO.SetActive(true);
            RoomOption[i].isOn = false; // Disabling Toggle, so dont have to click twice when building similar constructions
        }
    }

    private void SelectBuildPosition()
    {
        Vector3 newPosition = UI_helper.MoveWithMouse(RoomGO, 3, new Vector3(0, -0.1f, 0.9f));
        if (Input.GetMouseButtonDown(0) && !UI_helper.isPointerOverUI())
        {
            GameObject newRoom = Instantiate(RoomGO, RoomContainer.transform);
            newRoom.transform.position = newPosition;
            RoomGO.SetActive(false);
        }
    }

    public void SelectInterior(int i)
    {
        if (InteriorOption[i].isOn)
        {
            InteriorPrewiewGO.SetActive(true);
            IntPreview.InteriorIndex = i;
            InteriorOption[i].isOn = false; // Disabling Toggle, so dont have to click twice when building similar constructions
        }
    }

    // Update is called once per frame
    void LateUpdate () {        
        if (RoomGO.activeSelf == true)
        {
            SelectBuildPosition();
            if (Input.GetMouseButtonDown(1))
            {
                RoomGO.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        RoomGO.SetActive(false);
        InteriorPrewiewGO.SetActive(false);
    }

}