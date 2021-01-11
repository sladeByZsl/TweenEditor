using UnityEngine;
using DG.DemiLib.Core;
using System.Reflection;
using UnityEditor;
using DG.DemiLib;
using System.Collections;
using System.IO;
using System;
using System.Runtime.InteropServices;

namespace DG.DemiEditor
{
	public enum Format
	{
		RichText,
		WordWrap
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct DeSkinStyle
	{
		public GUIStyle free;
		public GUIStyle pro;

		public DeSkinStyle(GUIStyle style)
		{
			this = new DeSkinStyle();
			this.free = style;
			this.pro = new GUIStyle(style);
		}

		public DeSkinStyle(GUIStyle free, GUIStyle pro)
		{
			this.free = free;
			this.pro = pro;
		}

		public static implicit operator GUIStyle(DeSkinStyle v)
		{
			return (GUIUtils.isProSkin ? v.pro : v.free);
		}

	}

	public class Box
	{
		public GUIStyle def;
		public GUIStyle flat;
		public GUIStyle flatAlpha10;
		public GUIStyle flatAlpha25;
		public DeSkinStyle sticky;
		public DeSkinStyle stickyTop;

		public Box()
		{
		}

		internal void Init()
		{
			this.def = new GUIStyle(GUI.skin.box).Padding (6, 6, 6, 6);
			this.flat = new GUIStyle (this.def).Background (DeStylePalette.whiteSquare);
			this.flatAlpha10 = new GUIStyle (this.def).Background (DeStylePalette.whiteSquareAlpha10);
			this.flatAlpha25 = new GUIStyle (this.def).Background (DeStylePalette.whiteSquareAlpha25);
			this.sticky = new DeSkinStyle (new GUIStyle (this.flatAlpha25).MarginTop (-2).MarginBottom (0));
			this.stickyTop = new DeSkinStyle (new GUIStyle (this.flatAlpha25).MarginTop (-2).MarginBottom (7), new GUIStyle (this.flatAlpha10).MarginTop (-2).MarginBottom (7));
		}
	}

}