using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using DG.Tweening;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    //Tile Prefabs
    public GameObject Land;
    public GameObject Road;
    public GameObject Camp;
    public GameObject Woods;

    //Tile Stroing
    public GameObject mapOrigin;
    public GameObject saversOrigin;

    //IO readers and writers
    public string file_name;
    private string file_path;

    //Player Inis
    public Transform startPos;

    //Debug
    public bool isSave;

    private void Awake()
    {
        instance = this;
    }
    //public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //TODO: Start New or Load
        LoadGame();
    }

    private void Update()
    {
        if(isSave)
        {
            SaveGame();
        }
    }

    public void LoadGame()
    {
        string current_file_path =
            Application.dataPath +
            "/Logs/" +
            file_name;
        
        //read all content in current_file_path 
        string[] fileLines = File.ReadAllLines(current_file_path);
        
        for (var y = 0; y < fileLines.Length; y++)
        {
            string lineText = fileLines[y];
            //Debug.Log("FLength:" + fileLines.Length);

            char[] characters = lineText.ToCharArray();
            
            //read each y lines, split according to X
            for (int x = 0; x < characters.Length; x++)
            {
                //"p" for level, "w" for wall
                char c = characters[x];
                //Debug.Log("CLength:" + characters.Length);
                
                // create a holder for each 'letter' gameobject
                GameObject newObj = null;

                switch (c)
                {
                    case 'S':
                        newObj = Instantiate<GameObject>(Camp);
                        startPos.localPosition = new Vector2(x, y);
                        //currentPlayer = newObj;
                        break;
                    case '0':
                        newObj = Instantiate<GameObject>(Road);
                        break;
                    case '-':
                        newObj = Instantiate<GameObject>(Land);
                        break;
                    default:
                        newObj = null;
                        break;
                }
                

                if (newObj != null)
                {
                    if (!newObj.name.Contains("Player"))
                    {
                        newObj.transform.parent = mapOrigin.transform;
                    }
                    newObj.transform.localPosition = new Vector3(x,y,0);
                }
            
            }
        }
    }

    public void SaveGame()
    {
        file_name = "SavedLevel.txt";
        
        string current_file_path =
            Application.dataPath +
            "/Logs/" +
            file_name;
        string lineContent = "";

        File.WriteAllText(current_file_path, "");
        for (var y = 0; y < 8; y++)
        {
            GameObject SaversLine = saversOrigin.GetComponent<SaverOrigin>().listOfSavers[y];

            for (var x = 0; x < 16; x++)
            {
                GameObject levelSaver = SaversLine.GetComponent<LevelSavers>().levelSavers[x];

                if (levelSaver.GetComponent<LevelSaver>().currentObject == "land")
                {
                    lineContent += "-";
                }
                if (levelSaver.GetComponent<LevelSaver>().currentObject == "road")
                {
                    lineContent += "0";
                }
                if (levelSaver.GetComponent<LevelSaver>().currentObject == "camp")
                {
                    lineContent += "S";
                }
                if (levelSaver.GetComponent<LevelSaver>().currentObject == "woods")
                {
                    lineContent += "W";
                }
            }
            lineContent += "\n";
            File.AppendAllText(current_file_path,lineContent);
            lineContent = "";
        }

        isSave = false;
        Debug.Log("FileSaved!");
    }

    public void Build(Transform transform, GameObject gameobject)
    {

        if(GameManager.instance.buildTimes > 0)
        {
            if (gameobject.tag != "camp" && gameobject.tag != "land" && gameobject.tag != "woods")
            {
                Destroy(gameobject);
                GameObject newObj = Instantiate<GameObject>(Woods);
                newObj.transform.parent = mapOrigin.transform;
                newObj.transform.localPosition = transform.localPosition;
                GameManager.instance.buildTimes--;
                isSave = true;
            }
            else
            {
                Debug.Log("Not On Right Place");
            }

        }
        else
        {
            Debug.Log("Not Enough Times");
        }

    }
    
}
