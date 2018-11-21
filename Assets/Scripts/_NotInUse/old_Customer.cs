//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;



//public class Customer : Character {

//    //public delegate void Serve();
//    //public Serve GetServedDelegate;
//    public bool HasBeenServed;
//    public string DesiredAction = "RandomAction";

//    public Customer()
//    {
//        TargetRoom = Reception.Instance;
//        CountDown = 25;
//        WalkSpeed = 3;
//        SetCharacteristics();
//        SetAppearance();
//    }
    
//    protected override void SetCharacteristics()
//    {
//        int RandomNumber = Random.Range(0, 1000);
//        Name = "Customer_" + RandomNumber;
//    }

//    public override void ApplyCharacteristics()
//    {
//        MC.name = Name;
//    }

//    protected override void SetAppearance()
//    {
//        Appearance = new CharacterAppearance();
//        int Index = Random.Range(0, StreamingAssetsMNGR.Instance.Hat.Count);
//        Appearance.Outfit.Head = StreamingAssetsMNGR.Instance.Hat[Index];
//    }

//    public override void ApplyAppearance()
//    {
//        MC.Renderer.Outfit.Head.sprite = Appearance.Outfit.Head;
//        MC.Renderer.Outfit.Head.color = Random.ColorHSV();        
//    }

//    public override void SetFocus()
//    {
//        string RoomType = CurrentRoom.GetType().ToString();
//        switch (RoomType)
//        {
//            case "Reception":
//                Focus.Object = Reception.Instance.WaitInLinePoints[Focus.Index];
//                Reception.Instance.OccupiedSpot[Focus.Index] = true;
//                break;
//            case "Bar":
//                Bar room = CurrentRoom as Bar;
//                Focus.Object = room.FindSeat(Focus.Index);
//                room.AvailableSeats.Remove(Focus.Object);
//                break;
//        }
//    }

//    public void GetServed()
//    {
//        CustomerPanel.Instance.gameObject.SetActive(false);
//        Personnel personnel = CurrentRoom.GetPersonel().character as Personnel;
//        //MonoCharacter PersMC = CurrentRoom.GetPersonel();
//        personnel.Focus.MC = MC;
//        personnel.SetCustomersDesiredAction(DesiredAction);
//        personnel.RoomBehaviour = personnel.ReachFocusMC;        
//        personnel.state = State.Move;//to come out from Idle state

//        if (CurrentRoom.GetPersonel() == null)
//            CustomerPanel.Instance.ServeBTN.interactable = false;
//    }


//    protected override void AtReception()
//    {
//        ResetFocus = LeaveReception;
//        Reception room = Reception.Instance;
//        if (CountDown < 0)
//        {
//            switch (BehaviourStatusID)
//            {
//                case 10:
//                    Target = room.EntrancePoint.transform.position;
//                    BehaviourStatusID = 11;
//                    break;
//                case 11:
//                    Target = room.SpawnPoint.transform.position;
//                    BehaviourStatusID = 12;
//                    break;
//                case 12:
//                    MC.gameObject.SetActive(false);
//                    break;
//                default:
//                    Target.z = room.EntrancePoint.transform.position.z;
//                    BehaviourStatusID = 10;
//                    LeaveReception();
//                    break;
//            }
//        }
//        else if (!Focus.Object)
//        {
//            for (int i = 0; i < 5; i++)
//            {
//                if (room.OccupiedSpot[i] == false)
//                {
//                    Focus.Object = room.WaitInLinePoints[i];
//                    Target = Focus.Object.transform.position;
//                    room.OccupiedSpot[i] = true;
//                    Focus.Index = i;
//                    i = 5;
//                }
//            }            
//        }
//        else if (Focus.Index != 0 && room.OccupiedSpot[Focus.Index - 1] == false)
//        {
//            Focus.Object = room.WaitInLinePoints[Focus.Index - 1];
//            Target = Focus.Object.transform.position;
//            room.OccupiedSpot[Focus.Index - 1] = true;
//            room.OccupiedSpot[Focus.Index] = false;
//            Focus.Index--;
//        }
//    }

//    private void LeaveReception()
//    {
//        Reception room = Reception.Instance;
//        if (Focus.Object)
//        {
//            room.OccupiedSpot[Focus.Index] = false;
//            Focus.Object = null;
//        }
//    }

//    protected override void AtBar()
//    {
//        ResetFocus = LeaveBar;
//        Bar room = CurrentRoom as Bar;
//        switch (BehaviourStatusID)
//        {
//            case 10: //find a seat and select it as new target
//                if (room.AvailableSeats.Count != 0 && !Focus.Object)
//                {
//                    int RandomSeat = Random.Range(0, room.AvailableSeats.Count);
//                    Focus.Object = room.AvailableSeats[RandomSeat];
//                    room.AvailableSeats.Remove(Focus.Object);
//                    Target = Focus.Object.transform.position;
//                    Focus.Index = room.SeatIndex(Focus.Object);
//                    BehaviourStatusID = 11;
//                }
//                break;
//            case 11: //adding cust to custAtBar list + setting his orientation to correct side
//                room.custAtBar.Add(MC); 
//                int startIndex = Focus.Object.name.IndexOf("_");
//                string orientation = Focus.Object.name.Substring(startIndex + 1);
//                switch (orientation)
//                {
//                    case "Right":
//                        MC.transform.rotation = new Quaternion(0, -1, 0, 0);//facing from right to left
//                        break;
//                    case "Left":
//                        MC.transform.rotation = new Quaternion(0, 0, 0, 1);//facing from left to right
//                        break;
//                }
//                BehaviourStatusID = 12;
//                break;
//            case 12://here we play animation for cust.AnimationTime duration
//                AnimationWaitTime = 5f;
//                state = State.Animation;
//                BehaviourStatusID = 13;
//                break;
//            case 13://after main action finished playing idle animation till the rest of patience
//                AnimationWaitTime = CountDown;
//                break;
//            default: // move to center of the room
//                Target.z = room.MiddleOfTheRoom.transform.position.z;
//                BehaviourStatusID = 10;                
//                break;
//        }        
//    }

//    private void LeaveBar()
//    {
//        Bar room = CurrentRoom as Bar;
//        if (Focus.Object) //which is seat
//        {
//            room.AvailableSeats.Add(Focus.Object);
//            room.custAtBar.Remove(MC);
//            Focus.Object = null;
//        }
//    }

//}
