using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    //public static List<Room> roomsList = new List<Room>();
    public BoxCollider boxCollider;
    public SpriteRenderer spriteRenderer;
    public GameObject Doors, MiddleOfTheRoom;
    public RoomSaveData saveData = new RoomSaveData();

    public MonoCharacter[] AvailablePersonel;
    public List<MonoCharacter> AvailableCustomers;

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

        if (CharDragSelect.DraggedMC != null)
        {
            CharDragSelect.DraggedMC.character.TargetRoom = this; //Debug.Log(this);
        }
    }

    private void OnMouseExit()
    {
        spriteRenderer.gameObject.SetActive(false);
        if (CharDragSelect.DraggedMC != null)
        {
            CharDragSelect.DraggedMC.character.TargetRoom = CharDragSelect.DraggedMC.character.CurrentRoom;
        }
    }

    public void SaveRoomPosition()
    {
        saveData.X = transform.position.x;
        saveData.Y = transform.position.y;
        saveData.Z = transform.position.z;
    }

    public bool RegisterPersonel(MonoCharacter monoCharacter)
    {
        bool result = false;
        for (int i = 0; i < AvailablePersonel.Length; i++)
        {
            if (AvailablePersonel[i] == null)
            {
                AvailablePersonel[i] = monoCharacter;
                result = true;
                break;
            }                
        }
        return result;
    }

    public MonoCharacter GetPersonel()
    {
        MonoCharacter result = null;
        for (int i=0; i< AvailablePersonel.Length; i++)
        {
            if (AvailablePersonel[i].character.state == Character.State.Idle)
            {
                result = AvailablePersonel[i];
                break;
            }                
        }   
        return result;
    }

}
