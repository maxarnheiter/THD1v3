using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class PrefabManager : MonoBehaviour
{

    Dictionary<int, Prefab> prefabs;

    public PrefabManager()
    {
        prefabs = new Dictionary<int, Prefab>();
    }

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

    public int Count
    {
        get
        {
            if (prefabs == null)
                return 0;
            return prefabs.Count;
        }
    }

    public void Add(int id, Prefab prefab)
    {
        if (!prefabs.ContainsKey(id))
            prefabs.Add(id, prefab);
        else
            Debug.Log("Attempted to add prefab with duplicate id: " + id);
    }
   
    
}
