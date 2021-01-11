using UnityEngine;
using System.Collections;
using UnityEditor;

namespace DG.DemiLib
{
	public static class DeEditorPanelUtils
	{
		public static T ConnectToSourceAsset<T>(string adbFilePath, bool createIfMissing=false) where T: ScriptableObject
		{
			if (!DeEditorFileUtils.AssetExists (adbFilePath)) 
			{
				if (!createIfMissing) 
				{
					return default(T);
				}
				CreateScriptableAsset<T> (adbFilePath);
			}
			T local = (T)AssetDatabase.LoadAssetAtPath(adbFilePath,typeof(T));
			if (local == null) 
			{
				CreateScriptableAsset<T> (adbFilePath);
				local = (T)AssetDatabase.LoadAssetAtPath (adbFilePath, typeof(T));
			}
			return local;
		}
		private static void CreateScriptableAsset<T>(string adbFilePath) where T:ScriptableObject
		{
			AssetDatabase.CreateAsset (ScriptableObject.CreateInstance<T> (), adbFilePath);
		}
	}
}
