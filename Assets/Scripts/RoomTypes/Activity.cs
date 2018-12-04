using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity : MonoBehaviour {

    public Room Room;

    [SerializeField] protected Transform[] CustomerInteractionObjects;

    public BehaviourPattern PesrBehaviour, CustBehaviour;

    public List<myCharacterController> Customers = new List<myCharacterController>();

    public virtual void RegisterInteractionObjects() { }

    protected virtual void Awake()
    {
        Room = GetComponent<Room>();
    }

    public int FindIndexOfInteractionObj(Transform TargetObj)
    {        
        return System.Array.IndexOf(CustomerInteractionObjects, TargetObj);
    }

    public Transform FindInteractionObjByIndex(int ObjectIndex)
    {
        return CustomerInteractionObjects[ObjectIndex];
    }

    public virtual void Load(int ObjectIndex, myCharacterController CC) { }

    public virtual Transform CheckActivityWithinRoom()
    {
        //System.Predicate<Transform> IsWithinRoom = delegate (Transform item)
        //{ return item.GetComponentInParent<Room>().transform == Character.Focus.CurrentRoom.transform; };
        return null;
    }

}

[System.Serializable]
public struct InteractionObj
{
    public string Key;
    public Transform Object;
}
