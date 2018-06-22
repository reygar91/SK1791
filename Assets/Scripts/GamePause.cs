using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause: GameState
{

    public GamePause()
    {
        PauseUnpause();
    }

    ~GamePause()
    {
        PauseUnpause();
    }

    
}
