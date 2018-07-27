using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMNGR : Singleton<CharacterMNGR> {

    protected CharacterMNGR() { }

    public MonoCharacter HumanoidPrefab;
    public GameObject CustomerContainer;
    //private List<Character> CustList = new List<Character>();

    public List<Character> charList = new List<Character>();

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
                CountDown -= GameMNGR.Instance.Reputation;
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
        MonoCharacter charInstane = Instantiate(HumanoidPrefab, CustomerContainer.transform);
        charInstane.character = new Customer(charInstane);
        charList.Add(charInstane.character);
    }

    public void DestroyCharactersAndResetCharList()
    {
        foreach (Character character in charList)
        {
            Destroy(character.monoCharacter.gameObject);
        }
        charList = new List<Character>();
    }
}
