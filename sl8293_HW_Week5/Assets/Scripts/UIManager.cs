using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Text buildTimesText;
    public Text healthText;
    public Text logsCount;
    public Text stonesCount;
    public Text buttonText;
    public Text ArmorCount;

    public GameObject InGamePanel;
    public GameObject BadPanel;
    public GameObject HappyPanel;

    public Text happyLogs;
    public Text happyStones;
    public Text badLogs;
    public Text badStones;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        buildTimesText.text = GameManager.instance.buildTimes.ToString();
        healthText.text = PlayerController.instance.currentHealth + "/" + PlayerController.instance.maxHealth;
        logsCount.text = GameManager.instance.woodResources.ToString();
        stonesCount.text = GameManager.instance.rockResources.ToString();
        ArmorCount.text = PlayerController.instance.armor.ToString();

        if(PlayerController.instance.willReturn)
        {
            buttonText.text = "Back to Camp";
        }
        
        if(!PlayerController.instance.willReturn)
        {
            buttonText.text = "Keep Going";
        }
    }

    public void BackToCamp()
    {
        PlayerController.instance.willReturn = !PlayerController.instance.willReturn;
    }

    public void HappyEnd()
    {
        InGamePanel.SetActive(false);
        HappyPanel.SetActive(true);
        happyLogs.text = "Logs:" + GameManager.instance.woodResources.ToString();
        happyStones.text = "Stones:" + GameManager.instance.rockResources.ToString();
    }

    public void BadEnd()
    {
        InGamePanel.SetActive(false);
        BadPanel.SetActive(true);
        badLogs.text = "Logs:" + Mathf.FloorToInt(GameManager.instance.woodResources * .4f).ToString();
        badStones.text = "Stones:" + Mathf.FloorToInt(GameManager.instance.rockResources * .4f).ToString();
    }
}
