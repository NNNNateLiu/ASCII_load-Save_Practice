using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings
{
    public string name;
    public string type;

    //this is the base constructor
    public Buildings(string name, string type)
    {
        this.name = name;
        type = "default buildings";
    }

    //this is a virtual function, that can be overridden
    public virtual void OnBuild()
    {
        
    }
}
