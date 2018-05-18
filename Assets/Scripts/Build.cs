using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Build : MonoBehaviour {

    public Toggle[] RoomToggles, SizeToggles;
    public RectTransform SizeGroup;
    public BuildPreview Preview;
    private int RoomIndex;
    


    public void SelectSize(int i)
    {
        if (SizeToggles[i].isOn)
        {
            Preview.Rooms[3 * RoomIndex + i].transform.SetParent(Preview.transform);
            Preview.Rooms[3 * RoomIndex + i].SetActive(true);
            Preview.gameObject.SetActive(true);
            SizeToggles[i].isOn = false; // Disabling Toggle, so dont have to click twice when building similar constructions
            RoomToggles[RoomIndex].isOn = false;
            SizeGroup.gameObject.SetActive(false);
        }
    }

    public void SelectRoom(int i)
    {
        if (RoomToggles[i].isOn)
        {
            RoomIndex = i;
            SizeGroup.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        if (Preview)
        {
            Preview.gameObject.SetActive(false);
        }
    }

}