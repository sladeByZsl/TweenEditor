using DG.Tweening.Core;
using UnityEngine;
using DG.Tweening.Plugins.Options;

namespace DG.Tweening.Plugins.Core
{
	internal static class SpecialPluginsUtils
	{
		// Methods
		internal static bool SetCameraShakePosition(TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t)
		{
			if (!SetShake(t))
			{
				return false;
			}
			Camera target = t.target as Camera;
			if (target == null)
			{
				return false;
			}
			Vector3 vector = t.getter();
			Transform transform = target.transform;
			int length = t.endValue.Length;
			for (int i = 0; i < length; i++)
			{
				Vector3 vector2 = t.endValue[i];
				t.endValue[i] = (transform.localRotation * (vector2 - vector)) + vector;
			}
			return true;
		}

		internal static bool SetLookAt(TweenerCore<Quaternion, Vector3, QuaternionOptions> t)
		{
			Transform target = t.target as Transform;
			Vector3 forward = t.endValue - target.position;
			switch (t.plugOptions.axisConstraint)
			{
			case AxisConstraint.X:
				forward.x = 0f;
				break;

			case AxisConstraint.Y:
				forward.y = 0f;
				break;

			case AxisConstraint.Z:
				forward.z = 0f;
				break;
			}
			Vector3 eulerAngles = Quaternion.LookRotation(forward, t.plugOptions.up).eulerAngles;
			t.endValue = eulerAngles;
			return true;
		}

		internal static bool SetPunch(TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t)
		{
			Vector3 vector;
			try
			{
				vector = t.getter();
			}
			catch
			{
				return false;
			}
			t.isRelative = t.isSpeedBased = false;
			t.easeType = Ease.OutQuad;
			t.customEase = null;
			int length = t.endValue.Length;
			for (int i = 0; i < length; i++)
			{
				t.endValue[i] += vector;
			}
			return true;
		}

		internal static bool SetShake(TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t)
		{
			if (!SetPunch(t))
			{
				return false;
			}
			t.easeType = Ease.Linear;
			return true;
		}
	}
}