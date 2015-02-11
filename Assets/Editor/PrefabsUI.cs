using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


class PrefabsUI : EditorUI
{
    public override void Display(float width)
    {
        GUILayout.Button("Prefabs");
    }
}
