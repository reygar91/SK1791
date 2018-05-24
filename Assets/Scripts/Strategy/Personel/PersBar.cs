using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersBar : IPersBehaviour
{
    Personel pers;
    Bar bar;
    //Animator animator;
    Vector3 targetVector;
    Customer cust;
    int StatusID;

    public PersBar(Personel personel, Bar RoomType)
    {
        pers = personel;
        bar = RoomType;
        StatusID = 0;
    }

    public Vector3 LeaveRoom()
    {
        throw new System.NotImplementedException();
    }

    public Vector3 RoomBehaviour()
    {
        //Vector3 targetVector;
        switch (StatusID)
        {
            case 1://set random target to move along the room and change statusID so it was done only once 
                float delta = bar.Doors.transform.position.x - bar.MiddleOfTheRoom.transform.position.x;//distance from center of the room to the doors, considering doors located right to the center. doors.x > center.x
                float minX = bar.MiddleOfTheRoom.transform.position.x - delta;
                float maxX = bar.MiddleOfTheRoom.transform.position.x + delta;
                targetVector = new Vector3(Random.Range(minX, maxX),
                    pers.transform.position.y,
                    bar.MiddleOfTheRoom.transform.position.z);
                StatusID = 2;
                break;
            case 2:// cheking if there are any custs to serve or reached previous random target
                if (bar.custAtBar.Count != 0)
                {
                    StatusID = 3;
                }
                else if (pers.hasReachedTarget(targetVector))
                {
                    StatusID = 1;
                }
                break;
            case 3://setting target to guest X position and changing StatusID, so it is done only once
                int index = Random.Range(0, bar.custAtBar.Count);
                cust = bar.custAtBar[index];
                targetVector = new Vector3(cust.gameObject.transform.position.x,
                    pers.transform.position.y,
                    bar.MiddleOfTheRoom.transform.position.z);
                bar.custAtBar.Remove(cust);
                StatusID = 4;
                break;
            case 4://setting target to guest X,Z position & changing StatusID
                if (pers.hasReachedTarget(targetVector))
                {
                    targetVector = new Vector3(cust.gameObject.transform.position.x,
                        pers.transform.position.y,
                        cust.gameObject.transform.position.z);
                    StatusID = 5;
                }
                break;
            case 5:
                if (pers.hasReachedTarget(targetVector))
                {
                    GoldManager.AddGold(150);
                    StatusID = 0;
                }
                break;
            default://when just entered the room or just served cust
                targetVector = new Vector3(pers.transform.position.x, 
                    pers.transform.position.y, 
                    bar.MiddleOfTheRoom.transform.position.z);
                if (pers.hasReachedTarget(targetVector))
                {
                    StatusID = 1;
                }
                break;
        }
        return targetVector;
    }

    public void SwitchRoom()
    {
        throw new System.NotImplementedException();
    }
}
