using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public static List<Room> roomsList = new List<Room>();
    public BoxCollider boxCollider;
    public SpriteRenderer spriteRenderer;
    public GameObject Doors, MiddleOfTheRoom;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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
}
