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

    public GameObject creatureOrigin;
    public GameObject player;
    public GameObject slime;
    public int slimeGenerateCounts;

    private float time;

    public bool generateSlimes = false;

    public List<GameObject> slimePoolPerLoop = new List<GameObject>();

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
        time += Time.deltaTime;
        if (time >= autoSavingTimer)
        {
            time = 0;
            MapManager.instance.SaveGame();
        }

        buildTimersText.text = "BuildTimer: " + buildTimes;

        if(generateSlimes)
        {
            SpwanSlimes();
            generateSlimes = false;
        }
    }

    public void SpwanSlimes()
    {
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
}
