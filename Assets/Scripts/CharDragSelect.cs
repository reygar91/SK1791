using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharDragSelect : MonoBehaviour {

    private MonoCharacter MC;
    private Vector3 dist, StartPosition;
    private float posX, posY, countDown;
    private Collider colider;
    public static MonoCharacter DraggedMC; //or selected MC
    public bool IsSelectable, IsDragable;

    private void Awake()
    {
        MC = GetComponent<MonoCharacter>();
        colider = GetComponent<Collider>();
    }

    private void OnMouseDown()
    {       
        countDown = 0;
        DraggedMC = MC;
        StartCoroutine("CountDown");
    }

    private void OnMouseUp()
    {
        //DraggedMC = null;
        //int yCoor = 4 * Mathf.FloorToInt((transform.position.y) / 4);
        //transform.position = new Vector3(transform.position.x, yCoor, transform.position.z);
        if (countDown >= .5f)
        {
            transform.position = StartPosition; //Debug.Log("!!!!" + MC.name +" "+ MC.CurrentRoom + "=>" + MC.TargetRoom);

            colider.enabled = true;

            foreach (Room item in BuildMNGR.Instance.roomsList)
            {
                item.boxCollider.gameObject.layer = 2;
            }
        } else
            InputMNGR.Instance.CustomerClicked(DraggedMC);

        DraggedMC = null;

        StopCoroutine("CountDown");
    }

    private IEnumerator CountDown()
    {
        bool roomMode = false;
        while (true)
        {
            //Debug.Log(countDown); 
            if (roomMode)
            {
                Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
                transform.position = worldPos;
                MC.character.AnimationWaitTime += Time.deltaTime;
            }
            else if (countDown < .5f)
            {
                countDown += Time.deltaTime;
                //Input.mousePosition.Set();
            }
            else
            {
                dist = Camera.main.WorldToScreenPoint(transform.position);
                posX = Input.mousePosition.x - dist.x;
                posY = Input.mousePosition.y - dist.y;                

                DraggedMC = MC;
                colider.enabled = false;
                MC.character.behaviourData = MC.character.Behaviour.BehaviourData;//to reload correct behaviour after enabling collider
                StartPosition = MC.transform.position;
                MC.character.state = Character.State.Animation;
                MC.character.AnimationWaitTime = 2 * Time.deltaTime;
                foreach (Room item in BuildMNGR.Instance.roomsList)
                {
                    item.boxCollider.gameObject.layer = 0;
                }
                roomMode = true;
            }
            yield return null;
        }
    }

}
