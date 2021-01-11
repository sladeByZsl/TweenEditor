using UnityEngine;
using System.Collections;

namespace DG.DemiEditor
{
	public class Misc
	{
		public GUIStyle line;
		public Misc()
		{
		}
		internal void Init()
		{
			this.line = new GUIStyle (GUI.skin.box).Padding (0, 0, 0, 0).Margin (0, 0, 0, 0).Background (DeStylePalette.whiteSquare);
		}
	}
}
