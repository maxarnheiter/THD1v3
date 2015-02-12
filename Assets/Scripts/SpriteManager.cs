using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SpriteManager : MonoBehaviour, ISerializationCallbackReceiver
{

    [SerializeField] List<string> keys;
    [SerializeField] List<Sprite> values;

    public Dictionary<string, Sprite> sprites;

    public SpriteManager()
    {
        keys = new List<string>();
        values = new List<Sprite>();
        sprites = new Dictionary<string, Sprite>();
    }

    void Reset()
    {
        this.gameObject.name = "Sprite Manager";
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

        foreach (var kvp in sprites)
        {
            keys.Add(kvp.Key);
            values.Add(kvp.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        sprites = new Dictionary<string, Sprite>();
        for (int i = 0; i != Mathf.Min(keys.Count, values.Count); i++)
            sprites.Add(keys[i], values[i]);
    }

    public int Count
    {
        get
        {
            if (sprites == null)
                return 0;
            return sprites.Count;
        }
    }

    public void Add(string spriteName, Sprite sprite)
    {
        if (!sprites.ContainsKey(spriteName))
            sprites.Add(spriteName, sprite);
        else
            Debug.Log("Attempted to add sprite with duplicate name: " + spriteName);
    }

    

   
}