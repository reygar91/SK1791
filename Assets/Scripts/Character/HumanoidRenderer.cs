using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidRenderer : MonoBehaviour {

    public CharacterBodyRenderer Body;
    public CharacterOutfitRenderer Outfit;
}
[System.Serializable]
public class CharacterBodyRenderer
{
    public SpriteRenderer Head, Body;
    public CharacterShoulderRenderer shoulderRight, shoulderLeft;
    public CharacterLegRenderer legRight, legLeft;    
}
[System.Serializable]
public class CharacterOutfitRenderer
{
    public SpriteRenderer Head, Body;
    public CharacterShoulderRenderer shoulderRight, shoulderLeft;
    public CharacterLegRenderer legRight, legLeft;
}
[System.Serializable]
public class CharacterShoulderRenderer
{
    public SpriteRenderer Upper, Lower;
}
[System.Serializable]
public class CharacterLegRenderer
{
    public SpriteRenderer Upper, Lower, Feet;
}
