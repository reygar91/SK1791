using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outfit : MonoBehaviour {
    //[Header("SpriteRenderers")]    
    public SpriteRenderer Head, Body, RightShoulderUp, RightShoulderLow, RightLegUp, RightLegLow, RightFeet,
        LeftShoulderUp, LeftShoulderLow, LeftLegUp, LeftLegLow, LeftFeet;

    public List<Sprite> Outfits = new List<Sprite>();


    public void SetOutfit()
    {
        Head.sprite = Outfits[Random.Range(0, 3)];
        Head.color = Random.ColorHSV();
    }

}
