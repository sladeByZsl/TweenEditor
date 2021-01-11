using UnityEngine;
using System.IO;
using System.Reflection;
using UnityEditor;
using DG.Tweening;
using System;

namespace DG.DOTweenEditor.Core
{
	public static class EditorUtils
	{
		// Fields
		private static string _demigiantDir;
		private static string _dotweenDir;
		private static string _dotweenProDir;
		private static string _editorADBDir;
		private static bool _hasCheckedForPro;
		private static bool _hasPro;
		private static string _proVersion;

		// Methods
		static EditorUtils()
		{
			isOSXEditor = Application.platform == RuntimePlatform.OSXEditor;
			bool flag1 = Application.platform == RuntimePlatform.WindowsEditor;
			pathSlash = flag1 ? @"\" : "/";
			pathSlashToReplace = flag1 ? "/" : @"\";
			projectPath = Application.dataPath;
			projectPath = projectPath.Substring(0, projectPath.LastIndexOf("/"));
			projectPath = projectPath.Replace(pathSlashToReplace, pathSlash);
			assetsPath = projectPath + pathSlash + "Assets";
		}

		public static string ADBPathToFullPath(string adbPath)
		{
			adbPath = adbPath.Replace(pathSlashToReplace, pathSlash);
			return (projectPath + pathSlash + adbPath);
		}

		public static bool AssetExists(string adbPath)
		{
			string path = ADBPathToFullPath(adbPath);
			if (!File.Exists(path))
			{
				return Directory.Exists(path);
			}
			return true;
		}

		private static void CheckForPro()
		{
			_hasCheckedForPro = true;
			try
			{
				_proVersion = Assembly.Load("DOTweenPro, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null").GetType("DG.Tweening.DOTweenPro").GetField("Version", BindingFlags.Public | BindingFlags.Static).GetValue(null) as string;
				_hasPro = true;
			}
			catch
			{
				_hasPro = false;
				_proVersion = "-";
			}
		}

		public static T ConnectToSourceAsset<T>(string adbFilePath, bool createIfMissing=false) where T: ScriptableObject
		{
			if (!AssetExists(adbFilePath))
			{
				if (!createIfMissing)
				{
					return default(T);
				}
				CreateScriptableAsset<T>(adbFilePath);
			}
			T local = (T) AssetDatabase.LoadAssetAtPath(adbFilePath, typeof(T));
			if (local == null)
			{
				CreateScriptableAsset<T>(adbFilePath);
				local = (T) AssetDatabase.LoadAssetAtPath(adbFilePath, typeof(T));
			}
			return local;
		}

		private static void CreateScriptableAsset<T>(string adbFilePath) where T: ScriptableObject
		{
			AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<T>(), adbFilePath);
		}

		public static void DelayedCall(float delay, Action callback)
		{
			new DelayedCall(delay, callback);
		}

		private static void DeleteAssetsIfExist(string[] adbFilePaths)
		{
			foreach (string str in adbFilePaths)
			{
				if (AssetExists(str))
				{
					AssetDatabase.DeleteAsset(str);
				}
			}
		}

		public static void DeleteOldDemiLibCore()
		{
			string assemblyFilePath = GetAssemblyFilePath(typeof(DOTween).Assembly);
			string str2 = (assemblyFilePath.IndexOf("/") != -1) ? "/" : @"\";
			assemblyFilePath = assemblyFilePath.Substring(0, assemblyFilePath.LastIndexOf(str2));
			assemblyFilePath = assemblyFilePath.Substring(0, assemblyFilePath.LastIndexOf(str2)) + str2 + "DemiLib";
			string adbPath = FullPathToADBPath(assemblyFilePath);
			if (AssetExists(adbPath))
			{
				string str4 = adbPath + "/Core";
				if (AssetExists(str4))
				{
					string[] adbFilePaths = new string[] { adbPath + "/DemiLib.dll", adbPath + "/DemiLib.xml", adbPath + "/DemiLib.dll.mdb", adbPath + "/Editor/DemiEditor.dll", adbPath + "/Editor/DemiEditor.xml", adbPath + "/Editor/DemiEditor.dll.mdb", adbPath + "/Editor/Imgs" };
					DeleteAssetsIfExist(adbFilePaths);
					if (AssetExists(adbPath + "/Editor") && (Directory.GetFiles(assemblyFilePath + str2 + "Editor").Length == 0))
					{
						AssetDatabase.DeleteAsset(adbPath + "/Editor");
						AssetDatabase.ImportAsset(str4);
					}
				}
			}
		}

		public static bool DOTweenSetupRequired()
		{
			if (!Directory.Exists(dotweenDir))
			{
				return false;
			}
			return ((Directory.GetFiles(dotweenDir, "*.addon").Length != 0) || (hasPro && (Directory.GetFiles(dotweenProDir, "*.addon").Length > 0)));
		}

		public static string FullPathToADBPath(string fullPath)
		{
			return fullPath.Substring(projectPath.Length + 1).Replace(@"\", "/");
		}

		public static string GetAssemblyFilePath(Assembly assembly)
		{
			string str = Uri.UnescapeDataString(new UriBuilder(assembly.CodeBase).Path);
			if (str.Substring(str.Length - 3) == "dll")
			{
				return str;
			}
			return Path.GetFullPath(assembly.Location);
		}

		public static void SetEditorTexture(Texture2D texture, FilterMode filterMode = FilterMode.Point, int maxTextureSize=0x20)
		{
			if (texture != null && texture.wrapMode != TextureWrapMode.Clamp)
			{
				string assetPath = AssetDatabase.GetAssetPath(texture);
				TextureImporter atPath = AssetImporter.GetAtPath(assetPath) as TextureImporter;
				atPath.textureType = TextureImporterType.GUI;
				atPath.npotScale = 0;
				atPath.filterMode = filterMode;
				atPath.wrapMode = TextureWrapMode.Clamp;
				atPath.maxTextureSize = maxTextureSize;
				//atPath.textureFormat = TextureImporterFormat.AutomaticTruecolor;
				atPath.textureCompression = TextureImporterCompression.CompressedHQ;
                AssetDatabase.ImportAsset(assetPath);
			}
		}

		private static void StoreDOTweenDirs()
		{
			_dotweenDir = Path.GetDirectoryName(GetAssemblyFilePath(Assembly.GetExecutingAssembly()));
			string str = (_dotweenDir.IndexOf("/") != -1) ? "/" : @"\";
			_dotweenDir = _dotweenDir.Substring(0, _dotweenDir.LastIndexOf(str) + 1);
			_dotweenProDir = _dotweenDir.Substring(0, _dotweenDir.LastIndexOf(str));
			_dotweenProDir = _dotweenProDir.Substring(0, _dotweenProDir.LastIndexOf(str) + 1) + "DOTweenPro" + str;
			_demigiantDir = _dotweenDir.Substring(0, _dotweenDir.LastIndexOf(str));
			_demigiantDir = _demigiantDir.Substring(0, _demigiantDir.LastIndexOf(str) + 1);
			if (_demigiantDir.Substring(_demigiantDir.Length - 10, 9) != "Demigiant")
			{
				_demigiantDir = null;
			}
			_dotweenDir = _dotweenDir.Replace(pathSlashToReplace, pathSlash);
			_dotweenProDir = _dotweenProDir.Replace(pathSlashToReplace, pathSlash);
			if (_demigiantDir != null)
			{
				_demigiantDir = _demigiantDir.Replace(pathSlashToReplace, pathSlash);
			}
		}

		private static void StoreEditorADBDir()
		{
			_editorADBDir = Path.GetDirectoryName(GetAssemblyFilePath(Assembly.GetExecutingAssembly())).Substring(Application.dataPath.Length + 1).Replace(@"\", "/") + "/";
		}

		// Properties
		public static string assetsPath {
			set;
			get;
		}

		public static string demigiantDir
		{
			get
			{
				if (string.IsNullOrEmpty(_demigiantDir))
				{
					StoreDOTweenDirs();
				}
				return _demigiantDir;
			}
		}

		public static string dotweenDir
		{
			get
			{
				if (string.IsNullOrEmpty(_dotweenDir))
				{
					StoreDOTweenDirs();
				}
				return _dotweenDir;
			}
		}

		public static string dotweenProDir
		{
			get
			{
				if (string.IsNullOrEmpty(_dotweenProDir))
				{
					StoreDOTweenDirs();
				}
				return _dotweenProDir;
			}
		}

		public static string editorADBDir
		{
			get
			{
				if (string.IsNullOrEmpty(_editorADBDir))
				{
					StoreEditorADBDir();
				}
				return _editorADBDir;
			}
		}

		public static bool hasPro
		{
			get
			{
				if (!_hasCheckedForPro)
				{
					CheckForPro();
				}
				return _hasPro;
			}
		}

		public static bool isOSXEditor
		{
			set;
			get;
		}

		public static string pathSlash
		{
			set;
			get;
		}

		public static string pathSlashToReplace
		{
			set;
			get;
		}

		public static string projectPath
		{
			set;
			get;
		}

		public static string proVersion
		{
			get
			{
				if (!_hasCheckedForPro)
				{
					CheckForPro();
				}
				return _proVersion;
			}
		}
	}
}