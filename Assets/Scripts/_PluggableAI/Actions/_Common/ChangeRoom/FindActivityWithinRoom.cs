using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/FindActivityWithinRoom")]
public class FindActivityWithinRoom : Action {
    public override bool Act(myCharacterController Character)
    {
        ActivityContainer SearchResult = new ActivityContainer();
        for (int i =0; i < Character.possibleBehaviours.Length; i++)
        {
            SearchResult = Character.possibleBehaviours[i].CheckAvailabilityWithinRoom(Character);
            if (SearchResult.Obj != null)
            {
                i = Character.possibleBehaviours.Length;
                Character.behaviour = SearchResult.activity.CustBehaviour;
                Character.Focus.Activity = SearchResult.activity;
                Character.Focus.TargetObj = SearchResult.Obj;
            }
        }
        return true;
        
        ////Character.Focus.CurrentRoom.Activities[0].in
        //System.Predicate<Transform> IsWithinRoom = delegate (Transform item) 
        //{ return item.GetComponentInParent<Room>().transform == Character.Focus.CurrentRoom.transform; };
        ////System.Predicate<Transform> predicate = FindType;
        //foreach (Activity activity in Character.Focus.CurrentRoom.Activities)
        //{
        //    //activity
        //}
        //List<Transform> Seats = BarMNGR.Instance.AvailableSeats.FindAll(IsWithinRoom);
        //throw new System.NotImplementedException();
    }

    //private bool FindType(Transform item, myCharacterController myCharacter)
    //{
    //    return item.parent.parent.parent == myCharacter.Focus.CurrentRoom.transform;
    //}
}
