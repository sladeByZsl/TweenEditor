using UnityEditor;
using DG.DemiLib;
using DG.Tweening.Core;

namespace DG.DOTweenEditor.Core
{
	[CustomEditor(typeof(ABSAnimationComponent))]
	public class ABSAnimationInspector : Editor
	{
		// Fields
		public static ColorPalette colors = new ColorPalette();
		public SerializedProperty onCompleteProperty;
		public SerializedProperty onPlayProperty;
		public SerializedProperty onStartProperty;
		public SerializedProperty onStepCompleteProperty;
		public SerializedProperty onTweenCreatedProperty;
		public SerializedProperty onUpdateProperty;
		public static StylePalette styles = new StylePalette();

		// Methods
		public override void OnInspectorGUI()
		{
			DeGUI.BeginGUI(colors, styles);
		}
	}
}