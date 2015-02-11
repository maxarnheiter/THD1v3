using UnityEngine;
using System.Collections.Generic;

public class InstanceManager : MonoBehaviour 
{
    
    Dictionary<int, Instance> instances;

    public InstanceManager()
    {
        instances = new Dictionary<int, Instance>();
    }

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

    public int Count
    {
        get
        {
            if (instances == null)
                return 0;
            return instances.Count;
        }
    }
}
