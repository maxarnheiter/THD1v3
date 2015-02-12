using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


class PrefabsUI : EditorUI
{

    GameEditor gameEditor;

    float sidebarWidth;
    float maincontentWidth;

    public PrefabsUI(GameEditor gameEditor)
    {
        this.gameEditor = gameEditor;
        sidebarWidth = 300f;
    }

    public override void Display(float width)
    {

        maincontentWidth = width - sidebarWidth;

        if (gameEditor.prefabManager == null)
            return;

        EditorGUILayout.BeginHorizontal();

            DisplaySideBar(sidebarWidth);

            DisplayMainContent(maincontentWidth);

        EditorGUILayout.EndHorizontal();
    }

    void DisplaySideBar(float width)
    {
        GUILayout.Label("Prefabs loaded: " + gameEditor.prefabManager.Count, GUILayout.Width(width));
    }

    void DisplayMainContent(float width)
    {

    }
}
