//using UnityEngine;
//using System.Collections;
//using UnityEditor;
//using DG.Tweening.Core;
//using System.Text;
//using DG.Tweening;
//using DG.DOTweenEditor.Core;
//
//namespace DG.DOTweenEditor
//{
//	[CustomEditor(typeof(DOTweenComponent))]
//	public class DOTweenInspector : Editor
//	{
//		private bool _showPausedTweensData = true;
//		private bool _showPlayingTweensData = true;
//		private readonly StringBuilder _strBuilder;
//		//private string _title;
//
//		public DOTweenInspector()
//		{
//			this._strBuilder = new StringBuilder ();
//		}
//
//		private void OnEnable()
//		{
//			this._strBuilder.Remove (0, this._strBuilder.Length);
//			this._strBuilder.Append ("DOTween v").Append (DOTween.Version);
//			if (DOTween.isDebugBuild) {
//				this._strBuilder.Append ("[Debug build]");
//			} 
//			else 
//			{
//				this._strBuilder.Append ("[Release build]");
//			}
//			if (EditorUtils.hasPro) {
//				this._strBuilder.Append ("\nDOTweenPro v").Append (EditorUtils.proVersion);
//			} 
//			else 
//			{
//				this._strBuilder.Append ("\nDOTweenPro not installed");
//			}
//			//this._title = this._strBuilder.ToString ();
//		}
//
//		public override void OnInspectorGUI()
//		{
//			EditorGUIUtils.SetGUIStyles (null);
//			int totActiveTweens = TweenManager.totActiveTweens;
//			int num2 = TweenManager.TotalPlayingTweens ();
//			int num3 = totActiveTweens - num2;
//			int totActiveDefaultTweens = TweenManager.totActiveDefaultTweens;
//			int totActiveLateTweens = TweenManager.totActiveLateTweens;
//			//GUILayout.Space (4f);
////			GUILayout.BeginHorizontal (new GUILayoutOption[0]);
////			if (GUILayout.Button ("Documentation", new GUILayoutOption[0])) 
////			{
////				Application.OpenURL ("http://dotween.demigiant.com/documentation.php");
////			}
////			if (GUILayout.Button ("Check Updates", new GUILayoutOption[0])) 
////			{
////				Application.OpenURL ("http://dotween.demigiant.com/download.php?v=" + DOTween.Version);
////			}
////			GUILayout.EndHorizontal ();
////			GUILayout.BeginHorizontal (new GUILayoutOption[0]);
////			if (GUILayout.Button (this._showPlayingTweensData ? "Hide Playing Tweens" : "Show Playing Tweens", new GUILayoutOption[0])) 
////			{
////				this._showPlayingTweensData = !this._showPlayingTweensData;
////			}
////			if (GUILayout.Button (this._showPausedTweensData ? "Hide Paused Tweens" : "Show Paused Tweens", new GUILayoutOption[0])) 
////			{
////				this._showPausedTweensData = !this._showPausedTweensData;
////			}
//			//GUILayout.EndHorizontal ();
//			GUILayout.Space (8f);
//			this._strBuilder.Length = 0;
//			this._strBuilder.Append ("Active tweens:").Append (totActiveTweens).Append ("(").Append (TweenManager.totActiveTweeners).Append ("/").Append (TweenManager.totActiveSequences).Append (")").Append ("\nDefault/Late tweens:").Append (totActiveDefaultTweens).Append ("/").Append (totActiveLateTweens).Append ("\nPlaying tweens:").Append (num2);
//			if (this._showPlayingTweensData) 
//			{
//				foreach (Tween tween in TweenManager._activeTweens) 
//				{
//					bool temp = tween.IsPlaying();
//					if ((tween != null) && temp) 
//					{
//						this._strBuilder.Append ("\n -[").Append (tween.tweenType).Append ("]").Append (tween.target);
//					}
//				}
//			}
//			this._strBuilder.Append ("\nPaused tweens:").Append (num3);
//			if (this._showPausedTweensData) 
//			{
//				foreach (Tween tween2 in TweenManager._activeTweens) 
//				{
//					if ((tween2 != null) && !tween2.IsPlaying()) 
//					{
//						this._strBuilder.Append ("\n -[").Append (tween2.tweenType).Append ("]").Append (tween2.target);
//					}
//				}
//			}
//			this._strBuilder.Append ("\nPooled tweens:").Append (TweenManager.TotalPooledTweens ()).Append ("(").Append (TweenManager.totPooledTweeners).Append ("/").Append (TweenManager.totPooledSequences).Append (")");
//			GUILayout.Label (this._strBuilder.ToString (), new GUILayoutOption[0]);
//			GUILayout.Space (8f);
//			this._strBuilder.Remove (0, this._strBuilder.Length);
//			this._strBuilder.Append ("Tweens Capacity:").Append (TweenManager.maxTweeners).Append ("/").Append (TweenManager.maxSequences).Append ("\nMax Simultaneous Active Tweens:").Append (DOTween.maxActiveTweenersReached).Append ("/").Append (DOTween.maxActiveSequencesReached);
//			GUILayout.Label (this._strBuilder.ToString (), new GUILayoutOption[0]);
//			GUILayout.Space (8f);
//			this._strBuilder.Remove (0, this._strBuilder.Length);
//			this._strBuilder.Append("SETTINGS ▼");
//			this._strBuilder.Append("\nSafe Mode: ").Append(DOTween.useSafeMode ? "ON" : "OFF");
//			this._strBuilder.Append("\nLog Behaviour: ").Append(DOTween.logBehaviour);
//			this._strBuilder.Append("\nShow Unity Editor Report: ").Append(DOTween.showUnityEditorReport);
//			this._strBuilder.Append("\nTimeScale (Unity/DOTween): ").Append(Time.timeScale).Append("/").Append(DOTween.timeScale);
//			GUILayout.Label(this._strBuilder.ToString(), new GUILayoutOption[0]);
//			GUILayout.Label("NOTE: DOTween's TimeScale is not the same as Unity's Time.timeScale: it is actually multiplied by it except for tweens that are set to update independently", EditorGUIUtils.wordWrapItalicLabelStyle, new GUILayoutOption[0]);
//			GUILayout.Space(8f);
//			this._strBuilder.Remove(0, this._strBuilder.Length);
//			this._strBuilder.Append("DEFAULTS ▼");
//			this._strBuilder.Append("\ndefaultRecyclable: ").Append(DOTween.defaultRecyclable);
//			this._strBuilder.Append("\ndefaultUpdateType: ").Append(DOTween.defaultUpdateType);
//			this._strBuilder.Append("\ndefaultTSIndependent: ").Append(DOTween.defaultTimeScaleIndependent);
//			this._strBuilder.Append("\ndefaultAutoKill: ").Append(DOTween.defaultAutoKill);
//			this._strBuilder.Append("\ndefaultAutoPlay: ").Append(DOTween.defaultAutoPlay);
//			this._strBuilder.Append("\ndefaultEaseType: ").Append(DOTween.defaultEaseType);
//			this._strBuilder.Append("\ndefaultLoopType: ").Append(DOTween.defaultLoopType);
//			GUILayout.Label(this._strBuilder.ToString(), new GUILayoutOption[0]);
//			GUILayout.Space(10f);
//		}
//	}
//}