using UnityEngine;
using System.Collections;
using System;
using DG.DemiLib.Core;
using System.Runtime.InteropServices;

namespace DG.DemiLib
{
	[Serializable, StructLayout(LayoutKind.Sequential)]
	public struct DeSkinColor
	{
		public Color free;
		public Color pro;

		public DeSkinColor(Color free, Color pro)
		{
			this.free = free;
			this.pro = pro;
		}

		public DeSkinColor(Color color)
		{
			this = new DeSkinColor();
			this.free = color;
			this.pro = color;
		}

		public static implicit operator Color(DeSkinColor v)
		{
			return (GUIUtils.isProSkin ? v.pro : v.free);
		}

		public static implicit operator DeSkinColor(Color v)
		{
			return new DeSkinColor(v);
		}

		public override string ToString ()
		{
			return string.Format ("{0},{1}", this.free, this.pro);
		}
	}
}