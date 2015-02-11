using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

class SceneInputHandler
{
    public SceneInputHandler()
    {
        SceneView.onSceneGUIDelegate -= HandleInput;
        SceneView.onSceneGUIDelegate += HandleInput;
    }

    void HandleInput(SceneView sceneView)
    {

    }
}

