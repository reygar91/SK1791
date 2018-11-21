//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public static class BehaviourMNGR {

//    //public static void ChangeRoom(MonoCharacter MC)
//    //{
//    //    Transform Doors, Middle, Char;
//    //    Doors = MC.character.CurrentRoom.Doors.transform;
//    //    Middle = MC.character.CurrentRoom.MiddleOfTheRoom.transform;
//    //    Char = MC.transform;
//    //    switch (MC.character.BehaviourStatusID)
//    //    {
//    //        case 1://align with doors
//    //            MC.character.Target.x = Doors.position.x;
//    //            MC.character.BehaviourStatusID = 2;
//    //            break;
//    //        case 2://move to the doors
//    //            MC.character.Target.z = Doors.position.z;
//    //            MC.character.BehaviourStatusID = 3;
//    //            break;
//    //        case 3:
//    //            Char.position = MC.character.TargetRoom.Doors.transform.position;
//    //            MC.character.Target = MC.character.TargetRoom.Doors.transform.position;
//    //            MC.character.Target.z -= 1.5f;
//    //            MC.character.BehaviourStatusID = 4;
//    //            break;
//    //        case 4:
//    //            //empty, so to do nothing untill he enters to a new room
//    //            break;
//    //        default://move to the center of the room
//    //            MC.character.Target.z = Middle.position.z;
//    //            MC.character.BehaviourStatusID = 1;
//    //            MC.ReleaseOOI(MC);
//    //            break;
//    //    }
//    //}

//    public static MonoCharacter.BehaviourDelegate GetBehaviour(MonoCharacter MC)
//    {
//        MonoCharacter.BehaviourDelegate behaviour;
//        string CharacterType = MC.character.GetType().ToString();
//        string RoomType = MC.character.CurrentRoom.GetType().ToString();
//        switch (CharacterType)
//        {
//            case "MainCharacter":
//                behaviour = GetMainCHaracterBehaviour(RoomType);
//                break;
//            case "Customer":
//                behaviour = GetCustomerBehaviour(RoomType);
//                break;
//            case "Personnel":
//                behaviour = GetPersonnelBehaviour(RoomType);
//                break;
//            default:
//                behaviour = null;
//                break;
//        }
//        return behaviour;
//    }

//    private static MonoCharacter.BehaviourDelegate GetMainCHaracterBehaviour(string RoomType)
//    {
//        MonoCharacter.BehaviourDelegate behaviour;
//        switch (RoomType)
//        {
//            case "Reception":
                
//                break;
//            case "Bar":
//                break;
//        }
//        return null;
//    }

//    private static MonoCharacter.BehaviourDelegate GetCustomerBehaviour(string RoomType)
//    {
//        MonoCharacter.BehaviourDelegate behaviour;
//        switch (RoomType)
//        {
//            case "Reception":
//                behaviour = BehaviourOfCustomer.AtReception;
//                break;
//            case "Bar":
//                behaviour = BehaviourOfCustomer.AtBar;
//                break;
//            default:
//                behaviour = null;
//                break;
//        }
//        return behaviour;
//    }

//    private static MonoCharacter.BehaviourDelegate GetPersonnelBehaviour(string RoomType)
//    {
//        MonoCharacter.BehaviourDelegate behaviour;
//        switch (RoomType)
//        {
//            case "Reception":
//                break;
//            case "Bar":
//                break;
//        }
//        return null;
//    }
//}
