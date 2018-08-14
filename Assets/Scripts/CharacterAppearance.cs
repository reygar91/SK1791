using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAppearance {

    public CharacterBody Body = new CharacterBody();
    public CharacterOutfit Outfit = new CharacterOutfit();

}


public class CharacterBody
{
    public Sprite Head, Body;
    public CharacterShoulder shoulderRight, shoulderLeft; //may thow null ref exception, cuz there is no "new characterShoulder()"
    public CharacterLeg legRight, legLeft;
}

public class CharacterOutfit
{
    public Sprite Head, Body;
    public CharacterShoulder shoulderRight, shoulderLeft;
    public CharacterLeg legRight, legLeft;
}

public class CharacterShoulder
{
    public Sprite Upper, Lower;
}

public class CharacterLeg
{
    public Sprite Upper, Lower, Feet;
}
