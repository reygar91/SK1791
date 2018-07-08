﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameController : Singleton<GameController> {

    public GameObject MenuPanel;
    public GameObject CustomerOriginal, CustomerContainer; //IMPORTANT CustOrig must be not active, or in coroutine it lead to endless Instantiations
    public GameObject[] Panels;
    /* Panels:
     * 0 - CustPanel
     * 1 - PersonelPanel
     * */

    //public Toggle PauseToggle;
 
    //public List<Character> CharList = new List<Character>();

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

    //private IEnumerator SpawnCustomer()
    //{
    //    GameObject[] CustomerList = new GameObject[1];
    //    CustomerList[0] = Instantiate(CustomerOriginal, CustomerContainer.transform);
    //    int CountDown = 0;
    //    while (true)
    //    {
    //        if (TimeFlow.Instance.isPause)
    //        {
    //            yield return new WaitWhile(() => TimeFlow.Instance.isPause);
    //        }
    //        else
    //        {
    //            CountDown -= Reputation;
    //            if (CountDown <= 0)
    //            {
    //                for (int i = 0; i < CustomerList.Length; i++)
    //                {
    //                    if (CustomerList[i].gameObject.activeSelf == false)
    //                    {
    //                        CustomerList[i].gameObject.SetActive(true);
    //                        break;
    //                    }
    //                    else if (i == (CustomerList.Length - 1))
    //                    {
    //                        Array.Resize(ref CustomerList, i + 2); //increasing size of array also increases current bumber of loops in FOR cycle
    //                        CustomerList[i + 1] = Instantiate(CustomerOriginal, CustomerContainer.transform);//instantiates inactive cust, which will be activated on next loop
    //                    }
    //                }
    //                CountDown = 35;
    //            }
    //            yield return new WaitForSeconds(1 / TimeFlow.Instance.timeSpeed);
    //        }            
    //    }
    //}


    private Save CreateSaveGameObject()
    {
        Save save = new Save();
        int i = 0;
        foreach (Character character in CharacterManager.Instance.charList)
        {
            //Target target = targetGameObject.GetComponent<Target>();
            if (character.monoCharacter.gameObject.activeSelf)
            {
                CharData data = new CharData
                {
                    state = character.state,
                    prevState = character.prevState,
                    Behaviour = character.Behaviour.GetType().ToString(),
                    BehaviourStateID = character.Behaviour.GetStatusID(),
                    X = character.monoCharacter.transform.position.x,
                    Y = character.monoCharacter.transform.position.y,
                    Z = character.monoCharacter.transform.position.z,
                    TargetX = character.monoCharacter.Target.x,
                    TargetY = character.monoCharacter.Target.y,
                    AnimationWaitTime = character.AnimationWaitTime,
                    CountDown = character.CountDown
                };
                save.Characters.Add(data);
                //save.livingTargetsTypes.Add((int)target.activeRobot.GetComponent<Robot>().type);
                i++;
            }
        }

        save.gold = GoldManager.Instance.Gold;
        //save.time = TimeFlow.Instance;

        return save;
    }


    public void SaveGame()
    {
        // 1
        Save save = CreateSaveGameObject();

        // 2
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        // 3 here original script made reset of the scene

        Debug.Log("Game Saved");
    }

}