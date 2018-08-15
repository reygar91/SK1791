using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMNGR : MonoBehaviour {

    protected CharacterMNGR() { }
    [SerializeField]
    private MonoCharacter HumanoidPrefab;
    [SerializeField]
    private GameObject CharacterContainer;

    public List<MonoCharacter> MCPool = new List<MonoCharacter>();
    public List<MonoCharacter> ActiveMC = new List<MonoCharacter>();

    public static CharacterMNGR Instance;

    private void Awake()
    {
        Instance = this;        
    }

    private void Start()
    {
        StartCoroutine("SpawnCustomer"); //later this should start after some dialogue
    }

    private void Update()
    {
        //.ToArray returns Array copy of List
        foreach (MonoCharacter MC in ActiveMC.ToArray())
        {
            MC.Tick();
        }
    }

    private IEnumerator SpawnCustomer()
    {
        int CountDown = 0;
        while (true)
        {
            if (TimeMNGR.Instance.isPause)
            {
                yield return new WaitWhile(() => TimeMNGR.Instance.isPause);
            }
            else
            {
                CountDown -= InputMNGR.Instance.Reputation;
                if (CountDown <= 0)
                {
                    SpawnCharacter(1);//1 for Customer
                    CountDown = 35;
                }
                yield return new WaitForSeconds(1 / TimeMNGR.Instance.timeSpeed);
            }
        }
    }

    //public MonoCharacter SpawnAndActivateCharacter(int CharacterID)
    //{
    //    MonoCharacter MC = null;
    //    if (ActiveMC.Count == 0)
    //    {
    //        InstantiateAndRegisterCharacter();
    //    }
    //    for (int i = 0; i < ActiveMC.Count; i++)
    //    {
    //        if (ActiveMC[i].gameObject.activeSelf == false)
    //        {
    //            ActiveMC[i].character = CreateCharacter(CharacterID);
    //            ActiveMC[i].gameObject.SetActive(true);
    //            MC = ActiveMC[i];
    //            break;
    //        }
    //        else if (i == (ActiveMC.Count - 1))
    //        {
    //            InstantiateAndRegisterCharacter();
    //        }
    //    }
    //    return MC;
    //}

    public MonoCharacter SpawnCharacter(int CharacterID)
    {
        MonoCharacter MC = GetMonoCharacter();
        MC.character = CreateCharacter(CharacterID);
        MC.gameObject.SetActive(true);
        return MC;
    }

    public MonoCharacter GetMonoCharacter()
    {
        MonoCharacter MC = null;
        if (MCPool.Count == 0)
            MC = Instantiate(HumanoidPrefab, CharacterContainer.transform);
        else
            MC = MCPool[0];
        return MC;
    }


    public Character CreateCharacter(int CharacterID)
    {
        Character character;
        switch (CharacterID)
        {
            case 0:
                character = new Character();
                break;
            case 1:
                character = new Customer();
                break;
            case 2:
                character = new Personnel();
                break;
            default:
                character = null;
                break;
        }
        return character;
    }

}
