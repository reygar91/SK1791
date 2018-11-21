using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorForPersonnel : MonoBehaviour {

    private BehaviourController BC;
    private Vector3 dist, StartPosition;
    private float posX, posY, countDown;
    private Collider colider;
    public static BehaviourController SelectedPersonnel; //or selected MC
    public bool IsSelectable, IsDragable;

    public static PanelOfPersonnel personnelPanel;
    private static BehaviourController previousBC;

    private void Awake()
    {
        BC = GetComponent<BehaviourController>();
        colider = GetComponent<Collider>();
    }

    private void OnMouseDown()
    {
        countDown = 0;
        SelectedPersonnel = BC;
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
                BC.character.AnimationWaitTime += Time.deltaTime;
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

                SelectedPersonnel = BC;
                colider.enabled = false;
                StartPosition = BC.transform.position;
                BC.character.state = Character.State.Animation;
                BC.character.AnimationWaitTime = 2 * Time.deltaTime;
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
            //BC.Focus.TeleportDestination = Room.selectedRoom.Doors.transform;
            //Debug.Log("Character Teleportation Focus: " + BC.Focus.TeleportDestination.position);

            transform.position = StartPosition;

            colider.enabled = true;

            foreach (Room item in BuildMNGR.Instance.roomsList)
            {
                item.boxCollider.gameObject.layer = 2;
            }
        }
        else
            PersonnelClicked();

        SelectedPersonnel = null;

        StopCoroutine("CountDown");
    }

    private void PersonnelClicked()
    {
        SelectedPersonnel = BC;
        personnelPanel.UpdateSelectedPersonnel();
        GameObject Panel = personnelPanel.gameObject;
        if (!Panel.activeSelf)
        {
            Panel.SetActive(true);
        }
        else if (BC == previousBC)
            Panel.SetActive(false);
        //customerPanel.ToggleServeBTN();
        previousBC = BC;
    }
}
