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
        MonoCharacter charInstane = Instantiate(HumanoidPrefab, CustomerContainer.transform);
        charInstane.character = new Customer(charInstane);
        charList.Add(charInstane.character);
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
                    SpawnCust(0);
                    CountDown = 35;
                }
                yield return new WaitForSeconds(1 / TimeMNGR.Instance.timeSpeed);
            }
        }
    }

    public Character SpawnCust(int prototypeID)
    {
        Character cust = null;
        for (int i = 0; i < charList.Count; i++)
        {
            if (charList[i].monoCharacter.gameObject.activeSelf == false)
            {
                charList[i].monoCharacter.character.prototypeID = prototypeID;
                charList[i].monoCharacter.gameObject.SetActive(true);
                cust = charList[i];
                break;
            }
            else if (i == (charList.Count - 1))
            {
                MonoCharacter charInstane = Instantiate(HumanoidPrefab, CustomerContainer.transform);
                charInstane.character = new Customer(charInstane);
                charList.Add(charInstane.character);
            }
        }
        return cust;
    }
}
