﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneMNGR : MonoBehaviour {

    public static SceneMNGR Instance;

    private void Awake()
    {
        Instance = this;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("MainMenu");        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        Debug.Log("SceneMNGR_Loaded in " + sceneName);
        switch (sceneName)
        {
            case "LogoPreloader":
                break;
            case "MainMenu":
                MainMenuButtons.Instance.NewGame.onClick.AddListener(NewGame);
                MainMenuButtons.Instance.Load.onClick.AddListener(LoadGame);
                //MainMenuSceneReferences.Instance.Options.onClick.AddListener();
                MainMenuButtons.Instance.Quit.onClick.AddListener(Quit);
                break;
            case "Main":
                MainMenuButtons.Instance.Save.onClick.AddListener(SaveGame);
                MainMenuButtons.Instance.Load.onClick.AddListener(LoadGame);
                MainMenuButtons.Instance.ToMainMenu.onClick.AddListener(ToMainMenu);
                break;
            default:
                Debug.Log("Scene not Found");
                break;
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Main");
        SceneManager.sceneLoaded += NewGameLoaded;
    }

    private void NewGameLoaded(Scene scene, LoadSceneMode mode)
    {
        myEventMNGR.Instance.ActiveEvents.Add(new StartGameTutorial());
        //CharacterMNGR.Instance.SpawnCharacter("MainCharacter");
        SceneManager.sceneLoaded -= NewGameLoaded;
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

        Debug.Log("Game Saved to " + Application.persistentDataPath);
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        Room[] rooms = BuildMNGR.Instance.roomsList.ToArray();
        save.Rooms = new RoomSaveData[rooms.Length];
        for (int i=0; i < save.Rooms.Length; i++)
        {
            Room room = rooms[i];
            RoomSaveData data = new RoomSaveData
            {
                typeAndSizeID = room.saveData.typeAndSizeID,
                X = room.saveData.X,
                Y = room.saveData.Y,
                Z = room.saveData.Z
            };
            save.Rooms[i] = data;
        }


        
        BehaviourController[] ActiveBC = CharacterMNGR.Instance.ActiveCharacters.ToArray();
        save.Characters = new CharSaveData[ActiveBC.Length];
        for (int i = 0; i < save.Characters.Length; i++)
        {
            BehaviourController BC = ActiveBC[i];
            CharSaveData data = GatherCharacterSaveData(BC);
            save.Characters[i] = data;
        }
        

        save.ActiveEvents = myEventMNGR.Instance.GetActiveEventsSignatures();

        save.gold = GoldMNGR.Instance.Gold;
        save.time = TimeMNGR.Instance.timePassed;

        return save;
    }

    private CharSaveData GatherCharacterSaveData(BehaviourController BC)
    {
        CharSaveData data = new CharSaveData
        {
            state = BC.character.state,
            prevState = BC.character.prevState,
            X = BC.transform.position.x,
            Y = BC.transform.position.y,
            Z = BC.transform.position.z,
            AnimationWaitTime = BC.character.AnimationWaitTime,
            CountDown = BC.character.CountDown,
            //TargetRoomIndex = BuildMNGR.Instance.roomsList.IndexOf(BC.character.TargetRoom),
            CharacterType = BC.character.GetType().ToString(),
            BehaviourStatusID = BC.ActionID,
            Focus = BC.Focus.SaveFocusData()
        };                   
        return data;
    }
    

    public void LoadGame()
    {
        // 1
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            SceneManager.LoadScene("Main");
            SceneManager.sceneLoaded += LoadGameEvent;
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }

    private void LoadGameEvent(Scene scene, LoadSceneMode mode)
    {
        // 2
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
        Save save = (Save)bf.Deserialize(file);
        file.Close();

        // 3

        foreach (RoomSaveData data in save.Rooms)
        {
            BuildMNGR.Instance.InstantiateRoom(BuildMNGR.Instance.Rooms[data.typeAndSizeID], new Vector3(data.X, data.Y, data.Z));
        }

        foreach (CharSaveData data in save.Characters)
        {
            //BehaviourController BC = CharacterMNGR.Instance.SpawnCharacter(data.CharacterType);//number corresponds to CharacterID
            //BC.character.state = data.state;
            //BC.character.prevState = data.prevState;
            //BC.transform.position = new Vector3(data.X, data.Y, data.Z);
            //BC.character.AnimationWaitTime = data.AnimationWaitTime;
            //BC.character.CountDown = data.CountDown;
            //BC.ActionID = data.BehaviourStatusID;
            //if (data.Focus.NotSet)
            //    BC.character.Focus = data.Focus;
        }

        myEventMNGR.Instance.LoadEventsFromSignatures(save.ActiveEvents);

        // 4
        GoldMNGR.Instance.AddGold(save.gold);
        TimeMNGR.Instance.timePassed = save.time;

        Debug.Log("Game Loaded");

        SceneManager.sceneLoaded -= LoadGameEvent;

        TimeMNGR.Instance.Pause.isOn = true; //Debug.Log("SetOnPause");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        //If we are running in a standalone build of the game
    #if UNITY_STANDALONE
        //Quit the application
        Application.Quit();
    #endif

        //If we are running in the editor
    #if UNITY_EDITOR
        //Stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }

}
