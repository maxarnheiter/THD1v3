using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;


class SpritesUI : EditorUI
{
    SpriteManager spriteManager;
    PrefabManager prefabManager;

    float sidebarWidth;
    float maincontentWidth;
    float scrollbarAdjust;

    SpriteWizard wizard;

    Vector2 scrollviewSize;

    bool showOnlyUnused;

    float buttonPadding;

    Dictionary<string, Texture2D> textureCache;

    List<KeyValuePair<string, Sprite>> spriteSelection;

    public SpritesUI(SpriteManager SpriteManager, PrefabManager PrefabManager)
    {
        spriteManager = SpriteManager;
        prefabManager = PrefabManager;

        sidebarWidth = 300f;
        scrollviewSize = Vector2.zero;
        buttonPadding = 12f;
        scrollbarAdjust = 40f;
        spriteSelection = new List<KeyValuePair<string, Sprite>>();

        textureCache = new Dictionary<string, Texture2D>();

        BuildTextureCache();
    }

    public override void Display(float width)
    {
        maincontentWidth = width - sidebarWidth;

        EditorGUILayout.BeginHorizontal();

            DisplaySidebarUI(sidebarWidth);

            DisplayMaincontentUI(maincontentWidth - scrollbarAdjust);

        EditorGUILayout.EndHorizontal();
    }

    void DisplaySidebarUI(float width)
    {
        EditorGUILayout.BeginVertical();

            GUILayout.Label("Sprites loaded: " + spriteManager.Count, GUILayout.Width(width));

            EditorGUILayout.BeginHorizontal();

            if(GUILayout.Button("Clear Selection", GUILayout.Width(width / 2)))
            {
                spriteSelection.Clear();
            }

            if(GUILayout.Button("Open Wizard", GUILayout.Width(width / 2)))
            {
                if (wizard == null)
                {
                    wizard = new SpriteWizard(spriteSelection, textureCache, prefabManager);
                    wizard.Show();
                }

            }

            EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
    }

    void DisplayMaincontentUI(float width)
    {
        EditorGUILayout.BeginVertical();

            GUILayout.Label("", GUILayout.Width(width));

            scrollviewSize = EditorGUILayout.BeginScrollView(scrollviewSize);

                int currentWidth = 0;

                EditorGUILayout.BeginHorizontal();
        
                    foreach (var spriteKVP in spriteManager.sprites)
                    {
                        if (currentWidth >= width)
                        {
                            currentWidth = 0;
                            GUILayout.EndHorizontal();
                            GUILayout.BeginHorizontal();
                        }

                        if (!IsFiltered(spriteKVP))
                        {
                            DisplaySpriteButton(spriteKVP);

                            currentWidth = (int)(currentWidth + spriteKVP.Value.rect.width + (2 * buttonPadding));
                        }
                    
                    }
        
                EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndScrollView();

        EditorGUILayout.EndVertical();
    }

    void DisplaySpriteButton(KeyValuePair<string, Sprite> spriteKVP)
    {

        if (spriteSelection.Contains(spriteKVP))
            GUI.enabled = false;

        Texture2D texture;
        textureCache.TryGetValue(spriteKVP.Key, out texture);

        if(texture != null)
        if (GUILayout.Button(texture, GUILayout.Width(texture.width + buttonPadding), GUILayout.Height(texture.height + buttonPadding)))
        {
            spriteSelection.Add(spriteKVP);
        }

        GUI.enabled = true;
    }


    bool IsFiltered(KeyValuePair<string, Sprite> spriteKVP)
    {
        if (!showOnlyUnused)
            return false;

        if (prefabManager.prefabs == null || prefabManager.Count == 0)
            return false;

        if (prefabManager.prefabs.Any(p => p.Value.spriteName == spriteKVP.Key))
            return true;

        return false;
    }

    void BuildTextureCache()
    {

        foreach(var spriteKVP in spriteManager.sprites)
        {
            Sprite sprite = spriteKVP.Value;

            var texture = new Texture2D ((int)sprite.rect.width, (int)sprite.rect.height);
		
		    texture.SetPixels (sprite.texture.GetPixels ((int)sprite.rect.x, (int)sprite.rect.y, (int)sprite.rect.width, (int)sprite.rect.height));
		    texture.alphaIsTransparency = true;
		    texture.Apply ();

            textureCache.Add(spriteKVP.Key, texture);
        }
    }
}

