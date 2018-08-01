using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMNGR : MonoBehaviour {

    protected CharacterMNGR() { }
    [SerializeField]
    private MonoCharacter HumanoidPrefab;
    [SerializeField]
    private GameObject CharacterContainer;

    public List<Character> charList = new List<Character>();

    public static CharacterMNGR Instance;

    private void Awake()
    {
        Instance = this;        
    }

    private void Start()
    {
        StartCoroutine("SpawnCustomer");
    }

    private void Update()
    {
        foreach (Character character in charList)
        {
            character.monoCharacter.Tick();
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
                    SpawnAndActivateCharacter(0);
                    CountDown = 35;
                }
                yield return new WaitForSeconds(1 / TimeMNGR.Instance.timeSpeed);
            }
        }
    }

    public Character SpawnAndActivateCharacter(int prototypeID)
    {
        Character character = null;
        if (charList.Count == 0)
        {
            InstantiateAndRegisterCharacter();
        }
        for (int i = 0; i < charList.Count; i++)
        {
            if (charList[i].monoCharacter.gameObject.activeSelf == false)
            {
                charList[i].monoCharacter.character.prototypeID = prototypeID;
                charList[i].monoCharacter.gameObject.SetActive(true);
                character = charList[i];
                break;
            }
            else if (i == (charList.Count - 1))
            {
                InstantiateAndRegisterCharacter();
            }
        }
        return character;
    }

    private void InstantiateAndRegisterCharacter()
    {
        MonoCharacter charInstane = Instantiate(HumanoidPrefab, CharacterContainer.transform);
        charInstane.character = new Customer(charInstane);
        charList.Add(charInstane.character);
    }

}
