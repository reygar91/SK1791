//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CharacterMNGR : MonoBehaviour {

//    protected CharacterMNGR() { }
//    [SerializeField]
//    private MonoCharacter HumanoidPrefab;
//    [SerializeField]
//    private GameObject CharacterContainer;

//    public List<MonoCharacter> MCPool = new List<MonoCharacter>();
//    public List<MonoCharacter> ActiveMC = new List<MonoCharacter>();

//    public static CharacterMNGR Instance;

//    private void Awake()
//    {
//        Instance = this;        
//    }

//    private void Start()
//    {
//        StartCoroutine("SpawnCustomer"); //later this should start after some dialogue
//    }

//    private void Update()
//    {
//        //.ToArray returns Array copy of List
//        foreach (MonoCharacter MC in ActiveMC.ToArray())
//        {
//            MC.Tick();
//        }
//    }

//    private IEnumerator SpawnCustomer()
//    {
//        int CountDown = 0;
//        while (true)
//        {
//            if (TimeMNGR.Instance.Pause.isOn)
//            {
//                yield return new WaitWhile(() => TimeMNGR.Instance.Pause.isOn);
//            }
//            else
//            {
//                CountDown -= InputMNGR.Instance.Reputation;
//                if (CountDown <= 0)
//                {
//                    SpawnCharacter("Customer");
//                    CountDown = 35;
//                }
//                yield return new WaitForSeconds(1 / TimeMNGR.Instance.timeSpeed);
//            }
//        }
//    }

    

//    public MonoCharacter SpawnCharacter(string CharacterType)
//    {
//        MonoCharacter MC = GetMonoCharacter();
//        MC.character = CreateCharacter(CharacterType);
//        MC.gameObject.SetActive(true);
//        return MC;
//    }

//    public MonoCharacter GetMonoCharacter()
//    {
//        MonoCharacter MC = null;
//        if (MCPool.Count == 0)
//            MC = Instantiate(HumanoidPrefab, CharacterContainer.transform);
//        else
//            MC = MCPool[0];
//        return MC;
//    }


//    public Character CreateCharacter(string CharacterType)
//    {
//        Character character;
//        switch (CharacterType)
//        {
//            case "MainCharacter":
//                character = new MainCharacter();
//                break;
//            case "Customer":
//                character = new Customer();
//                break;
//            case "Personnel":
//                character = new Personnel();
//                break;
//            default:
//                character = null;
//                break;
//        }
//        return character;
//    }

//}
