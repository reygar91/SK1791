using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : Character {

    public Customer(MonoCharacter instance) : base(instance)
    {
        WalkSpeed = 3;
    }


    public override void EnterTriggerBehaviour(Collider other, Transform transform)
    {
        GameObject another = other.transform.parent.gameObject;
        
        if (other.tag == "Room")
        {
            if (another.name.Contains("Reception"))
            {
                monoCharacter.CurrentRoom = Reception.instance;
                Behaviour = new CustReception(this);
                monoCharacter.Target = transform.position; //make itself a target so hasReachedTarged evaluates to true
            }
            if (another.name.Contains("Bar"))
            {
                monoCharacter.CurrentRoom = another.GetComponent<Bar>();
                Behaviour = new CustBar(this);
                monoCharacter.Target = transform.position; //make itself a target so hasReachedTarged evaluates to true
            }
        }
    }
    

}
