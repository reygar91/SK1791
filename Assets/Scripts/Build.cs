﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Build : MonoBehaviour {

    public Toggle[] RoomToggles, SizeToggles;
    public RectTransform SizeGroup;
    public BuildPreview Preview;
    private int RoomIndex;

    private void OnEnable()
    {
        Reception.Instance.boxCollider.gameObject.layer = 0;
        foreach (Room item in BuildMNGR.Instance.roomsList)
        {
            item.boxCollider.gameObject.layer = 0;
        }
    }

    private void OnDisable()
    {
        Reception.Instance.boxCollider.gameObject.layer = 2;
        foreach (Room item in BuildMNGR.Instance.roomsList)
        {
            item.boxCollider.gameObject.layer = 2;
        }
        if (Preview)
        {
            Preview.gameObject.SetActive(false);
        }
    }


    public void SelectSize(int i)
    {
        if (SizeToggles[i].isOn)
        {
            Preview.Rooms[3 * RoomIndex + i].transform.SetParent(Preview.transform);
            Preview.Rooms[3 * RoomIndex + i].gameObject.SetActive(true);
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

}