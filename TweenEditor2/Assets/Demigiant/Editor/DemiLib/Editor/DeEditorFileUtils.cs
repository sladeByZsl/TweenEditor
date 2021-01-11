using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

namespace DG.DemiLib
{
	public static class DeEditorFileUtils
	{

		private static string _fooProjectPath;
		public static readonly string ADBPathSlash;
		public static readonly string ADBPathSlashToReplace;
		public static readonly string PathSlash;
		public static readonly string PathSlashToReplace;

		public static string projectPath
		{
			get
			{
				if (_fooProjectPath == null) 
				{
					_fooProjectPath = Application.dataPath;
					_fooProjectPath = _fooProjectPath.Substring (0, _fooProjectPath.LastIndexOf (ADBPathSlash));
					_fooProjectPath = _fooProjectPath.Replace (ADBPathSlash, PathSlash);
				}
				return _fooProjectPath;
			}
		}

		public static string assetsPath
		{
			get{ return (projectPath + PathSlash + "Assets"); }
		}

		static DeEditorFileUtils()
		{
			ADBPathSlash = "/";
			ADBPathSlashToReplace = @"\";
			bool flag = Application.platform == RuntimePlatform.WindowsEditor;
			PathSlash = flag ? @"\" : "/";
			PathSlashToReplace = flag ? "/" : @"\";
		}

		public static string ADBPathToFullPath(string adbPath)
		{
			adbPath = adbPath.Replace (ADBPathSlash, PathSlash);
			return (projectPath + PathSlash + adbPath);
		}

		public static bool AssetExists(string adbPath)
		{
			string path = ADBPathToFullPath (adbPath);
			return (File.Exists (path) || Directory.Exists (path));
		}

		public static int GetScriptExecutionOrder(MonoBehaviour monobehaviour)
		{
			return MonoImporter.GetExecutionOrder (MonoScript.FromMonoBehaviour (monobehaviour));
		}

		public static string GUIDToExistingAssetPath(string guid)
		{
			if (!string.IsNullOrEmpty (guid)) 
			{
				string str = AssetDatabase.GUIDToAssetPath (guid);
				if (string.IsNullOrEmpty (str))
				{
					return string.Empty;
				}
				if (AssetExists (str)) 
				{
					return str;
				}
			}
			return string.Empty;
		}

		public static string MonoInstanceADBDir(MonoBehaviour monobehaviour)
		{
			string asstPath = AssetDatabase.GetAssetPath (MonoScript.FromMonoBehaviour (monobehaviour));
			return asstPath.Substring (0,asstPath.LastIndexOf(ADBPathSlash));
		}

		public static string MonoInstanceADBDir(ScriptableObject scriptableObj)
		{
			string assetPath = AssetDatabase.GetAssetPath (MonoScript.FromScriptableObject (scriptableObj));
			return assetPath.Substring (0,assetPath.LastIndexOf(ADBPathSlash));
		}

		public static string MonoInstanceADBPath(MonoBehaviour monobehaviour)
		{
			return AssetDatabase.GetAssetPath (MonoScript.FromMonoBehaviour (monobehaviour));
		}

		public static string MonoInstanceADBPath(ScriptableObject scriptableObj)
		{
			return AssetDatabase.GetAssetPath (MonoScript.FromScriptableObject (scriptableObj));
		}

		public static void SetScriptExecutionOrder(MonoBehaviour monobehaviour, int order)
		{
			MonoImporter.SetExecutionOrder (MonoScript.FromMonoBehaviour (monobehaviour), order);
		}
	}
}
