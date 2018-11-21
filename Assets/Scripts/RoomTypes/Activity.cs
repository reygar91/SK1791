using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity : MonoBehaviour {

    [SerializeField] protected Transform[] CustomerInteractionObjects;

    public Room Room;

    public List<BehaviourController> Customers = new List<BehaviourController>();

    public virtual void RegisterInteractionObjects() { }

    protected virtual void Awake()
    {
        Room = GetComponent<Room>();
    }

}
