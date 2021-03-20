using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSaver : MonoBehaviour
{
    public string currentObjectTag;
    public GameObject currentObject;

    private void OnCollisionEnter2D(Collision2D other)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        currentObjectTag = other.gameObject.tag;
        currentObject = other.gameObject;
    }
}
