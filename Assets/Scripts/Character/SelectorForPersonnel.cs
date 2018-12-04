using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorForPersonnel : MonoBehaviour {

    private myCharacterController CC;
    private Vector3 dist, StartPosition;
    private float posX, posY, countDown;
    private Collider2D colider;
    public static myCharacterController SelectedPersonnel; //or selected MC
    public bool IsSelectable, IsDragable;

    public static PanelOfPersonnel personnelPanel;
    private static myCharacterController previousBC;

    private void Awake()
    {
        CC = GetComponent<myCharacterController>();
        colider = GetComponent<Collider2D>();
    }

    private void OnMouseDown()
    {
        countDown = 0;
        SelectedPersonnel = CC;
        StartCoroutine("CountDown");
    }

    private IEnumerator CountDown()
    {
        bool roomMode = false;
        while (true)
        {
            if (roomMode)
            {
                Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
                transform.position = worldPos;
                CC.Stats.AnimationWaitTime += Time.deltaTime;
            }
            else if (countDown < .5f)
            {
                countDown += Time.deltaTime;
            }
            else
            {
                dist = Camera.main.WorldToScreenPoint(transform.position);
                posX = Input.mousePosition.x - dist.x;
                posY = Input.mousePosition.y - dist.y;

                SelectedPersonnel = CC;
                colider.enabled = false;
                StartPosition = CC.transform.position;
                CC.Stats.state = myCharacterStats.State.Animation;
                CC.Stats.AnimationWaitTime = 2 * Time.deltaTime;
                foreach (Room item in BuildMNGR.Instance.roomsList)
                {
                    item.boxCollider.gameObject.layer = 0;
                }
                roomMode = true;
            }
            yield return null;
        }
    }

    private void OnMouseUp()
    {
        if (countDown >= .5f)
        {
            transform.position = StartPosition;

            colider.enabled = true;

            foreach (Room item in BuildMNGR.Instance.roomsList)
            {
                item.boxCollider.gameObject.layer = 2;
            }

            CC.Focus.Activity = Room.selectedRoom.Activities[0];//later should add Activity selector
            CC.NextBehaviour = CC.Focus.Activity.PesrBehaviour;
            CC.behaviour = MyBehavioursCollection.Instance.ChangeRoom;
            CC.ActionID = 0;
        }
        else
            PersonnelClicked();

        SelectedPersonnel = null;

        StopCoroutine("CountDown");
    }

    private void PersonnelClicked()
    {
        SelectedPersonnel = CC;
        personnelPanel.UpdateSelectedPersonnel();
        GameObject Panel = personnelPanel.gameObject;
        if (!Panel.activeSelf)
        {
            Panel.SetActive(true);
        }
        else if (CC == previousBC)
            Panel.SetActive(false);
        //customerPanel.ToggleServeBTN();
        previousBC = CC;
    }
}
