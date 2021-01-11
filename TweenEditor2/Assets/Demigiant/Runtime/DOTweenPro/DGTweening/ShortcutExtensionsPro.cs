using UnityEngine;
using DG.Tweening.Core;
using DG.Tweening.Plugins;

namespace DG.Tweening
{
	public static class ShortcutExtensionsPro
	{
		// Methods
		public static Tweener DOSpiral(this Rigidbody target, float duration, Vector3? axis=null, SpiralMode mode=SpiralMode.Expand, float speed=1f, float frequency=10f, float depth=0f, bool snapping=false)
		{
			TweenerCore<Vector3, Vector3, SpiralOptions> local1;
			if (Mathf.Approximately(speed, 0f))
			{
				speed = 1f;
			}
			if (axis.HasValue)
			{
				Vector3? nullable = axis;
				Vector3 zero = Vector3.zero;
				if (!(nullable.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() == zero) : true) : false))
				{
					goto Label_0066;
				}
			}
			axis = new Vector3?(Vector3.forward);
			Label_0066:
			local1 = DOTween.To<Vector3, Vector3, SpiralOptions>(SpiralPlugin.Get(), () => target.position, new DOSetter<Vector3>(target.MovePosition), axis.Value, duration).SetTarget<TweenerCore<Vector3, Vector3, SpiralOptions>>(target);
			local1.plugOptions.mode = mode;
			local1.plugOptions.speed = speed;
			local1.plugOptions.frequency = frequency;
			local1.plugOptions.depth = depth;
			local1.plugOptions.snapping = snapping;
			return local1;
		}

		public static Tweener DOSpiral(this Transform target, float duration, Vector3? axis=null, SpiralMode mode=SpiralMode.Expand, float speed=1f, float frequency=10f, float depth=0f, bool snapping=false)
		{
			TweenerCore<Vector3, Vector3, SpiralOptions> local1;
			if (Mathf.Approximately(speed, 0f))
			{
				speed = 1f;
			}
			if (axis.HasValue)
			{
				Vector3? nullable = axis;
				Vector3 zero = Vector3.zero;
				if (!(nullable.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() == zero) : true) : false))
				{
					goto Label_0066;
				}
			}
			axis = new Vector3?(Vector3.forward);
			Label_0066:
			local1 = DOTween.To<Vector3, Vector3, SpiralOptions>(SpiralPlugin.Get(), () => target.localPosition, delegate (Vector3 x) {
				target.localPosition = x;
			}, axis.Value, duration).SetTarget<TweenerCore<Vector3, Vector3, SpiralOptions>>(target);
			local1.plugOptions.mode = mode;
			local1.plugOptions.speed = speed;
			local1.plugOptions.frequency = frequency;
			local1.plugOptions.depth = depth;
			local1.plugOptions.snapping = snapping;
			return local1;
		}
	}
}