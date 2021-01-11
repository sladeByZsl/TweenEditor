using UnityEngine;
using DG.Tweening.Core;
using DG.DemiLib;
using UnityEditor;

namespace DG.DOTweenEditor.Core
{
	public class AnimationInspectorGUI
	{
		// Methods
		public static void AnimationEvents(ABSAnimationInspector inspector, ABSAnimationComponent src)
		{
			GUILayout.Space(6f);
			StickyTitle("Events");
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			src.hasOnStart = DeGUILayout.ToggleButton(src.hasOnStart, new GUIContent("OnStart", "Event called the first time the tween starts, after any eventual delay"), ABSAnimationInspector.styles.button.tool, new GUILayoutOption[0]);
			src.hasOnPlay = DeGUILayout.ToggleButton(src.hasOnPlay, new GUIContent("OnPlay", "Event called each time the tween status changes from a pause to a play state (including the first time the tween starts playing), after any eventual delay"), ABSAnimationInspector.styles.button.tool, new GUILayoutOption[0]);
			src.hasOnUpdate = DeGUILayout.ToggleButton(src.hasOnUpdate, new GUIContent("OnUpdate", "Event called every frame while the tween is playing"), ABSAnimationInspector.styles.button.tool, new GUILayoutOption[0]);
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			src.hasOnStepComplete = DeGUILayout.ToggleButton(src.hasOnStepComplete, new GUIContent("OnStep", "Event called at the end of each loop cycle"), ABSAnimationInspector.styles.button.tool, new GUILayoutOption[0]);
			src.hasOnComplete = DeGUILayout.ToggleButton(src.hasOnComplete, new GUIContent("OnComplete", "Event called at the end of the tween, all loops included"), ABSAnimationInspector.styles.button.tool, new GUILayoutOption[0]);
			src.hasOnTweenCreated = DeGUILayout.ToggleButton(src.hasOnTweenCreated, new GUIContent("OnCreated", "Event called as soon as the tween is instantiated"), ABSAnimationInspector.styles.button.tool, new GUILayoutOption[0]);
			GUILayout.EndHorizontal();
			if ((((src.hasOnStart || src.hasOnPlay) || (src.hasOnUpdate || src.hasOnStepComplete)) || src.hasOnComplete) || src.hasOnTweenCreated)
			{
				inspector.serializedObject.Update();
				DeGUILayout.BeginVBox(DeGUI.styles.box.stickyTop);
				if (src.hasOnStart)
				{
					EditorGUILayout.PropertyField(inspector.onStartProperty, new GUILayoutOption[0]);
				}
				if (src.hasOnPlay)
				{
					EditorGUILayout.PropertyField(inspector.onPlayProperty, new GUILayoutOption[0]);
				}
				if (src.hasOnUpdate)
				{
					EditorGUILayout.PropertyField(inspector.onUpdateProperty, new GUILayoutOption[0]);
				}
				if (src.hasOnStepComplete)
				{
					EditorGUILayout.PropertyField(inspector.onStepCompleteProperty, new GUILayoutOption[0]);
				}
				if (src.hasOnComplete)
				{
					EditorGUILayout.PropertyField(inspector.onCompleteProperty, new GUILayoutOption[0]);
				}
				if (src.hasOnTweenCreated)
				{
					EditorGUILayout.PropertyField(inspector.onTweenCreatedProperty, new GUILayoutOption[0]);
				}
				inspector.serializedObject.ApplyModifiedProperties();
				DeGUILayout.EndVBox();
			}
			else
			{
				GUILayout.Space(4f);
			}
		}

		public static void StickyTitle(string text)
		{
			if (ABSAnimationInspector.styles.custom.stickyTitle == null) 
			{
				ABSAnimationInspector.styles.custom.Init ();
			}

			GUILayout.Label(text, ABSAnimationInspector.styles.custom.stickyTitle, new GUILayoutOption[0]);
			DeGUILayout.HorizontalDivider(new Color?(ABSAnimationInspector.colors.custom.stickyDivider), 2, 0, 0);
		}
	}
}