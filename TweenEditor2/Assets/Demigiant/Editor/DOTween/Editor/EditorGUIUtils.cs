using UnityEngine;
using DG.Tweening;
using UnityEditor;
using System;

namespace DG.DOTweenEditor.Core
{
	public static class EditorGUIUtils
	{
		// Fields
		private static bool _additionalStylesSet;
		private static Texture2D _logo;
		private static bool _stylesSet;
		public static GUIStyle boldLabelStyle;
		public static GUIStyle btIconStyle;
		public static GUIStyle btImgStyle;
		public static GUIStyle btStyle;
		internal static readonly string[] FilteredEaseTypes;
		public static GUIStyle handlelabelStyle;
		public static GUIStyle handleSelectedLabelStyle;
		public static GUIStyle logoIconStyle;
		public static GUIStyle popupButton;
		public static GUIStyle redLabelStyle;
		public static GUIStyle setupLabelStyle;
		public static GUIStyle sideBtStyle;
		public static GUIStyle sideLogoIconBoldLabelStyle;
		public static GUIStyle titleStyle;
		public static GUIStyle wordWrapItalicLabelStyle;
		public static GUIStyle wordWrapLabelStyle;
		public static GUIStyle wordWrapTextArea;
		public static GUIStyle wrapCenterLabelStyle;

		// Methods
		static EditorGUIUtils()
		{
			FilteredEaseTypes = new string[] { 
				"Linear", "InSine", "OutSine", "InOutSine", "InQuad", "OutQuad", "InOutQuad", "InCubic", "OutCubic", "InOutCubic", "InQuart", "OutQuart", "InOutQuart", "InQuint", "OutQuint", "InOutQuint", 
				"InExpo", "OutExpo", "InOutExpo", "InCirc", "OutCirc", "InOutCirc", "InElastic", "OutElastic", "InOutElastic", "InBack", "OutBack", "InOutBack", "InBounce", "OutBounce", "InOutBounce", ":: AnimationCurve"
			};
		}



		public static Ease FilteredEasePopup(Ease currEase)
		{
			int index = (currEase == Ease.INTERNAL_Custom) ? (FilteredEaseTypes.Length - 1) : Array.IndexOf<string>(FilteredEaseTypes, currEase.ToString());
			if (index == -1)
			{
				index = 0;
			}
			index = EditorGUILayout.Popup("Ease", index, FilteredEaseTypes, new GUILayoutOption[0]);
			if (index != (FilteredEaseTypes.Length - 1))
			{
				return (Ease) Enum.Parse(typeof(Ease), FilteredEaseTypes[index]);
			}
			return Ease.INTERNAL_Custom;
		}

		public static void InspectorLogo()
		{
			GUILayout.Box(logo, logoIconStyle, new GUILayoutOption[0]);
		}

		public static void SetGUIStyles(Vector2? footerSize=null)
		{
			if (!_additionalStylesSet && footerSize.HasValue)
			{
				_additionalStylesSet = true;
				Vector2 vector = footerSize.Value;
				btImgStyle = new GUIStyle(GUI.skin.button);
				btImgStyle.normal.background = null;
				btImgStyle.imagePosition = ImagePosition.ImageOnly;
				btImgStyle.padding = new RectOffset(0, 0, 0, 0);
				btImgStyle.fixedWidth = vector.x;
				btImgStyle.fixedHeight = vector.y;
			}
			if (!_stylesSet)
			{
				_stylesSet = true;
				boldLabelStyle = new GUIStyle(GUI.skin.label);
				boldLabelStyle.fontStyle = FontStyle.Bold;
				redLabelStyle = new GUIStyle(GUI.skin.label);
				redLabelStyle.normal.textColor = Color.red;
				setupLabelStyle = new GUIStyle(boldLabelStyle);
				setupLabelStyle.alignment = TextAnchor.MiddleCenter;
				wrapCenterLabelStyle = new GUIStyle(GUI.skin.label);
				wrapCenterLabelStyle.wordWrap = true;
				wrapCenterLabelStyle.alignment = TextAnchor.MiddleCenter;
				btStyle = new GUIStyle(GUI.skin.button);
				btStyle.padding = new RectOffset(0, 0, 10, 10);
				GUIStyle style1 = new GUIStyle(GUI.skin.label) {
					fontSize = 12,
					fontStyle = FontStyle.Bold
				};
				titleStyle = style1;
				GUIStyle style2 = new GUIStyle(GUI.skin.label) {
					normal = { textColor = Color.white },
					alignment = TextAnchor.MiddleLeft
				};
				handlelabelStyle = style2;
				GUIStyle style3 = new GUIStyle(handlelabelStyle) {
					normal = { textColor = Color.yellow },
					fontStyle = FontStyle.Bold
				};
				handleSelectedLabelStyle = style3;
				wordWrapLabelStyle = new GUIStyle(GUI.skin.label);
				wordWrapLabelStyle.wordWrap = true;
				wordWrapItalicLabelStyle = new GUIStyle(wordWrapLabelStyle);
				wordWrapItalicLabelStyle.fontStyle = FontStyle.Italic;
				logoIconStyle = new GUIStyle(GUI.skin.box);
				logoIconStyle.active.background = (Texture2D) (logoIconStyle.normal.background = null);
				logoIconStyle.margin = new RectOffset(0, 0, 0, 0);
				logoIconStyle.padding = new RectOffset(0, 0, 0, 0);
				sideBtStyle = new GUIStyle(GUI.skin.button);
				sideBtStyle.margin.top = 1;
				sideBtStyle.padding = new RectOffset(0, 0, 2, 2);
				sideLogoIconBoldLabelStyle = new GUIStyle(boldLabelStyle);
				sideLogoIconBoldLabelStyle.alignment = TextAnchor.MiddleLeft;
				sideLogoIconBoldLabelStyle.padding.top = 2;
				wordWrapTextArea = new GUIStyle(GUI.skin.textArea);
				wordWrapTextArea.wordWrap = true;
				popupButton = new GUIStyle(EditorStyles.popup);
				popupButton.fixedHeight = 18f;
				RectOffset margin = popupButton.margin;
				margin.top++;
				btIconStyle = new GUIStyle(GUI.skin.button);
				RectOffset padding = btIconStyle.padding;
				padding.left -= 2;
				btIconStyle.fixedWidth = 24f;
				btIconStyle.stretchWidth = false;
			}
		}

		public static bool ToggleButton(bool toggled, GUIContent content,GUIStyle guiStyle=null, params GUILayoutOption[] options)
		{
			GUI.backgroundColor = toggled ? Color.green : Color.white;
			if ((guiStyle == null) ? GUILayout.Button(content, options) : GUILayout.Button(content, guiStyle, options))
			{
				toggled = !toggled;
				GUI.changed = true;
			}
			GUI.backgroundColor = GUI.backgroundColor;
			return toggled;
		}

		// Properties
		public static Texture2D logo
		{
			get
			{
				if (_logo == null)
				{
					string temp = "Assets" + EditorUtils.editorADBDir + "Imgs/DOTweenIcon.png";
					_logo = AssetDatabase.LoadAssetAtPath(temp, typeof(Texture2D)) as Texture2D;
					EditorUtils.SetEditorTexture(_logo, FilterMode.Bilinear);
				}
				return _logo;
			}
		}
	}

}