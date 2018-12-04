using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PossibleActivity : ScriptableObject {

    public abstract ActivityContainer CheckAvailabilityWithinRoom(myCharacterController Character);

    public abstract bool CheckAvailabilityOverAll(myCharacterController Character);

}

public struct ActivityContainer
{
    public Activity activity;
    public Transform Obj;
}
