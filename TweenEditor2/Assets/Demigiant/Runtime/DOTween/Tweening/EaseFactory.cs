
using UnityEngine;
using DG.Tweening.Core.Easing;

namespace DG.Tweening
{
	public class EaseFactory
	{
		
		public static EaseFunction StopMotion(int motionFps, EaseFunction customEase)
		{
			float motionDelay = 1f / ((float) motionFps);
			return (time, duration, overshootOrAmplitude, period) => customEase((time < duration) ? (time - (time % motionDelay)) : time, duration, overshootOrAmplitude, period);
		}

		public static EaseFunction StopMotion(int motionFps, Ease? ease)
		{
			EaseFunction customEase = EaseManager.ToEaseFunction(!ease.HasValue ? DOTween.defaultEaseType : ease.Value);
			return StopMotion(motionFps, customEase);
		}

		public static EaseFunction StopMotion(int motionFps, AnimationCurve animCurve)
		{
			return StopMotion(motionFps, new EaseFunction(new EaseCurve(animCurve).Evaluate));
		}
	}

}