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
    private float a;
    private float b;
    private float c;
    private float d;
    private float e;


    private void Start()
    {
        wayPoints = MapManager.instance.wayPoints;
        DOTween.defaultEaseType = Ease.Linear;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 0 && timer <= 1)
        {
            a += 1f / 1f * Time.deltaTime;
            transform.position = Vector3.Lerp(MapManager.instance.startPos.position, wayPoints[0].position, a);
            e = 0;
        }
        if(timer > 1 && timer <= 5)
        {
            b += 1f / 4f * Time.deltaTime;
            transform.position = Vector3.Lerp(wayPoints[0].position, wayPoints[1].position, b);
            a = 0;
        }
        if(timer > 5 && timer <= 7)
        {
            c += 1f / 2f * Time.deltaTime;
            transform.position = Vector3.Lerp(wayPoints[1].position, wayPoints[2].position, c);
            b = 0;
        }
        if (timer > 7 && timer <= 11)
        {
            d += 1f / 4f * Time.deltaTime;
            transform.position = Vector3.Lerp(wayPoints[2].position, wayPoints[3].position, d);
            c = 0;
        }
        if(timer > 11 && timer <= 12)
        {
            e += 1f / 1f * Time.deltaTime;
            transform.position = Vector3.Lerp(wayPoints[3].position, MapManager.instance.startPos.position, e);
            d = 0;
        }
        if(timer > 12)
        {
            timer = 0;
        }
    }

    private void Movement(int a)
    {

        canMoveToNext = true;
    }

    private void OnTriggerEnter2D(Collider2D creature)
    {
        if(creature.gameObject.tag == "slime")
        {
            Destroy(creature.gameObject);
            GameManager.instance.slimePoolPerLoop.Remove(creature.gameObject);
        }

        if (creature.gameObject.tag == "wolf")
        {
            Destroy(creature.gameObject);
            GameManager.instance.slimePoolPerLoop.Remove(creature.gameObject);
        }
    }

}
