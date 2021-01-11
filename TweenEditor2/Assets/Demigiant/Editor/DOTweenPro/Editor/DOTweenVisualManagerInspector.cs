using UnityEditor;
using DG.Tweening;
using UnityEngine;
using DG.Tweening.Core;
using UnityEditorInternal;
using DG.DOTweenEditor.Core;

namespace DG.DOTweenEditor
{
	[CustomEditor(typeof(DOTweenVisualManager))]
	public class DOTweenVisualManagerInspector : Editor
	{
		// Fields
		private DOTweenVisualManager _src;

		// Methods
		private void OnEnable()
		{
			this._src = base.target as DOTweenVisualManager;
			if (!Application.isPlaying)
			{
				MonoBehaviour[] components = this._src.GetComponents<MonoBehaviour>();
				int index = ArrayUtility.IndexOf<MonoBehaviour>(components, this._src);
				int num2 = 0;
				for (int i = 0; i < index; i++)
				{
					if (components[i] is ABSAnimationComponent)
					{
						num2++;
					}
				}
				while (num2 > 0)
				{
					num2--;
					ComponentUtility.MoveComponentUp(this._src);
				}
			}
		}

		public override void OnInspectorGUI()
		{
			EditorGUIUtils.SetGUIStyles(null);
			EditorGUIUtility.labelWidth = 80f;
			EditorGUIUtils.InspectorLogo();
			VisualManagerPreset preset = this._src.preset;
			this._src.preset = (VisualManagerPreset) EditorGUILayout.EnumPopup("Preset", this._src.preset, new GUILayoutOption[0]);
			if ((preset != this._src.preset) && (this._src.preset == VisualManagerPreset.PoolingSystem))
			{
				this._src.onEnableBehaviour = OnEnableBehaviour.RestartFromSpawnPoint;
				this._src.onDisableBehaviour = OnDisableBehaviour.Rewind;
			}
			GUILayout.Space(6f);
			bool flag = this._src.preset > VisualManagerPreset.Custom;
			OnEnableBehaviour onEnableBehaviour = this._src.onEnableBehaviour;
			OnDisableBehaviour onDisableBehaviour = this._src.onDisableBehaviour;
			this._src.onEnableBehaviour = (OnEnableBehaviour) EditorGUILayout.EnumPopup(new GUIContent("On Enable", "Eventual actions to perform when this gameObject is activated"), this._src.onEnableBehaviour, new GUILayoutOption[0]);
			this._src.onDisableBehaviour = (OnDisableBehaviour) EditorGUILayout.EnumPopup(new GUIContent("On Disable", "Eventual actions to perform when this gameObject is deactivated"), this._src.onDisableBehaviour, new GUILayoutOption[0]);
			if ((flag && (onEnableBehaviour != this._src.onEnableBehaviour)) || (onDisableBehaviour != this._src.onDisableBehaviour))
			{
				this._src.preset = VisualManagerPreset.Custom;
			}
			if (GUI.changed)
			{
				EditorUtility.SetDirty(this._src);
			}
		}
	}
}