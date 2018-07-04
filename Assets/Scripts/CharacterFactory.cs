using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFactory : Singleton<CharacterFactory>
{
    public Character CustomerOriginal;
    public GameObject CustomerContainer;
    public List<Character> CustList = new List<Character>();

    protected CharacterFactory() { }    

    // Use this for initialization
    void Start()
    {
        StartCoroutine("SpawnCustomer");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SpawnCustomer()
    {
        //Customer[] CustomerList = new Customer[1];       
        CustList.Add(Instantiate(CustomerOriginal, CustomerContainer.transform));
        GameController.Instance.CharList.Add(CustList[CustList.Count-1]);
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
                    for (int i = 0; i < CustList.Count; i++)
                    {
                        if (CustList[i].gameObject.activeSelf == false)
                        {
                            CustList[i].gameObject.SetActive(true);
                            break;
                        }
                        else if (i == (CustList.Count - 1))
                        {
                            CustList.Add(Instantiate(CustomerOriginal, CustomerContainer.transform));
                            GameController.Instance.CharList.Add(CustList[CustList.Count - 1]);
                        }
                    }
                    CountDown = 35;
                }
                yield return new WaitForSeconds(1 / TimeFlow.Instance.timeSpeed);
            }
        }
    }
}
