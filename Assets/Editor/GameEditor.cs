using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[InitializeOnLoad]
public static class GameEditor 
{

    static SceneInputHandler inputHandler;
    static SceneRenderer sceneRenderer;

    static InstanceManager instanceManager;
    static PrefabManager prefabManager;
    static SpriteManager spriteManager;

    static GameEditor()
    {
        inputHandler = new SceneInputHandler();
        sceneRenderer = new SceneRenderer();

        instanceManager = GameObject.FindObjectOfType<InstanceManager>();
        prefabManager = GameObject.FindObjectOfType<PrefabManager>();
        spriteManager = GameObject.FindObjectOfType<SpriteManager>();
    }





   
}
