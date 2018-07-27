using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class GameMNGR : Singleton<GameMNGR> {

    public GameObject MenuPanel;
    public GameObject CustomerOriginal, CustomerContainer; //IMPORTANT CustOrig must be not active, or in coroutine it lead to endless Instantiations
    public GameObject[] Panels;
    /* Panels:
     * 0 - CustPanel
     * 1 - PersonelPanel
     * */

    public int Reputation = 1;//at 0 there will be only 1 cust spawned initially, at 7 - cust spawns every 5 sec

    public CommandPattern CancelButton, JumpButton;

    public BuildPreview buildPreview;

    protected GameMNGR() { }

    // Use this for initialization
    void Start () {
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


    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        foreach (Room room in Room.roomsList)
        {
            RoomSaveData data = new RoomSaveData
            {
                typeAndSizeID = room.saveData.typeAndSizeID,
                X = room.saveData.X,
                Y = room.saveData.Y,
                Z = room.saveData.Z
            };
            save.Rooms.Add(data);
        }

        foreach (Character character in CharacterMNGR.Instance.charList)
        {
            if (character.monoCharacter.gameObject.activeSelf)
            {
                CharSaveData data = GatherCharacterSaveData(character);
                save.Characters.Add(data);
            }
        }

        save.ActiveEvents = myEventMNGR.Instance.GetActiveEventsSignatures();

        save.gold = GoldMNGR.Instance.Gold;
        //save.time = TimeFlow.Instance;

        return save;
    }

    private CharSaveData GatherCharacterSaveData(Character character)
    {
        CharSaveData data = new CharSaveData
        {
            state = character.state,
            prevState = character.prevState,
            X = character.monoCharacter.transform.position.x,
            Y = character.monoCharacter.transform.position.y,
            Z = character.monoCharacter.transform.position.z,
            AnimationWaitTime = character.AnimationWaitTime,
            CountDown = character.CountDown,
            TargetRoomIndex = Room.roomsList.IndexOf(character.monoCharacter.TargetRoom)
        }; Debug.Log("TargetRoomIndex = " + data.TargetRoomIndex);
        if (character.Behaviour != null)
        {
            data.behaviour = character.Behaviour.BehaviourData;
        }
        return data;
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

        Debug.Log("Game Saved to " + Application.persistentDataPath);
    }

    public void LoadGame()
    {
        // 1
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            //here scene has to be reloaded or reseted
            CharacterMNGR.Instance.DestroyCharactersAndResetCharList();
            Room.DestroyRoomsAndResetRoomList();
            Reception.instance.ResetOccupiedSpots();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            // 2
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            // 3
            foreach (RoomSaveData data in save.Rooms)
            {
                Room newRoom = buildPreview.InstantiateRoom(buildPreview.Rooms[data.typeAndSizeID], new Vector3(data.X, data.Y, data.Z));
                newRoom.gameObject.SetActive(true);
            }

            foreach (CharSaveData data in save.Characters)
            {
                Character character = CharacterMNGR.Instance.SpawnAndActivateCharacter(0);
                character.state = data.state;
                character.prevState = data.prevState;
                character.monoCharacter.transform.position = new Vector3 (data.X, data.Y, data.Z);
                character.AnimationWaitTime = data.AnimationWaitTime;
                character.CountDown = data.CountDown;
                if (data.TargetRoomIndex == -1)
                    character.monoCharacter.TargetRoom = Reception.instance;
                else
                    character.monoCharacter.TargetRoom = Room.roomsList[data.TargetRoomIndex];
                if (data.behaviour != null)
                    character.behaviourData = data.behaviour;
            }

            myEventMNGR.Instance.LoadEventsFromSignatures(save.ActiveEvents);

            // 4
            GoldMNGR.Instance.AddGold(save.gold);

            Debug.Log("Game Loaded");

            //Unpause();
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }

}
