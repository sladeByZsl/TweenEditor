using UnityEngine;
using DG.DemiLib;
namespace DG.DemiEditor
{
	public static class GUIStyleExtensions
	{
		public static GUIStyle Add(this GUIStyle style, params object[] formats)
		{
			foreach (object obj2 in formats) 
			{
				var type = obj2.GetType ();
				if (type == typeof(Format)) 
				{
					switch (((Format)obj2)) 
					{
					case Format.RichText:
						style.richText = true;
						break;
					case Format.WordWrap:
						style.wordWrap = true;
						break;
					}
				} 
				else if (type == typeof(FontStyle)) 
				{
					style.fontStyle = (FontStyle)obj2;
				} 
				else if (type == typeof(TextAnchor)) 
				{
					style.alignment = (TextAnchor)obj2;
				}
				else if (type == typeof(int)) 
				{
					style.fontSize = (int)obj2;
				}
				else if ((type == typeof(Color)) || (type == typeof(DeSkinColor))) 
				{
					style.normal.textColor = style.active.textColor = (type == typeof(Color))?((Color)obj2):((Color)((DeSkinColor)obj2));
				}
			}
			return style;
		}

		public static GUIStyle Background(this GUIStyle style, Texture2D background)
		{
			style.normal.background = style.active.background = background;
			return style;
		}

		public static GUIStyle Border(this GUIStyle style, RectOffset border)
		{
			style.border = border;
			return style;
		}

		public static GUIStyle Border(this GUIStyle style, int left, int right, int top, int bottom)
		{
			style.border = new RectOffset (left, right, top, bottom);
			return style;
		}

		public static GUIStyle Clone(this GUIStyle style, params object[] formats)
		{
			return new GUIStyle(style).Add(formats);
		}
		public static GUIStyle ContentOffset(this GUIStyle style, Vector2 offset)
		{
			style.contentOffset = offset;
			return style;
		}
		public static GUIStyle ContentOffsetX(this GUIStyle style, float offsetX)
		{
			Vector2 contentOffset = style.contentOffset;
			contentOffset.x = offsetX;
			style.contentOffset = contentOffset;
			return style;
		}
		public static GUIStyle ContentOffsetY(this GUIStyle style, float offsetY)
		{
			Vector2 contentOffset = style.contentOffset;
			contentOffset.y = offsetY;
			style.contentOffset = contentOffset;
			return style;
		}
		public static GUIStyle Height(this GUIStyle style, int height)
		{
			style.fixedHeight = height;
			return style;
		}
		public static GUIStyle Margin(this GUIStyle style, RectOffset margin)
		{
			style.margin = margin;
			return style;
		}
		public static GUIStyle Margin(this GUIStyle style, int left, int right, int top, int bottom)
		{
			style.margin = new RectOffset(left, right, top, bottom);
			return style;
		}
		public static GUIStyle MarginBottom(this GUIStyle style, int bottom)
		{
			style.margin.bottom = bottom;
			return style;
		}
		public static GUIStyle MarginLeft(this GUIStyle style, int left)
		{
			style.margin.left = left;
			return style;
		}
		public static GUIStyle MarginRight(this GUIStyle style, int right)
		{
			style.margin.right = right;
			return style;
		}
		public static GUIStyle MarginTop(this GUIStyle style, int top)
		{
			style.margin.top = top;
			return style;
		}
		public static GUIStyle Padding(this GUIStyle style, int padding)
		{ 
			style.padding = new RectOffset(padding,padding,padding,padding);
			return style;
		}
		public static GUIStyle Padding(this GUIStyle style,  int left, int right, int top, int bottom)
		{
			style.padding = new RectOffset(left, right, top, bottom);
			return style;
		}
		public static GUIStyle PaddingBottom(this GUIStyle style, int bottom)
		{
			style.padding.bottom = bottom;
			return style;
		}
		public static GUIStyle PaddingLeft(this GUIStyle style, int left)
		{
			style.padding.left = left;
			return style;
		}
		public static GUIStyle PaddingRight(this GUIStyle style, int right)
		{
			style.padding.right = right;
			return style;
		}
		public static GUIStyle PaddingTop(this GUIStyle style, int top)
		{
			style.padding.top = top;
			return style;
		}
		public static GUIStyle StretchHeight(this GUIStyle style, bool doStretch=true)
		{
			style.stretchHeight = doStretch;
			return style;
		}
		public static GUIStyle StretchWidth(this GUIStyle style, bool doStretch=true)
		{
			style.stretchWidth = doStretch;
			return style;
		}
		public static GUIStyle Width(this GUIStyle style, float width)
		{
			style.fixedWidth = width;
			return style;
		}
	}
}
