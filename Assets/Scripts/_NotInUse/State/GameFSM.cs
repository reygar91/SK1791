//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GameFSM  {

//    public enum State
//    {
//        Regular, RoomSelect, Pause, CutScene, Dialog
//    }
//    protected State gameFSM = State.Regular;

//    protected CommandPattern cancelButton, jumpButton;


//    public void ChangeGameState()
//    {
//        switch (gameFSM)
//        {
//            case State.CutScene:
//                break;
//            case State.Dialog:

//                break;
//            case State.Pause:
//                //jumpButton = new Pause();
//                break;
//            case State.Regular:

//                break;
//            case State.RoomSelect:

//                break;
//        }
//    }

//    public void CancelButton()
//    {
//        cancelButton.Execute();
//    }

//    public void JumpButton()
//    {
//        jumpButton.Execute();
//    }


//}
