using UnityEngine;
using System.Collections;
using System;

namespace DG.DemiLib
{
	[Serializable]
	public class DeColorPalette
	{
		public DeColorBG bg;
		public DeColorContent content;

		public DeColorPalette()
		{
			this.bg = new DeColorBG ();
			this.content = new DeColorContent ();
		}
	}
}