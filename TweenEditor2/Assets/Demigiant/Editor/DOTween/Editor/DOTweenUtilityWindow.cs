using UnityEditor;
using UnityEngine;
using DG.Tweening.Core;
using System.IO;
using DG.Tweening;
using DG.DOTweenEditor.Core;
using System.Runtime.InteropServices;

namespace DG.DOTweenEditor
{
	
	internal class DOTweenUtilityWindow : EditorWindow
	{
		// Fields
		private Texture2D _footerImg;
		private Vector2 _footerSize;
		private static readonly float _HalfBtSize;
		private Texture2D _headerImg;
		private Vector2 _headerSize;
		private string _innerTitle;
		private int _selectedTab;
		private string[] _settingsLocation;
		private bool _setupRequired;
		private DOTweenSettings _src;
		private string[] _tabLabels;
		private const string _Title = "DOTween Utility Panel";
		private static readonly Vector2 _WinSize;
		public const string Id = "DOTweenVersion";
		public const string IdPro = "DOTweenProVersion";

		// Methods
		static DOTweenUtilityWindow()
		{
			_WinSize = new Vector2(300f, 405f);
			_HalfBtSize = (_WinSize.x * 0.5f) - 6f;
		}
		public DOTweenUtilityWindow()
		{
			this._tabLabels = new string[] { "Setup", "Preferences" };
			this._settingsLocation = new string[] { "Assets > Resources", "DOTween > Resources", "Demigiant > Resources" };
		}
			
		private void Connect( bool forceReconnect=false)
		{
			if ((this._src == null) || forceReconnect)
			{
				LocationData to = new LocationData(EditorUtils.assetsPath + EditorUtils.pathSlash + "Resources");
				LocationData data2 = new LocationData(EditorUtils.dotweenDir + "Resources");
				bool flag = EditorUtils.demigiantDir != null;
				LocationData data3 = flag ? new LocationData(EditorUtils.demigiantDir + "Resources") : new LocationData();
				if (this._src == null)
				{
					this._src = EditorUtils.ConnectToSourceAsset<DOTweenSettings>(to.adbFilePath, false);
					if (this._src == null)
					{
						this._src = EditorUtils.ConnectToSourceAsset<DOTweenSettings>(data2.adbFilePath, false);
					}
					if ((this._src == null) & flag)
					{
						this._src = EditorUtils.ConnectToSourceAsset<DOTweenSettings>(data3.adbFilePath, false);
					}
				}
				if (this._src == null)
				{
					if (!Directory.Exists(to.dir))
					{
						AssetDatabase.CreateFolder(to.adbParentDir, "Resources");
					}
					this._src = EditorUtils.ConnectToSourceAsset<DOTweenSettings>(to.adbFilePath, true);
				}
				switch (this._src.storeSettingsLocation)
				{
				case DOTweenSettings.SettingsLocation.AssetsDirectory:
					{
						LocationData[] from = new LocationData[] { data2, data3 };
						this.MoveSrc(from, to);
						return;
					}
				case DOTweenSettings.SettingsLocation.DOTweenDirectory:
					{
						LocationData[] dataArray2 = new LocationData[] { to, data3 };
						this.MoveSrc(dataArray2, data2);
						return;
					}
				case DOTweenSettings.SettingsLocation.DemigiantDirectory:
					{
						LocationData[] dataArray3 = new LocationData[] { to, data2 };
						this.MoveSrc(dataArray3, data3);
						return;
					}
				}
			}
		}

		private void DrawPreferencesGUI()
		{
			GUILayout.Space(40f);
			if (GUILayout.Button("Reset", EditorGUIUtils.btStyle, new GUILayoutOption[0]))
			{
				this._src.useSafeMode = true;
				this._src.showUnityEditorReport = false;
				this._src.timeScale = 1f;
				this._src.useSmoothDeltaTime = false;
				this._src.logBehaviour = LogBehaviour.ErrorsOnly;
				this._src.drawGizmos = true;
				this._src.defaultRecyclable = false;
				this._src.defaultAutoPlay = AutoPlay.All;
				this._src.defaultUpdateType = UpdateType.Normal;
				this._src.defaultTimeScaleIndependent = false;
				this._src.defaultEaseType = Ease.OutQuad;
				this._src.defaultEaseOvershootOrAmplitude = 1.70158f;
				this._src.defaultEasePeriod = 0f;
				this._src.defaultAutoKill = true;
				this._src.defaultLoopType = LoopType.Restart;
				EditorUtility.SetDirty(this._src);
			}
			GUILayout.Space(8f);
			this._src.useSafeMode = EditorGUILayout.Toggle("Safe Mode", this._src.useSafeMode, new GUILayoutOption[0]);
			this._src.timeScale = EditorGUILayout.FloatField("DOTween's TimeScale", this._src.timeScale, new GUILayoutOption[0]);
			this._src.useSmoothDeltaTime = EditorGUILayout.Toggle("Smooth DeltaTime", this._src.useSmoothDeltaTime, new GUILayoutOption[0]);
			this._src.showUnityEditorReport = EditorGUILayout.Toggle("Editor Report", this._src.showUnityEditorReport, new GUILayoutOption[0]);
			this._src.logBehaviour = (LogBehaviour) EditorGUILayout.EnumPopup("Log Behaviour", this._src.logBehaviour, new GUILayoutOption[0]);
			this._src.drawGizmos = EditorGUILayout.Toggle("Draw Path Gizmos", this._src.drawGizmos, new GUILayoutOption[0]);
			DOTweenSettings.SettingsLocation storeSettingsLocation = this._src.storeSettingsLocation;
			this._src.storeSettingsLocation = (DOTweenSettings.SettingsLocation) EditorGUILayout.Popup("Settings Location", (int) this._src.storeSettingsLocation, this._settingsLocation, new GUILayoutOption[0]);
			if (this._src.storeSettingsLocation != storeSettingsLocation)
			{
				if ((this._src.storeSettingsLocation == DOTweenSettings.SettingsLocation.DemigiantDirectory) && (EditorUtils.demigiantDir == null))
				{
					EditorUtility.DisplayDialog("Change DOTween Settings Location", "Demigiant directory not present (must be the parent of DOTween's directory)", "Ok");
					if (storeSettingsLocation == DOTweenSettings.SettingsLocation.DemigiantDirectory)
					{
						this._src.storeSettingsLocation = DOTweenSettings.SettingsLocation.AssetsDirectory;
						this.Connect(true);
					}
					else
					{
						this._src.storeSettingsLocation = storeSettingsLocation;
					}
				}
				else
				{
					this.Connect(true);
				}
			}
			GUILayout.Space(8f);
			GUILayout.Label("DEFAULTS ▼", new GUILayoutOption[0]);
			this._src.defaultRecyclable = EditorGUILayout.Toggle("Recycle Tweens", this._src.defaultRecyclable, new GUILayoutOption[0]);
			this._src.defaultAutoPlay = (AutoPlay) EditorGUILayout.EnumPopup("AutoPlay", this._src.defaultAutoPlay, new GUILayoutOption[0]);
			this._src.defaultUpdateType = (UpdateType) EditorGUILayout.EnumPopup("Update Type", this._src.defaultUpdateType, new GUILayoutOption[0]);
			this._src.defaultTimeScaleIndependent = EditorGUILayout.Toggle("TimeScale Independent", this._src.defaultTimeScaleIndependent, new GUILayoutOption[0]);
			this._src.defaultEaseType = (Ease) EditorGUILayout.EnumPopup("Ease", this._src.defaultEaseType, new GUILayoutOption[0]);
			this._src.defaultEaseOvershootOrAmplitude = EditorGUILayout.FloatField("Ease Overshoot", this._src.defaultEaseOvershootOrAmplitude, new GUILayoutOption[0]);
			this._src.defaultEasePeriod = EditorGUILayout.FloatField("Ease Period", this._src.defaultEasePeriod, new GUILayoutOption[0]);
			this._src.defaultAutoKill = EditorGUILayout.Toggle("AutoKill", this._src.defaultAutoKill, new GUILayoutOption[0]);
			this._src.defaultLoopType = (LoopType) EditorGUILayout.EnumPopup("Loop Type", this._src.defaultLoopType, new GUILayoutOption[0]);
			if (GUI.changed)
			{
				EditorUtility.SetDirty(this._src);
			}
		}

		private void DrawSetupGUI()
		{
			Rect position = new Rect(0f, 30f, this._headerSize.x, this._headerSize.y);
			GUI.DrawTexture(position, this._headerImg, ScaleMode.StretchToFill, false);
			GUILayout.Space((position.y + this._headerSize.y) + 2f);
			GUILayout.Label(this._innerTitle, DOTween.isDebugBuild ? EditorGUIUtils.redLabelStyle : EditorGUIUtils.boldLabelStyle, new GUILayoutOption[0]);
			if (this._setupRequired)
			{
				GUI.backgroundColor = Color.red;
				GUILayout.BeginVertical(GUI.skin.box, new GUILayoutOption[0]);
				GUILayout.Label("DOTWEEN SETUP REQUIRED", EditorGUIUtils.setupLabelStyle, new GUILayoutOption[0]);
				GUILayout.EndVertical();
				GUI.backgroundColor = Color.white;
			}
			else
			{
				GUILayout.Space(8f);
			}
			if (GUILayout.Button("Setup DOTween...", EditorGUIUtils.btStyle, new GUILayoutOption[0]))
			{
				DOTweenSetupMenuItem.Setup(false);
				this._setupRequired = EditorUtils.DOTweenSetupRequired();
			}
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.Width(_HalfBtSize) };
			if (GUILayout.Button("Documentation", EditorGUIUtils.btStyle, options))
			{
				Application.OpenURL("http://dotween.demigiant.com/documentation.php");
			}
			GUILayoutOption[] optionArray2 = new GUILayoutOption[] { GUILayout.Width(_HalfBtSize) };
			if (GUILayout.Button("Support", EditorGUIUtils.btStyle, optionArray2))
			{
				Application.OpenURL("http://dotween.demigiant.com/support.php");
			}
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayoutOption[] optionArray3 = new GUILayoutOption[] { GUILayout.Width(_HalfBtSize) };
			if (GUILayout.Button("Changelog", EditorGUIUtils.btStyle, optionArray3))
			{
				Application.OpenURL("http://dotween.demigiant.com/download.php");
			}
			GUILayoutOption[] optionArray4 = new GUILayoutOption[] { GUILayout.Width(_HalfBtSize) };
			if (GUILayout.Button("Check Updates", EditorGUIUtils.btStyle, optionArray4))
			{
				Application.OpenURL("http://dotween.demigiant.com/download.php?v=" + DOTween.Version);
			}
			GUILayout.EndHorizontal();
			GUILayout.Space(14f);
			if (GUILayout.Button(this._footerImg, EditorGUIUtils.btImgStyle, new GUILayoutOption[0]))
			{
				Application.OpenURL("http://www.demigiant.com/");
			}
		}

		private void MoveSrc(LocationData[] from, LocationData to)
		{
			if (!Directory.Exists(to.dir))
			{
				AssetDatabase.CreateFolder(to.adbParentDir, "Resources");
			}
			foreach (LocationData data in from)
			{
				if (File.Exists(data.filePath))
				{
					AssetDatabase.MoveAsset(data.adbFilePath, to.adbFilePath);
					AssetDatabase.DeleteAsset(data.adbFilePath);
					if ((Directory.GetDirectories(data.dir).Length == 0) && (Directory.GetFiles(data.dir).Length == 0))
					{
						AssetDatabase.DeleteAsset(EditorUtils.FullPathToADBPath(data.dir));
					}
				}
			}
			this._src = EditorUtils.ConnectToSourceAsset<DOTweenSettings>(to.adbFilePath, true);
		}

		private void OnEnable()
		{
			this._innerTitle = "DOTween v" + DOTween.Version + (DOTween.isDebugBuild ? " [Debug build]" : " [Release build]");
			if (EditorUtils.hasPro)
			{
				this._innerTitle = this._innerTitle + "\nDOTweenPro v" + EditorUtils.proVersion;
			}
			else
			{
				this._innerTitle = this._innerTitle + "\nDOTweenPro not installed";
			}
			if (this._headerImg == null)
			{
				this._headerImg = AssetDatabase.LoadAssetAtPath("Assets" + EditorUtils.editorADBDir + "Imgs/Header.jpg", typeof(Texture2D)) as Texture2D;
				//Debug.LogWarning ("**************_headerImg:" + _headerImg);
				EditorUtils.SetEditorTexture(this._headerImg, FilterMode.Bilinear, 0x200);
				this._headerSize.x = _WinSize.x;
				this._headerSize.y = (int) ((_WinSize.x * this._headerImg.height) / ((float) this._headerImg.width));
				this._footerImg = AssetDatabase.LoadAssetAtPath("Assets" + EditorUtils.editorADBDir + (EditorGUIUtility.isProSkin? "Imgs/Footer.png" : "Imgs/Footer_dark.png"), typeof(Texture2D)) as Texture2D;
				//Debug.LogWarning ("**************_headerImg:" + _headerImg);
				EditorUtils.SetEditorTexture(this._footerImg, FilterMode.Bilinear, 0x100);
				this._footerSize.x = _WinSize.x;
				this._footerSize.y = (int) ((_WinSize.x * this._footerImg.height) / ((float) this._footerImg.width));
			}
			this._setupRequired = EditorUtils.DOTweenSetupRequired();
		}

		private void OnGUI()
		{
			this.Connect(false);
			EditorGUIUtils.SetGUIStyles(new Vector2?(this._footerSize));
			if (Application.isPlaying)
			{
				GUILayout.Space(40f);
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUILayout.Space(40f);
				GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.ExpandWidth(true) };
				GUILayout.Label("DOTween Utility Panel\nis disabled while in Play Mode", EditorGUIUtils.wrapCenterLabelStyle, options);
				GUILayout.Space(40f);
				GUILayout.EndHorizontal();
			}
			else
			{
				Rect position = new Rect(0f, 0f, this._headerSize.x, 30f);
				this._selectedTab = GUI.Toolbar(position, this._selectedTab, this._tabLabels);
				if (this._selectedTab == 1)
				{
					this.DrawPreferencesGUI();
				}
				else
				{
					this.DrawSetupGUI();
				}
			}
		}

		private void OnHierarchyChange()
		{
			base.Repaint();
		}

		public static void Open()
		{
			DOTweenUtilityWindow local1 = EditorWindow.GetWindow<DOTweenUtilityWindow>(true, "DOTween Utility Panel", true);
			local1.minSize = _WinSize;
			local1.maxSize = _WinSize;
			local1.ShowUtility();
			EditorPrefs.SetString("DOTweenVersion", DOTween.Version);
			EditorPrefs.SetString("DOTweenProVersion", EditorUtils.proVersion);
		}

		[MenuItem("Tools/DOTween Utility Panel")]
		private static void ShowWindow()
		{
			Open();
		}
		[MenuItem("Tools/DOTween Utility Setup")]
		public static void SetupWindow()
		{
			DOTweenSetupMenuItem.Setup(false);
		}

		// Nested Types
		[StructLayout(LayoutKind.Sequential)]
		private struct LocationData
		{
			public string dir;
			public string filePath;
			public string adbFilePath;
			public string adbParentDir;
			public LocationData(string srcDir)
			{
				this = new DOTweenUtilityWindow.LocationData();
				this.dir = srcDir;
				this.filePath = this.dir + EditorUtils.pathSlash + "DoTweenSettings.asset";
				this.adbFilePath = EditorUtils.FullPathToADBPath(this.filePath);
				this.adbParentDir = EditorUtils.FullPathToADBPath(this.dir.Substring(0, this.dir.LastIndexOf(EditorUtils.pathSlash)));
			}
		}
	}
}
