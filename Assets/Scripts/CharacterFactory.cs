//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CharacterFactory : Singleton<CharacterFactory>
//{
//    public MonoCharacter HumanoidPrefab;
//    public GameObject CustomerContainer;
//    public List<Character> CustList = new List<Character>();

//    protected CharacterFactory() { }

//    // Use this for initialization
//    void Start()
//    {
//        StartCoroutine("SpawnCustomer");
//    }

//    private IEnumerator SpawnCustomer()
//    {
//        MonoCharacter charInstane = Instantiate(HumanoidPrefab, CustomerContainer.transform);
//        charInstane.character = new Customer(charInstane);
//        CustList.Add(charInstane.character);
//        int CountDown = 0;
//        while (true)
//        {
//            if (TimeFlow.Instance.isPause)
//            {
//                yield return new WaitWhile(() => TimeFlow.Instance.isPause);
//            }
//            else
//            {
//                CountDown -= GameController.Instance.Reputation;
//                if (CountDown <= 0)
//                {
//                    SpawnCust(0);
//                    CountDown = 35;
//                }
//                yield return new WaitForSeconds(1 / TimeFlow.Instance.timeSpeed);
//            }
//        }
//    }

//    public void SpawnCust(int prototypeID)
//    {
//        for (int i = 0; i < CustList.Count; i++)
//        {
//            if (CustList[i].monoCharacter.gameObject.activeSelf == false)
//            {
//                CustList[i].monoCharacter.character.prototypeID = prototypeID;
//                CustList[i].monoCharacter.gameObject.SetActive(true);
//                break;
//            }
//            else if (i == (CustList.Count - 1))
//            {
//                MonoCharacter charInstane = Instantiate(HumanoidPrefab, CustomerContainer.transform);
//                charInstane.character = new Customer(charInstane);
//                CustList.Add(charInstane.character);
//            }
//        }
//    }

//}
