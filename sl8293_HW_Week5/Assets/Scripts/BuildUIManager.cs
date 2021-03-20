using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BuildUIManager : MonoBehaviour
{
    public static BuildUIManager instance;

    public Text healthText;
    public Text logsCount;
    public Text stonesCount;
    public Text buttonText;
    public Text ArmorCount;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        healthText.text = BuildGameManager.instance.startMaxHealth.ToString();
        logsCount.text = BuildGameManager.instance.woodResources.ToString();
        stonesCount.text = BuildGameManager.instance.rockResources.ToString();
        ArmorCount.text = BuildGameManager.instance.startArmor.ToString();

    }

    public void StartNewRound()
    {
        BuildGameManager.instance.SavePlayerInfo();
        SceneManager.LoadScene(0);
    }
}
