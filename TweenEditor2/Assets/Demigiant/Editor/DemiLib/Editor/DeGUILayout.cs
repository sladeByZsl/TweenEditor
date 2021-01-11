using UnityEngine;
using System.Collections;
using DG.DemiEditor;
using UnityEditor;

namespace DG.DemiLib
{
	public static class DeGUILayout
	{
		private static int _activePressButtonId;

		static DeGUILayout()
		{
			_activePressButtonId = -1;
		}

		public static void BeginToolbar(params GUILayoutOption[] options)
		{
			BeginToolbar (Color.white, null, options);
		}

		public static void BeginToolbar(Color backgroundShade, params GUILayoutOption[] options)
		{
			BeginToolbar (backgroundShade, null, options);
		}

		public static void BeginToolbar(GUIStyle style, params GUILayoutOption[] options)
		{
			BeginToolbar (Color.white, style, options);
		}

		public static void BeginToolbar(Color backgroundShade, GUIStyle style, params GUILayoutOption[] options)
		{
			GUI.backgroundColor = backgroundShade;
			GUILayout.BeginHorizontal (style ?? DeGUI.styles.toolbar.def, options);
			GUI.backgroundColor = Color.white;
		}

		public static void BeginVBox(GUIStyle style)
		{
			BeginVBox(null, style);
		}

		public static void BeginVBox(Color? color=null, GUIStyle style=null)
		{
			Color color2 = !color.HasValue ? Color.white : color.Value;
			if (style == null) 
			{
				style = DeGUI.styles.box.def;
			}
			Color backgroundColor = GUI.backgroundColor;
			GUI.backgroundColor = color2;
			GUILayout.BeginVertical (style, new GUILayoutOption[0]);
			GUI.backgroundColor = backgroundColor;
		}

		public static bool ColoredButton(Color shader, Color contentColor, string text, params GUILayoutOption[] options)
		{
			return ColoredButton (shader, contentColor, new GUIContent (text, ""), null, options);
		}
		public static bool ColoredButton(Color shader, Color contentColor, GUIContent content, params GUILayoutOption[] options)
		{
			return ColoredButton (shader, contentColor, content, null, options);
		}
		public static bool ColoredButton(Color shader, Color contentColor, string text, GUIStyle guiStyle, params GUILayoutOption[] options)
		{
			return ColoredButton (shader, contentColor, new GUIContent (text, ""), guiStyle, options);
		}
		public static bool ColoredButton(Color shader, Color contentColor, GUIContent content, GUIStyle guiStyle, params GUILayoutOption[] options)
		{
			Color backgroundColor = GUI.backgroundColor;
			GUI.backgroundColor = shader;
			if (guiStyle == null) 
			{
				guiStyle = DeGUI.styles.button.def;
			}
			object[] formats = new object[]{ contentColor };
			bool flag = GUILayout.Button (content, guiStyle.Clone (formats), options);
			GUI.backgroundColor = backgroundColor;
			return flag;
		}

		public static void EndToolbar()
		{
			GUILayout.EndHorizontal ();
		}

		public static void EndVBox()
		{
			GUILayout.EndVertical ();
		}

		public static void HorizontalDivider(Color ? color=null, int height=1, int topMargin=5, int bottomMargin=5)
		{
			Color backgroundColor = GUI.backgroundColor;
			GUI.backgroundColor = !color.HasValue ? ((Color)DeGUI.colors.bg.divider) : color.Value;
			GUILayout.Space ((float)topMargin);
			GUILayoutOption[] options = new GUILayoutOption[]{ GUILayout.Height ((float)height) };
			if (DeGUI.styles.misc.line == null)
				DeGUI.styles.misc.Init ();
			GUILayout.BeginHorizontal (DeGUI.styles.misc.line, options);
			GUILayout.FlexibleSpace ();
			GUILayout.EndHorizontal ();
			GUILayout.Space ((float)bottomMargin);
			GUI.backgroundColor = backgroundColor;
		}

		public static bool PressButton(string text, GUIStyle guiStyle, params GUILayoutOption[] options)
		{
			return PressButton (new GUIContent (text, ""), guiStyle, options);
		}
		public static bool PressButton(GUIContent content, GUIStyle guiStyle, params GUILayoutOption[] options)
		{
			GUILayout.Button (content, guiStyle, options);
			int controlID = GUIUtility.GetControlID (FocusType.Passive);
			int hotControl = GUIUtility.hotControl;
			bool flag = (hotControl > 1) && GUILayoutUtility.GetLastRect ().Contains (Event.current.mousePosition);
			if (flag && (_activePressButtonId != controlID)) 
			{
				_activePressButtonId = controlID;
				return true;
			}
			if (!flag && (hotControl < 1)) 
			{
				_activePressButtonId = -1;
			}
			return false;
		}

		public static UnityEngine.Object SceneField(string label, UnityEngine.Object obj)
		{
			if ((obj != null) && !obj.ToString ().EndsWith (".SceneAsset")) 
			{
				obj = null;
			}
			return EditorGUILayout.ObjectField (label, obj, typeof(UnityEngine.Object), false, new GUILayoutOption[0]);
		}
		public static bool ToggleButton(bool toggled, string text, params GUILayoutOption[] options)
		{
			return ToggleButton (toggled, new GUIContent (text, ""), null, null, options);
		}
		public static bool ToggleButton(bool toggled, GUIContent content, params GUILayoutOption[] options)
		{
			return ToggleButton (toggled, content, null, null, options);
		}
		public static bool ToggleButton(bool toggled, string text, GUIStyle guiStyle, params GUILayoutOption[] options)
		{
			return ToggleButton (toggled, new GUIContent (text, ""), null, guiStyle, options);
		}
		public static bool ToggleButton(bool toggled, GUIContent content, GUIStyle guiStyle, params GUILayoutOption[] options)
		{
			return ToggleButton (toggled, content, null, guiStyle, options);
		}
		public static bool ToggleButton(bool toggled, string text, DeColorPalette colorPalette, GUIStyle guiStyle=null, params GUILayoutOption[] options)
		{
			return ToggleButton (toggled, new GUIContent (text, ""), colorPalette, guiStyle, options);
		}
		public static bool ToggleButton(bool toggled, GUIContent content, DeColorPalette colorPalette, GUIStyle guiStyle=null, params GUILayoutOption[] options)
		{
			DeColorPalette palette = colorPalette ?? DeGUI.colors;
			Color backgroundColor = GUI.backgroundColor;
			GUI.backgroundColor = toggled ? palette.bg.toggleOn : palette.bg.toggleOff;
			if (guiStyle == null) 
			{
				guiStyle = DeGUI.styles.button.def;
			}
			object[] formats = new object[] { toggled ? palette.content.toggleOn : palette.content.toggleOff };
			if (GUILayout.Button (content, guiStyle.Clone (formats), options)) 
			{
				toggled = !toggled;
				GUI.changed = true;
			}
			GUI.backgroundColor = backgroundColor;
			return toggled;
		}

		public static void Toolbar(string text, params GUILayoutOption[] options)
		{
			Toolbar (text, Color.white, null, null, options);
		}
		public static void Toolbar(string text, Color backgroundShade, params GUILayoutOption[] options)
		{
			Toolbar (text, backgroundShade, null, null, options);
		}
		public static void Toolbar(string text, GUIStyle toolbarStyle, params GUILayoutOption[] options)
		{
			Toolbar (text, Color.white, toolbarStyle, null, options);
		}
		public static void Toolbar(string text, Color backgroundShade,  GUIStyle toolbarStyle, params GUILayoutOption[] options)
		{
			Toolbar (text, backgroundShade, toolbarStyle, null, options);
		}
		public static void Toolbar(string text, GUIStyle toolbarStyle, GUIStyle labelStyle, params GUILayoutOption[] options)
		{
			Toolbar (text, Color.white, toolbarStyle, labelStyle, options);
		}
		public static void Toolbar(string text, Color backgroundShade,  GUIStyle toolbarStyle, GUIStyle labelStyle, params GUILayoutOption[] options)
		{
			BeginToolbar (backgroundShade, toolbarStyle, options);
			if (labelStyle == null) 
			{
				labelStyle = DeGUI.styles.label.toolbar;
			}
			GUILayout.Label (text, labelStyle, new GUILayoutOption[0]);
			EndToolbar ();
		}
		public static bool ToolbarFoldoutButton(bool toggled, string text=null)
		{
			if (GUILayout.Button (text, string.IsNullOrEmpty (text) ? (toggled ? DeGUI.styles.button.toolFoldoutOpen : DeGUI.styles.button.toolFoldoutClosed) : (toggled ? DeGUI.styles.button.toolFoldoutOpenWLabel : DeGUI.styles.button.toolFoldoutClosedWLabel), new GUILayoutOption[0])) 
			{
				toggled = !toggled;
				GUI.changed = true;
			}
			return toggled;
		}
	}
}
