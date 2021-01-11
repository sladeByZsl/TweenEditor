using UnityEngine;
using System.Collections;
using UnityEditor;


namespace DG.DemiEditor
{
	public class Button
	{
		public GUIStyle def;
		public GUIStyle tool;
		public GUIStyle toolFoldoutClosed;
		public GUIStyle toolFoldoutClosedWLabel;
		public GUIStyle toolFoldoutOpen;
		public GUIStyle toolFoldoutOpenWLabel;
		public GUIStyle toolIco;
		public GUIStyle ToolL;


		internal void Init()
		{
			this.def = new GUIStyle (GUI.skin.button);
			this.tool = new GUIStyle (EditorStyles.toolbarButton).ContentOffsetY (-1f);
			this.ToolL = new GUIStyle (EditorStyles.toolbarButton).Height (0x17).ContentOffsetY (0f);
			this.toolIco = new GUIStyle (this.tool).StretchWidth (false).Width (22f).ContentOffsetX (-1f);
			GUIStyle style = new GUIStyle (GUI.skin.button) {
				alignment = TextAnchor.UpperLeft
			};
			style.active.background = null;
			style.fixedWidth = 14f;
			style.normal.background = EditorStyles.foldout.normal.background;
			style.border = EditorStyles.foldout.border;
			style.padding = new RectOffset (14, 0, 0, 0);
			this.toolFoldoutClosed = style.MarginTop (2);
			this.toolFoldoutClosedWLabel = new GUIStyle (this.toolFoldoutClosed).Width (0f).StretchWidth (false);
			this.toolFoldoutOpen = new GUIStyle (this.toolFoldoutClosed){ normal = { background = EditorStyles.foldout.onNormal.background } };
			this.toolFoldoutOpenWLabel = new GUIStyle (this.toolFoldoutClosedWLabel){ normal = { background = EditorStyles.foldout.onNormal.background } };
		}
	}

}
