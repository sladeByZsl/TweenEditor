using UnityEngine.Events;
using UnityEngine;
using System;

namespace DG.Tweening.Core
{
	public enum DOTweenAnimationType
	{
		None,
		Move,
		LocalMove,
		Rotate,
		LocalRotate,
		Scale,
		Color,
		Fade,
		Text,
		PunchPosition,
		PunchRotation,
		PunchScale,
		ShakePosition,
		ShakeRotation,
		ShakeScale,
		CameraAspect,
		CameraBackgroundColor,
		CameraFieldOfView,
		CameraOrthoSize,
		CameraPixelRect,
		CameraRect,
		UIWidthHeight,
        BezierMove,
    }
	public enum OnDisableBehaviour
	{
		None,
		Pause,
		Rewind,
		Kill,
		KillAndComplete,
		DestroyGameObject
	}

	public enum OnEnableBehaviour
	{
		None,
		Play,
		Restart,
		RestartFromSpawnPoint
	}

	public enum TargetType
	{
		Unset,
		Camera,
		CanvasGroup,
		Image,
		Light,
		RectTransform,
		Renderer,
		SpriteRenderer,
		Rigidbody,
		Rigidbody2D,
		Text,
		Transform,
		tk2dBaseSprite,
		tk2dTextMesh,
		TextMeshPro,
		TextMeshProUGUI
	}

	public enum VisualManagerPreset
	{
		Custom,
		PoolingSystem
	}

	//[AddComponentMenu("")]
	public abstract class ABSAnimationComponent : MonoBehaviour
	{
		// Fields
		public bool hasOnComplete;
		public bool hasOnPlay;
		public bool hasOnStart;
		public bool hasOnStepComplete;
		public bool hasOnTweenCreated;
		public bool hasOnUpdate;
		public bool isSpeedBased;
		public UnityEvent onComplete;
		public UnityEvent onPlay;
		public UnityEvent onStart;
		public UnityEvent onStepComplete;
		public UnityEvent onTweenCreated;
		public UnityEvent onUpdate;
		[NonSerialized]
		public Tween tween;
		public UpdateType updateType;

		// Methods
		protected ABSAnimationComponent()
		{
		}

		public abstract void DOComplete();
		public abstract void DOKill();
		public abstract void DOPause();
		public abstract void DOPlay();
		public abstract void DOPlayBackwards();
		public abstract void DOPlayForward();
		public abstract void DORestart(bool fromHere=false);
		public abstract void DORewind();
		public abstract void DOTogglePause();
	}

}