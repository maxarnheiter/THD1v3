using UnityEngine;
using UnityEditor;


class SpriteWizard : EditorWindow
{

    static void Init() 
	{
		SpriteWizard spriteWizard = (SpriteWizard)EditorWindow.GetWindow (typeof(SpriteWizard));
	}
	
	public SpriteWizard()
	{
	
	}

    public void OnGUI()
    {
    }

}

