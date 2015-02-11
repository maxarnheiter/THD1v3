﻿using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class GameEditorWindow : EditorWindow 
{

    EditorUI currentUI;

    QuickbarUI quickbarUI;
    ResourcesUI resourcesUI;
    PrefabsUI prefabsUI;
    SpritesUI spritesUI;

    float resourcesButtonWidth = 100f;
    float prefabsButtonWidth = 80f;
    float spritesButtonWidth = 80f;



    [MenuItem("THD/Game Editor")]
    static void Init()
    {
        GameEditorWindow gameEditorWindow = (GameEditorWindow)EditorWindow.GetWindow(typeof(GameEditorWindow));
    }

    void OnEnable()
    {
        title = "Game Editor";
        quickbarUI = new QuickbarUI();
        resourcesUI = new ResourcesUI();
        prefabsUI = new PrefabsUI();
        spritesUI = new SpritesUI();

        currentUI = resourcesUI;
    }

    void OnGUI()
    {
        float width = this.position.width;

        quickbarUI.Display(width);

        SelectionBar();

        currentUI.Display(width);
      
    }

    void SelectionBar()
    {
        EditorGUILayout.BeginHorizontal();

        GUILayout.FlexibleSpace();

            if (currentUI == resourcesUI)
                GUI.enabled = false;
            if (GUILayout.Button("Resources"))
                currentUI = resourcesUI;
            GUI.enabled = true;

            if (currentUI == spritesUI)
                GUI.enabled = false;
            if (GUILayout.Button("Sprites"))
                currentUI = spritesUI;
            GUI.enabled = true;

            if (currentUI == prefabsUI)
                GUI.enabled = false;
            if (GUILayout.Button("Prefabs"))
                currentUI = prefabsUI;
            GUI.enabled = true;

        GUILayout.FlexibleSpace();

        EditorGUILayout.EndHorizontal();
    }
}