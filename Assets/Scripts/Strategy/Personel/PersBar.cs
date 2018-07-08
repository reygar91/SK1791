//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PersBar : IPersBehaviour
//{
//    Personel pers;
//    Bar bar;
//    //Animator animator;
//    Vector3 targetVector;
//    Customer cust;
//    int StatusID;

//    public PersBar(Personel personel, Bar RoomType)
//    {
//        pers = personel;
//        bar = RoomType;
//        StatusID = 0;
//    }

//    public Vector3 LeaveRoom()
//    {
//        throw new System.NotImplementedException();
//    }

//    public Vector3 RoomBehaviour()
//    {
//        Vector3 persPos = pers.transform.position;
//        Vector3 custPos;
//        Vector3 Middle = bar.MiddleOfTheRoom.transform.position;
//        switch (StatusID)
//        {
//            case 1:// cheking if there are any custs to serve or reached previous random target
//                if (bar.custAtBar.Count != 0)
//                {
//                    StatusID = 3;
//                }
//                else
//                {
//                    StatusID = 2;
//                }
//                break;
//            case 2://set random target to move along the room and change statusID so it was done only once 
//                float delta = bar.Doors.transform.position.x - Middle.x;//distance from center of the room to the doors, considering doors located right to the center. doors.x > center.x
//                float minX = Middle.x - delta;
//                float maxX = Middle.x + delta;
//                targetVector = new Vector3(Random.Range(minX, maxX), persPos.y, Middle.z);
//                StatusID = 1;                
//                break;
//            case 3://setting target to guest X position and changing StatusID, so it is done only once
//                int index = Random.Range(0, bar.custAtBar.Count);
//                cust = bar.custAtBar[index];
//                custPos = cust.gameObject.transform.position;
//                targetVector = new Vector3(custPos.x, persPos.y, Middle.z);
//                bar.custAtBar.Remove(cust);
//                StatusID = 4;
//                break;
//            case 4://setting target to guest X,Z position & changing StatusID
//                custPos = cust.gameObject.transform.position;
//                targetVector = new Vector3(custPos.x, persPos.y, custPos.z);
//                StatusID = 5;
//                break;
//            case 5:
//                GoldManager.Instance.AddGold(150);
//                StatusID = 0;
//                break;
//            default://when just entered the room or just served cust
//                targetVector = new Vector3(persPos.x, persPos.y, Middle.z);
//                StatusID = 1;
//                break;
//        }
//        return targetVector;
//    }

//    public void SwitchRoom()
//    {
//        throw new System.NotImplementedException();
//    }
//}
