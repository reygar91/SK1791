using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager> {

    protected CharacterManager() { }

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
            if (TimeFlow.Instance.isPause)
            {
                yield return new WaitWhile(() => TimeFlow.Instance.isPause);
            }
            else
            {
                CountDown -= GameController.Instance.Reputation;
                if (CountDown <= 0)
                {
                    SpawnCust(0);
                    CountDown = 35;
                }
                yield return new WaitForSeconds(1 / TimeFlow.Instance.timeSpeed);
            }
        }
    }

    public void SpawnCust(int prototypeID)
    {
        for (int i = 0; i < charList.Count; i++)
        {
            if (charList[i].monoCharacter.gameObject.activeSelf == false)
            {
                charList[i].monoCharacter.character.prototypeID = prototypeID;
                charList[i].monoCharacter.gameObject.SetActive(true);
                break;
            }
            else if (i == (charList.Count - 1))
            {
                MonoCharacter charInstane = Instantiate(HumanoidPrefab, CustomerContainer.transform);
                charInstane.character = new Customer(charInstane);
                charList.Add(charInstane.character);
            }
        }
    }
}
