using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
using UnityEditor;
[InitializeOnLoad]
#endif
public class AutoSave : MonoBehaviour 
{
	#if UNITY_EDITOR
	static AutoSave()
	{
		EditorApplication.playmodeStateChanged = () =>
		{
			if(EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
			{
				Debug.Log("Auto-Saving scene before entering Play mode: " + EditorApplication.currentScene);
				EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
				AssetDatabase.SaveAssets();
			}
		};
	}
	#endif
}
