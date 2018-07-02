//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PauseState : GameFSM
//{
//    public PauseState()
//    {

//    }

//}

//public class DialogState : GameFSM
//{
//    public DialogState()
//    {
//        //jumpButton = new DialogNext();
//    }

//}

//public class RegularState : GameFSM
//{
//    public RegularState()
//    {
//        jumpButton = new Pause();
//    }

//}

//public class RoomMode : GameFSM
//{
//    public RoomMode()
//    {
//        jumpButton = new Pause();
//        jumpButton.Execute();//I assume this should put game on pause
//        foreach (Room item in Room.roomsList)
//        {
//            item.boxCollider.gameObject.layer = 0;
//        }
//    }
//}


