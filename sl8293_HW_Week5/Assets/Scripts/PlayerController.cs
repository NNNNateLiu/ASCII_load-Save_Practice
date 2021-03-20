using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public List<Transform> wayPoints;
    //private int i = 0;
    //private bool canMoveToNext = true;
    public float timer;
    private float a;
    private float b;
    private float c;
    private float d;
    private float e;

    public bool willReturn;

    //player round info
    public int maxHealth = 20;
    public int currentHealth = 20;
    public int armor = 0;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        wayPoints = MapManager.instance.wayPoints;
        maxHealth = GameManager.instance.initMaxHealth;
        armor = GameManager.instance.initArmor;
        currentHealth = GameManager.instance.initMaxHealth;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 0 && timer <= 2)
        {
            a += 1f / 2f * Time.deltaTime;
            transform.position = Vector3.Lerp(MapManager.instance.startPos.position, wayPoints[0].position, a);
            e = 0;
        }
        if(timer > 2 && timer <= 10)
        {
            b += 1f / 8f * Time.deltaTime;
            transform.position = Vector3.Lerp(wayPoints[0].position, wayPoints[1].position, b);
            a = 0;
        }
        if(timer > 10 && timer <= 14)
        {
            c += 1f / 4f * Time.deltaTime;
            transform.position = Vector3.Lerp(wayPoints[1].position, wayPoints[2].position, c);
            b = 0;
        }
        if (timer > 14 && timer <= 22)
        {
            d += 1f / 8f * Time.deltaTime;
            transform.position = Vector3.Lerp(wayPoints[2].position, wayPoints[3].position, d);
            c = 0;
        }
        if(timer > 22 && timer <= 24)
        {
            e += 1f / 2f * Time.deltaTime;
            transform.position = Vector3.Lerp(wayPoints[3].position, MapManager.instance.startPos.position, e);
            d = 0;
        }
        if(timer > 24)
        {
            timer = 0;
        }

        if(currentHealth <= 0)
        {
            GameManager.instance.isDead = true;
            Destroy(gameObject);
        }

        if(GameManager.instance.isBack)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "slime")
        {
            Destroy(other.gameObject);
            GameManager.instance.slimePoolPerLoop.Remove(other.gameObject);
            currentHealth -= (2 - Mathf.FloorToInt(armor * .2f));

            int dropType = Random.Range(1, 5);
            string strDrop = dropType.ToString();
            char[] chars = strDrop.ToCharArray();

            switch(chars[0])
            {
                case '1':
                    GameManager.instance.woodResources += 1;
                    GameManager.instance.rockResources += 1;
                    GameManager.instance.buildTimes += 1;
                    break;
                case '2':
                    GameManager.instance.rockResources += 2;
                    GameManager.instance.buildTimes += 1;
                    break;
                case '3':
                    GameManager.instance.woodResources += 2;
                    GameManager.instance.buildTimes += 1;
                    break;
                case '4':
                    GameManager.instance.rockResources += 1;
                    GameManager.instance.buildTimes += 2;
                    break;
                case '5':
                    GameManager.instance.rockResources += 1;
                    GameManager.instance.buildTimes += 2;
                    break;
                default:
                    break;
            }
        }

        if (other.gameObject.tag == "wolf")
        {
            Destroy(other.gameObject);
            GameManager.instance.wolfPoolPerLoop.Remove(other.gameObject);
            currentHealth -= (5 - Mathf.FloorToInt(armor * .2f)); ;

            int dropType = Random.Range(1, 5);
            string strDrop = dropType.ToString();
            char[] chars = strDrop.ToCharArray();

            switch (chars[0])
            {
                case '1':
                    GameManager.instance.woodResources += 2;
                    GameManager.instance.rockResources += 2;
                    GameManager.instance.buildTimes += 2;
                    break;
                case '2':
                    GameManager.instance.rockResources += 4;
                    GameManager.instance.buildTimes += 2;
                    break;
                case '3':
                    GameManager.instance.woodResources += 4;
                    GameManager.instance.buildTimes += 2;
                    break;
                case '4':
                    GameManager.instance.rockResources += 2;
                    GameManager.instance.buildTimes += 4;
                    break;
                case '5':
                    GameManager.instance.rockResources += 2;
                    GameManager.instance.buildTimes += 4;
                    break;
                default:
                    break;
            }
        }
        
        if (other.gameObject.tag == "camp")
        {
            if(willReturn)
            {
                GameManager.instance.isBack = true;
            }
            else
            {
                int healthModifier = Mathf.FloorToInt(maxHealth * .4f);
                if (currentHealth + healthModifier >= maxHealth)
                {
                    currentHealth = maxHealth;
                }
                else
                {
                    currentHealth = currentHealth + healthModifier;
                }
            }
        }
    }


}
