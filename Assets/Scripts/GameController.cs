using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject MenuPanel;
   // public Transform[] WayPoint;
    /*
     * 0 - SpawnPoint
     * 1 - BarEntrance
     */
    public GameObject CustomerOriginal, CustomerContainer; //IMPORTANT CustOrig must be not active, or in coroutine it lead to endless Instantiations
    //private Customer[] CustomerList;
    public GameObject[] Panels;
    /* Panels:
     * 0 - CustPanel
     * 1 - PersonelPanel
     * */

    public Toggle PauseToggle, GameStateToggle;
    public GameObject CutSceneUI;
 
    public static List<Character> CharList = new List<Character>();

    public int Reputation = 1;//at 0 there will be only 1 cust spawned initially, at 7 - cust spawns every 5 sec

    public static GameFSM gameState;

    // Use this for initialization
    void Start () {
        StartCoroutine("SpawnCustomer");
        gameState = new RegularState();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Cancel") && !DisableWithEsc.ActivePanel())//should remove 2nd condition and put it to state
        {
            MenuPanel.SetActive(true);
        }
        if (Input.GetButtonDown("Jump") && !CutSceneUI.activeSelf) //and then define command with Command Pattern
        {
            gameState.JumpButton();
            //PauseToggle.isOn = !PauseToggle.isOn;
        }
    }

    private IEnumerator SpawnCustomer()
    {
        GameObject[] CustomerList = new GameObject[1];
        CustomerList[0] = Instantiate(CustomerOriginal, CustomerContainer.transform);
        int CountDown = 0;
        while (true)
        {
            if (TimeFlow.isPause)
            {
                yield return new WaitWhile(() => TimeFlow.isPause);
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
                yield return new WaitForSeconds(1 / TimeFlow.timeSpeed);
            }            
        }
    }
    
}
