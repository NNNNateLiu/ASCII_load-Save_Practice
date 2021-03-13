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

    public GameObject player;

    public GameObject slime;
    public int slimeGenerateCounts;

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
        if (time >= autoSavingTimer)
        {
            time = 0;
            MapManager.instance.SaveGame();
        }

        buildTimersText.text = "BuildTimer: " + buildTimes;

    }

    public void SpwanSlimes()
    {
        //TODO: generate slimes on Roads tiles
        List<Vector3> slimeGenerateTransform = new List<Vector3>();

        for (var i = 0; i <= slimeGenerateCounts; i++)
        {
            GameObject newObj = Instantiate<GameObject>(slime);
            Vector3 tempPos = MapManager.instance.
                roadsPool[Random.Range(0, MapManager.instance.roadsPool.Count)].transform.position;
            slimeGenerateTransform.Add(tempPos);
            //newObj.transform.localPosition = 
        }
    }
}
