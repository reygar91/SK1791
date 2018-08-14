using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AppearanceMNGR : MonoBehaviour {

    public static AppearanceMNGR Instance;

    public List<Sprite> Hat = new List<Sprite>(); /// <summary>
    /// later there should be list of custom Outfit class, which contains Sprite, possible color, description, modificators
    /// </summary>

    private void Awake()
    {
        Instance = this;
    }

    void Start () {
        Hat = LoadFiles("/Outfit/Hat/");
    }

    private List<Sprite> LoadFiles(string AdditionalPath)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Application.streamingAssetsPath + AdditionalPath);
        //Debug.Log("Streaming Assets Path: " + directoryInfo);
        FileInfo[] allFiles = directoryInfo.GetFiles("*.*");

        ListAndFile listAndFile = new ListAndFile
        {
            List = new List<Sprite>
        {
            null
        }
        };
        foreach (FileInfo file in allFiles)
        {
            listAndFile.File = file;
            StartCoroutine("LoadFile", listAndFile);
        }
        return listAndFile.List;
    }

    IEnumerator LoadFile(ListAndFile listAndFile)
    {
        if (listAndFile.File.Name.Contains("meta"))
        {
            yield break;
        }
        else
        {
            string outfitFileWithoutExtension = Path.GetFileNameWithoutExtension(listAndFile.File.ToString());
            //Debug.Log(outfitFileWithoutExtension);
            string wwwOutfitFilePath = "file://" + listAndFile.File.FullName.ToString();
            WWW www = new WWW(wwwOutfitFilePath);
            yield return www;

            listAndFile.List.Add(Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f)));
        }
    }

}

public class ListAndFile
{
    public FileInfo File;
    public List<Sprite> List;
}
