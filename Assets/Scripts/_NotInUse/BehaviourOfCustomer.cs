//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BehaviourOfCustomer {

//    public static void AtReception(MonoCharacter MC)
//    {
//        Reception room = Reception.Instance;
//        if (MC.character.CountDown < 0)
//        {
//            switch (MC.character.BehaviourStatusID)
//            {
//                case 10:
//                    MC.character.Target = room.EntrancePoint.transform.position;
//                    MC.character.BehaviourStatusID = 11;
//                    break;
//                case 11:
//                    MC.character.Target = room.SpawnPoint.transform.position;
//                    MC.character.BehaviourStatusID = 12;
//                    break;
//                case 12:
//                    MC.gameObject.SetActive(false);
//                    break;
//                default:
//                    MC.character.Target = new Vector3(MC.transform.position.x, MC.transform.position.y, room.EntrancePoint.transform.position.z);
//                    MC.character.BehaviourStatusID = 10;
//                    LeaveReception(MC);
//                    break;
//            }
//        }
//        else if (!MC.character.ObjectOfInterest)
//        {
//            for (int i = 0; i < 5; i++)
//            {
//                if (room.OccupiedSpot[i] == false)
//                {
//                    MC.character.ObjectOfInterest = room.WaitInLinePoints[i];
//                    MC.character.Target = MC.character.ObjectOfInterest.transform.position;
//                    room.OccupiedSpot[i] = true;
//                    MC.character.OOI_Index = i;
//                    i = 5;
//                }
//            }
//            MC.ReleaseOOI = LeaveReception;
//        }
//        else if (MC.character.OOI_Index != 0 && room.OccupiedSpot[MC.character.OOI_Index - 1] == false)
//        {
//            MC.character.ObjectOfInterest = room.WaitInLinePoints[MC.character.OOI_Index - 1];
//            MC.character.Target = MC.character.ObjectOfInterest.transform.position;
//            room.OccupiedSpot[MC.character.OOI_Index - 1] = true;
//            room.OccupiedSpot[MC.character.OOI_Index] = false;
//            MC.character.OOI_Index--;
//        }
//    }

//    private static void LeaveReception(MonoCharacter MC)
//    {
//        Reception room = Reception.Instance;
//        if (MC.character.ObjectOfInterest)
//        {
//            room.OccupiedSpot[MC.character.OOI_Index] = false; //Debug.Log(cust.name + "_triggers");
//            MC.character.ObjectOfInterest = null;
//        }
//    }


//    public static void AtBar(MonoCharacter MC)
//    {
//        //GameObject Seat = MC.character.ObjectOfInterest;
//        Bar room = MC.character.CurrentRoom as Bar;
//        Customer cust = MC.character as Customer;
//        switch (MC.character.BehaviourStatusID)
//        {
//            case 10: //find a seat and select it as new target
//                if (room.AvailableSeats.Count != 0 && !MC.character.ObjectOfInterest)
//                {
//                    int RandomSeat = Random.Range(0, room.AvailableSeats.Count);
//                    MC.character.ObjectOfInterest = room.AvailableSeats[RandomSeat];
//                    room.AvailableSeats.Remove(MC.character.ObjectOfInterest);
//                    MC.character.Target = MC.character.ObjectOfInterest.transform.position;
//                    MC.character.OOI_Index = room.SeatIndex(MC.character.ObjectOfInterest);
//                    MC.character.BehaviourStatusID = 11;
//                }
//                break;
//            case 11: //adding cust to custAtBar list + setting his orientation to correct side
//                room.custAtBar.Add(MC); //Debug.Log(MC.character.ObjectOfInterest);
//                int startIndex = MC.character.ObjectOfInterest.name.IndexOf("_");
//                string orientation = MC.character.ObjectOfInterest.name.Substring(startIndex + 1);
//                switch (orientation)
//                {
//                    case "Right":
//                        MC.transform.rotation = new Quaternion(0, -1, 0, 0);//facing from right to left
//                        break;
//                    case "Left":
//                        MC.transform.rotation = new Quaternion(0, 0, 0, 1);//facing from left to right
//                        break;
//                }
//                MC.character.BehaviourStatusID = 12;
//                break;
//            case 12://here we play animation for cust.AnimationTime duration
//                cust.AnimationWaitTime = 5f; //Debug.Log(StatusID);
//                cust.state = Character.State.Animation;
//                MC.character.BehaviourStatusID = 13;
//                break;
//            case 13://after main action finished playing idle animation till the rest of patience
//                cust.AnimationWaitTime = cust.CountDown; //Debug.Log(StatusID);
//                //cust.Wait = true; 
//                break;
//            default: // move to center of the room
//                MC.character.Target = new Vector3(MC.transform.position.x, MC.transform.position.y, room.MiddleOfTheRoom.transform.position.z);
//                MC.character.BehaviourStatusID = 10;
//                MC.ReleaseOOI = LeaveBar;
//                break;
//        }
//    }

//    private static void LeaveBar(MonoCharacter MC)
//    {
//        GameObject Seat = MC.character.ObjectOfInterest;
//        Bar room = MC.character.CurrentRoom as Bar;
//        if (Seat)
//        {
//            room.AvailableSeats.Add(Seat);
//            room.custAtBar.Remove(MC);
//            MC.character.ObjectOfInterest = null;
//        }
//    }

//}
