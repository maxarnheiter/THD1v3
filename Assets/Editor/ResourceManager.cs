using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;


public class ResourceManager
{

    GameEditor gameEditor;

    public ResourceManager(GameEditor gameEditor)
    {
        this.gameEditor = gameEditor;

        if (gameEditor.instanceManager == null)
            ImportInstanceManager();

        if (gameEditor.prefabManager == null)
            ImportPrefabManager();

        if (gameEditor.spriteManager == null)
            ImportSpriteManager();
    }

    public void CreateInstanceManager()
    {
        var newObj = new GameObject("", typeof(InstanceManager));
        gameEditor.instanceManager = newObj.GetComponent<InstanceManager>();
    }

    public void CreateSpriteManager()
    {
        var newObj = new GameObject("", typeof(SpriteManager));
        gameEditor.spriteManager = newObj.GetComponent<SpriteManager>();
        LoadSprites(gameEditor.spriteManager);
    }

    public void CreatePrefabManager()
    {
        var newObj = new GameObject("", typeof(PrefabManager));
        gameEditor.prefabManager = newObj.GetComponent<PrefabManager>();
        LoadPrefabs(gameEditor.prefabManager);
        EditorUtility.SetDirty(gameEditor.prefabManager);
    }

    public void ImportInstanceManager()
    {
        if (GameObject.FindObjectOfType<InstanceManager>() != null)
        {
            gameEditor.instanceManager = GameObject.FindObjectOfType<InstanceManager>();
        }
    }

    public void ImportSpriteManager()
    {
        if (GameObject.FindObjectOfType<SpriteManager>() != null)
        {
            gameEditor.spriteManager = GameObject.FindObjectOfType<SpriteManager>();
        }
    }

    public void ImportPrefabManager()
    {
        if (GameObject.FindObjectOfType<PrefabManager>() != null)
        {
            gameEditor.prefabManager = GameObject.FindObjectOfType<PrefabManager>();
        }
    }

    public void LoadSprites(SpriteManager spriteManager)
    {
        var rawObjs = Resources.LoadAll("Sprites/");

        if (rawObjs == null || rawObjs.Count() <= 0)
        {
            Debug.Log("No sprites could be loaded.");
            return;
        }

        foreach (var obj in rawObjs)
        {
            Sprite sprite = obj as Sprite;

            if (sprite != null)
                spriteManager.Add(sprite.name, sprite);
        }

        Debug.Log("Loaded " + spriteManager.Count + " sprites.");

    }

    public void LoadPrefabs(PrefabManager prefabManager)
    {
        var rawObjs = Resources.LoadAll("Prefabs/");

        if (rawObjs == null || rawObjs.Count() <= 0)
        {
            Debug.Log("No prefab objects could be loaded.");
            return;
        }

        foreach (var obj in rawObjs)
        {
            Prefab prefab = (obj as GameObject).GetComponent<Prefab>();

            if (prefab != null)
                prefabManager.Add(prefab.id, prefab);
        }

        Debug.Log("Loaded " + prefabManager.Count + " prefabs.");
    }
}

