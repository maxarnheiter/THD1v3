using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


class ResourcesUI : EditorUI
{
    public override void Display(float width)
    {
        GUILayout.Button("Resources");
    }
}
