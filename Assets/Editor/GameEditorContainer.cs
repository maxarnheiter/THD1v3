using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public static class GameEditorContainer
{

    public static GameEditor gameEditor;

    static GameEditorContainer()
    {
        gameEditor = new GameEditor();
    }
}

