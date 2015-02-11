using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SpriteManager : MonoBehaviour
{

    Dictionary<string, Texture2D> sprites;

    void Reset()
    {
        if (this.gameObject.name != "Sprite Manager")
            this.gameObject.name = "Sprite Manager";
    }

    void Start()
    {

    }

    void Update()
    {

    }

   
}