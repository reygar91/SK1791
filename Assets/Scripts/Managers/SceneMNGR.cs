using System.IO;
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
            case "MainMenu":
                MainMenuButtons.Instance.NewGame.onClick.AddListener(NewGame);
                MainMenuButtons.Instance.Load.onClick.AddListener(LoadGame);
                //MainMenuSceneReferences.Instance.Options.onClick.AddListener();
                MainMenuButtons.Instance.Quit.onClick.AddListener(Quit);
                break;
            case "Main":
                MainMenuButtons.Instance.Save.onClick.AddListener(SaveGame);
                MainMenuButtons.Instance.Load.onClick.AddListener(LoadGame);
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

        // 3 here original script made reset of the scene

        Debug.Log("Game Saved to " + Application.persistentDataPath);
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


    

    public void LoadGame()
    {
        // 1
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            //here scene has to be reloaded or reseted
            //CharacterMNGR.Instance.DestroyCharactersAndResetCharList();
            //Room.DestroyRoomsAndResetRoomList();
            //Reception.instance.ResetOccupiedSpots();
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
        BuildPreview buildPreview = MainMenuButtons.Instance.buildPreview;

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
            character.monoCharacter.transform.position = new Vector3(data.X, data.Y, data.Z);
            character.AnimationWaitTime = data.AnimationWaitTime;
            character.CountDown = data.CountDown;
            if (data.TargetRoomIndex == -1)
                character.monoCharacter.TargetRoom = Reception.Instance;
            else
                character.monoCharacter.TargetRoom = Room.roomsList[data.TargetRoomIndex];
            if (data.behaviour != null)
                character.behaviourData = data.behaviour;
        }

        myEventMNGR.Instance.LoadEventsFromSignatures(save.ActiveEvents);

        // 4
        GoldMNGR.Instance.AddGold(save.gold);

        Debug.Log("Game Loaded");

        SceneManager.sceneLoaded -= LoadGameEvent;

        InputMNGR.Instance.JumpButton.Execute();
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
