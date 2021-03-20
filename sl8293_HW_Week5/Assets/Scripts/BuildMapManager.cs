using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using DG.Tweening;

public class BuildMapManager : MonoBehaviour
{
    public static BuildMapManager instance;

    //Tile Prefabs
    public GameObject Land;
    public GameObject Road;
    public GameObject Village;
    public GameObject Castle;

    //Tile Stroing
    public GameObject mapOrigin;
    public GameObject saversOrigin;

    //IO readers and writers
    public string file_name;
    private string file_path;

    // pools
    public List<GameObject> roadsPool;
    public List<GameObject> landsPool;
    public List<GameObject> castlePool;
    public List<GameObject> villagePool;

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
        file_name = "SavedCamp.txt";

        string current_file_path =
            Application.dataPath +
            "/Logs/" +
            file_name;
        
        //read all content in current_file_path 
        string[] fileLines = File.ReadAllLines(current_file_path);
        
        for (var y = 0; y < fileLines.Length; y++)
        {
            string lineText = fileLines[y];

            char[] characters = lineText.ToCharArray();
            
            for (int x = 0; x < characters.Length; x++)
            {
                char c = characters[x];

                GameObject newObj = null;

                switch (c)
                {
                    case 'C':
                        newObj = Instantiate<GameObject>(Castle);
                        castlePool.Add(newObj);
                        break;
                    case 'V':
                        newObj = Instantiate<GameObject>(Village);
                        villagePool.Add(newObj);
                        break;
                    case '-':
                        newObj = Instantiate<GameObject>(Land);
                        landsPool.Add(newObj);
                        break;
                    case 'R':
                        newObj = Instantiate<GameObject>(Road);
                        roadsPool.Add(newObj);
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
                    newObj.transform.localPosition = new Vector3(x, y, 0);
                }
            }
        }
    }

    public void SaveGame()
    {
        landsPool.Clear();
        roadsPool.Clear();
        villagePool.Clear();
        castlePool.Clear();

        file_name = "SavedCamp.txt";
        
        string current_file_path =
            Application.dataPath +
            "/Logs/" +
            file_name;
        string lineContent = "";

        File.WriteAllText(current_file_path, "");
        for (var y = 0; y < 5; y++)
        {
            GameObject SaversLine = saversOrigin.GetComponent<SaverOrigin>().listOfSavers[y];

            for (var x = 0; x < 5; x++)
            {
                GameObject levelSaver = SaversLine.GetComponent<LevelSavers>().levelSavers[x];

                if (levelSaver.GetComponent<LevelSaver>().currentObjectTag == "land")
                {
                    landsPool.Add(levelSaver.GetComponent<LevelSaver>().currentObject);
                    lineContent += "-";
                }
                if (levelSaver.GetComponent<LevelSaver>().currentObjectTag == "castlee")
                {
                    castlePool.Add(levelSaver.GetComponent<LevelSaver>().currentObject);
                    lineContent += "C";
                }
                if (levelSaver.GetComponent<LevelSaver>().currentObjectTag == "village")
                {
                    villagePool.Add(levelSaver.GetComponent<LevelSaver>().currentObject);
                    lineContent += "V";
                }
                if (levelSaver.GetComponent<LevelSaver>().currentObjectTag == "road")
                {
                    roadsPool.Add(levelSaver.GetComponent<LevelSaver>().currentObject);
                    lineContent += "R";
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

            if (gameobject.tag == "road")
            {
                if(BuildGameManager.instance.woodResources >= 30 && BuildGameManager.instance.rockResources >= 15)
                {
                    Destroy(gameobject);
                    GameObject newObj = Instantiate<GameObject>(Castle);
                    newObj.transform.parent = mapOrigin.transform;
                    newObj.transform.localPosition = transform.localPosition;
                    castlePool.Add(newObj);

                    BuildGameManager.instance.woodResources -= 30;
                    BuildGameManager.instance.rockResources -= 15;

                    BuildGameManager.instance.startArmor += 1;
                    SaveGame();
                }
                else
                {
                    //TODO: UI Show No Enough Resources
                }
                
            }

            if (gameobject.tag == "land")
            {
                if (BuildGameManager.instance.woodResources >= 15 && BuildGameManager.instance.rockResources >= 10)
                {
                    Destroy(gameobject);
                    GameObject newObj = Instantiate<GameObject>(Village);
                    newObj.transform.parent = mapOrigin.transform;
                    newObj.transform.localPosition = transform.localPosition;
                    castlePool.Add(newObj);
                
                    BuildGameManager.instance.woodResources -= 15;
                    BuildGameManager.instance.rockResources -= 10;

                    BuildGameManager.instance.startMaxHealth += 2;

                    isSave = true;
                }
                else
                {
                    //TODO: UI Show No Enough Resources
                }
            }
    }
    
}
