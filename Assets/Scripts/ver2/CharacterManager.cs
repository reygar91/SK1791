using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager> {

    protected CharacterManager() { }

    public List<MonoCharacter> charList = new List<MonoCharacter>();

    private void Update()
    {
        foreach (MonoCharacter character in charList)
        {
            character.MoveTo();
        }
    }


}
