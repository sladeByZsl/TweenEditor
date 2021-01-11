namespace DG.Tweening.Core.Easing
{
	public static class Bounce
	{
		// Methods
		public static float EaseIn(float time, float duration, float unusedOvershootOrAmplitude, float unusedPeriod)
		{
			return (1f - EaseOut(duration - time, duration, -1f, -1f));
		}

		public static float EaseInOut(float time, float duration, float unusedOvershootOrAmplitude, float unusedPeriod)
		{
			if (time < (duration * 0.5f))
			{
				return (EaseIn(time * 2f, duration, -1f, -1f) * 0.5f);
			}
			return ((EaseOut((time * 2f) - duration, duration, -1f, -1f) * 0.5f) + 0.5f);
		}

		public static float EaseOut(float time, float duration, float unusedOvershootOrAmplitude, float unusedPeriod)
		{
			if ((time /= duration) < 0.3636364f)
			{
				return ((7.5625f * time) * time);
			}
			if (time < 0.7272727f)
			{
				return (((7.5625f * (time -= 0.5454546f)) * time) + 0.75f);
			}
			if (time < 0.9090909f)
			{
				return (((7.5625f * (time -= 0.8181818f)) * time) + 0.9375f);
			}
			return (((7.5625f * (time -= 0.9545454f)) * time) + 0.984375f);
		}
	}
}