using DG.DemiEditor;
using UnityEngine;
using DG.DemiLib;

namespace DG.DOTweenEditor.Core
{
	public class StylePalette : DeStylePalette
	{
		// Fields
		public readonly Custom custom = new Custom();

		// Nested Types
		public class Custom : DeStyleSubPalette
		{
			// Fields
			public GUIStyle stickyTitle;
			public GUIStyle stickyToolbar;

			// Methods
			public override void Init()
			{
				if (DeGUI.styles.toolbar.flat == null) 
				{
					DeGUI.styles.toolbar.Init ();
				}
				this.stickyToolbar = new GUIStyle(DeGUI.styles.toolbar.flat);
				object[] objArray1 = new object[] { FontStyle.Bold, 11 };
				this.stickyTitle = GUIStyleExtensions.ContentOffsetX(GUIStyleExtensions.MarginBottom(GUIStyleExtensions.Clone(new GUIStyle(GUI.skin.label), objArray1), 0), -2f);
			}
		}
	}
}