//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public class CharDragSelect : MonoBehaviour
//{

//    private BehaviourController BC;
//    private Vector3 dist, StartPosition;
//    private float posX, posY, countDown;
//    private Collider colider;
//    public static BehaviourController DraggedBC; //or selected MC
//    public bool IsSelectable, IsDragable;

//    private static GameObject PersPanel;
//    private static PanelOfCustomer customerPanel;
//    private static BehaviourController previousBC;

//    private void Awake()
//    {
//        BC = GetComponent<BehaviourController>();
//        colider = GetComponent<Collider>();
//        //customerPanel = CustomerPanel.Instance;
//        //PersPanel = UIMNGR.Instance.PersPanel;
//    }

//    private void OnMouseDown()
//    {
//        countDown = 0;
//        DraggedBC = BC;
//        StartCoroutine("CountDown");
//    }

//    private IEnumerator CountDown()
//    {
//        bool roomMode = false;
//        while (true)
//        {
//            if (roomMode)
//            {
//                Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
//                Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
//                transform.position = worldPos;
//                BC.character.AnimationWaitTime += Time.deltaTime;
//            }
//            else if (countDown < .5f)
//            {
//                countDown += Time.deltaTime;
//                //Input.mousePosition.Set();
//            }
//            else
//            {
//                dist = Camera.main.WorldToScreenPoint(transform.position);
//                posX = Input.mousePosition.x - dist.x;
//                posY = Input.mousePosition.y - dist.y;

//                DraggedBC = BC;
//                colider.enabled = false;
//                StartPosition = BC.transform.position;
//                BC.character.state = Character.State.Animation;
//                BC.character.AnimationWaitTime = 2 * Time.deltaTime;
//                foreach (Room item in BuildMNGR.Instance.roomsList)
//                {
//                    item.boxCollider.gameObject.layer = 0;
//                }
//                roomMode = true;
//            }
//            yield return null;
//        }
//    }

//    private void OnMouseUp()
//    {
//        if (countDown >= .5f)
//        {
//            transform.position = StartPosition;

//            colider.enabled = true;

//            foreach (Room item in BuildMNGR.Instance.roomsList)
//            {
//                item.boxCollider.gameObject.layer = 2;
//            }
//        }
//        else
//            CustomerClicked();

//        DraggedBC = null;

//        StopCoroutine("CountDown");
//    }

//    private void CustomerClicked()
//    {
//        string charType = BC.character.GetType().ToString();
//        switch (charType)
//        {
//            case "Customer":
//                GameObject Panel = customerPanel.gameObject;
//                if (!Panel.activeSelf)
//                {
//                    Panel.SetActive(true);
//                }
//                else if (BC == previousBC)
//                    Panel.SetActive(false);
//                //customerPanel.ToggleServeBTN();                
//                break;
//        }
//        previousBC = BC;
//    }



//}
