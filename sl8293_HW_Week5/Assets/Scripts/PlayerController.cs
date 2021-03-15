using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public List<Transform> wayPoints;
    private int i = 0;
    private bool canMoveToNext = true;
    public float timer;

    private void Start()
    {
        wayPoints = MapManager.instance.wayPoints;
        DOTween.defaultEaseType = Ease.Linear;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 0 && timer <= 3)
        {
            //transform.position = Vector3.Lerp(MapManager.instance.startPos.position, wayPoints[0].position, 3);
            transform.DOMove(wayPoints[0].position, 3, false);
        }
        if(timer > 3 && timer <= 9)
        {
            transform.DOMove(wayPoints[1].position, 6, false);
        }
        if(timer > 9 && timer <= 16)
        {
            transform.DOMove(wayPoints[2].position, 6, false);
        }
        if (timer > 16 && timer <= 22)
        {
            transform.DOMove(wayPoints[3].position, 6, false);
            
        }
        if(timer > 22)
        {
            timer = 0;
        }
    }

    private void Movement(int a)
    {

        canMoveToNext = true;
    }

}
