using UnityEngine;

namespace DG.Tweening.Core.Easing
{
	public class EaseCurve
	{
		// Fields
		private readonly AnimationCurve _animCurve;

		// Methods
		public EaseCurve(AnimationCurve animCurve)
		{
			this._animCurve = animCurve;
		}

		public float Evaluate(float time, float duration, float unusedOvershoot, float unusedPeriod)
		{
			Keyframe keyframe = this._animCurve[this._animCurve.length - 1];
			float num = keyframe.time;
			float num2 = time / duration;
			return this._animCurve.Evaluate(num2 * num);
		}
	}
}