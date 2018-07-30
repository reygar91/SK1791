using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : Singleton<MainMenu>
{
    private void Awake()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            case "MainMenu":
                break;
            case "Main":
                MainSceneReferences.Instance.Windows.EscMenu.Save.onClick.AddListener(SaveGame);
                MainSceneReferences.Instance.Windows.EscMenu.Load.onClick.AddListener(LoadGame);
                break;
            default:
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Main");
        myEventMNGR.Instance.ActiveEvents.Add(new StartGameTutorial());
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
            BuildPreview buildPreview = MainSceneReferences.Instance.buildPreview;

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
