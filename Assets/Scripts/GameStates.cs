using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : GameFSM
{
    public PauseState()
    {

    }

    ~PauseState()
    {
        
    }
}

public class DialogState : GameFSM
{
    public DialogState()
    {
        jumpButton = new DialogNext();
    }

    ~DialogState()
    {
        jumpButton = new Pause();
    }
}

public class RegularState : GameFSM
{
    public RegularState()
    {
        jumpButton = new Pause();
    }

    ~RegularState()
    {
    }
}

public class RoomMode : GameFSM
{
    public RoomMode()
    {
        jumpButton = new Pause();
        jumpButton.Execute();//I assume this should put game on pause
        foreach (Room item in Room.roomsList)
        {
            item.boxCollider.gameObject.layer = 0;
        }
    }

    ~RoomMode()
    {
        foreach (Room item in Room.roomsList)
        {
            item.boxCollider.gameObject.layer = 2;
        }
        jumpButton.Execute();//I assume this should resume game from pause
    }
}


