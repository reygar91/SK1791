using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerVer2 : CharacterVer2 {

    
    public override void EnterTriggerBehaviour(Collider other, Transform transform)
    {
        GameObject another = other.transform.parent.gameObject;
        if (other.tag == "Room")
        {
            if (another.name.Contains("Reception"))
            {
                if (Behaviour != null)
                {
                    Behaviour.SwitchRoom(); //Debug.Log("leave room onTirggerEnter switch");
                }
                Behaviour = new CustReception(this, reception);
                Target = transform.position; //make itself a target so hasReachedTarged evaluates to true
            }
            if (another.name.Contains("Bar"))
            {
                Behaviour.SwitchRoom();
                Bar RoomType = another.GetComponent<Bar>();
                Behaviour = new CustBar(this, RoomType);
                Target = transform.position; //make itself a target so hasReachedTarged evaluates to true

            }
        }
    }
    

}
