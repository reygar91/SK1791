using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : Character {

    public Customer()
    {
        TargetRoom = Reception.Instance;
        CountDown = 25;
        WalkSpeed = 3;
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


    private void SetAppearance()
    {

    }

    private void SetCharacteristics()
    {

    }
    

}
