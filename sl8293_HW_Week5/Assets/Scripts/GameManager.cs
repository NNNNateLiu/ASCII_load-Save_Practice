using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //TODO: Read&Save to a file
    //ORDER!!!: initMaxHealth, initArmor, woodResources, rockResources
    public int woodResources;
    public int rockResources;
    public int initMaxHealth;
    public int initArmor;

    public static GameManager instance;
    public int buildTimes;
    public float autoSavingTimer;

    public GameObject creatureOrigin;
    public GameObject player;
    public GameObject slime;
    public GameObject wolf;

    public int slimeGenerateCounts;

    private float slimeTimer = 12;
    private float wolfTimer = 24;
    private bool isPaused = false;

    public bool generateSlimes = false;
    public bool isDead = false;
    public bool isBack = false;

    public List<GameObject> slimePoolPerLoop = new List<GameObject>();
    public List<GameObject> wolfPoolPerLoop = new List<GameObject>();

    public string file_name;
    private string file_path;

    private void Awake()
    {
        instance = this;

        //read from file
        string current_file_path =
            Application.dataPath +
            "/Logs/" +
            file_name;

        string fileLines = File.ReadAllText(current_file_path);
        string[] fileScores = fileLines.Split(',');

        initMaxHealth = int.Parse(fileScores[0]);
        initArmor = int.Parse(fileScores[1]);

    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject newPlayer = Instantiate(player, MapManager.instance.startPos.position, Quaternion.identity);
        newPlayer.transform.parent = creatureOrigin.transform;
    }

    // Update is called once per frame
    void Update()
    {
        slimeTimer += Time.deltaTime;
        wolfTimer += Time.deltaTime;

        if(slimeTimer >= 12)
        {
            SpwanSlimes();
            slimeTimer = 0;
        }

        if (wolfTimer >= 24)
        {
            SpwanWolf();
            wolfTimer = 0;
        }


        // pause the game
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPaused = !isPaused;

            if(isPaused)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        //TODO: Death Event
        if(isDead)
        {
            UIManager.instance.BadEnd();

            if (Input.GetKeyDown(KeyCode.P))
            {
                Dead_SavePlayerInfo();
                SceneManager.LoadScene(1);
            }
        }

        if(isBack)
        {
            UIManager.instance.HappyEnd();

            if (Input.GetKeyDown(KeyCode.P))
            {
                Back_SavePlayerInfo();
                SceneManager.LoadScene(1);
            }
        }
    }

    public void SpwanSlimes()
    {
        MapManager.instance.SaveGame();
        List<GameObject> allSlimeGenerateTransform = MapManager.instance.roadsPool;

        for (var i = 0; i <= slimeGenerateCounts - 1; i++)
        {
            bool canAdd = true;
            //get a random pos in allslime
            Vector3 tempPos = allSlimeGenerateTransform[Random.Range(0, allSlimeGenerateTransform.Count)].transform.position;

            if(slimePoolPerLoop != null)
            {
                for (var j = 0; j <= slimePoolPerLoop.Count - 1; j++)
                {
                    if (tempPos == slimePoolPerLoop[j].transform.position)
                    {
                        canAdd = false;
                        break;
                    }
                }
            }
            

            if (canAdd)
            {
                GameObject newObj = Instantiate<GameObject>(slime);
                newObj.transform.localPosition = tempPos;
                slimePoolPerLoop.Add(newObj);
            }
            else
            {
                i--;
            }
        }

        
    }

    public void SpwanWolf()
    {
        if(MapManager.instance.woodsPool.Count > 0)
        {
            for (var i = 0; i < MapManager.instance.woodsPool.Count; i++)
            {
                GameObject newObj = Instantiate<GameObject>(wolf);
                newObj.transform.localPosition = MapManager.instance.woodsPool[i].transform.position;
                wolfPoolPerLoop.Add(newObj);
            }
        }
    }

    public void Back_SavePlayerInfo()
    {
        string current_file_path =
            Application.dataPath +
            "/Logs/" +
            file_name;

        string fileLines = File.ReadAllText(current_file_path);
        string[] fileScores = fileLines.Split(',');

        int previousWood = int.Parse(fileScores[2]);
        int previousStone = int.Parse(fileScores[3]);
        fileScores[2] = (GameManager.instance.woodResources + previousWood).ToString();
        fileScores[3] = (GameManager.instance.rockResources + previousStone).ToString();

        string fileContent = fileScores[0] + "," + fileScores[1] + "," + fileScores[2] + "," + fileScores[3];

        File.WriteAllText(current_file_path, fileContent);
    }

    public void Dead_SavePlayerInfo()
    {

        string current_file_path =
        Application.dataPath +
        "/Logs/" +
        file_name;

        string fileLines = File.ReadAllText(current_file_path);
        string[] fileScores = fileLines.Split(',');

        int previousWood = int.Parse(fileScores[2]);
        int previousStone = int.Parse(fileScores[3]);

        fileScores[2] = (Mathf.FloorToInt(GameManager.instance.woodResources * .4f) + previousWood).ToString();
        fileScores[3] = (Mathf.FloorToInt(GameManager.instance.rockResources * .4f) + previousStone).ToString();

        string fileContent = fileScores[0] + "," + fileScores[1] + "," + fileScores[2] + "," + fileScores[3];

        File.WriteAllText(current_file_path, fileContent);

    }
}
