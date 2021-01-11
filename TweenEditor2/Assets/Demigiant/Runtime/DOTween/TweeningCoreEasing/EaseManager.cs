using System;

namespace DG.Tweening.Core.Easing
{
	public static class EaseManager
	{
		// Fields
		private const float _PiOver2 = 1.570796f;
		private const float _TwoPi = 6.283185f;

		public static EaseFunction m_Linear = Linear;
		public static EaseFunction m_InSine = InSine;
		public static EaseFunction m_OutSine = OutSine;
		public static EaseFunction m_InOutSine = InOutSine;
		public static EaseFunction m_InQuad = InQuad;
		public static EaseFunction m_OutQuad = OutQuad;
		public static EaseFunction m_InOutQuad = InOutQuad;
		public static EaseFunction m_InCubic = InCubic;
		public static EaseFunction m_OutCubic = OutCubic;
		public static EaseFunction m_InOutCubic = InOutCubic;
		public static EaseFunction m_InQuart = InQuart;
		public static EaseFunction m_OutQuart = OutQuart;
		public static EaseFunction m_InOutQuart = InOutQuart;
		public static EaseFunction m_InQuint = InQuint;
		public static EaseFunction m_OutQuint = OutQuint;
		public static EaseFunction m_InOutQuint = InOutQuint;
		public static EaseFunction m_InExpo = InExpo;
		public static EaseFunction m_OutExpo = OutExpo;
		public static EaseFunction m_InOutExpo = InOutExpo;
		public static EaseFunction m_InCirc = InCirc;
		public static EaseFunction m_OutCirc = OutCirc;
		public static EaseFunction m_InOutCirc = InOutCirc;
		public static EaseFunction m_InElastic = InElastic;
		public static EaseFunction m_OutElastic = OutElastic;
		public static EaseFunction m_InOutElastic = InOutElastic;
		public static EaseFunction m_InBack = InBack;
		public static EaseFunction m_OutBack = OutBack;
		public static EaseFunction m_InOutBack = InOutBack;
		public static EaseFunction m_InBounce = InBounce;
		public static EaseFunction m_OutBounce = OutBounce;
		public static EaseFunction m_InOutBounce = InOutBounce;
		public static EaseFunction m_Flash = Flashs;
		public static EaseFunction m_InFlash = InFlash;
		public static EaseFunction m_OutFlash = OutFlash;
		public static EaseFunction m_InOutFlash = InOutFlash;
		public static EaseFunction m_INTERNAL_Zero = INTERNAL_Zero;
		public static EaseFunction m_Other = Other;

		public static float Linear(float time, float duration, float overshootOrAmplitude, float period)
		{
			return (time / duration);
		}

		public static float InSine(float time, float duration, float overshootOrAmplitude, float period)
		{
			return (-((float) Math.Cos((double) ((time / duration) * 1.570796f))) + 1f);
		}

		public static float OutSine(float time, float duration, float overshootOrAmplitude, float period)
		{
			return (float) Math.Sin((double) ((time / duration) * 1.570796f));
		}

		public static float InOutSine(float time, float duration, float overshootOrAmplitude, float period)
		{
			return (-0.5f * (((float) Math.Cos((double) ((3.141593f * time) / duration))) - 1f));
		}

		public static float InQuad(float time, float duration, float overshootOrAmplitude, float period)
		{
			return ((time /= duration) *time);
		}

		public static float OutQuad(float time, float duration, float overshootOrAmplitude, float period)
		{
			return (-(time /= duration) * (time - 2f));
		}

		public static float InOutQuad(float time, float duration, float overshootOrAmplitude, float period)
		{
			if ((time /= (duration * 0.5f)) < 1f)
			{
				return ((0.5f * time) * time);
			}
			return (-0.5f * ((--time * (time - 2f)) - 1f));
		}


		public static float InCubic(float time, float duration, float overshootOrAmplitude, float period)
		{
			return (((time /= duration) * time) * time);
		}

		public static float OutCubic(float time, float duration, float overshootOrAmplitude, float period)
		{
			return ((((time = (time / duration) - 1f) * time) * time) + 1f);
		}

		public static float InOutCubic(float time, float duration, float overshootOrAmplitude, float period)
		{
			if ((time /= (duration * 0.5f)) < 1f)
			{
				return (((0.5f * time) * time) * time);
			}
			return (0.5f * ((((time -= 2f) * time) * time) + 2f));
		}

		public static float InQuart(float time, float duration, float overshootOrAmplitude, float period)
		{
			return ((((time /= duration) * time) * time) * time);
		}

		public static float OutQuart(float time, float duration, float overshootOrAmplitude, float period)
		{
			return -(((((time = (time / duration) - 1f) * time) * time) * time) - 1f);
		}

		public static float InOutQuart(float time, float duration, float overshootOrAmplitude, float period)
		{
			if ((time /= (duration * 0.5f)) < 1f)
			{
				return ((((0.5f * time) * time) * time) * time);
			}
			return (-0.5f * (((((time -= 2f) * time) * time) * time) - 2f));
		}

		public static float InQuint(float time, float duration, float overshootOrAmplitude, float period)
		{
			return (((((time /= duration) * time) * time) * time) * time);
		}

		public static float OutQuint(float time, float duration, float overshootOrAmplitude, float period)
		{
			return ((((((time = (time / duration) - 1f) * time) * time) * time) * time) + 1f);
		}

		public static float InOutQuint(float time, float duration, float overshootOrAmplitude, float period)
		{
			if ((time /= (duration * 0.5f)) < 1f)
			{
				return (((((0.5f * time) * time) * time) * time) * time);
			}
			return (0.5f * ((((((time -= 2f) * time) * time) * time) * time) + 2f));
		}

		public static float InExpo(float time, float duration, float overshootOrAmplitude, float period)
		{
			if (time != 0f)
			{
				return (float) Math.Pow(2.0, (double) (10f * ((time / duration) - 1f)));
			}
			return 0f;
		}

		public static float OutExpo(float time, float duration, float overshootOrAmplitude, float period)
		{
			if (time == duration)
			{
				return 1f;
			}
			return (-((float) Math.Pow(2.0, (double) ((-10f * time) / duration))) + 1f);
		}

		public static float InOutExpo(float time, float duration, float overshootOrAmplitude, float period)
		{
			if (time == 0f)
			{
				return 0f;
			}
			if (time == duration)
			{
				return 1f;
			}
			if ((time /= (duration * 0.5f)) < 1f)
			{
				return (0.5f * ((float) Math.Pow(2.0, (double) (10f * (time - 1f)))));
			}
			return (0.5f * (-((float) Math.Pow(2.0, (double) (-10f * --time))) + 2f));
		}

		public static float InCirc(float time, float duration, float overshootOrAmplitude, float period)
		{
			return -(((float) Math.Sqrt((double) (1f - ((time /= duration) * time)))) - 1f);
		}

		public static float OutCirc(float time, float duration, float overshootOrAmplitude, float period)
		{
			return (float) Math.Sqrt((double) (1f - ((time = (time / duration) - 1f) * time)));
		}

		public static float InOutCirc(float time, float duration, float overshootOrAmplitude, float period)
		{
			if ((time /= (duration * 0.5f)) < 1f)
			{
				return (-0.5f * (((float) Math.Sqrt((double) (1f - (time * time)))) - 1f));
			}
			return (0.5f * (((float) Math.Sqrt((double) (1f - ((time -= 2f) * time)))) + 1f));
		}

		public static float InElastic(float time, float duration, float overshootOrAmplitude, float period)
		{
			float num;
			if (time == 0f)
			{
				return 0f;
			}
			if ((time /= duration) == 1f)
			{
				return 1f;
			}
			if (period == 0f)
			{
				period = duration * 0.3f;
			}
			if (overshootOrAmplitude < 1f)
			{
				overshootOrAmplitude = 1f;
				num = period / 4f;
			}
			else
			{
				num = (period / 6.283185f) * ((float) Math.Asin((double) (1f / overshootOrAmplitude)));
			}
			return -((overshootOrAmplitude * ((float) Math.Pow(2.0, (double) (10f * --time)))) * ((float) Math.Sin((double) ((((time * duration) - num) * 6.283185f) / period))));
		}

		public static float OutElastic(float time, float duration, float overshootOrAmplitude, float period)
		{
			float num;
			if (time == 0f)
			{
				return 0f;
			}
			if ((time /= duration) == 1f)
			{
				return 1f;
			}
			if (period == 0f)
			{
				period = duration * 0.3f;
			}
			if (overshootOrAmplitude < 1f)
			{
				overshootOrAmplitude = 1f;
				num = period / 4f;
			}
			else
			{
				num = (period / 6.283185f) * ((float) Math.Asin((double) (1f / overshootOrAmplitude)));
			}
			return (((overshootOrAmplitude * ((float) Math.Pow(2.0, (double) (-10f * time)))) * ((float) Math.Sin((double) ((((time * duration) - num) * 6.283185f) / period)))) + 1f);
		}


		public static float InOutElastic(float time, float duration, float overshootOrAmplitude, float period)
		{
			float num;
			if (time == 0f)
			{
				return 0f;
			}
			if ((time /= (duration * 0.5f)) == 2f)
			{
				return 1f;
			}
			if (period == 0f)
			{
				period = duration * 0.45f;
			}
			if (overshootOrAmplitude < 1f)
			{
				overshootOrAmplitude = 1f;
				num = period / 4f;
			}
			else
			{
				num = (period / 6.283185f) * ((float) Math.Asin((double) (1f / overshootOrAmplitude)));
			}
			if (time < 1f)
			{
				return (-0.5f * ((overshootOrAmplitude * ((float) Math.Pow(2.0, (double) (10f * --time)))) * ((float) Math.Sin((double) ((((time * duration) - num) * 6.283185f) / period)))));
			}
			return ((((overshootOrAmplitude * ((float) Math.Pow(2.0, (double) (-10f * --time)))) * ((float) Math.Sin((double) ((((time * duration) - num) * 6.283185f) / period)))) * 0.5f) + 1f);
		
		}

		public static float InBack(float time, float duration, float overshootOrAmplitude, float period)
		{
			return (((time /= duration) * time) * (((overshootOrAmplitude + 1f) * time) - overshootOrAmplitude));
		}

		public static float OutBack(float time, float duration, float overshootOrAmplitude, float period)
		{
			return ((((time = (time / duration) - 1f) * time) * (((overshootOrAmplitude + 1f) * time) + overshootOrAmplitude)) + 1f);
		}

		public static float InOutBack(float time, float duration, float overshootOrAmplitude, float period)
		{
			if ((time /= (duration * 0.5f)) < 1f)
			{
				return (0.5f * ((time * time) * ((((overshootOrAmplitude *= 1.525f) + 1f) * time) - overshootOrAmplitude)));
			}
			return (0.5f * ((((time -= 2f) * time) * ((((overshootOrAmplitude *= 1.525f) + 1f) * time) + overshootOrAmplitude)) + 2f));
		}

		public static float InBounce(float time, float duration, float overshootOrAmplitude, float period)
		{
			return Bounce.EaseIn(time, duration, overshootOrAmplitude, period);
		}

		public static float OutBounce(float time, float duration, float overshootOrAmplitude, float period)
		{
			return Bounce.EaseOut(time, duration, overshootOrAmplitude, period);
		}

		public static float InOutBounce(float time, float duration, float overshootOrAmplitude, float period)
		{
			return Bounce.EaseInOut(time, duration, overshootOrAmplitude, period);
		}

		public static float Flashs(float time, float duration, float overshootOrAmplitude, float period)
		{
			return Flash.Ease(time, duration, overshootOrAmplitude, period);
		}

		public static float InFlash(float time, float duration, float overshootOrAmplitude, float period)
		{
			return Flash.EaseIn(time, duration, overshootOrAmplitude, period);
		}

		public static float OutFlash(float time, float duration, float overshootOrAmplitude, float period)
		{
			return Flash.EaseOut(time, duration, overshootOrAmplitude, period);
		}

		public static float InOutFlash(float time, float duration, float overshootOrAmplitude, float period)
		{
			return Flash.EaseInOut(time, duration, overshootOrAmplitude, period);
		}

		public static float INTERNAL_Zero(float time, float duration, float overshootOrAmplitude, float period)
		{
			return 1f;
		}

		public static float Other(float time, float duration, float overshootOrAmplitude, float period)
		{
			return (-(time /= duration) * (time - 2f));
		}

		public static float Evaluate(Tween t, float time, float duration, float overshootOrAmplitude, float period)
		{
			return Evaluate(t.easeType, t.customEase, time, duration, overshootOrAmplitude, period);
		}

		public static float Evaluate(Ease easeType, EaseFunction customEase, float time, float duration, float overshootOrAmplitude, float period)
		{
			switch (easeType)
			{
			case Ease.Linear:
				return m_Linear(time, duration, overshootOrAmplitude, period);
			case Ease.InSine:
				return m_InSine(time, duration, overshootOrAmplitude, period);
			case Ease.OutSine:
				return m_OutSine(time, duration, overshootOrAmplitude, period);
			case Ease.InOutSine:
				return m_InOutSine(time, duration, overshootOrAmplitude, period);
			case Ease.InQuad:
				return m_InQuad(time, duration, overshootOrAmplitude, period);
			case Ease.OutQuad:
				return m_OutQuad(time, duration, overshootOrAmplitude, period);
			case Ease.InOutQuad:
				return m_InOutQuad(time, duration, overshootOrAmplitude, period);
			case Ease.InCubic:
				return m_InCubic(time, duration, overshootOrAmplitude, period);
			case Ease.OutCubic:
				return m_OutCubic(time, duration, overshootOrAmplitude, period);
			case Ease.InOutCubic:
				return m_InOutCubic(time, duration, overshootOrAmplitude, period);
			case Ease.InQuart:
				return m_InQuart(time, duration, overshootOrAmplitude, period);
			case Ease.OutQuart:
				return m_OutQuart(time, duration, overshootOrAmplitude, period);
			case Ease.InOutQuart:
				return m_InOutQuart(time, duration, overshootOrAmplitude, period);
			case Ease.InQuint:
				return m_InQuint(time, duration, overshootOrAmplitude, period);
			case Ease.OutQuint:
				return m_OutQuint(time, duration, overshootOrAmplitude, period);
			case Ease.InOutQuint:
				return m_InOutQuint(time, duration, overshootOrAmplitude, period);
			case Ease.InExpo:
				return m_InExpo(time, duration, overshootOrAmplitude, period);
			case Ease.OutExpo:
				return m_OutExpo(time, duration, overshootOrAmplitude, period);
			case Ease.InOutExpo:
				return m_InOutExpo(time, duration, overshootOrAmplitude, period);
			case Ease.InCirc:
				return m_InCirc(time, duration, overshootOrAmplitude, period);
			case Ease.OutCirc:
				return m_OutCirc(time, duration, overshootOrAmplitude, period);
			case Ease.InOutCirc:
				return m_InOutCirc(time, duration, overshootOrAmplitude, period);
			case Ease.OutElastic:
				return m_OutElastic(time, duration, overshootOrAmplitude, period);
			case Ease.InOutElastic:
				return m_InOutElastic(time, duration, overshootOrAmplitude, period);
			case Ease.InBack:
				return m_InBack(time, duration, overshootOrAmplitude, period);
			case Ease.OutBack:
				return m_OutBack(time, duration, overshootOrAmplitude, period);
			case Ease.InOutBack:
				return m_InOutBack(time, duration, overshootOrAmplitude, period);
			case Ease.InBounce:
				return m_InBounce(time, duration, overshootOrAmplitude, period);

			case Ease.OutBounce:
				return m_OutBounce(time, duration, overshootOrAmplitude, period);

			case Ease.InOutBounce:
				return m_InOutBounce(time, duration, overshootOrAmplitude, period);

			case Ease.Flash:
				return m_Flash(time, duration, overshootOrAmplitude, period);

			case Ease.InFlash:
				return m_InFlash(time, duration, overshootOrAmplitude, period);

			case Ease.OutFlash:
				return m_OutFlash(time, duration, overshootOrAmplitude, period);

			case Ease.InOutFlash:
				return m_InOutFlash(time, duration, overshootOrAmplitude, period);

			case Ease.INTERNAL_Zero:
				return m_INTERNAL_Zero(time, duration, overshootOrAmplitude, period);

			case Ease.INTERNAL_Custom:
				return customEase(time, duration, overshootOrAmplitude, period);
			}
			return m_Other(time, duration, overshootOrAmplitude, period);
		}

		public static EaseFunction ToEaseFunction(Ease ease)
		{
			switch (ease)
			{
			case Ease.Linear:
				return m_Linear;
			case Ease.InSine:
				return m_InSine;
			case Ease.OutSine:
				return m_OutSine;
			case Ease.InOutSine:
				return m_InOutSine;
			case Ease.InQuad:
				return m_InQuad;
			case Ease.OutQuad:
				return m_OutQuad;
			case Ease.InOutQuad:
				return m_InOutQuad;
			case Ease.InCubic:
				return m_InCubic;
			case Ease.OutCubic:
				return m_OutCubic;
			case Ease.InOutCubic:
				return m_InOutCubic;
			case Ease.InQuart:
				return m_InQuart;
			case Ease.OutQuart:
				return m_OutQuart;
			case Ease.InOutQuart:
				return m_InOutQuart;
			case Ease.InQuint:
				return m_InQuint;
			case Ease.OutQuint:
				return m_OutQuint;
			case Ease.InOutQuint:
				return m_InOutQuint;
			case Ease.InExpo:
				return m_InExpo;
			case Ease.OutExpo:
				return m_OutExpo;
			case Ease.InOutExpo:
				return m_InOutExpo;
			case Ease.InCirc:
				return m_InCirc;
			case Ease.OutCirc:
				return m_OutCirc;
			case Ease.InOutCirc:
				return m_InOutCirc;
			case Ease.OutElastic:
				return m_OutElastic;
			case Ease.InOutElastic:
				return m_InOutElastic;
			case Ease.InBack:
				return m_InBack;
			case Ease.OutBack:
				return m_OutBack;
			case Ease.InOutBack:
				return m_InOutBack;
			case Ease.InBounce:
				return m_InBounce;

			case Ease.OutBounce:
				return m_OutBounce;

			case Ease.InOutBounce:
				return m_InOutBounce;

			case Ease.Flash:
				return m_Flash;

			case Ease.InFlash:
				return m_InFlash;

			case Ease.OutFlash:
				return m_OutFlash;

			case Ease.InOutFlash:
				return m_InOutFlash;

			case Ease.INTERNAL_Zero:
				return m_INTERNAL_Zero;
			}
			return m_Other;

		}
	}
}