using UnityEngine;
using System.Collections;
using UnityEditor;
using DG.Tweening.Core;

namespace DG.DOTweenEditor
{
	[CustomEditor(typeof(DOTweenSettings))]
	public class DOTweenSettingsInspector : Editor
	{
		
		// Fields
		//private DOTweenSettings _src;

		// Methods
		public DOTweenSettingsInspector()
		{

		}
		private void OnEnable()
		{
			//this._src = base.target as DOTweenSettings;
		}

		public override void OnInspectorGUI()
		{
			GUI.enabled = false;
			base.DrawDefaultInspector();
			GUI.enabled = true;
		}
	}
}


