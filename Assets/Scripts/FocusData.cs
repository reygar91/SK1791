using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusData : MonoBehaviour
{
    public int Index;
    public bool NotSet = false;
    public BehaviourController BC;
    public GameObject Object;
    //public Transform TeleportDestination; //Teleporter usually would be Doors
    //public Room RoomToBeLeft;//CurrentRoom
    public Room Room;
    public Activity Activity;

    public FocusData SaveFocusData()
    {
        if (Object)
        {
            NotSet = true;
        }
        if (BC)
        {
            NotSet = true;
            Index = CharacterMNGR.Instance.ActiveCharacters.IndexOf(BC);
            Debug.Log(BC.name + " with index " + Index + " saved");
        }
        return this;
    }

    private void OnDisable()
    {
        BC = null;
        Object = null;
        //CurrentRoom = null;
        //RoomToBeLeft = null;
    }
    //public GameObject ObjectOfInterest; // GO can not be serialized, need to change bar to look like reception, 2 arrays 1 with GO another with availability index
}
