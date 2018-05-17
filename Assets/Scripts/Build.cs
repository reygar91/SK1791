using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Build : MonoBehaviour {

    public Toggle[] RoomOption, InteriorOption;
    public BuildPreview[] RoomOriginals;
    /*Room Originals:
     * 0 - RoomSize 6
     * 1 - RoomSize 9
     * 2 - RoomSize 12
     */
    public RectTransform SizeGroup;
    private BuildPreview Preview;
    private int InteriorIndex;
    


    public void SelectRoom(int i)
    {
        if (RoomOption[i].isOn)
        {
            Preview = RoomOriginals[i];
            Preview.Interior = Preview.InteriorOption[InteriorIndex];
            Preview.gameObject.SetActive(true);
            RoomOption[i].isOn = false; // Disabling Toggle, so dont have to click twice when building similar constructions
            InteriorOption[InteriorIndex].isOn = false;
            SizeGroup.gameObject.SetActive(false);
        }
    }

    public void SelectInterior(int i)
    {
        if (InteriorOption[i].isOn)
        {
            InteriorIndex = i;
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