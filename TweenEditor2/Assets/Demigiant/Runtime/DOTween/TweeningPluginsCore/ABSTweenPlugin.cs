using DG.Tweening.Core;
using DG.Tweening.Core.Enums;

namespace DG.Tweening.Plugins.Core
{
	public abstract class ABSTweenPlugin<T1, T2, TPlugOptions> : ITweenPlugin where TPlugOptions: struct
	{
		// Methods
		protected ABSTweenPlugin()
		{
		}

		public abstract T2 ConvertToStartValue(TweenerCore<T1, T2, TPlugOptions> t, T1 value);
		public abstract void EvaluateAndApply(TPlugOptions options, Tween t, bool isRelative, DOGetter<T1> getter, DOSetter<T1> setter, float elapsed, T2 startValue, T2 changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice);
		public abstract float GetSpeedBasedDuration(TPlugOptions options, float unitsXSecond, T2 changeValue);
		public abstract void Reset(TweenerCore<T1, T2, TPlugOptions> t);
		public abstract void SetChangeValue(TweenerCore<T1, T2, TPlugOptions> t);
		public abstract void SetFrom(TweenerCore<T1, T2, TPlugOptions> t, bool isRelative);
		public abstract void SetRelativeEndValue(TweenerCore<T1, T2, TPlugOptions> t);
	}

}