using UnityEngine;
using System.Collections;

namespace DG.DemiEditor
{
	public class Label
	{
		public GUIStyle bold;
		public GUIStyle toolbar;
		public GUIStyle toolbarBox;
		public GUIStyle toolbarL;
		public GUIStyle wordwrap;
		public GUIStyle wordwrapRichtText;

		public Label()
		{
		}
		internal void Init()
		{
			object[] formats = new object[]{FontStyle.Bold};
			this.bold = new GUIStyle (GUI.skin.label).Add(formats);
			object[] objArray2 = new object[]{ Format.WordWrap};
			this.wordwrap = new GUIStyle (GUI.skin.label).Add (objArray2);
			object[] objArray3 = new object[]{ Format.RichText };
			this.wordwrapRichtText = this.wordwrap.Clone (objArray3);
			object[] objArray4 = new object[]{ 9 };
			this.toolbar = new GUIStyle (GUI.skin.label).Add (objArray4).ContentOffset (new Vector2 (-2f, 0f));
			this.toolbarL = new GUIStyle (this.toolbar).ContentOffsetY (2f);
			this.toolbarBox = new GUIStyle (this.toolbar).ContentOffsetY (0f);
		}
	}
}
