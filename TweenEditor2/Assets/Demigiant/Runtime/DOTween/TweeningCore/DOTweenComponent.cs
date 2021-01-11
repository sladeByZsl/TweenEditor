using UnityEngine;
using DG.Tweening;
using System.Collections;
using DG.Tweening.Core;
using System.Diagnostics;
using System.Collections.Generic;
using System;

namespace DG.Tweening.Core
{
	//[AddComponentMenu("DOTween/DOTweenComponent")]
	public class DOTweenComponent : MonoBehaviour, IDOTweenInit
	{
		// Fields
		private bool _duplicateToDestroy;
		private float _unscaledDeltaTime;
		private float _unscaledTime;
		//public int inspectorUpdater;

		// Methods
		private void Awake()
		{
			//this.inspectorUpdater = 0;
			this._unscaledTime = Time.realtimeSinceStartup;
		}

		internal static void Create()
		{
			if (DOTween.instance == null)
			{
				GameObject target = new GameObject("[DOTween]");
				UnityEngine.Object.DontDestroyOnLoad(target);
				DOTween.instance = target.AddComponent<DOTweenComponent>();
			}
		}

		internal static void DestroyInstance()
		{
			if (DOTween.instance != null)
			{
				UnityEngine.Object.Destroy(DOTween.instance.gameObject);
			}
			DOTween.instance = null;
		}

		private void FixedUpdate()
		{
			if (TweenManager.hasActiveFixedTweens && (Time.timeScale > 0f))
			{
				TweenManager.Update(UpdateType.Fixed, (DOTween.useSmoothDeltaTime ? Time.smoothDeltaTime : Time.deltaTime) * DOTween.timeScale, ((DOTween.useSmoothDeltaTime ? Time.smoothDeltaTime : Time.deltaTime) / Time.timeScale) * DOTween.timeScale);
			}
		}

		private void LateUpdate()
		{
			if (TweenManager.hasActiveLateTweens)
			{
				TweenManager.Update(UpdateType.Late, (DOTween.useSmoothDeltaTime ? Time.smoothDeltaTime : Time.deltaTime) * DOTween.timeScale, this._unscaledDeltaTime * DOTween.timeScale);
			}
		}

		private void OnApplicationQuit()
		{
			DOTween.isQuitting = true;
		}

		private void OnDestroy()
		{
			if (!this._duplicateToDestroy)
			{
				if (DOTween.showUnityEditorReport)
				{
					Debugger.LogReport(string.Concat(new object[] { "REPORT > Max overall simultaneous active Tweeners/Sequences: ", DOTween.maxActiveTweenersReached, "/", DOTween.maxActiveSequencesReached }));
				}
				if (DOTween.instance == this)
				{
					DOTween.instance = null;
				}
			}
		}

		private void OnDrawGizmos()
		{
			if (DOTween.drawGizmos && DOTween.isUnityEditor)
			{
				int count = DOTween.GizmosDelegates.Count;
				if (count != 0)
				{
					for (int i = 0; i < count; i++)
					{
						DOTween.GizmosDelegates[i]();
					}
				}
			}
		}

		//private void OnLevelWasLoaded()
		//{
		//	if (DOTween.useSafeMode)
		//	{
		//		DOTween.Validate();
		//	}
		//}

		public IDOTweenInit SetCapacity(int tweenersCapacity, int sequencesCapacity)
		{
			TweenManager.SetCapacities(tweenersCapacity, sequencesCapacity);
			return this;
		}

		private void Start()
		{
			if (DOTween.instance != this)
			{
				this._duplicateToDestroy = true;
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		private void Update()
		{
			this._unscaledDeltaTime = Time.realtimeSinceStartup - this._unscaledTime;
			if (TweenManager.hasActiveDefaultTweens)
			{
				TweenManager.Update(UpdateType.Normal, (DOTween.useSmoothDeltaTime ? Time.smoothDeltaTime : Time.deltaTime) * DOTween.timeScale, this._unscaledDeltaTime * DOTween.timeScale);
			}
			this._unscaledTime = Time.realtimeSinceStartup;
			if (DOTween.isUnityEditor)
			{
				//this.inspectorUpdater++;
				if (DOTween.showUnityEditorReport && TweenManager.hasActiveTweens)
				{
					if (TweenManager.totActiveTweeners > DOTween.maxActiveTweenersReached)
					{
						DOTween.maxActiveTweenersReached = TweenManager.totActiveTweeners;
					}
					if (TweenManager.totActiveSequences > DOTween.maxActiveSequencesReached)
					{
						DOTween.maxActiveSequencesReached = TweenManager.totActiveSequences;
					}
				}
			}
		}
		internal IEnumerator WaitForCompletion(Tween t)
		{
			yield return t.WaitForCompletion();

		}
		internal IEnumerator WaitForElapsedLoops(Tween t, int elapsedLoops)
		{
			yield return t.WaitForElapsedLoops(elapsedLoops);
		}

		internal IEnumerator WaitForKill(Tween t)
		{
			yield return t.WaitForKill();
		}
			
		internal IEnumerator WaitForPosition(Tween t, float position)
		{
			yield return t.WaitForPosition(position);
		}

		internal IEnumerator WaitForRewind(Tween t)
		{
			yield return t.WaitForRewind();
		}

		internal IEnumerator WaitForStart(Tween t)
		{
			yield return t.WaitForStart();
		}
	}
}