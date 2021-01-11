using UnityEngine;
using System.Collections;
using System.Reflection;
using UnityEditor;
using DG.DemiLib;

namespace DG.DemiEditor
{
	public class DeStylePalette
	{
		private static string _fooAdbImgsDir;
		private static Texture2D _fooWhiteSquare;
		private static Texture2D _fooWhiteSquareAlpha10;
		private static Texture2D _fooWhiteSquareAlpha25;
		private static Texture2D _fooWhiteSquareAlpha50;
		public readonly Box box;
		public readonly Button button;
		protected bool initialized;
		public readonly Label label;
		public readonly Misc misc;
		public readonly Toolbar toolbar;

		public DeStylePalette()
		{
			this.box = new Box();
			this.button = new Button();
			this.label = new Label();
			this.toolbar = new Toolbar();
			this.misc = new Misc();
		}

		internal void Init()
		{
			if (!this.initialized) 
			{
				this.initialized = true;
				this.box.Init ();
				this.button.Init ();
				this.label.Init ();
				this.toolbar.Init ();
				this.misc.Init ();
				FieldInfo[] fields = base.GetType ().GetFields (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
				foreach (FieldInfo info in fields) 
				{
					if (info.FieldType.BaseType == typeof(DeStyleSubPalette)) 
					{
						((DeStyleSubPalette)info.GetValue (this)).Init ();
					}
				}
			}
		}

		private static string _adbImgsDir
		{
			get
			{
				if (_fooAdbImgsDir == null) 
				{
					_fooAdbImgsDir = Assembly.GetExecutingAssembly().ADBDir()+"/Imgs/";
				}
				return _fooAdbImgsDir;
			}
		}

		public static Texture2D whiteSquare
		{
			get
			{ 
				if (_fooWhiteSquare == null) 
				{
					_fooWhiteSquare = AssetDatabase.LoadAssetAtPath ("Assets" + _adbImgsDir + "whiteSquare.png", typeof(Texture2D)) as Texture2D;
					_fooWhiteSquare.SetFormat (FilterMode.Point, 16);
				}
				return _fooWhiteSquare;
			}
		}

		public static Texture2D whiteSquareAlpha10
		{
			get
			{
				if (_fooWhiteSquareAlpha10 == null) 
				{
					_fooWhiteSquareAlpha10 = AssetDatabase.LoadAssetAtPath ("Assets" + _adbImgsDir + "whiteSquareAlpha10.png", typeof(Texture2D)) as Texture2D;
					_fooWhiteSquareAlpha10.SetFormat (FilterMode.Point, 16);
				}
				return _fooWhiteSquareAlpha10;
			}
		}

		public static Texture2D whiteSquareAlpha25
		{
			get
			{
				if (_fooWhiteSquareAlpha25 == null) 
				{
					_fooWhiteSquareAlpha25 = AssetDatabase.LoadAssetAtPath ("Assets" + _adbImgsDir + "whiteSquareAlpha25.png", typeof(Texture2D)) as Texture2D;
					_fooWhiteSquareAlpha25.SetFormat (FilterMode.Point, 16);
				}
				return _fooWhiteSquareAlpha25;
			}
		}

		public static Texture2D whiteSquareAlpha50
		{
			get
			{
				if (_fooWhiteSquareAlpha50 == null) 
				{
					_fooWhiteSquareAlpha50 = AssetDatabase.LoadAssetAtPath ("Assets" + _adbImgsDir + "whiteSquareAlpha50.png", typeof(Texture2D)) as Texture2D;
					_fooWhiteSquareAlpha50.SetFormat (FilterMode.Point, 16);
				}
				return _fooWhiteSquareAlpha50;
			}
		}
	}

	public abstract class DeStyleSubPalette
	{
		protected DeStyleSubPalette()
		{

		}
		public abstract void Init();
	}
}
