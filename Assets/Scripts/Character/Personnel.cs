using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personnel : Character
{
    /// <summary>
    /// Beauty, Stamina, Sensitivity, Temper, Intelect, Will(Character), Pride() - 
    /// 
    /// </summary>

    public Personnel()
    {
        TargetRoom = Reception.Instance;
        CountDown = 600;
        WalkSpeed = 3;
        SetCharacteristics();
        SetAppearance();
    }

    protected override void SetCharacteristics()
    {
        int RandomNumber = Random.Range(0, 1000);
        Name = "Personnel" + RandomNumber;
    }

    public override void ApplyCharacteristics()
    {
        MC.name = Name;
    }

    protected override void SetAppearance()
    {
        Appearance = new CharacterAppearance();
        int Index = Random.Range(0, StreamingAssetsMNGR.Instance.Hat.Count);
        Appearance.Outfit.Head = StreamingAssetsMNGR.Instance.Hat[Index];
    }

    public override void ApplyAppearance()
    {
        MC.Renderer.Outfit.Head.sprite = Appearance.Outfit.Head;
        MC.Renderer.Outfit.Head.color = Random.ColorHSV();
    }

    public override void SetFocus()
    {
        string RoomType = CurrentRoom.GetType().ToString();
        switch (RoomType)
        {
            case "Reception":                
                break;
            case "Bar":
                Focus.MC = CharacterMNGR.Instance.ActiveMC[Focus.Index]; 
                break;
        }        
    }

    protected override void AtReception()
    {
        Reception room = Reception.Instance;
        switch (BehaviourStatusID)
        {
            case 10:
                state = State.Idle;
                InputMNGR.Instance.Serve.interactable = true;
                //AnimationWaitTime = 5.0f;
                break;
            default:
                Target.x = room.Doors.transform.position.x;
                Target.z = room.MiddleOfTheRoom.transform.position.z;
                BehaviourStatusID = 10;
                break;
        }
    }

    protected override void AtBar()
    {
        Vector3 Middle = CurrentRoom.MiddleOfTheRoom.transform.position;
        Bar room = CurrentRoom as Bar;
        switch (BehaviourStatusID)
        {
            case 11:// cheking if there are any custs to serve or reached previous random target
                if (room.custAtBar.Count != 0)
                    BehaviourStatusID = 13;
                else
                    BehaviourStatusID = 12;
                break;
            case 12://set random target to move along the room and change statusID so it was done only once 
                float delta = room.Doors.transform.position.x - Middle.x;//distance from center of the room to the doors, considering doors located right to the center. doors.x > center.x
                float minX = Middle.x - delta;
                float maxX = Middle.x + delta;
                Target.x = Random.Range(minX, maxX);
                BehaviourStatusID = 11;
                break;
            case 13://setting target to guest X position and changing StatusID, so it is done only once
                int index = Random.Range(0, room.custAtBar.Count);
                Focus.MC = room.custAtBar[index];
                Target.x = Focus.MC.transform.position.x;
                room.custAtBar.Remove(Focus.MC);
                Focus.MC.character.CountDown += 5;
                BehaviourStatusID = 14;
                break;
            case 14://setting target to guest X,Z position & changing StatusID
                Target.z = Focus.MC.transform.position.z;
                BehaviourStatusID = 15;
                break;
            case 15:
                GoldMNGR.Instance.AddGold(150);
                Focus.MC = null;
                BehaviourStatusID = 10;
                break;
            default://when just entered the room or just served cust
                Target.z = Middle.z;
                BehaviourStatusID = 11;
                break;
        }
    }

    public void SetCustomersDesiredAction(string desiredAction)
    {
        switch (desiredAction)
        {
            case "RandomAction":
                DoAction = RandomAction;
                break;
            default:
                break;
        }
    }

    private void RandomAction()
    {
        state = State.Animation;
        Customer customer = Focus.MC.character as Customer;
        customer.state = State.Animation;
        AnimationWaitTime = 5.0f;
        customer.AnimationWaitTime = 5.0f;
        CountDownMultiplier = 2;
        customer.CountDownMultiplier = 2;
        int BaseCostValue = 50;
        GoldMNGR.Instance.AddGold(BaseCostValue);
        customer.HasBeenServed = true;
        Debug.Log("RandomAction between " + Name + " and " + Focus.MC.name);
    }

}
