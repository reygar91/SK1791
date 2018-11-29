using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    //public static List<Room> roomsList = new List<Room>();
    public BoxCollider2D boxCollider;
    public SpriteRenderer spriteRenderer;
    public Transform Doors, MiddleOfTheRoom;
    public RoomSaveData saveData = new RoomSaveData();

    //public BehaviourController[] AvailablePersonel;

    public List<Activity> Activities;

    public static Room selectedRoom;

    //public BehaviourPattern PersonnelBehaviour;

    public int BuildPrice;

    private void OnMouseDown()
    {
        if (!UI_helper.isPointerOverUI())
        {
            //Debug.Log("MouseDown over Room" + gameObject.name + " => " + gameObject.transform.position);
        }        
    }

    private void OnMouseEnter()
    {
        spriteRenderer.gameObject.SetActive(true);
        selectedRoom = this; //Debug.Log(selectedRoom.name);
    }

    private void OnMouseExit()
    {
        spriteRenderer.gameObject.SetActive(false);
        selectedRoom = null; //Debug.Log(selectedRoom);
    }

    

    //public bool RegisterPersonel(BehaviourController controller)
    //{
    //    bool result = false;
    //    for (int i = 0; i < AvailablePersonel.Length; i++)
    //    {
    //        if (AvailablePersonel[i] == null)
    //        {
    //            AvailablePersonel[i] = controller;
    //            result = true;
    //            break;
    //        }                
    //    }
    //    return result;
    //}

    //public BehaviourController GetPersonel()
    //{
    //    BehaviourController result = null;
    //    for (int i=0; i< AvailablePersonel.Length; i++)
    //    {
    //        if (AvailablePersonel[i].character.state == Character.State.Idle)
    //        {
    //            result = AvailablePersonel[i];
    //            break;
    //        }                
    //    }   
    //    return result;
    //}

    public void CustomAwake()
    {
        SaveRoomPosition();
        DefineRoomTypes();
    }

    private void SaveRoomPosition()
    {
        saveData.X = transform.position.x;
        saveData.Y = transform.position.y;
        saveData.Z = transform.position.z;
    }

    private void DefineRoomTypes()
    {
        foreach (Activity type in GetComponents<Activity>())
        {
            Activities.Add(type);
            type.RegisterInteractionObjects();
        }
    }


    //public void RegisterInteractionObjects()
    //{
    //    foreach (RoomType type in types)
    //    {
            
    //    }
    //}

}
