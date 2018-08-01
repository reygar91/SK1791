using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMNGR : MonoBehaviour {

    public static BuildMNGR Instance;

    public List<Room> roomsList = new List<Room>();

    private void Awake()
    {
        Instance = this;
    }


}
