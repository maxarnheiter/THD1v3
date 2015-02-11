using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;


class ResourcesUI : EditorUI
{
    GameEditor gameEditor;

    float sidebarWidth;
    float maincontentWidth;

    float findButtonWidth;
    float newButtonWidth;

    public ResourcesUI(GameEditor gameEditor)
    {
        this.gameEditor = gameEditor;

        findButtonWidth = 50f;
        newButtonWidth = 50f;
    }

    public override void Display(float width)
    {
        sidebarWidth = 300f;
        maincontentWidth = width - sidebarWidth;

        EditorGUILayout.BeginHorizontal();

            DisplaySideBar(sidebarWidth);

            DisplayMainContent(maincontentWidth);

        EditorGUILayout.EndHorizontal();
    }
    
    void DisplaySideBar(float width)
    {
        EditorGUILayout.BeginVertical();

        #region GameEditor.InstanceManager
        EditorGUILayout.BeginHorizontal();

            gameEditor.instanceManager = EditorGUILayout.ObjectField(gameEditor.instanceManager, typeof(InstanceManager), true, GUILayout.Width(width - findButtonWidth - newButtonWidth)) as InstanceManager;

            if (gameEditor.instanceManager != null)
                GUI.enabled = false;
            if (GUILayout.Button("Find", GUILayout.Width(findButtonWidth)))
            {
                gameEditor.resourceManager.ImportInstanceManager();
            }
            GUI.enabled = true;

            if (gameEditor.instanceManager != null)
                GUI.enabled = false;
            if (GUILayout.Button("New", GUILayout.Width(findButtonWidth)))
            {
                gameEditor.resourceManager.CreateInstanceManager();
            }
            GUI.enabled = true;

        EditorGUILayout.EndHorizontal();
        #endregion

        #region gameEditor.prefabManager
        EditorGUILayout.BeginHorizontal();

        gameEditor.prefabManager = EditorGUILayout.ObjectField(gameEditor.prefabManager, typeof(PrefabManager), true, GUILayout.Width(width - findButtonWidth - newButtonWidth)) as PrefabManager;

        if (gameEditor.prefabManager != null)
            GUI.enabled = false;
        if (GUILayout.Button("Find", GUILayout.Width(findButtonWidth)))
        {
            gameEditor.resourceManager.ImportPrefabManager();
        }
        GUI.enabled = true;

        if (gameEditor.prefabManager != null)
            GUI.enabled = false;
        if (GUILayout.Button("New", GUILayout.Width(findButtonWidth)))
        {
            gameEditor.resourceManager.CreatePrefabManager();
        }
        GUI.enabled = true;

        EditorGUILayout.EndHorizontal();
        #endregion

        #region gameEditor.spriteManager
        EditorGUILayout.BeginHorizontal();

        gameEditor.spriteManager = EditorGUILayout.ObjectField(gameEditor.spriteManager, typeof(SpriteManager), true, GUILayout.Width(width - findButtonWidth - newButtonWidth)) as SpriteManager;

        if (gameEditor.spriteManager != null)
            GUI.enabled = false;
        if (GUILayout.Button("Find", GUILayout.Width(findButtonWidth)))
        {
            gameEditor.resourceManager.ImportSpriteManager();
        }
        GUI.enabled = true;

        if (gameEditor.spriteManager != null)
            GUI.enabled = false;
        if (GUILayout.Button("New", GUILayout.Width(findButtonWidth)))
        {
            gameEditor.resourceManager.CreateSpriteManager();
        }
        GUI.enabled = true;

        EditorGUILayout.EndHorizontal();
        #endregion


        EditorGUILayout.EndVertical();
    }


 

    void DisplayMainContent(float width)
    {

    }
     
}
