using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickCollider : MonoBehaviour
{
    private void OnMouseDown()
    {
        MapManager.instance.Build(transform,gameObject);
    }
}
