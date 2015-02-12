using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class PrefabManager : MonoBehaviour, ISerializationCallbackReceiver
{

    [SerializeField] List<int> keys;
    [SerializeField] List<Prefab> values;

    public Dictionary<int, Prefab> prefabs;

    public PrefabManager()
    {
        keys = new List<int>();
        values = new List<Prefab>();
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

    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();

        foreach(var kvp in prefabs)
        {
            keys.Add(kvp.Key);
            values.Add(kvp.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        prefabs = new Dictionary<int, Prefab>();
        for (int i = 0; i != Mathf.Min(keys.Count, values.Count); i++)
            prefabs.Add(keys[i], values[i]);
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
