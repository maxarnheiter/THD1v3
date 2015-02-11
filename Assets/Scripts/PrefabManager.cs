using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class PrefabManager : MonoBehaviour
{

    Dictionary<int, Prefab> prefabs;

    void Reset()
    {
        if (this.gameObject.name != "Prefab Manager")
            this.gameObject.name = "Prefab Manager";
    }

    void Start()
    {

    }

    void Update()
    {

    }

    
}
