using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Enums;

namespace DG.Tweening
{
	public delegate float EaseFunction(float time, float duration, float overshootOrAmplitude, float period);

	public abstract class Tween : ABSSequentiable
	{
		// Fields
		internal bool active;
		internal int activeId = -1;
		internal bool autoKill;
		internal int completedLoops;
		internal bool creationLocked;
		internal EaseFunction customEase;
		internal float delay;
		internal bool delayComplete = true;
		internal float duration;
		public float easeOvershootOrAmplitude;
		public float easePeriod;
		public Ease easeType;
		internal float elapsedDelay;
		internal float fullDuration;
		public object id;
		public bool isBackwards;
		internal bool isBlendable;
		internal bool isComplete;
		internal bool isFrom;
		internal bool isIndependentUpdate;
		internal bool isPlaying;
		internal bool isRecyclable;
		internal bool isRelative;
		internal bool isSequenced;
		public bool isSpeedBased;
		internal int loops;
		internal LoopType loopType;
		internal int miscInt = -1;
		public TweenCallback onComplete;
		public TweenCallback onKill;
		public TweenCallback onPause;
		public TweenCallback onPlay;
		public TweenCallback onRewind;
		public TweenCallback onStepComplete;
		public TweenCallback onUpdate;
		public TweenCallback<int> onWaypointChange;
		internal bool playedOnce;
		internal float position;
		internal Sequence sequenceParent;
		internal SpecialStartupMode specialStartupMode;
		internal bool startupDone;
		public object target;
		public float timeScale;
		internal Type typeofT1;
		internal Type typeofT2;
		internal Type typeofTPlugOptions;
		internal UpdateType updateType;

		// Methods
		protected Tween()
		{
			this.activeId = -1;
			this.delayComplete = true;
			this.miscInt = -1;
		}

		internal abstract bool ApplyTween(float prevPosition, int prevCompletedLoops, int newCompletedSteps, bool useInversePosition, UpdateMode updateMode, UpdateNotice updateNotice);
		internal static bool DoGoto(Tween t, float toPosition, int toCompletedLoops, UpdateMode updateMode)
		{
			if (!t.startupDone && !t.Startup())
			{
				return true;
			}
			if (!t.playedOnce && (updateMode == UpdateMode.Update))
			{
				t.playedOnce = true;
				if (t.onStart != null)
				{
					OnTweenCallback(t.onStart);
					if (!t.active)
					{
						return true;
					}
				}
				if (t.onPlay != null)
				{
					OnTweenCallback(t.onPlay);
					if (!t.active)
					{
						return true;
					}
				}
			}
			float position = t.position;
			int completedLoops = t.completedLoops;
			t.completedLoops = toCompletedLoops;
			bool flag = (t.position <= 0f) && (completedLoops <= 0);
			bool isComplete = t.isComplete;
			if (t.loops != -1)
			{
				t.isComplete = t.completedLoops == t.loops;
			}
			int newCompletedSteps = 0;
			if (updateMode == UpdateMode.Update)
			{
				if (t.isBackwards)
				{
					newCompletedSteps = (t.completedLoops < completedLoops) ? (completedLoops - t.completedLoops) : (((toPosition <= 0f) && !flag) ? 1 : 0);
					if (isComplete)
					{
						newCompletedSteps--;
					}
				}
				else
				{
					newCompletedSteps = (t.completedLoops > completedLoops) ? (t.completedLoops - completedLoops) : 0;
				}
			}
			else if (t.tweenType == TweenType.Sequence)
			{
				newCompletedSteps = completedLoops - toCompletedLoops;
				if (newCompletedSteps < 0)
				{
					newCompletedSteps = -newCompletedSteps;
				}
			}
			t.position = toPosition;
			if (t.position > t.duration)
			{
				t.position = t.duration;
			}
			else if (t.position <= 0f)
			{
				if ((t.completedLoops > 0) || t.isComplete)
				{
					t.position = t.duration;
				}
				else
				{
					t.position = 0f;
				}
			}
			bool isPlaying = t.isPlaying;
			if (t.isPlaying)
			{
				if (!t.isBackwards)
				{
					t.isPlaying = !t.isComplete;
				}
				else
				{
					t.isPlaying = (t.completedLoops != 0) || (t.position > 0f);
				}
			}
			bool useInversePosition = (t.loopType == LoopType.Yoyo) && ((t.position < t.duration) ? ((t.completedLoops % 2) > 0) : ((t.completedLoops % 2) == 0));
			UpdateNotice updateNotice = (!flag && (((t.loopType == LoopType.Restart) && (t.completedLoops != completedLoops)) || ((t.position <= 0f) && (t.completedLoops <= 0)))) ? UpdateNotice.RewindStep : UpdateNotice.None;
			if (t.ApplyTween(position, completedLoops, newCompletedSteps, useInversePosition, updateMode, updateNotice))
			{
				return true;
			}
			if ((t.onUpdate != null) && (updateMode != UpdateMode.IgnoreOnUpdate))
			{
				OnTweenCallback(t.onUpdate);
			}
			if (((t.position <= 0f) && (t.completedLoops <= 0)) && (!flag && (t.onRewind != null)))
			{
				OnTweenCallback(t.onRewind);
			}
			if (((newCompletedSteps > 0) && (updateMode == UpdateMode.Update)) && (t.onStepComplete != null))
			{
				for (int i = 0; i < newCompletedSteps; i++)
				{
					OnTweenCallback(t.onStepComplete);
				}
			}
			if ((t.isComplete && !isComplete) && (t.onComplete != null))
			{
				OnTweenCallback(t.onComplete);
			}
			if (((!t.isPlaying & isPlaying) && (!t.isComplete || !t.autoKill)) && (t.onPause != null))
			{
				OnTweenCallback(t.onPause);
			}
			return (t.autoKill && t.isComplete);
		}

		internal static bool OnTweenCallback(TweenCallback callback)
		{
			if (DOTween.useSafeMode)
			{
				try
				{
					callback();
					goto Label_0054;
				}
				catch (Exception exception)
				{
					Debugger.LogWarning("An error inside a tween callback was silently taken care of > " + exception.Message + "\n\n" + exception.StackTrace + "\n\n");
					return false;
				}
			}
			callback();
			Label_0054:
			return true;
		}

		internal static bool OnTweenCallback<T>(TweenCallback<T> callback, T param)
		{
			if (DOTween.useSafeMode)
			{
				try
				{
					callback(param);
					goto Label_0031;
				}
				catch (Exception exception)
				{
					Debugger.LogWarning("An error inside a tween callback was silently taken care of > " + exception.Message);
					return false;
				}
			}
			callback(param);
			Label_0031:
			return true;
		}

		internal virtual void Reset()
		{
			this.timeScale = 1f;
			this.isBackwards = false;
			this.id = null;
			this.isIndependentUpdate = false;
			base.onStart = this.onPlay = this.onRewind = this.onUpdate = this.onComplete = this.onStepComplete = (TweenCallback) (this.onKill = null);
			this.onWaypointChange = null;
			this.target = null;
			this.isFrom = false;
			this.isBlendable = false;
			this.isSpeedBased = false;
			this.duration = 0f;
			this.loops = 1;
			this.delay = 0f;
			this.isRelative = false;
			this.customEase = null;
			this.isSequenced = false;
			this.sequenceParent = null;
			this.specialStartupMode = SpecialStartupMode.None;
			this.creationLocked = this.startupDone = this.playedOnce = false;
			this.position = this.fullDuration = this.completedLoops = 0;
			this.isPlaying = this.isComplete = false;
			this.elapsedDelay = 0f;
			this.delayComplete = true;
			this.miscInt = -1;
		}

		internal abstract bool Startup();
		internal virtual float UpdateDelay(float elapsed)
		{
			return 0f;
		}

		internal abstract bool Validate();

		// Properties
		public float fullPosition
		{
			get
			{
				return this.Elapsed(true);
			}
			set
			{
				this.Goto(value, this.isPlaying);
			}
		}
	}
}