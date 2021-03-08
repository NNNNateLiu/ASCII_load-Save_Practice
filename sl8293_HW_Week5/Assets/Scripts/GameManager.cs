using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int buildTimes;
    public float autoSavingTimer;
    public Text buildTimersText;

    private float time;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= autoSavingTimer)
        {
            time = 0;
            MapManager.instance.SaveGame();
        }

        buildTimersText.text = "BuildTimer: " + buildTimes;

    }
}
