using UnityEngine;
using System;

namespace DG.Tweening.Core.Easing
{
	public static class Flash
	{
		// Methods
		public static float Ease(float time, float duration, float overshootOrAmplitude, float period)
		{
			int stepIndex = Mathf.CeilToInt((time / duration) * overshootOrAmplitude);
			float stepDuration = duration / overshootOrAmplitude;
			time -= stepDuration * (stepIndex - 1);
			float dir = ((stepIndex % 2) != 0) ? ((float) 1) : ((float) (-1));
			if (dir < 0f)
			{
				time -= stepDuration;
			}
			float res = (time * dir) / stepDuration;
			return WeightedEase(overshootOrAmplitude, period, stepIndex, stepDuration, dir, res);
		}

		public static float EaseIn(float time, float duration, float overshootOrAmplitude, float period)
		{
			int stepIndex = Mathf.CeilToInt((time / duration) * overshootOrAmplitude);
			float stepDuration = duration / overshootOrAmplitude;
			time -= stepDuration * (stepIndex - 1);
			float dir = ((stepIndex % 2) != 0) ? ((float) 1) : ((float) (-1));
			if (dir < 0f)
			{
				time -= stepDuration;
			}
			time *= dir;
			float res = (time /= stepDuration) * time;
			return WeightedEase(overshootOrAmplitude, period, stepIndex, stepDuration, dir, res);
		}

		public static float EaseInOut(float time, float duration, float overshootOrAmplitude, float period)
		{
			int stepIndex = Mathf.CeilToInt((time / duration) * overshootOrAmplitude);
			float stepDuration = duration / overshootOrAmplitude;
			time -= stepDuration * (stepIndex - 1);
			float dir = ((stepIndex % 2) != 0) ? ((float) 1) : ((float) (-1));
			if (dir < 0f)
			{
				time -= stepDuration;
			}
			time *= dir;
			float res = ((time /= (stepDuration * 0.5f)) < 1f) ? ((0.5f * time) * time) : (-0.5f * ((--time * (time - 2f)) - 1f));
			return WeightedEase(overshootOrAmplitude, period, stepIndex, stepDuration, dir, res);
		}

		public static float EaseOut(float time, float duration, float overshootOrAmplitude, float period)
		{
			int stepIndex = Mathf.CeilToInt((time / duration) * overshootOrAmplitude);
			float stepDuration = duration / overshootOrAmplitude;
			time -= stepDuration * (stepIndex - 1);
			float dir = ((stepIndex % 2) != 0) ? ((float) 1) : ((float) (-1));
			if (dir < 0f)
			{
				time -= stepDuration;
			}
			time *= dir;
			float res = -(time /= stepDuration) * (time - 2f);
			return WeightedEase(overshootOrAmplitude, period, stepIndex, stepDuration, dir, res);
		}

		private static float WeightedEase(float overshootOrAmplitude, float period, int stepIndex, float stepDuration, float dir, float res)
		{
			float num = 0f;
			float num2 = 0f;
			if (period > 0f)
			{
				float num4 = (float) Math.Truncate((double) overshootOrAmplitude);
				num2 = overshootOrAmplitude - num4;
				if ((num4 % 2f) > 0f)
				{
					num2 = 1f - num2;
				}
				num2 = (num2 * stepIndex) / overshootOrAmplitude;
				num = (res * (overshootOrAmplitude - stepIndex)) / overshootOrAmplitude;
			}
			else if (period < 0f)
			{
				period = -period;
				num = (res * stepIndex) / overshootOrAmplitude;
			}
			float num3 = num - res;
			res += (num3 * period) + num2;
			if (res > 1f)
			{
				res = 1f;
			}
			return res;
		}
	}
}