using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save {

    public List<Character> Characters = new List<Character>();
    public List<Room> Rooms = new List<Room>();

    public int gold = 0;
    public int time = 0;

}
