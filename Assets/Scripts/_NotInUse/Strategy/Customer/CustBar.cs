//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CustBar : ICharBehaviour
//{
//    Customer cust;
//    MonoCharacter MC;
//    Bar room;
//    Vector3 targetVector;
//    int StatusID, targetIndex;
//    GameObject Seat;

//    public CustBar(MonoCharacter monoCharacter)
//    {
//        MC = monoCharacter;
//        cust = MC.character as Customer;        
//        room = MC.character.CurrentRoom as Bar;
//        //Debug.Log(MC.name + "=> CustBar called");
//        if (cust.behaviourData != null)
//        {
//            targetIndex = cust.behaviourData.OOI_Index;
//            Seat = room.FindSeat(targetIndex);
//            room.AvailableSeats.Remove(Seat);
//            StatusID = cust.behaviourData.StateID;
//            targetVector = new Vector3(cust.behaviourData.TargetX, MC.transform.position.y, cust.behaviourData.TargetZ);
//            cust.behaviourData = null;
//            //Debug.Log(MC.name + "=> CustBar with behaviourData called");
//        }
//        //Debug.Log("new CustBar => StatusID = " + StatusID);
//    }


//    public BehaviourData BehaviourData
//    {
//        get
//        {
//            BehaviourData data = new BehaviourData
//            {
//                OOI_Index = targetIndex,
//                StateID = StatusID,
//                TargetX = targetVector.x,
//                TargetZ = targetVector.z
//            };
//            return data;
//        }
//    }

//    public Vector3 RoomBehaviour()
//    {
//        switch (StatusID)
//        {
//            case 10: //find a seat and select it as new target
//                if (room.AvailableSeats.Count != 0 && !Seat)
//                {
//                    int RandomSeat = Random.Range(0, room.AvailableSeats.Count);
//                    Seat = room.AvailableSeats[RandomSeat];
//                    room.AvailableSeats.Remove(Seat);
//                    targetVector = Seat.transform.position;
//                    targetIndex = room.SeatIndex(Seat);
//                    StatusID = 11;
//                }
//                break;
//            case 11: //adding cust to custAtBar list + setting his orientation to correct side
//                    room.custAtBar.Add(MC);
//                    int startIndex = Seat.name.IndexOf("_");
//                    string orientation = Seat.name.Substring(startIndex + 1);
//                    switch (orientation)
//                    {
//                        case "Right":
//                            MC.transform.rotation = new Quaternion(0, -1, 0, 0);//facing from right to left
//                            break;
//                        case "Left":
//                            MC.transform.rotation = new Quaternion(0, 0, 0, 1);//facing from left to right
//                            break;
//                    }
//                    StatusID = 12;
//                break;
//            case 12://here we play animation for cust.AnimationTime duration
//                cust.AnimationWaitTime = 5f; //Debug.Log(StatusID);
//                cust.state = Character.State.Animation;             
//                StatusID = 13;
//                break;
//            case 13://after main action finished playing idle animation till the rest of patience
//                cust.AnimationWaitTime = cust.CountDown; //Debug.Log(StatusID);
//                //cust.Wait = true; 
//                break;
//            default: // move to center of the room
//                targetVector = new Vector3(MC.transform.position.x, MC.transform.position.y, room.MiddleOfTheRoom.transform.position.z);
//                StatusID = 10;
//                break;
//        }
//        return targetVector;
//    }

//    private void SwitchRoom()
//    {
//        if (Seat)
//        {
//            room.AvailableSeats.Add(Seat);
//            room.custAtBar.Remove(MC);
//        }        
//    }
//}
