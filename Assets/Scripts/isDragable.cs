using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isDragable : MonoBehaviour {

    Vector3 dist;
    float posX;
    float posY;
    Collider colider;

    private void Awake()
    {
        colider = GetComponent<Collider>();
    }

    private void OnMouseDown()
    {
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;

        colider.enabled = false;        

        StartCoroutine("CountDown");
    }

    //private void OnMouseDrag()
    //{
    //    Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
    //    Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
    //    transform.position = worldPos;
    //}

    private void OnMouseUp()
    {
        int yCoor = 4 * Mathf.FloorToInt((transform.position.y) / 4);
        transform.position = new Vector3(transform.position.x,yCoor, transform.position.z);

        colider.enabled = true;

        foreach (Room item in Room.roomsList)
        {
            item.boxCollider.gameObject.layer = 2;
        }

        StopCoroutine("CountDown");
    }

    private IEnumerator CountDown()
    {
        float countDown = 0;
        bool roomMode = false;
        while (true)
        {
            //Debug.Log(countDown); 
            if (roomMode)
            {
                Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
                transform.position = worldPos;                
            } else if (countDown < .5f)
            {
                countDown += Time.deltaTime;
            } else
            {
                foreach (Room item in Room.roomsList)
                {
                    item.boxCollider.gameObject.layer = 0;
                }
                roomMode = true;
            }
            yield return null;
        }
    }

    }
