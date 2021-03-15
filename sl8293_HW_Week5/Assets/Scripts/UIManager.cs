using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text buildTimesText;
    public Text healthText;
    public Text logsCount;
    public Text stonesCount;

    private void Update()
    {
        buildTimesText.text = GameManager.instance.buildTimes.ToString();
        healthText.text = PlayerController.instance.maxHealth + "/" + PlayerController.instance.currentHealth;
        logsCount.text = GameManager.instance.woodResources.ToString();
        stonesCount.text = GameManager.instance.rockResources.ToString();
    }
}
