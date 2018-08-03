using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMNGR : MonoBehaviour {

    public static BuildMNGR Instance;

    public List<Room> roomsList = new List<Room>();
    public BuildPermissionGrid Grid;

    private void Awake()
    {
        Instance = this;
        Grid = new BuildPermissionGrid(4, 9);
    }


}
