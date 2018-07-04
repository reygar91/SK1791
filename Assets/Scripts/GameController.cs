using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Singleton<GameController> {

    public GameObject MenuPanel;
    public GameObject CustomerOriginal, CustomerContainer; //IMPORTANT CustOrig must be not active, or in coroutine it lead to endless Instantiations
    public GameObject[] Panels;
    /* Panels:
     * 0 - CustPanel
     * 1 - PersonelPanel
     * */

    //public Toggle PauseToggle;
 
    public List<Character> CharList = new List<Character>();

    public int Reputation = 1;//at 0 there will be only 1 cust spawned initially, at 7 - cust spawns every 5 sec

    public CommandPattern CancelButton, JumpButton;

    protected GameController() { }

    // Use this for initialization
    void Start () {
        //StartCoroutine("SpawnCustomer");
        CancelButton = new DisablePanel(MenuPanel);
        JumpButton = new Pause();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Cancel"))
        {
            CancelButton.Execute();
        }
        if (Input.GetButtonDown("Jump"))
        {
            JumpButton.Execute();
        }
    }

    private IEnumerator SpawnCustomer()
    {
        GameObject[] CustomerList = new GameObject[1];
        CustomerList[0] = Instantiate(CustomerOriginal, CustomerContainer.transform);
        int CountDown = 0;
        while (true)
        {
            if (TimeFlow.Instance.isPause)
            {
                yield return new WaitWhile(() => TimeFlow.Instance.isPause);
            }
            else
            {
                CountDown -= Reputation;
                if (CountDown <= 0)
                {
                    for (int i = 0; i < CustomerList.Length; i++)
                    {
                        if (CustomerList[i].gameObject.activeSelf == false)
                        {
                            CustomerList[i].gameObject.SetActive(true);
                            break;
                        }
                        else if (i == (CustomerList.Length - 1))
                        {
                            Array.Resize(ref CustomerList, i + 2); //increasing size of array also increases current bumber of loops in FOR cycle
                            CustomerList[i + 1] = Instantiate(CustomerOriginal, CustomerContainer.transform);//instantiates inactive cust, which will be activated on next loop
                        }
                    }
                    CountDown = 35;
                }
                yield return new WaitForSeconds(1 / TimeFlow.Instance.timeSpeed);
            }            
        }
    }
    
}
