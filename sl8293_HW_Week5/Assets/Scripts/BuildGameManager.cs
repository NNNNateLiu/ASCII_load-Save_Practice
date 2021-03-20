using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BuildGameManager : MonoBehaviour
{
    public static BuildGameManager instance;
    public string file_name;

    public int woodResources;
    public int rockResources;
    public int startMaxHealth;
    public int startArmor;

    private float autoSavingTimer = 5;
    private float autoSavingClock = 0;

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

        startMaxHealth = int.Parse(fileScores[0]);
        startArmor = int.Parse(fileScores[1]);
        woodResources = int.Parse(fileScores[2]);
        rockResources = int.Parse(fileScores[3]);
    }

    public void Update()
    {
        if(autoSavingClock < autoSavingTimer)
        {
            autoSavingClock += Time.deltaTime;
        }
        else
        {
            autoSavingClock = 0;
            Debug.Log("autoSave");
        }
    }
    public void SavePlayerInfo()
    {
        string current_file_path =
            Application.dataPath +
            "/Logs/" +
            "SavedPlayerInfo.txt";

        string fileLines = File.ReadAllText(current_file_path);
        string[] fileScores = fileLines.Split(',');

        fileScores[0] = (BuildGameManager.instance.startMaxHealth).ToString();
        fileScores[1] = (BuildGameManager.instance.startArmor).ToString();
        fileScores[2] = (BuildGameManager.instance.woodResources).ToString();
        fileScores[3] = (BuildGameManager.instance.rockResources).ToString();

        string fileContent = fileScores[0] + "," + fileScores[1] + "," + fileScores[2] + "," + fileScores[3];

        File.WriteAllText(current_file_path, fileContent);
    }
}
