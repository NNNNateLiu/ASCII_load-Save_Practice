using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildOnClickCollider : MonoBehaviour
{
    private void OnMouseDown()
    {
        BuildMapManager.instance.Build(transform,gameObject);
    }
}
