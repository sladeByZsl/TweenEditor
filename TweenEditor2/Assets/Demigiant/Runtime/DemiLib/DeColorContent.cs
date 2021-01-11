using UnityEngine;
using System.Collections;
using System;

namespace DG.DemiLib
{
	[Serializable]
	public class DeColorContent
	{
		public DeSkinColor critical;
		public DeSkinColor def;
		public DeSkinColor toggleOff;
		public DeSkinColor toggleOn;

		public DeColorContent()
		{
			this.critical = new DeSkinColor(new Color(1.0f,0.8458418f,0.4411765f,1.0f), new Color(1.0f,0.1691176f,0.1691176f,1.0f));
			this.def = new DeSkinColor(new Color(0.7f,0.7f,0.7f,1f));
			this.toggleOff = new DeSkinColor(new Color(1.0f,0.9686275f,0.6980392f,1.0f), new Color(0.8117647f,1.0f,0.5607843f,1.0f));
			this.toggleOn = new DeSkinColor(new Color(0.3529412f,0.3647059f,0.3647059f,1.0f), new Color(0.5294118f,0.5294118f,0.5294118f,1.0f));
		}
	}
}