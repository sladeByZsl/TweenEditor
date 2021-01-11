using System;
using DG.DemiLib;
using UnityEngine;

namespace DG.DOTweenEditor.Core
{
	[Serializable]
	public class ColorPalette : DeColorPalette
	{
		// Fields
		public Custom custom = new Custom();

		// Nested Types
		[Serializable]
		public class Custom
		{
			// Fields
			public DeSkinColor stickyDivider = new DeSkinColor(Color.black, new Color(0.5f, 0.5f, 0.5f));
		}
	}
}