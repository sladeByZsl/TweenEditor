using System.Runtime.InteropServices;
using UnityEngine;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using System;

namespace DG.Tweening.Plugins
{
	[StructLayout(LayoutKind.Sequential)]
	public struct SpiralOptions
	{
		public float depth;
		public float frequency;
		public float speed;
		public SpiralMode mode;
		public bool snapping;
		internal float unit;
		internal Quaternion axisQ;
	}

	public class SpiralPlugin : ABSTweenPlugin<Vector3, Vector3, SpiralOptions>
	{
		// Fields
		public static readonly Vector3 DefaultDirection = Vector3.forward;

		// Methods
		public override Vector3 ConvertToStartValue(TweenerCore<Vector3, Vector3, SpiralOptions> t, Vector3 value)
		{
			return value;
		}

		public override void EvaluateAndApply(SpiralOptions options, Tween t, bool isRelative, DOGetter<Vector3> getter, DOSetter<Vector3> setter, float elapsed, Vector3 startValue, Vector3 changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			float num = EaseManager.Evaluate(t, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			float num2 = ((options.mode == SpiralMode.ExpandThenContract) && (num > 0.5f)) ? (0.5f - (num - 0.5f)) : num;
			if (t.loopType == LoopType.Incremental)
			{
				num += t.isComplete ? ((float) (t.completedLoops - 1)) : ((float) t.completedLoops);
			}
			float num3 = (duration * options.speed) * num;
			options.unit = (duration * options.speed) * num2;
			Vector3 pNewValue = new Vector3(options.unit * Mathf.Cos(num3 * options.frequency), options.unit * Mathf.Sin(num3 * options.frequency), options.depth * num);
			pNewValue = ((Vector3) (options.axisQ * pNewValue)) + startValue;
			if (options.snapping)
			{
				pNewValue.x = (float) Math.Round((double) pNewValue.x);
				pNewValue.y = (float) Math.Round((double) pNewValue.y);
				pNewValue.z = (float) Math.Round((double) pNewValue.z);
			}
			setter(pNewValue);
		}

		public static ABSTweenPlugin<Vector3, Vector3, SpiralOptions> Get()
		{
			return PluginsManager.GetCustomPlugin<SpiralPlugin, Vector3, Vector3, SpiralOptions>();
		}

		public override float GetSpeedBasedDuration(SpiralOptions options, float unitsXSecond, Vector3 changeValue)
		{
			return unitsXSecond;
		}

		public override void Reset(TweenerCore<Vector3, Vector3, SpiralOptions> t)
		{
		}

		public override void SetChangeValue(TweenerCore<Vector3, Vector3, SpiralOptions> t)
		{
			t.plugOptions.speed *= 10f / t.plugOptions.frequency;
			t.plugOptions.axisQ = Quaternion.LookRotation(t.endValue, Vector3.up);
		}

		public override void SetFrom(TweenerCore<Vector3, Vector3, SpiralOptions> t, bool isRelative)
		{
		}

		public override void SetRelativeEndValue(TweenerCore<Vector3, Vector3, SpiralOptions> t)
		{
		}
	}

}