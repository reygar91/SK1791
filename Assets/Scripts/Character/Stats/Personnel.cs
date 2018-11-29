using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personnel : myCharacterStats
{
    /// <summary>
    /// Beauty, Stamina, Sensitivity, Temper, Intelect, Will(Character), Pride() - 
    /// 
    /// </summary>

    public Personnel()
    {
        //TargetRoom = Reception.Instance;
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

    protected override void SetAppearance()
    {
        Appearance = new CharacterAppearance();
        int Index = Random.Range(0, StreamingAssetsMNGR.Instance.Hat.Count);
        Appearance.Outfit.Head = StreamingAssetsMNGR.Instance.Hat[Index];
    }

    public override myCharacterController Spawn()
    {
        myCharacterController CC = PersonnelFactory.Instance.SpawnPersonnel(this);
        return CC;
    }

    public override void OnEnable(myCharacterController MC)
    {
        ApplyCharacteristics(MC);
        ApplyAppearance(MC);
        Register(MC);
    }
    public override void OnDisable(myCharacterController MC)
    {
        UnRegister(MC);
    }
    public override void CountDownReached_0(myCharacterController MC)
    {
        MC.NextBehaviour = MyBehavioursCollection.Instance.Personnel.CountDown_0;
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
        if (PersonnelFactory.Instance.PersonnelPool.Contains(MC))
            PersonnelFactory.Instance.PersonnelPool.Remove(MC);
        CharacterMNGR.Instance.ActiveCharacters.Add(MC);
    }
    private void UnRegister(myCharacterController MC)
    {
        CharacterMNGR.Instance.ActiveCharacters.Remove(MC);
        PersonnelFactory.Instance.PersonnelPool.Add(MC);
    }

    //public override void SetFocus()
    //{
    //    string RoomType = CurrentRoom.GetType().ToString();
    //    switch (RoomType)
    //    {
    //        case "Reception":
    //            break;
    //        case "Bar":
    //            Focus.MC = CharacterMNGR.Instance.ActiveMC[Focus.Index];
    //            break;
    //    }
    //}

    //public void SetCustomersDesiredAction(string desiredAction)
    //{
    //    switch (desiredAction)
    //    {
    //        case "RandomAction":
    //            DoAction = RandomAction;
    //            break;
    //        default:
    //            break;
    //    }
    //}

    private void RandomAction()
    {
        //state = State.Animation;
        //Customer customer = Focus.BC.character as Customer;
        //customer.state = State.Animation;
        //AnimationWaitTime = 5.0f;
        //customer.AnimationWaitTime = 5.0f;
        //CountDownMultiplier = 2;
        //customer.CountDownMultiplier = 2;
        //int BaseCostValue = 50;
        //GoldMNGR.Instance.AddGold(BaseCostValue);
        //customer.HasBeenServed = true;
        //Debug.Log("RandomAction between " + Name + " and " + Focus.BC.name);
    }

}
