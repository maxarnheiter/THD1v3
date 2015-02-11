using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SpriteManager : MonoBehaviour
{

    Dictionary<string, Texture2D> sprites;

    public SpriteManager()
    {
        sprites = new Dictionary<string, Texture2D>();
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

    public int Count
    {
        get
        {
            if (sprites == null)
                return 0;
            return sprites.Count;
        }
    }

    public void Add(string spriteName, Texture2D texture)
    {
        if (!sprites.ContainsKey(spriteName))
            sprites.Add(spriteName, texture);
        else
            Debug.Log("Attempted to add sprite with duplicate name: " + spriteName);
    }

    

   
}