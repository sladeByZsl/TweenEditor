using UnityEngine;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

namespace DG.Tweening
{
	public delegate void TweenCallback<in T>(T value);

	public delegate void TweenCallback();


	public static class DOVirtual
	{
		// Methods
		public static Tween DelayedCall(float delay, TweenCallback callback, bool ignoreTimeScale=true)
		{
			return DOTween.Sequence().AppendInterval(delay).OnStepComplete<Sequence>(callback).SetUpdate<Sequence>(UpdateType.Normal, ignoreTimeScale).SetAutoKill<Sequence>(true);
		}

		public static float EasedValue(float from, float to, float lifetimePercentage, Ease easeType)
		{
			return (from + ((to - from) * EaseManager.Evaluate(easeType, null, lifetimePercentage, 1f, DOTween.defaultEaseOvershootOrAmplitude, DOTween.defaultEasePeriod)));
		}

		public static float EasedValue(float from, float to, float lifetimePercentage, AnimationCurve easeCurve)
		{
			return (from + ((to - from) * EaseManager.Evaluate(Ease.INTERNAL_Custom, new EaseFunction(new EaseCurve(easeCurve).Evaluate), lifetimePercentage, 1f, DOTween.defaultEaseOvershootOrAmplitude, DOTween.defaultEasePeriod)));
		}

		public static float EasedValue(float from, float to, float lifetimePercentage, Ease easeType, float overshoot)
		{
			return (from + ((to - from) * EaseManager.Evaluate(easeType, null, lifetimePercentage, 1f, overshoot, DOTween.defaultEasePeriod)));
		}

		public static float EasedValue(float from, float to, float lifetimePercentage, Ease easeType, float amplitude, float period)
		{
			return (from + ((to - from) * EaseManager.Evaluate(easeType, null, lifetimePercentage, 1f, amplitude, period)));
		}

		public static Tweener Float(float from, float to, float duration, TweenCallback<float> onVirtualUpdate)
		{
			float val = from;
			return DOTween.To(() => val, delegate (float x) {
				val = x;
			}, to, duration).OnUpdate<TweenerCore<float, float, FloatOptions>>(delegate {
				onVirtualUpdate(val);
			});
		}
	}
}