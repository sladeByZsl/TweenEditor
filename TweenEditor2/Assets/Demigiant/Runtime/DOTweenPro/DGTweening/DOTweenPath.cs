using UnityEngine;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DG.Tweening.Plugins.Core.PathCore;
using System.Collections.Generic;


namespace DG.Tweening
{
	[AddComponentMenu("DoTween/DoTween Utility Path")]
	public class DOTweenPath : ABSAnimationComponent
	{
		// Fields
		public bool assignForwardAndUp;
		public bool autoKill;
		public bool autoPlay;
		public float delay;
		public float duration = 1f;
		public AnimationCurve easeCurve;
		public Ease easeType = Ease.OutQuad;
		public Vector3 forwardDirection;
		public List<Vector3> fullWps;
		public HandlesDrawMode handlesDrawMode;
		public HandlesType handlesType;
		public DOTweenInspectorMode inspectorMode;
		public bool isClosedPath;
		public bool isLocal;
		public Vector3 lastSrcPosition;
		public bool livePreview;
		public AxisConstraint lockRotation;
		public float lookAhead;
		public Vector3 lookAtPosition;
		public Transform lookAtTransform;
		public int loops;
		public LoopType loopType;
		public OrientType orientType;
		public Path path;
		public Color pathColor;
		public PathMode pathMode;
		public int pathResolution;
		public PathType pathType;
		public float perspectiveHandleSize;
		public bool relative;
		public bool showIndexes;
		public Vector3 upDirection;
		public List<Vector3> wps;
		public bool wpsDropdown;

		// Methods
		public DOTweenPath()
		{
			//Debug.LogError ("*******************DOTweenPath************************");
			Keyframe[] keys = new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 1f) };
			this.easeCurve = new AnimationCurve(keys);
			this.loops = 1;
			this.lookAhead = 0.01f;
			this.autoPlay = true;
			this.autoKill = true;
			this.pathResolution = 10;
			this.pathMode = PathMode.Full3D;
			this.forwardDirection = Vector3.forward;
			this.upDirection = Vector3.up;
			this.wps = new List<Vector3>();
			this.fullWps = new List<Vector3>();
			this.livePreview = true;
			this.perspectiveHandleSize = 0.5f;
			this.showIndexes = true;
			this.pathColor = new Color(1f, 1f, 1f, 0.5f);
		}

		void Awake()
		{
			
			if ((this.path != null) && (this.wps.Count >= 1))
			{
				Vector3? nullable;
				this.path.AssignDecoder(this.path.type);
				if (DOTween.isUnityEditor)
				{
					DOTween.GizmosDelegates.Add(new TweenCallback(this.path.Draw));
					this.path.gizmoColor = this.pathColor;
				}
				if (this.isLocal)
				{
					Transform transform = base.transform;
					if (transform.parent != null)
					{
						Vector3 position = transform.parent.position;
						int length = this.path.wps.Length;
						for (int i = 0; i < length; i++)
						{
							this.path.wps[i] -= position;
						}
						length = this.path.controlPoints.Length;
						for (int j = 0; j < length; j++)
						{
							DG.Tweening.Plugins.Core.PathCore.LinearDecoder.ControlPoint point = this.path.controlPoints[j];
							Vector3 vectorRef = point.a;
							vectorRef -= position;
							vectorRef = point.b;
							vectorRef -= position;
							this.path.controlPoints[j] = point;
						}
					}
				}
				if (this.relative)
				{
					this.ReEvaluateRelativeTween();
				}
				if (base.GetComponent<SpriteRenderer>() != null)
				{
					this.pathMode = PathMode.TopDown2D;
				}
				TweenerCore<Vector3, Path, PathOptions> t = this.isLocal ? base.transform.DOLocalPath(this.path, this.duration, this.pathMode).SetOptions(this.isClosedPath, AxisConstraint.None, this.lockRotation) : base.transform.DOPath(this.path, this.duration, this.pathMode).SetOptions(this.isClosedPath, AxisConstraint.None, this.lockRotation);
				switch (this.orientType)
				{
				case OrientType.ToPath:
					if (!this.assignForwardAndUp)
					{
						nullable = null;
						t.SetLookAt(this.lookAhead, nullable, null);
						break;
					}
					t.SetLookAt(this.lookAhead, new Vector3?(this.forwardDirection), new Vector3?(this.upDirection));
					break;

				case OrientType.LookAtTransform:
					if (this.lookAtTransform != null)
					{
						if (!this.assignForwardAndUp)
						{
							nullable = null;
							t.SetLookAt(this.lookAtTransform, nullable, null);
							break;
						}
						t.SetLookAt(this.lookAtTransform, new Vector3?(this.forwardDirection), new Vector3?(this.upDirection));
					}
					break;

				case OrientType.LookAtPosition:
					if (!this.assignForwardAndUp)
					{
						nullable = null;
						t.SetLookAt(this.lookAtPosition, nullable, null);
						break;
					}
					t.SetLookAt(this.lookAtPosition, new Vector3?(this.forwardDirection), new Vector3?(this.upDirection));
					break;
				}
				//t.SetDelay<TweenerCore<Vector3, Path, PathOptions>>(this.delay).SetLoops<TweenerCore<Vector3, Path, PathOptions>>(this.loops, this.loopType).SetAutoKill<TweenerCore<Vector3, Path, PathOptions>>(this.autoKill).SetUpdate<TweenerCore<Vector3, Path, PathOptions>>(base.updateType).OnKill<TweenerCore<Vector3, Path, PathOptions>>((TweenCallback) (() => (base.tween = null)));
				if (base.isSpeedBased)
				{
					t.SetSpeedBased<TweenerCore<Vector3, Path, PathOptions>>();
				}
				if (this.easeType == Ease.INTERNAL_Custom)
				{
					t.SetEase<TweenerCore<Vector3, Path, PathOptions>>(this.easeCurve);
				}
				else
				{
					t.SetEase<TweenerCore<Vector3, Path, PathOptions>>(this.easeType);
				}
				if (base.hasOnStart)
				{
					if (base.onStart != null)
					{
						t.OnStart<TweenerCore<Vector3, Path, PathOptions>>(new TweenCallback(base.onStart.Invoke));
					}
				}
				else
				{
					base.onStart = null;
				}
				if (base.hasOnPlay)
				{
					if (base.onPlay != null)
					{
						t.OnPlay<TweenerCore<Vector3, Path, PathOptions>>(new TweenCallback(base.onPlay.Invoke));
					}
				}
				else
				{
					base.onPlay = null;
				}
				if (base.hasOnUpdate)
				{
					if (base.onUpdate != null)
					{
						t.OnUpdate<TweenerCore<Vector3, Path, PathOptions>>(new TweenCallback(base.onUpdate.Invoke));
					}
				}
				else
				{
					base.onUpdate = null;
				}
				if (base.hasOnStepComplete)
				{
					if (base.onStepComplete != null)
					{
						t.OnStepComplete<TweenerCore<Vector3, Path, PathOptions>>(new TweenCallback(base.onStepComplete.Invoke));
					}
				}
				else
				{
					base.onStepComplete = null;
				}
				if (base.hasOnComplete)
				{
					if (base.onComplete != null)
					{
						t.OnComplete<TweenerCore<Vector3, Path, PathOptions>>(new TweenCallback(base.onComplete.Invoke));
					}
				}
				else
				{
					base.onComplete = null;
				}
				if (this.autoPlay)
				{
					t.Play<TweenerCore<Vector3, Path, PathOptions>>();
				}
				else
				{
					t.Pause<TweenerCore<Vector3, Path, PathOptions>>();
				}
				base.tween = t;
				if (base.hasOnTweenCreated && (base.onTweenCreated != null))
				{
					base.onTweenCreated.Invoke();
				}
			}
		}

		public override void DOComplete()
		{
			base.tween.Complete();
		}

		public override void DOKill()
		{
			base.tween.Kill(false);
		}

		public override void DOPause()
		{
			base.tween.Pause<Tween>();
		}

		public override void DOPlay()
		{
			base.tween.Play<Tween>();
		}

		public override void DOPlayBackwards()
		{
			base.tween.PlayBackwards();
		}

		public override void DOPlayForward()
		{
			base.tween.PlayForward();
		}

		public override void DORestart(bool fromHere=false)
		{
			if (base.tween == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(base.tween);
				}
			}
			else
			{
				if ((fromHere && this.relative) && !this.isLocal)
				{
					this.ReEvaluateRelativeTween();
				}
				base.tween.Restart(true);
			}
		}

		public override void DORewind()
		{
			base.tween.Rewind(true);
		}

		public override void DOTogglePause()
		{
			base.tween.TogglePause();
		}

		public Vector3[] GetDrawPoints()
		{
			if ((this.path.wps == null) || (this.path.nonLinearDrawWps == null))
			{
				Debugger.LogWarning("Draw points not ready yet. Returning NULL");
				return null;
			}
			if (this.pathType == PathType.Linear)
			{
				return this.path.wps;
			}
			return this.path.nonLinearDrawWps;
		}

		public Vector3[] GetFullWps()
		{
			int count = this.wps.Count;
			int num2 = count + 1;
			if (this.isClosedPath)
			{
				num2++;
			}
			Vector3[] vectorArray = new Vector3[num2];
			vectorArray[0] = base.transform.position;
			for (int i = 0; i < count; i++)
			{
				vectorArray[i + 1] = this.wps[i];
			}
			if (this.isClosedPath)
			{
				vectorArray[num2 - 1] = vectorArray[0];
			}
			return vectorArray;
		}

		public Tween GetTween()
		{
			if ((base.tween != null) && base.tween.active)
			{
				return base.tween;
			}
			if (Debugger.logPriority > 1)
			{
				if (base.tween == null)
				{
					Debugger.LogNullTween(base.tween);
				}
				else
				{
					Debugger.LogInvalidTween(base.tween);
				}
			}
			return null;
		}

		private void OnDestroy()
		{
			if ((base.tween != null) && base.tween.active)
			{
				base.tween.Kill(false);
			}
			base.tween = null;
		}

		private void ReEvaluateRelativeTween()
		{
			Vector3 position = base.transform.position;
			if (position != this.lastSrcPosition)
			{
				Vector3 vector2 = position - this.lastSrcPosition;
				int length = this.path.wps.Length;
				for (int i = 0; i < length; i++)
				{
					this.path.wps[i] += vector2;
				}
				length = this.path.controlPoints.Length;
				for (int j = 0; j < length; j++)
				{
					DG.Tweening.Plugins.Core.PathCore.LinearDecoder.ControlPoint point = this.path.controlPoints[j];
					Vector3 vectorRef = point.a;
					vectorRef += vector2;
					vectorRef = point.b;
					vectorRef += vector2;
					this.path.controlPoints[j] = point;
				}
				this.lastSrcPosition = position;
			}
		}

		private void Reset()
		{
			this.path = new Path(this.pathType, this.wps.ToArray(), 10, new Color?(this.pathColor));
		}
	}
}