using UnityEngine;
using System.Collections.Generic;

public class InstanceManager : MonoBehaviour 
{

    Dictionary<int, Instance> instances;

    void Reset()
    {
        if (this.gameObject.name != "Instance Manager")
            this.gameObject.name = "Instance Manager";
    }

	void Start () 
    {

	}
	

	void Update () 
    {
	
	}
}
