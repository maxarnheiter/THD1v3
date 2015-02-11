using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class GameEditor
{

    public EditorState editorState;

    public SceneInputHandler inputHandler;
    public SceneRenderer sceneRenderer;

    public InstanceManager instanceManager;
    public PrefabManager prefabManager;
    public SpriteManager spriteManager;

    public ResourceManager resourceManager;

    public GameEditor()
    {
        editorState = new EditorState();

        inputHandler = new SceneInputHandler();
        sceneRenderer = new SceneRenderer();

        resourceManager = new ResourceManager(this);
    }
}
