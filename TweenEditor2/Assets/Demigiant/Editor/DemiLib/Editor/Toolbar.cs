using UnityEditor;
using DG.DemiEditor;
using UnityEngine;

namespace DG.DemiLib
{
	public class Toolbar
	{
		public GUIStyle box;
		public GUIStyle def;
		public GUIStyle flat;
		public GUIStyle large;
		public GUIStyle stickyTop;

		internal void Init()
		{
			this.def = new GUIStyle (EditorStyles.toolbar).Height (0x12).StretchWidth (true);
			this.large = new GUIStyle (this.def).Height (0x17);
			this.stickyTop = new GUIStyle (this.def).MarginTop (0);
			this.box = new GUIStyle (GUI.skin.box).Height (20).StretchWidth (true).Padding (5, 6, 1, 0).Margin (0, 0, 0, 0);
			this.flat = new GUIStyle (GUI.skin.box).Height (0x12).StretchWidth (true).Padding (5, 6, 0, 0).Margin (0, 0, 0, 0).Background (DeStylePalette.whiteSquare);
		}
	}
}
