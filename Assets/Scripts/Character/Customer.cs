using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Customer : Character
{

    //public delegate void Serve();
    //public Serve GetServedDelegate;
    public bool HasBeenServed;
    public string DesiredAction = "RandomAction";

    public Customer()
    {
        CountDown = 25;
        WalkSpeed = 3;
        SetCharacteristics();
        SetAppearance();
    }
    
    protected override void SetCharacteristics()
    {
        int RandomNumber = Random.Range(0, 1000);
        Name = "Customer_" + RandomNumber;
    }

    protected override void SetAppearance()
    {
        Appearance = new CharacterAppearance();
        int Index = Random.Range(0, StreamingAssetsMNGR.Instance.Hat.Count);
        Appearance.Outfit.Head = StreamingAssetsMNGR.Instance.Hat[Index];
    }

    public override void Spawn()
    {
        CustomerFactory.Instance.SpawnCustomer(this);
    }
    //public override void ApplyRoomBehaviour(BehaviourController MC)
    //{
    //    MC.behaviour = CurrentRoom.CustomersBehaviour;
    //}
    public override void OnEnable(BehaviourController MC)
    {
        ApplyCharacteristics(MC);
        ApplyAppearance(MC);
        Register(MC);
    }
    public override void OnDisable(BehaviourController MC)
    {
        UnRegister(MC);
    }
    public void ApplyCharacteristics(BehaviourController MC)
    {
        MC.name = Name;
    }

    public void ApplyAppearance(BehaviourController MC)
    {
        MC.Renderer.Outfit.Head.sprite = Appearance.Outfit.Head;
        MC.Renderer.Outfit.Head.color = Random.ColorHSV();
    }

    private void Register(BehaviourController MC)
    {
        if (CustomerFactory.Instance.CustomersPool.Contains(MC))
            CustomerFactory.Instance.CustomersPool.Remove(MC);
        CharacterMNGR.Instance.ActiveCharacters.Add(MC);
    }
    private void UnRegister(BehaviourController MC)
    {
        CharacterMNGR.Instance.ActiveCharacters.Remove(MC);
        CustomerFactory.Instance.CustomersPool.Add(MC);
    }

    //public override void SetFocus()
    //{
    //    string RoomType = CurrentRoom.GetType().ToString();
    //    switch (RoomType)
    //    {
    //        case "Reception":
    //            Focus.Object = Reception.Instance.WaitInLinePoints[Focus.Index];
    //            Reception.Instance.OccupiedSpot[Focus.Index] = true;
    //            break;
    //        case "Bar":
    //            Bar room = CurrentRoom as Bar;
    //            Focus.Object = room.FindSeat(Focus.Index);
    //            room.AvailableSeats.Remove(Focus.Object);
    //            break;
    //    }
    //}

    public void GetServed()
    {
        //CustomerPanel.Instance.gameObject.SetActive(false);
        //Personnel personnel = CurrentRoom.GetPersonel().character as Personnel;
        ////MonoCharacter PersMC = CurrentRoom.GetPersonel();
        //personnel.Focus.MC = MC;
        //personnel.SetCustomersDesiredAction(DesiredAction);
        //personnel.RoomBehaviour = personnel.ReachFocusMC;        
        //personnel.state = State.Move;//to come out from Idle state

        //if (CurrentRoom.GetPersonel() == null)
        //    CustomerPanel.Instance.ServeBTN.interactable = false;
    }


}
