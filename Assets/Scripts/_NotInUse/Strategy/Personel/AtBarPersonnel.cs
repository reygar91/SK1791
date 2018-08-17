//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AtBarPersonnel : ICharBehaviour
//{
//    Personnel pers;
//    MonoCharacter MC, custMC;
//    Bar room;
//    Vector3 targetVector;
//    int StatusID;
//    //Customer cust;

//    public AtBarPersonnel(MonoCharacter monoCharacter)
//    {
//        MC = monoCharacter;
//        pers = MC.character as Personnel;
//        room = MC.character.CurrentRoom as Bar;
//        if (pers.behaviourData != null)
//        {
//            custMC = CharacterMNGR.Instance.ActiveMC[pers.behaviourData.OOI_Index];
//            //targetIndex = pers.behaviourData.OOI_Index;
//            StatusID = pers.behaviourData.StateID;
//            targetVector = new Vector3(pers.behaviourData.TargetX, MC.transform.position.y, pers.behaviourData.TargetZ);
//            pers.behaviourData = null;
//            //Debug.Log(MC.name + "=> CustBar with behaviourData called");
//        }
//    }

//    public BehaviourData BehaviourData
//    {
//        get
//        {
//            BehaviourData data = new BehaviourData
//            {
//                OOI_Index = CharacterMNGR.Instance.ActiveMC.IndexOf(custMC),
//                StateID = StatusID,
//                TargetX = targetVector.x,
//                TargetZ = targetVector.z
//            };
//            return data;
//        }
//    }


//    public Vector3 RoomBehaviour()
//    {
//        Vector3 persPos = MC.transform.position;
//        Vector3 custPos;
//        Vector3 Middle = room.MiddleOfTheRoom.transform.position;
//        switch (StatusID)
//        {
//            case 11:// cheking if there are any custs to serve or reached previous random target
//                if (room.custAtBar.Count != 0)
//                {
//                    StatusID = 13;
//                }
//                else
//                {
//                    StatusID = 12;
//                }
//                break;
//            case 12://set random target to move along the room and change statusID so it was done only once 
//                float delta = room.Doors.transform.position.x - Middle.x;//distance from center of the room to the doors, considering doors located right to the center. doors.x > center.x
//                float minX = Middle.x - delta;
//                float maxX = Middle.x + delta;
//                targetVector = new Vector3(Random.Range(minX, maxX), persPos.y, Middle.z);
//                StatusID = 11;
//                break;
//            case 13://setting target to guest X position and changing StatusID, so it is done only once
//                int index = Random.Range(0, room.custAtBar.Count);
//                custMC = room.custAtBar[index];
//                custPos = custMC.transform.position;
//                targetVector = new Vector3(custPos.x, persPos.y, Middle.z);
//                room.custAtBar.Remove(custMC);
//                StatusID = 14;
//                break;
//            case 14://setting target to guest X,Z position & changing StatusID
//                custPos = custMC.transform.position;
//                targetVector = new Vector3(custPos.x, persPos.y, custPos.z);
//                StatusID = 15;
//                break;
//            case 15:
//                GoldMNGR.Instance.AddGold(150);
//                StatusID = 10;
//                break;
//            default://when just entered the room or just served cust
//                targetVector = new Vector3(persPos.x, persPos.y, Middle.z);
//                StatusID = 11;
//                break;
//        }
//        return targetVector;
//    }
//}
