using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Customer : Character {

    public Customer()
    {
        TargetRoom = Reception.Instance;
        CountDown = 25;
        WalkSpeed = 3;
        SetCharacteristics();
        SetAppearance();
    }


    public override void EnterTriggerBehaviour(Collider other, MonoCharacter monoCharacter)
    {
        GameObject another = other.transform.parent.gameObject;

        if (other.tag == "Room")
        {
            if (another.name.Contains("Reception"))
            {
                CurrentRoom = Reception.Instance;
                Behaviour = new CustReception(monoCharacter);
                Target = monoCharacter.transform.position; //make itself a target so hasReachedTarged evaluates to true
            }
            if (another.name.Contains("Bar"))
            {
                CurrentRoom = another.GetComponent<Bar>();
                Behaviour = new CustBar(monoCharacter);
                Target = monoCharacter.transform.position; //make itself a target so hasReachedTarged evaluates to true
            }
        }
    }
    
    protected override void SetCharacteristics()
    {
        int RandomNumber = Random.Range(0, 1000);
        name = "Customer_" + RandomNumber;
    }

    public override void ApplyCharacteristics(MonoCharacter monoCharacter)
    {
        monoCharacter.name = name;
    }

    protected override void SetAppearance()
    {
        appearance = new CharacterAppearance(); //Debug.Log(AppearanceMNGR.Instance.Hat.Count);
        int Index = Random.Range(0, StreamingAssetsMNGR.Instance.Hat.Count);
        appearance.Outfit.Head = StreamingAssetsMNGR.Instance.Hat[Index];
    }

    public override void ApplyAppearance(MonoCharacter monoCharacter)
    {
        monoCharacter.Renderer.Outfit.Head.sprite = appearance.Outfit.Head;
        monoCharacter.Renderer.Outfit.Head.color = Random.ColorHSV();        
    }

}
