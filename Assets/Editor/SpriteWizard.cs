using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;


class SpriteWizard : EditorWindow
{

    List<KeyValuePair<string, Sprite>> selectionList;
    KeyValuePair<string, Sprite> selectionNode;
    int selectionIndex;

    PrefabManager prefabManager;
    Dictionary<string, Texture2D> textureCache;

    int nextSetId = 0;

    PrefabType nextPrefabType;
    PrefabCategory nextPrefabCategory;
    PrefabColor nextPrefabColor;

    bool hasError;
    List<string> errorText;

    bool hasCompleted;
    string completionText;

    bool hasBoxCollider;
    bool isTrigger;

    static void Init() 
	{
		SpriteWizard spriteWizard = (SpriteWizard)EditorWindow.GetWindow (typeof(SpriteWizard));
	}

    public SpriteWizard(List<KeyValuePair<string, Sprite>> selection, Dictionary<string, Texture2D> TextureCache, PrefabManager PrefabManager)
	{
	    selectionList = selection;
		selectionNode = selection[0];
        textureCache = TextureCache;
        prefabManager = PrefabManager;
	}


	public void OnGUI()
	{
		GUILayout.Label ("Sprites in selection: " + selectionList.Count());
		
		EditorGUILayout.BeginHorizontal();
		
		GUILayout.FlexibleSpace();
		
			if(GUILayout.Button ("<", GUILayout.Width (30f)))
				selectionIndex--;
			
		if(GUILayout.Button (">", GUILayout.Width (30f)))
				selectionIndex++;
				
		GUILayout.FlexibleSpace();
		
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		
			selectionNode = selectionList[Mathf.Abs(selectionIndex) % selectionList.Count];

            Texture2D texture;
            textureCache.TryGetValue(selectionNode.Key, out texture);

            GUILayout.Button(texture, GUILayout.Width(texture.width), GUILayout.Height(texture.height));
		
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		
			GUILayout.Label (selectionNode.Key);
			
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.Space();
		
		EditorGUILayout.BeginHorizontal();
		
		nextSetId = EditorGUILayout.IntField("Set ID: ", nextSetId);
		
		if(GUILayout.Button("Get Next ID", GUILayout.Width (85f)))
			nextSetId = GetNextSetID();
		
		EditorGUILayout.EndHorizontal();

		nextPrefabType = (PrefabType)EditorGUILayout.EnumPopup ("Type: ", nextPrefabType);
		
		nextPrefabCategory = (PrefabCategory)EditorGUILayout.EnumPopup("Prefab Category: ", nextPrefabCategory);
		
		nextPrefabColor = (PrefabColor)EditorGUILayout.EnumPopup("Prefab Color: ", nextPrefabColor);
		
		hasBoxCollider = EditorGUILayout.Toggle("BoxCollider2D", hasBoxCollider);
		isTrigger = EditorGUILayout.Toggle ("isTrigger", isTrigger);
		
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		
		if(hasError)
		{
			GUIStyle style = new GUIStyle(EditorStyles.label);
			style.normal.textColor = Color.red;
			
			foreach(var error in errorText)
			{
				EditorGUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
					GUILayout.Label(error, style);
				GUILayout.FlexibleSpace();
				EditorGUILayout.EndHorizontal();
			}	
		}
		
		if(hasCompleted)
		{
			EditorGUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			
				GUIStyle style = new GUIStyle(EditorStyles.label);
				style.normal.textColor = Color.blue;
				GUILayout.Label(completionText, style);
				
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();
		}
		
		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		
			if(GUILayout.Button ("Make", GUILayout.Width (50f)))
				Make ();
				
			if(GUILayout.Button ("Close", GUILayout.Width (50f)))
				this.Close();
		
		EditorGUILayout.EndHorizontal();
	}
	
	void Make()
	{
		if(CheckForErrors())
			return;
			
		int nextPrefabId = GetNextPrefabID();
		
		var newObjects = new List<GameObject>();
		
		//Make all the new game objects
		foreach(var spriteKVP in selectionList)
		{
			var newObject = MakeGameObject(spriteKVP, nextPrefabId);
			
			if(newObject == null)
			{
				Debug.Log ("Failed to make game object during prefab creation wizard.");
				return;
			}
			newObjects.Add(newObject);
			nextPrefabId++;
		}
		
		//Save all the game objects as prefabs
		foreach(var newObject in newObjects)
		{
			PrefabUtility.CreatePrefab("Assets/Resources/Prefabs/" + newObject.name + ".prefab", newObject);
		}
		
		//Destroy the game objects in heirarchy
		foreach(var newObject in newObjects)
		{
			DestroyImmediate(newObject);
		}
		
		hasCompleted = true;
		completionText = selectionList.Count.ToString() + " prefabs were created successfully."; 
	}
	
	GameObject MakeGameObject(KeyValuePair<string, Sprite> spriteKVP, int nextPrefabId)
	{
		var newObject = new GameObject();
		newObject.name = nextPrefabId.ToString();
		
		//Sprite Renderer
		var spriteRenderer = newObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteKVP.Value;
		
		//Stack Component
		newObject.AddComponent<Stack>();
		
		//Box Collider 2D
		if(hasBoxCollider)
		{
			var boxCollider = newObject.AddComponent<BoxCollider2D>();
			boxCollider.size = new Vector2(1f, 1f);
			boxCollider.center = new Vector2(-0.5f, 0.5f);
		}
		
		//Prefab Component
		var prefab = newObject.AddComponent<Prefab>();
		prefab.id = nextPrefabId;
        prefab.spriteName = spriteKVP.Value.name;
        prefab.spriteWidth = (int)spriteKVP.Value.rect.width;
        prefab.spriteHeight = (int)spriteKVP.Value.rect.height;
		prefab.prefabType = nextPrefabType;
		prefab.prefabCategory = nextPrefabCategory;
		prefab.prefabColor = nextPrefabColor;
		
		return newObject;
	}
	
	bool CheckForErrors()
	{
		hasError = false;	
		errorText = new List<string>();
		
		if(nextPrefabType == PrefabType.Any)
		{
			hasError = true;
			errorText.Add("Prefab Type cannot be Any.");
		}
		
		if(nextPrefabCategory == PrefabCategory.Any)
		{
			hasError = true;
			errorText.Add("Prefab Category cannot be Any.");
		}
			
		if(nextPrefabColor == PrefabColor.Any)
		{
			hasError = true;
			errorText.Add ("Prefab Color cannot be Any.");
		}
		
		return hasError;	
	}
	
	int GetNextSetID()
	{
		int nextId = 0;
	
		if(prefabManager.prefabs == null)
			return nextId;

        if (prefabManager.prefabs.Count == 0)
			return nextId;

        nextId = prefabManager.prefabs.Max(p => p.Value.setId) + 1;
		
		return nextId + 1;
	}
	
	int GetNextPrefabID()
	{
		int nextId = 0;

        if (prefabManager.prefabs == null)
			return nextId;

        if (prefabManager.prefabs.Count == 0)
			return nextId;

        nextId = prefabManager.prefabs.Max(p => p.Value.id) + 1;
		
		return nextId + 1;
	}

}

