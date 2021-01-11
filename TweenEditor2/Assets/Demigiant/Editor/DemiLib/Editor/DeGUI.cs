using UnityEngine;
using System.Collections;
using DG.DemiEditor;
using UnityEditor;
using DG.DemiLib.Core;

namespace DG.DemiLib
{
	public static class DeGUI
	{
		private static DeColorPalette _defaultColorPalette;
		private static DeStylePalette _defaultStylePalette;
		public static DeColorPalette colors;
		public static readonly bool IsProSkin;
		public static DeStylePalette styles;

		static DeGUI()
		{
			GUIUtils.isProSkin = IsProSkin = EditorGUIUtility.isProSkin;
		}

		public static void BeginGUI( DeColorPalette guiColorPalette=null, DeStylePalette guiStylePalette=null)
		{
			ChangePalette (guiColorPalette, guiStylePalette);
		}
		public static void ChangePalette(DeColorPalette newColorPalette, DeStylePalette newStylePalette)
		{
			if (newColorPalette != null) {
				colors = newColorPalette;
			} 
			else 
			{
				if (_defaultColorPalette == null) 
				{
					_defaultColorPalette = new DeColorPalette ();
				}
				colors = _defaultColorPalette;
			}
			if (newStylePalette != null) {
				styles = newStylePalette;
			} 
			else 
			{
				if (_defaultStylePalette == null) 
				{
					_defaultStylePalette = new DeStylePalette ();
				}
				styles = _defaultStylePalette;
			}
			styles.Init ();
		}

		public static void FlatDivider(Rect rect, Color? color=null)
		{
			Color backgroundColor = GUI.backgroundColor;
			if (color.HasValue) 
			{
				GUI.backgroundColor = color.Value;
			}
			GUI.Box (rect, styles.box.def.ToString());
			GUI.backgroundColor = backgroundColor;
		}

		public static UnityEngine.Object SceneField(Rect rect, string label, UnityEngine.Object obj)
		{
			if((obj != null) && !obj.ToString().EndsWith(".SceneAsset"))
			{
				obj = null;
			}
			return EditorGUI.ObjectField (rect, label, obj, typeof(UnityEngine.Object), false);
		}

		public static bool ToggleButton(Rect rect, bool toggled, string text)
		{
			return ToggleButton (rect, toggled, new GUIContent (text, ""), null, null);
		}

		public static bool ToggleButton(Rect rect, bool toggled, GUIContent content)
		{
			return ToggleButton (rect, toggled, content, null, null);
		}

		public static bool ToggleButton(Rect rect, bool toggled, string text, GUIStyle guiStyle)
		{
			return ToggleButton (rect, toggled, new GUIContent (text, ""), null, guiStyle);
		}

		public static bool ToggleButton(Rect rect, bool toggled, GUIContent content, GUIStyle guiStyle)
		{
			return ToggleButton (rect, toggled, content, null, guiStyle);
		}

		public static bool ToggleButton(Rect rect, bool toggled, string text, DeColorPalette colorPalette, GUIStyle guiStyle=null)
		{
			return ToggleButton (rect, toggled, new GUIContent (text, ""), colorPalette, guiStyle);
		}

		public static bool ToggleButton(Rect rect, bool toggled, GUIContent content, DeColorPalette colorPalette, GUIStyle guiStyle=null)
		{
			DeColorPalette palette = colorPalette ?? colors;
			Color backgroundColor = GUI.backgroundColor;
			GUI.backgroundColor = toggled ? ((Color)palette.bg.toggleOn) : ((Color)palette.bg.toggleOff);
			if (guiStyle == null) 
			{
				guiStyle = styles.button.def;
			}
			object[] formats = new object[] { toggled ? palette.content.toggleOn : palette.content.toggleOff };
			if (GUI.Button (rect, content, guiStyle.Clone (formats))) 
			{
				toggled = !toggled;
				GUI.changed = true;
			}
			GUI.backgroundColor = backgroundColor;
			return toggled;
		}
	}
}
