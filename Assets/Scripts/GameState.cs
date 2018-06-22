using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState  {

    public enum GameFSM
    {
        Regular, RoomSelect, Pause, CutScene, Dialog
    }
    public static GameFSM gameFSM = GameFSM.Regular;

    protected void PauseUnpause()
    {
        foreach (Character item in GameController.CharList)
        {
            if (item.gameObject.activeSelf)
            {
                item.AnimatorComponent.enabled = !item.AnimatorComponent.enabled;
            }
        }
    }

    public void ChangeGameState()
    {
        GameState state;
        switch (gameFSM)
        {
            case GameFSM.CutScene:
                break;
            case GameFSM.Dialog:

                break;
            case GameFSM.Pause:
                state = new GamePause();
                break;
            case GameFSM.Regular:

                break;
            case GameFSM.RoomSelect:

                break;
        }
    }

}
