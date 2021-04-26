using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Buildings
{
    public int buildCost;

    public Stone(string name, string type, int buildCost):
        base(name, type)
    {
        this.buildCost = buildCost;
    }
    public override void OnBuild()
    {
        PlayerController.instance.currentHealth += 3;
        PlayerController.instance.maxHealth += 3;
        GameManager.instance.rockResources += 1;
        GameManager.instance.buildTimes -= buildCost;
    }

}
