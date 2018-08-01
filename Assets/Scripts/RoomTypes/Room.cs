using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    //public static List<Room> roomsList = new List<Room>();
    public BoxCollider boxCollider;
    public SpriteRenderer spriteRenderer;
    public GameObject Doors, MiddleOfTheRoom;
    public RoomSaveData saveData = new RoomSaveData();

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
            CharDragSelect.DraggedMC.TargetRoom = this; //Debug.Log(this);
        }
    }

    private void OnMouseExit()
    {
        spriteRenderer.gameObject.SetActive(false);
        if (CharDragSelect.DraggedMC != null)
        {
            CharDragSelect.DraggedMC.TargetRoom = CharDragSelect.DraggedMC.CurrentRoom;
        }
    }

    public void SaveRoomPosition()
    {
        saveData.X = transform.position.x;
        saveData.Y = transform.position.y;
        saveData.Z = transform.position.z;
    }

}
