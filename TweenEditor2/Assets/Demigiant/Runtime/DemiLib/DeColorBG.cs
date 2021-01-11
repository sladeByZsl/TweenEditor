using System;
using UnityEngine;
using DG.DemiLib.Core;

namespace DG.DemiLib
{
	[Serializable]
	public class DeColorBG
	{
		public DeSkinColor critical;
		public DeSkinColor def;
		public DeSkinColor divider;
		public DeSkinColor toggleOff;
		public DeSkinColor toggleOn;

		public DeColorBG()
		{
			this.critical = Color.red;
			this.def = Color.white;
			this.divider = new DeSkinColor(new Color(0.5f,0.5f,0.5f,1f), Color.black);
			this.toggleOff = Color.white;
			this.toggleOn = Color.green;
		}
	}
}
