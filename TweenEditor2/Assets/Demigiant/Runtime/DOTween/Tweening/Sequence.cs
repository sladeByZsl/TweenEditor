using System.Collections.Generic;
using DG.Tweening.Core;
using DG.Tweening.Core.Enums;
using DG.Tweening.Core.Easing;
using System;

namespace DG.Tweening
{
	public class Sequence : Tween
	{
		// Fields
		private readonly List<ABSSequentiable> _sequencedObjs;
		internal float lastTweenInsertTime;
		internal readonly List<Tween> sequencedTweens;

		// Methods
		internal Sequence()
		{
			this.sequencedTweens = new List<Tween>();
			this._sequencedObjs = new List<ABSSequentiable>();
			base.tweenType = TweenType.Sequence;
			this.Reset();
		}

		private static bool ApplyInternalCycle(Sequence s, float fromPos, float toPos, UpdateMode updateMode, bool useInverse, bool prevPosIsInverse, bool multiCycleStep=false)
		{
			if (toPos < fromPos)
			{
				for (int i = s._sequencedObjs.Count - 1; i > -1; i--)
				{
					if (!s.active)
					{
						return true;
					}
					ABSSequentiable sequentiable = s._sequencedObjs[i];
					if ((sequentiable.sequencedEndPosition >= toPos) && (sequentiable.sequencedPosition <= fromPos))
					{
						if (sequentiable.tweenType == TweenType.Callback)
						{
							if ((updateMode == UpdateMode.Update) & prevPosIsInverse)
							{
								Tween.OnTweenCallback(sequentiable.onStart);
							}
						}
						else
						{
							float to = toPos - sequentiable.sequencedPosition;
							if (to < 0f)
							{
								to = 0f;
							}
							Tween t = (Tween) sequentiable;
							if (t.startupDone)
							{
								t.isBackwards = true;
								if (TweenManager.Goto(t, to, false, updateMode))
								{
									return true;
								}
								if (multiCycleStep && (t.tweenType == TweenType.Sequence))
								{
									if ((s.position <= 0f) && (s.completedLoops == 0))
									{
										t.position = 0f;
									}
									else
									{
										bool flag = (s.completedLoops == 0) || (s.isBackwards && ((s.completedLoops < s.loops) || (s.loops == -1)));
										if (t.isBackwards)
										{
											flag = !flag;
										}
										if (useInverse)
										{
											flag = !flag;
										}
										if ((s.isBackwards && !useInverse) && !prevPosIsInverse)
										{
											flag = !flag;
										}
										t.position = flag ? 0f : t.duration;
									}
								}
							}
						}
					}
				}
			}
			else
			{
				int count = s._sequencedObjs.Count;
				for (int j = 0; j < count; j++)
				{
					if (!s.active)
					{
						return true;
					}
					ABSSequentiable sequentiable2 = s._sequencedObjs[j];
					if ((sequentiable2.sequencedPosition <= toPos) && (sequentiable2.sequencedEndPosition >= fromPos))
					{
						if (sequentiable2.tweenType == TweenType.Callback)
						{
							if ((updateMode == UpdateMode.Update) && (((!s.isBackwards && !useInverse) && !prevPosIsInverse) || ((s.isBackwards & useInverse) && !prevPosIsInverse)))
							{
								Tween.OnTweenCallback(sequentiable2.onStart);
							}
						}
						else
						{
							float num5 = toPos - sequentiable2.sequencedPosition;
							if (num5 < 0f)
							{
								num5 = 0f;
							}
							Tween tween2 = (Tween) sequentiable2;
							tween2.isBackwards = false;
							if (TweenManager.Goto(tween2, num5, false, updateMode))
							{
								return true;
							}
							if (multiCycleStep && (tween2.tweenType == TweenType.Sequence))
							{
								if ((s.position <= 0f) && (s.completedLoops == 0))
								{
									tween2.position = 0f;
								}
								else
								{
									bool flag2 = (s.completedLoops == 0) || (!s.isBackwards && ((s.completedLoops < s.loops) || (s.loops == -1)));
									if (tween2.isBackwards)
									{
										flag2 = !flag2;
									}
									if (useInverse)
									{
										flag2 = !flag2;
									}
									if ((s.isBackwards && !useInverse) && !prevPosIsInverse)
									{
										flag2 = !flag2;
									}
									tween2.position = flag2 ? 0f : tween2.duration;
								}
							}
						}
					}
				}
			}
			return false;
		}

		internal override bool ApplyTween(float prevPosition, int prevCompletedLoops, int newCompletedSteps, bool useInversePosition, UpdateMode updateMode, UpdateNotice updateNotice)
		{
			return DoApplyTween(this, prevPosition, prevCompletedLoops, newCompletedSteps, useInversePosition, updateMode);
		}

		internal static Sequence DoAppendInterval(Sequence inSequence, float interval)
		{
			inSequence.lastTweenInsertTime = inSequence.duration;
			inSequence.duration += interval;
			return inSequence;
		}

		internal static bool DoApplyTween(Sequence s, float prevPosition, int prevCompletedLoops, int newCompletedSteps, bool useInversePosition, UpdateMode updateMode)
		{
			float num3;
			float time = prevPosition;
			float position = s.position;
			if (s.easeType != Ease.Linear)
			{
				time = s.duration * EaseManager.Evaluate(s.easeType, s.customEase, time, s.duration, s.easeOvershootOrAmplitude, s.easePeriod);
				position = s.duration * EaseManager.Evaluate(s.easeType, s.customEase, position, s.duration, s.easeOvershootOrAmplitude, s.easePeriod);
			}
			float toPos = 0f;
			bool prevPosIsInverse = (s.loopType == LoopType.Yoyo) && ((time < s.duration) ? ((prevCompletedLoops % 2) > 0) : ((prevCompletedLoops % 2) == 0));
			if (s.isBackwards)
			{
				prevPosIsInverse = !prevPosIsInverse;
			}
			if (newCompletedSteps > 0)
			{
				int completedLoops = s.completedLoops;
				float num6 = s.position;
				int num7 = newCompletedSteps;
				int num8 = 0;
				num3 = time;
				if (updateMode == UpdateMode.Update)
				{
					while (num8 < num7)
					{
						if (num8 > 0)
						{
							num3 = toPos;
						}
						else if (prevPosIsInverse && !s.isBackwards)
						{
							num3 = s.duration - num3;
						}
						toPos = prevPosIsInverse ? 0f : s.duration;
						if (ApplyInternalCycle(s, num3, toPos, updateMode, useInversePosition, prevPosIsInverse, true))
						{
							return true;
						}
						num8++;
						if (s.loopType == LoopType.Yoyo)
						{
							prevPosIsInverse = !prevPosIsInverse;
						}
					}
					if ((completedLoops != s.completedLoops) || (Math.Abs((float) (num6 - s.position)) > float.Epsilon))
					{
						return !s.active;
					}
				}
				else
				{
					if ((s.loopType == LoopType.Yoyo) && ((newCompletedSteps % 2) != 0))
					{
						prevPosIsInverse = !prevPosIsInverse;
						time = s.duration - time;
					}
					newCompletedSteps = 0;
				}
			}
			if ((newCompletedSteps == 1) && s.isComplete)
			{
				return false;
			}
			if ((newCompletedSteps > 0) && !s.isComplete)
			{
				num3 = useInversePosition ? s.duration : 0f;
				if ((s.loopType == LoopType.Restart) && (toPos > 0f))
				{
					ApplyInternalCycle(s, s.duration, 0f, UpdateMode.Goto, false, false, false);
				}
			}
			else
			{
				num3 = useInversePosition ? (s.duration - time) : time;
			}
			return ApplyInternalCycle(s, num3, useInversePosition ? (s.duration - position) : position, updateMode, useInversePosition, prevPosIsInverse, false);
		}

		internal static Sequence DoInsert(Sequence inSequence, Tween t, float atPosition)
		{
			TweenManager.AddActiveTweenToSequence(t);
			atPosition += t.delay;
			inSequence.lastTweenInsertTime = atPosition;
			t.isSequenced = t.creationLocked = true;
			t.sequenceParent = inSequence;
			if (t.loops == -1)
			{
				t.loops = 1;
			}
			float num = t.duration * t.loops;
			t.autoKill = false;
			t.delay = t.elapsedDelay = 0f;
			t.delayComplete = true;
			t.isSpeedBased = false;
			t.sequencedPosition = atPosition;
			t.sequencedEndPosition = atPosition + num;
			if (t.sequencedEndPosition > inSequence.duration)
			{
				inSequence.duration = t.sequencedEndPosition;
			}
			inSequence._sequencedObjs.Add(t);
			inSequence.sequencedTweens.Add(t);
			return inSequence;
		}

		internal static Sequence DoInsertCallback(Sequence inSequence, TweenCallback callback, float atPosition)
		{
			SequenceCallback callback2;
			inSequence.lastTweenInsertTime = atPosition;
			callback2 = new SequenceCallback (atPosition, callback);
			callback2.sequencedEndPosition = atPosition;
			//base.sequencedPosition = callback2.sequencedEndPosition;

			inSequence._sequencedObjs.Add(callback2);
			if (inSequence.duration < atPosition)
			{
				inSequence.duration = atPosition;
			}
			return inSequence;
		}

		internal static Sequence DoPrepend(Sequence inSequence, Tween t)
		{
			if (t.loops == -1)
			{
				t.loops = 1;
			}
			float num = t.delay + (t.duration * t.loops);
			inSequence.duration += num;
			int count = inSequence._sequencedObjs.Count;
			for (int i = 0; i < count; i++)
			{
				ABSSequentiable local1 = inSequence._sequencedObjs[i];
				local1.sequencedPosition += num;
				local1.sequencedEndPosition += num;
			}
			return DoInsert(inSequence, t, 0f);
		}

		internal static Sequence DoPrependInterval(Sequence inSequence, float interval)
		{
			inSequence.lastTweenInsertTime = 0f;
			inSequence.duration += interval;
			int count = inSequence._sequencedObjs.Count;
			for (int i = 0; i < count; i++)
			{
				ABSSequentiable local1 = inSequence._sequencedObjs[i];
				local1.sequencedPosition += interval;
				local1.sequencedEndPosition += interval;
			}
			return inSequence;
		}

		internal static bool DoStartup(Sequence s)
		{
			if (((((s.sequencedTweens.Count == 0) && (s._sequencedObjs.Count == 0)) && ((s.onComplete == null) && (s.onKill == null))) && (((s.onPause == null) && (s.onPlay == null)) && ((s.onRewind == null) && (s.onStart == null)))) && ((s.onStepComplete == null) && (s.onUpdate == null)))
			{
				return false;
			}
			s.startupDone = true;
			s.fullDuration = (s.loops > -1) ? (s.duration * s.loops) : float.PositiveInfinity;
			s._sequencedObjs.Sort(new Comparison<ABSSequentiable>(Sequence.SortSequencedObjs));
			if (s.isRelative)
			{
				int count = s.sequencedTweens.Count;
				for (int i = 0; i < count; i++)
				{
					//Tween local1 = s.sequencedTweens[i];
					if (!s.isBlendable)
					{
						s.sequencedTweens[i].isRelative = true;
					}
				}
			}
			return true;
		}

		internal override void Reset()
		{
			base.Reset();
			this.sequencedTweens.Clear();
			this._sequencedObjs.Clear();
			this.lastTweenInsertTime = 0f;
		}

		internal static void Setup(Sequence s)
		{
			s.autoKill = DOTween.defaultAutoKill;
			s.isRecyclable = DOTween.defaultRecyclable;
			s.isPlaying = (DOTween.defaultAutoPlay == AutoPlay.All) || (DOTween.defaultAutoPlay == AutoPlay.AutoPlaySequences);
			s.loopType = DOTween.defaultLoopType;
			s.easeType = Ease.Linear;
			s.easeOvershootOrAmplitude = DOTween.defaultEaseOvershootOrAmplitude;
			s.easePeriod = DOTween.defaultEasePeriod;
		}

		private static int SortSequencedObjs(ABSSequentiable a, ABSSequentiable b)
		{
			if (a.sequencedPosition > b.sequencedPosition)
			{
				return 1;
			}
			if (a.sequencedPosition < b.sequencedPosition)
			{
				return -1;
			}
			return 0;
		}

		internal override bool Startup()
		{
			return DoStartup(this);
		}

		internal override bool Validate()
		{
			int count = this.sequencedTweens.Count;
			for (int i = 0; i < count; i++)
			{
				if (!this.sequencedTweens[i].Validate())
				{
					return false;
				}
			}
			return true;
		}
	}
}