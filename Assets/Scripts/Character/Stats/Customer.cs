using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Customer : myCharacterStats
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

    public override myCharacterController Spawn()
    {
        myCharacterController CC = CustomerFactory.Instance.SpawnCustomer(this);
        return CC;
    }

    public override void OnEnable(myCharacterController MC)
    {
        Register(MC);
        ApplyCharacteristics(MC);
        ApplyAppearance(MC);        
    }

    public override void OnDisable(myCharacterController MC)
    {
        UnRegister(MC);
    }

    public override void CountDownReached_0(myCharacterController MC)
    {
        base.CountDownReached_0(MC);
        MC.Focus.Activity = Reception.Instance;
        MC.Focus.TargetObj = Reception.Instance.TargetReceptionEntrance();
        MC.NextBehaviour = MyBehavioursCollection.Instance.Customer.CountDown_0;
    }

    public void ApplyCharacteristics(myCharacterController MC)
    {
        MC.name = Name;
    }

    public void ApplyAppearance(myCharacterController MC)
    {
        MC.Renderer.Outfit.Head.sprite = Appearance.Outfit.Head;
        MC.Renderer.Outfit.Head.color = Random.ColorHSV();
    }

    private void Register(myCharacterController MC)
    {
        if (CustomerFactory.Instance.CustomersPool.Contains(MC))
            CustomerFactory.Instance.CustomersPool.Remove(MC);
        CharacterMNGR.Instance.ActiveCharacters.Add(MC);
    }

    private void UnRegister(myCharacterController MC)
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
