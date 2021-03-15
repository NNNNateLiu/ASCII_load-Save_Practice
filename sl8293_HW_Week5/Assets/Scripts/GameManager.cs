using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //TODO: Pass to a file
    public int woodResources;
    public int rockResources;

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

    public List<GameObject> slimePoolPerLoop = new List<GameObject>();
    public List<GameObject> wolfPoolPerLoop = new List<GameObject>();

    private void Awake()
    {
        instance = this;
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
            Debug.Log(tempPos);

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
}
