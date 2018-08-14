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
        foreach (MonoCharacter MC in MCPool)
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
                    SpawnAndActivateCharacter(1);//1 for Customer
                    CountDown = 35;
                }
                yield return new WaitForSeconds(1 / TimeMNGR.Instance.timeSpeed);
            }
        }
    }

    public MonoCharacter SpawnAndActivateCharacter(int CharacterID)
    {
        MonoCharacter MC = null;
        if (MCPool.Count == 0)
        {
            InstantiateAndRegisterCharacter();
        }
        for (int i = 0; i < MCPool.Count; i++)
        {
            if (MCPool[i].gameObject.activeSelf == false)
            {
                MCPool[i].character = CreateCharacter(CharacterID);
                MCPool[i].gameObject.SetActive(true);
                MC = MCPool[i];
                break;
            }
            else if (i == (MCPool.Count - 1))
            {
                InstantiateAndRegisterCharacter();
            }
        }
        return MC;
    }

    private void InstantiateAndRegisterCharacter()
    {
        MonoCharacter charInstane = Instantiate(HumanoidPrefab, CharacterContainer.transform);
        //charInstane.character = new Customer();
        MCPool.Add(charInstane);
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
