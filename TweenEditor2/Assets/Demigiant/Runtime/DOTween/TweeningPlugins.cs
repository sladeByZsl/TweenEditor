using DG.Tweening.Plugins.Core;
using UnityEngine;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Core.Enums;
using System.Text;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using System;
using System.Text.RegularExpressions;

namespace DG.Tweening.Plugins
{
	internal class Color2Plugin : ABSTweenPlugin<Color2, Color2, ColorOptions>
	{
		// Methods
		public override Color2 ConvertToStartValue(TweenerCore<Color2, Color2, ColorOptions> t, Color2 value)
		{
			return value;
		}

		public override void EvaluateAndApply(ColorOptions options, Tween t, bool isRelative, DOGetter<Color2> getter, DOSetter<Color2> setter, float elapsed, Color2 startValue, Color2 changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			if (t.loopType == LoopType.Incremental)
			{
				startValue += (Color2) (changeValue * (t.isComplete ? ((float) (t.completedLoops - 1)) : ((float) t.completedLoops)));
			}
			if (t.isSequenced && (t.sequenceParent.loopType == LoopType.Incremental))
			{
				startValue += (Color2) ((changeValue * ((t.loopType == LoopType.Incremental) ? ((float) t.loops) : ((float) 1))) * (t.sequenceParent.isComplete ? ((float) (t.sequenceParent.completedLoops - 1)) : ((float) t.sequenceParent.completedLoops)));
			}
			float num = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			if (!options.alphaOnly)
			{
				startValue.ca.r += changeValue.ca.r * num;
				startValue.ca.g += changeValue.ca.g * num;
				startValue.ca.b += changeValue.ca.b * num;
				startValue.ca.a += changeValue.ca.a * num;
				startValue.cb.r += changeValue.cb.r * num;
				startValue.cb.g += changeValue.cb.g * num;
				startValue.cb.b += changeValue.cb.b * num;
				startValue.cb.a += changeValue.cb.a * num;
				setter(startValue);
			}
			else
			{
				Color2 pNewValue = getter();
				pNewValue.ca.a = startValue.ca.a + (changeValue.ca.a * num);
				pNewValue.cb.a = startValue.cb.a + (changeValue.cb.a * num);
				setter(pNewValue);
			}
		}

		public override float GetSpeedBasedDuration(ColorOptions options, float unitsXSecond, Color2 changeValue)
		{
			return (1f / unitsXSecond);
		}

		public override void Reset(TweenerCore<Color2, Color2, ColorOptions> t)
		{
		}

		public override void SetChangeValue(TweenerCore<Color2, Color2, ColorOptions> t)
		{
			t.changeValue = t.endValue - t.startValue;
		}

		public override void SetFrom(TweenerCore<Color2, Color2, ColorOptions> t, bool isRelative)
		{
			Color2 endValue = t.endValue;
			t.endValue = t.getter();
			if (isRelative)
			{
				t.startValue = new Color2(t.endValue.ca + endValue.ca, t.endValue.cb + endValue.cb);
			}
			else
			{
				t.startValue = new Color2(endValue.ca, endValue.cb);
			}
			Color2 pNewValue = t.endValue;
			if (!t.plugOptions.alphaOnly)
			{
				pNewValue = t.startValue;
			}
			else
			{
				pNewValue.ca.a = t.startValue.ca.a;
				pNewValue.cb.a = t.startValue.cb.a;
			}
			t.setter(pNewValue);
		}

		public override void SetRelativeEndValue(TweenerCore<Color2, Color2, ColorOptions> t)
		{
			TweenerCore<Color2, Color2, ColorOptions> core = t;
			core.endValue += t.startValue;
		}
	}

	public class ColorPlugin : ABSTweenPlugin<Color, Color, ColorOptions>
	{
		// Methods
		public override Color ConvertToStartValue(TweenerCore<Color, Color, ColorOptions> t, Color value)
		{
			return value;
		}

		public override void EvaluateAndApply(ColorOptions options, Tween t, bool isRelative, DOGetter<Color> getter, DOSetter<Color> setter, float elapsed, Color startValue, Color changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			if (t.loopType == LoopType.Incremental)
			{
				startValue += (Color) (changeValue * (t.isComplete ? ((float) (t.completedLoops - 1)) : ((float) t.completedLoops)));
			}
			if (t.isSequenced && (t.sequenceParent.loopType == LoopType.Incremental))
			{
				startValue += (Color) ((changeValue * ((t.loopType == LoopType.Incremental) ? ((float) t.loops) : ((float) 1))) * (t.sequenceParent.isComplete ? ((float) (t.sequenceParent.completedLoops - 1)) : ((float) t.sequenceParent.completedLoops)));
			}
			float num = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			if (!options.alphaOnly)
			{
				startValue.r += changeValue.r * num;
				startValue.g += changeValue.g * num;
				startValue.b += changeValue.b * num;
				startValue.a += changeValue.a * num;
				setter(startValue);
			}
			else
			{
				Color pNewValue = getter();
				pNewValue.a = startValue.a + (changeValue.a * num);
				setter(pNewValue);
			}
		}

		public override float GetSpeedBasedDuration(ColorOptions options, float unitsXSecond, Color changeValue)
		{
			return (1f / unitsXSecond);
		}

		public override void Reset(TweenerCore<Color, Color, ColorOptions> t)
		{
		}

		public override void SetChangeValue(TweenerCore<Color, Color, ColorOptions> t)
		{
			t.changeValue = t.endValue - t.startValue;
		}

		public override void SetFrom(TweenerCore<Color, Color, ColorOptions> t, bool isRelative)
		{
			Color endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = isRelative ? (t.endValue + endValue) : endValue;
			Color pNewValue = t.endValue;
			if (!t.plugOptions.alphaOnly)
			{
				pNewValue = t.startValue;
			}
			else
			{
				pNewValue.a = t.startValue.a;
			}
			t.setter(pNewValue);
		}

		public override void SetRelativeEndValue(TweenerCore<Color, Color, ColorOptions> t)
		{
			TweenerCore<Color, Color, ColorOptions> core = t;
			core.endValue += t.startValue;
		}
	}


	public class DoublePlugin : ABSTweenPlugin<double, double, NoOptions>
	{
		// Methods
		public override double ConvertToStartValue(TweenerCore<double, double, NoOptions> t, double value)
		{
			return value;
		}

		public override void EvaluateAndApply(NoOptions options, Tween t, bool isRelative, DOGetter<double> getter, DOSetter<double> setter, float elapsed, double startValue, double changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			if (t.loopType == LoopType.Incremental)
			{
				startValue += changeValue * (t.isComplete ? ((double) (t.completedLoops - 1)) : ((double) t.completedLoops));
			}
			if (t.isSequenced && (t.sequenceParent.loopType == LoopType.Incremental))
			{
				startValue += (changeValue * ((t.loopType == LoopType.Incremental) ? ((double) t.loops) : ((double) 1))) * (t.sequenceParent.isComplete ? ((double) (t.sequenceParent.completedLoops - 1)) : ((double) t.sequenceParent.completedLoops));
			}
			setter(startValue + (changeValue * EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod)));
		}

		public override float GetSpeedBasedDuration(NoOptions options, float unitsXSecond, double changeValue)
		{
			float num = ((float) changeValue) / unitsXSecond;
			if (num < 0f)
			{
				num = -num;
			}
			return num;
		}

		public override void Reset(TweenerCore<double, double, NoOptions> t)
		{
		}

		public override void SetChangeValue(TweenerCore<double, double, NoOptions> t)
		{
			t.changeValue = t.endValue - t.startValue;
		}

		public override void SetFrom(TweenerCore<double, double, NoOptions> t, bool isRelative)
		{
			double endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = isRelative ? (t.endValue + endValue) : endValue;
			t.setter(t.startValue);
		}

		public override void SetRelativeEndValue(TweenerCore<double, double, NoOptions> t)
		{
			t.endValue += t.startValue;
		}
	}

	public class FloatPlugin : ABSTweenPlugin<float, float, FloatOptions>
	{
		// Methods
		public override float ConvertToStartValue(TweenerCore<float, float, FloatOptions> t, float value)
		{
			return value;
		}

		public override void EvaluateAndApply(FloatOptions options, Tween t, bool isRelative, DOGetter<float> getter, DOSetter<float> setter, float elapsed, float startValue, float changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			if (t.loopType == LoopType.Incremental)
			{
				startValue += changeValue * (t.isComplete ? ((float) (t.completedLoops - 1)) : ((float) t.completedLoops));
			}
			if (t.isSequenced && (t.sequenceParent.loopType == LoopType.Incremental))
			{
				startValue += (changeValue * ((t.loopType == LoopType.Incremental) ? ((float) t.loops) : ((float) 1))) * (t.sequenceParent.isComplete ? ((float) (t.sequenceParent.completedLoops - 1)) : ((float) t.sequenceParent.completedLoops));
			}
			setter(!options.snapping ? (startValue + (changeValue * EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod))) : ((float) Math.Round((double) (startValue + (changeValue * EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod))))));
		}

		public override float GetSpeedBasedDuration(FloatOptions options, float unitsXSecond, float changeValue)
		{
			float num = changeValue / unitsXSecond;
			if (num < 0f)
			{
				num = -num;
			}
			return num;
		}

		public override void Reset(TweenerCore<float, float, FloatOptions> t)
		{
		}

		public override void SetChangeValue(TweenerCore<float, float, FloatOptions> t)
		{
			t.changeValue = t.endValue - t.startValue;
		}

		public override void SetFrom(TweenerCore<float, float, FloatOptions> t, bool isRelative)
		{
			float endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = isRelative ? (t.endValue + endValue) : endValue;
			t.setter(!t.plugOptions.snapping ? t.startValue : ((float) Math.Round((double) t.startValue)));
		}

		public override void SetRelativeEndValue(TweenerCore<float, float, FloatOptions> t)
		{
			t.endValue += t.startValue;
		}
	}


	public class IntPlugin : ABSTweenPlugin<int, int, NoOptions>
	{
		// Methods
		public override int ConvertToStartValue(TweenerCore<int, int, NoOptions> t, int value)
		{
			return value;
		}

		public override void EvaluateAndApply(NoOptions options, Tween t, bool isRelative, DOGetter<int> getter, DOSetter<int> setter, float elapsed, int startValue, int changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			if (t.loopType == LoopType.Incremental)
			{
				startValue += changeValue * (t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
			}
			if (t.isSequenced && (t.sequenceParent.loopType == LoopType.Incremental))
			{
				startValue += (changeValue * ((t.loopType == LoopType.Incremental) ? t.loops : 1)) * (t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
			}
			setter((int) Math.Round((double) (startValue + (changeValue * EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod)))));
		}

		public override float GetSpeedBasedDuration(NoOptions options, float unitsXSecond, int changeValue)
		{
			float num = ((float) changeValue) / unitsXSecond;
			if (num < 0f)
			{
				num = -num;
			}
			return num;
		}

		public override void Reset(TweenerCore<int, int, NoOptions> t)
		{
		}

		public override void SetChangeValue(TweenerCore<int, int, NoOptions> t)
		{
			t.changeValue = t.endValue - t.startValue;
		}

		public override void SetFrom(TweenerCore<int, int, NoOptions> t, bool isRelative)
		{
			int endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = isRelative ? (t.endValue + endValue) : endValue;
			t.setter(t.startValue);
		}

		public override void SetRelativeEndValue(TweenerCore<int, int, NoOptions> t)
		{
			t.endValue += t.startValue;
		}
	}


	public class LongPlugin : ABSTweenPlugin<long, long, NoOptions>
	{
		// Methods
		public override long ConvertToStartValue(TweenerCore<long, long, NoOptions> t, long value)
		{
			return value;
		}

		public override void EvaluateAndApply(NoOptions options, Tween t, bool isRelative, DOGetter<long> getter, DOSetter<long> setter, float elapsed, long startValue, long changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			if (t.loopType == LoopType.Incremental)
			{
				startValue += changeValue * (t.isComplete ? ((long) (t.completedLoops - 1)) : ((long) t.completedLoops));
			}
			if (t.isSequenced && (t.sequenceParent.loopType == LoopType.Incremental))
			{
				startValue += (changeValue * ((t.loopType == LoopType.Incremental) ? ((long) t.loops) : ((long) 1))) * (t.sequenceParent.isComplete ? ((long) (t.sequenceParent.completedLoops - 1)) : ((long) t.sequenceParent.completedLoops));
			}
			setter((long) Math.Round((double) (startValue + (changeValue * EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod)))));
		}

		public override float GetSpeedBasedDuration(NoOptions options, float unitsXSecond, long changeValue)
		{
			float num = ((float) changeValue) / unitsXSecond;
			if (num < 0f)
			{
				num = -num;
			}
			return num;
		}

		public override void Reset(TweenerCore<long, long, NoOptions> t)
		{
		}

		public override void SetChangeValue(TweenerCore<long, long, NoOptions> t)
		{
			t.changeValue = t.endValue - t.startValue;
		}

		public override void SetFrom(TweenerCore<long, long, NoOptions> t, bool isRelative)
		{
			long endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = isRelative ? (t.endValue + endValue) : endValue;
			t.setter(t.startValue);
		}

		public override void SetRelativeEndValue(TweenerCore<long, long, NoOptions> t)
		{
			t.endValue += t.startValue;
		}
	}

	public class PathPlugin : ABSTweenPlugin<Vector3, Path, PathOptions>
	{
		// Fields
		public const float MinLookAhead = 0.0001f;

		// Methods
		public override Path ConvertToStartValue(TweenerCore<Vector3, Path, PathOptions> t, Vector3 value)
		{
			return t.endValue;
		}

		public override void EvaluateAndApply(PathOptions options, Tween t, bool isRelative, DOGetter<Vector3> getter, DOSetter<Vector3> setter, float elapsed, Path startValue, Path changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			if ((t.loopType == LoopType.Incremental) && !options.isClosedPath)
			{
				int loopIncrement = t.isComplete ? (t.completedLoops - 1) : t.completedLoops;
				if (loopIncrement > 0)
				{
					changeValue = changeValue.CloneIncremental(loopIncrement);
				}
			}
			float perc = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			float num2 = changeValue.ConvertToConstantPathPerc(perc);
			Vector3 point = changeValue.GetPoint(num2, false);
			changeValue.targetPosition = point;
			setter(point);
			if ((options.mode != PathMode.Ignore) && (options.orientType != OrientType.None))
			{
				this.SetOrientation(options, t, changeValue, num2, point, updateNotice);
			}
			bool isMovingForward = !usingInversePosition;
			if (t.isBackwards)
			{
				isMovingForward = !isMovingForward;
			}
			int waypointIndexFromPerc = changeValue.GetWaypointIndexFromPerc(perc, isMovingForward);
			if (waypointIndexFromPerc != t.miscInt)
			{
				t.miscInt = waypointIndexFromPerc;
				if (t.onWaypointChange != null)
				{
					Tween.OnTweenCallback<int>(t.onWaypointChange, waypointIndexFromPerc);
				}
			}
		}

		public static ABSTweenPlugin<Vector3, Path, PathOptions> Get()
		{
			return PluginsManager.GetCustomPlugin<PathPlugin, Vector3, Path, PathOptions>();
		}

		public override float GetSpeedBasedDuration(PathOptions options, float unitsXSecond, Path changeValue)
		{
			return (changeValue.length / unitsXSecond);
		}

		public override void Reset(TweenerCore<Vector3, Path, PathOptions> t)
		{
			t.endValue.Destroy();
			t.startValue = t.endValue = (Path) (t.changeValue = null);
		}

		public override void SetChangeValue(TweenerCore<Vector3, Path, PathOptions> t)
		{
			Transform target = (Transform) t.target;
			if ((t.plugOptions.orientType == OrientType.ToPath) && t.plugOptions.useLocalPosition)
			{
				t.plugOptions.parent = target.parent;
			}
			if (t.endValue.isFinalized)
			{
				t.changeValue = t.endValue;
			}
			else
			{
				Vector3 currTargetVal = t.getter();
				Path endValue = t.endValue;
				int length = endValue.wps.Length;
				int num2 = 0;
				bool flag = false;
				bool flag2 = false;
				if (endValue.wps[0] != currTargetVal)
				{
					flag = true;
					num2++;
				}
				if (t.plugOptions.isClosedPath && (endValue.wps[length - 1] != currTargetVal))
				{
					flag2 = true;
					num2++;
				}
				Vector3[] vectorArray = new Vector3[length + num2];
				int num3 = flag ? 1 : 0;
				if (flag)
				{
					vectorArray[0] = currTargetVal;
				}
				for (int i = 0; i < length; i++)
				{
					vectorArray[i + num3] = endValue.wps[i];
				}
				if (flag2)
				{
					vectorArray[vectorArray.Length - 1] = vectorArray[0];
				}
				endValue.wps = vectorArray;
				endValue.FinalizePath(t.plugOptions.isClosedPath, t.plugOptions.lockPositionAxis, currTargetVal);
				t.plugOptions.startupRot = target.rotation;
				t.plugOptions.startupZRot = target.eulerAngles.z;
				t.changeValue = t.endValue;
			}
		}

		public override void SetFrom(TweenerCore<Vector3, Path, PathOptions> t, bool isRelative)
		{
		}

		public void SetOrientation(PathOptions options, Tween t, Path path, float pathPerc, Vector3 tPos, UpdateNotice updateNotice)
		{
			Vector3 point;
			Transform target = (Transform) t.target;
			Quaternion identity = Quaternion.identity;
			if (updateNotice == UpdateNotice.RewindStep)
			{
				target.rotation = options.startupRot;
			}
			switch (options.orientType)
			{
			case OrientType.ToPath:
				if ((path.type != PathType.Linear) || (options.lookAhead > 0.0001f))
				{
					float perc = pathPerc + options.lookAhead;
					if (perc > 1f)
					{
						perc = options.isClosedPath ? (perc - 1f) : ((path.type == PathType.Linear) ? 1f : 1.00001f);
					}
					point = path.GetPoint(perc, false);
					break;
				}
				point = (tPos + path.wps[path.linearWPIndex]) - path.wps[path.linearWPIndex - 1];
				break;

			case OrientType.LookAtTransform:
				if (options.lookAtTransform != null)
				{
					path.lookAtPosition = new Vector3?(options.lookAtTransform.position);
					identity = Quaternion.LookRotation(options.lookAtTransform.position - target.position, target.up);
				}
				goto Label_037E;

			case OrientType.LookAtPosition:
				path.lookAtPosition = new Vector3?(options.lookAtPosition);
				identity = Quaternion.LookRotation(options.lookAtPosition - target.position, target.up);
				goto Label_037E;

			default:
				goto Label_037E;
			}
			if (path.type == PathType.Linear)
			{
				Vector3 vector3 = path.wps[path.wps.Length - 1];
				if (point == vector3)
				{
					point = (tPos == vector3) ? (vector3 + (vector3 - path.wps[path.wps.Length - 2])) : vector3;
				}
			}
			Vector3 up = target.up;
			if (options.useLocalPosition && (options.parent != null))
			{
				point = options.parent.TransformPoint(point);
			}
			if (options.lockRotationAxis != AxisConstraint.None)
			{
				if ((options.lockRotationAxis & AxisConstraint.X) == AxisConstraint.X)
				{
					Vector3 position = target.InverseTransformPoint(point);
					position.y = 0f;
					point = target.TransformPoint(position);
					up = (options.useLocalPosition && (options.parent != null)) ? options.parent.up : Vector3.up;
				}
				if ((options.lockRotationAxis & AxisConstraint.Y) == AxisConstraint.Y)
				{
					Vector3 vector5 = target.InverseTransformPoint(point);
					if (vector5.z < 0f)
					{
						vector5.z = -vector5.z;
					}
					vector5.x = 0f;
					point = target.TransformPoint(vector5);
				}
				if ((options.lockRotationAxis & AxisConstraint.Z) == AxisConstraint.Z)
				{
					if (options.useLocalPosition && (options.parent != null))
					{
						up = options.parent.TransformDirection(Vector3.up);
					}
					else
					{
						up = target.TransformDirection(Vector3.up);
					}
					up.z = options.startupZRot;
				}
			}
			if (options.mode == PathMode.Full3D)
			{
				Vector3 forward = point - target.position;
				if (forward == Vector3.zero)
				{
					forward = target.forward;
				}
				identity = Quaternion.LookRotation(forward, up);
			}
			else
			{
				float y = 0f;
				float z = Utils.Angle2D(target.position, point);
				if (z < 0f)
				{
					z = 360f + z;
				}
				if (options.mode == PathMode.Sidescroller2D)
				{
					y = (point.x < target.position.x) ? ((float) 180) : ((float) 0);
					if ((z > 90f) && (z < 270f))
					{
						z = 180f - z;
					}
				}
				identity = Quaternion.Euler(0f, y, z);
			}
			Label_037E:
			if (options.hasCustomForwardDirection)
			{
				identity *= options.forward;
			}
			target.rotation = identity;
		}

		public override void SetRelativeEndValue(TweenerCore<Vector3, Path, PathOptions> t)
		{
			if (!t.endValue.isFinalized)
			{
				Vector3 vector = t.getter();
				int length = t.endValue.wps.Length;
				for (int i = 0; i < length; i++)
				{
					Vector3 vectorRef = t.endValue.wps[i];
					vectorRef += vector;
				}
			}
		}
	}

	public class QuaternionPlugin : ABSTweenPlugin<Quaternion, Vector3, QuaternionOptions>
	{
		// Methods
		public override Vector3 ConvertToStartValue(TweenerCore<Quaternion, Vector3, QuaternionOptions> t, Quaternion value)
		{
			return value.eulerAngles;
		}

		public override void EvaluateAndApply(QuaternionOptions options, Tween t, bool isRelative, DOGetter<Quaternion> getter, DOSetter<Quaternion> setter, float elapsed, Vector3 startValue, Vector3 changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			Vector3 euler = startValue;
			if (t.loopType == LoopType.Incremental)
			{
				euler += (Vector3) (changeValue * (t.isComplete ? ((float) (t.completedLoops - 1)) : ((float) t.completedLoops)));
			}
			if (t.isSequenced && (t.sequenceParent.loopType == LoopType.Incremental))
			{
				euler += (Vector3) ((changeValue * ((t.loopType == LoopType.Incremental) ? ((float) t.loops) : ((float) 1))) * (t.sequenceParent.isComplete ? ((float) (t.sequenceParent.completedLoops - 1)) : ((float) t.sequenceParent.completedLoops)));
			}
			float num = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			switch (options.rotateMode)
			{
			case RotateMode.WorldAxisAdd:
			case RotateMode.LocalAxisAdd:
				{
					Quaternion rotation = Quaternion.Euler(startValue);
					euler.x = changeValue.x * num;
					euler.y = changeValue.y * num;
					euler.z = changeValue.z * num;
					if (options.rotateMode == RotateMode.WorldAxisAdd)
					{
						setter(((rotation * Quaternion.Inverse(rotation)) * Quaternion.Euler(euler)) * rotation);
						return;
					}
					setter(rotation * Quaternion.Euler(euler));
					return;
				}
			}
			euler.x += changeValue.x * num;
			euler.y += changeValue.y * num;
			euler.z += changeValue.z * num;
			setter(Quaternion.Euler(euler));
		}

		public override float GetSpeedBasedDuration(QuaternionOptions options, float unitsXSecond, Vector3 changeValue)
		{
			return (changeValue.magnitude / unitsXSecond);
		}

		public override void Reset(TweenerCore<Quaternion, Vector3, QuaternionOptions> t)
		{
		}

		public override void SetChangeValue(TweenerCore<Quaternion, Vector3, QuaternionOptions> t)
		{
			if ((t.plugOptions.rotateMode == RotateMode.Fast) && !t.isRelative)
			{
				Vector3 endValue = t.endValue;
				if (endValue.x > 360f)
				{
					endValue.x = endValue.x % 360f;
				}
				if (endValue.y > 360f)
				{
					endValue.y = endValue.y % 360f;
				}
				if (endValue.z > 360f)
				{
					endValue.z = endValue.z % 360f;
				}
				Vector3 vector2 = endValue - t.startValue;
				float num = (vector2.x > 0f) ? vector2.x : -vector2.x;
				if (num > 180f)
				{
					vector2.x = (vector2.x > 0f) ? -(360f - num) : (360f - num);
				}
				num = (vector2.y > 0f) ? vector2.y : -vector2.y;
				if (num > 180f)
				{
					vector2.y = (vector2.y > 0f) ? -(360f - num) : (360f - num);
				}
				num = (vector2.z > 0f) ? vector2.z : -vector2.z;
				if (num > 180f)
				{
					vector2.z = (vector2.z > 0f) ? -(360f - num) : (360f - num);
				}
				t.changeValue = vector2;
			}
			else if ((t.plugOptions.rotateMode == RotateMode.FastBeyond360) || t.isRelative)
			{
				t.changeValue = t.endValue - t.startValue;
			}
			else
			{
				t.changeValue = t.endValue;
			}
		}

		public override void SetFrom(TweenerCore<Quaternion, Vector3, QuaternionOptions> t, bool isRelative)
		{
			Vector3 endValue = t.endValue;
			t.endValue = t.getter().eulerAngles;
			if ((t.plugOptions.rotateMode == RotateMode.Fast) && !t.isRelative)
			{
				t.startValue = endValue;
			}
			else if (t.plugOptions.rotateMode == RotateMode.FastBeyond360)
			{
				t.startValue = t.endValue + endValue;
			}
			else
			{
				Quaternion quaternion;
				Quaternion rotation = t.getter();
				if (t.plugOptions.rotateMode == RotateMode.WorldAxisAdd)
				{
					quaternion = ((rotation * Quaternion.Inverse(rotation)) * Quaternion.Euler(endValue)) * rotation;
					t.startValue = quaternion.eulerAngles;
				}
				else
				{
					quaternion = rotation * Quaternion.Euler(endValue);
					t.startValue = quaternion.eulerAngles;
				}
				t.endValue = -endValue;
			}
			t.setter(Quaternion.Euler(t.startValue));
		}

		public override void SetRelativeEndValue(TweenerCore<Quaternion, Vector3, QuaternionOptions> t)
		{
			TweenerCore<Quaternion, Vector3, QuaternionOptions> core = t;
			core.endValue += t.startValue;
		}
	}

	public class RectOffsetPlugin : ABSTweenPlugin<RectOffset, RectOffset, NoOptions>
	{
		// Fields
		private static RectOffset _r = new RectOffset();

		// Methods
		public override RectOffset ConvertToStartValue(TweenerCore<RectOffset, RectOffset, NoOptions> t, RectOffset value)
		{
			return new RectOffset(value.left, value.right, value.top, value.bottom);
		}

		public override void EvaluateAndApply(NoOptions options, Tween t, bool isRelative, DOGetter<RectOffset> getter, DOSetter<RectOffset> setter, float elapsed, RectOffset startValue, RectOffset changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			_r.left = startValue.left;
			_r.right = startValue.right;
			_r.top = startValue.top;
			_r.bottom = startValue.bottom;
			if (t.loopType == LoopType.Incremental)
			{
				int num2 = t.isComplete ? (t.completedLoops - 1) : t.completedLoops;
				_r.left += changeValue.left * num2;
				_r.right += changeValue.right * num2;
				_r.top += changeValue.top * num2;
				_r.bottom += changeValue.bottom * num2;
			}
			if (t.isSequenced && (t.sequenceParent.loopType == LoopType.Incremental))
			{
				int num3 = ((t.loopType == LoopType.Incremental) ? t.loops : 1) * (t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
				_r.left += changeValue.left * num3;
				_r.right += changeValue.right * num3;
				_r.top += changeValue.top * num3;
				_r.bottom += changeValue.bottom * num3;
			}
			float num = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			setter(new RectOffset((int) Math.Round((double) (_r.left + (changeValue.left * num))), (int) Math.Round((double) (_r.right + (changeValue.right * num))), (int) Math.Round((double) (_r.top + (changeValue.top * num))), (int) Math.Round((double) (_r.bottom + (changeValue.bottom * num)))));
		}

		public override float GetSpeedBasedDuration(NoOptions options, float unitsXSecond, RectOffset changeValue)
		{
			float right = changeValue.right;
			if (right < 0f)
			{
				right = -right;
			}
			float bottom = changeValue.bottom;
			if (bottom < 0f)
			{
				bottom = -bottom;
			}
			return (((float) Math.Sqrt((double) ((right * right) + (bottom * bottom)))) / unitsXSecond);
		}

		public override void Reset(TweenerCore<RectOffset, RectOffset, NoOptions> t)
		{
			t.startValue = t.endValue = (RectOffset) (t.changeValue = null);
		}

		public override void SetChangeValue(TweenerCore<RectOffset, RectOffset, NoOptions> t)
		{
			t.changeValue = new RectOffset(t.endValue.left - t.startValue.left, t.endValue.right - t.startValue.right, t.endValue.top - t.startValue.top, t.endValue.bottom - t.startValue.bottom);
		}

		public override void SetFrom(TweenerCore<RectOffset, RectOffset, NoOptions> t, bool isRelative)
		{
			RectOffset endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = endValue;
			if (isRelative)
			{
				t.startValue.left += t.endValue.left;
				t.startValue.right += t.endValue.right;
				t.startValue.top += t.endValue.top;
				t.startValue.bottom += t.endValue.bottom;
			}
			t.setter(t.startValue);
		}

		public override void SetRelativeEndValue(TweenerCore<RectOffset, RectOffset, NoOptions> t)
		{
			t.endValue.left += t.startValue.left;
			t.endValue.right += t.startValue.right;
			t.endValue.top += t.startValue.top;
			t.endValue.bottom += t.startValue.bottom;
		}
	}

	public class RectPlugin : ABSTweenPlugin<Rect, Rect, RectOptions>
	{
		// Methods
		public override Rect ConvertToStartValue(TweenerCore<Rect, Rect, RectOptions> t, Rect value)
		{
			return value;
		}

		public override void EvaluateAndApply(RectOptions options, Tween t, bool isRelative, DOGetter<Rect> getter, DOSetter<Rect> setter, float elapsed, Rect startValue, Rect changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			if (t.loopType == LoopType.Incremental)
			{
				int num2 = t.isComplete ? (t.completedLoops - 1) : t.completedLoops;
				startValue.x += changeValue.x * num2;
				startValue.y += changeValue.y * num2;
				startValue.width += changeValue.width * num2;
				startValue.height += changeValue.height * num2;
			}
			if (t.isSequenced && (t.sequenceParent.loopType == LoopType.Incremental))
			{
				int num3 = ((t.loopType == LoopType.Incremental) ? t.loops : 1) * (t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
				startValue.x += changeValue.x * num3;
				startValue.y += changeValue.y * num3;
				startValue.width += changeValue.width * num3;
				startValue.height += changeValue.height * num3;
			}
			float num = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			startValue.x += changeValue.x * num;
			startValue.y += changeValue.y * num;
			startValue.width += changeValue.width * num;
			startValue.height += changeValue.height * num;
			if (options.snapping)
			{
				startValue.x = (float) Math.Round((double) startValue.x);
				startValue.y = (float) Math.Round((double) startValue.y);
				startValue.width = (float) Math.Round((double) startValue.width);
				startValue.height = (float) Math.Round((double) startValue.height);
			}
			setter(startValue);
		}

		public override float GetSpeedBasedDuration(RectOptions options, float unitsXSecond, Rect changeValue)
		{
			float height = changeValue.height;
			float width = changeValue.width;
			return (((float) Math.Sqrt((double) ((width * width) + (height * height)))) / unitsXSecond);
		}

		public override void Reset(TweenerCore<Rect, Rect, RectOptions> t)
		{
		}

		public override void SetChangeValue(TweenerCore<Rect, Rect, RectOptions> t)
		{
			t.changeValue = new Rect(t.endValue.x - t.startValue.x, t.endValue.y - t.startValue.y, t.endValue.width - t.startValue.width, t.endValue.height - t.startValue.height);
		}

		public override void SetFrom(TweenerCore<Rect, Rect, RectOptions> t, bool isRelative)
		{
			Rect endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = endValue;
			if (isRelative)
			{
				t.startValue.x += t.endValue.x;
				t.startValue.y += t.endValue.y;
				t.startValue.width += t.endValue.width;
				t.startValue.height += t.endValue.height;
			}
			Rect startValue = t.startValue;
			if (t.plugOptions.snapping)
			{
				startValue.x = (float) Math.Round((double) startValue.x);
				startValue.y = (float) Math.Round((double) startValue.y);
				startValue.width = (float) Math.Round((double) startValue.width);
				startValue.height = (float) Math.Round((double) startValue.height);
			}
			t.setter(startValue);
		}

		public override void SetRelativeEndValue(TweenerCore<Rect, Rect, RectOptions> t)
		{
			t.endValue.x += t.startValue.x;
			t.endValue.y += t.startValue.y;
			t.endValue.width += t.startValue.width;
			t.endValue.height += t.startValue.height;
		}
	}

	public class StringPlugin : ABSTweenPlugin<string, string, StringOptions>
	{
		// Fields
		private static readonly StringBuilder _Buffer = new StringBuilder();
		private static readonly List<char> _OpenedTags = new List<char>();

		// Methods
		private StringBuilder Append(string value, int startIndex, int length, bool richTextEnabled)
		{
			if (!richTextEnabled)
			{
				_Buffer.Append(value, startIndex, length);
				return _Buffer;
			}
			_OpenedTags.Clear();
			bool flag = false;
			int num = value.Length;
			int num2 = 0;
			while (num2 < length)
			{
				char ch = value[num2];
				if (ch == '<')
				{
					bool flag2 = flag;
					char ch2 = value[num2 + 1];
					flag = (num2 >= (num - 1)) || (ch2 != '/');
					if (flag)
					{
						_OpenedTags.Add((ch2 == '#') ? 'c' : ch2);
					}
					else
					{
						_OpenedTags.RemoveAt(_OpenedTags.Count - 1);
					}
					Match match = Regex.Match(value.Substring(num2), "<.*?(>)");
					if (match.Success)
					{
						if (!flag && !flag2)
						{
							char[] chArray;
							char ch3 = value[num2 + 1];
							if (ch3 == 'c')
							{
								chArray = new char[] { '#', 'c' };
							}
							else
							{
								chArray = new char[] { ch3 };
							}
							for (int i = num2 - 1; i > -1; i--)
							{
								if (((value[i] == '<') && (value[i + 1] != '/')) && (Array.IndexOf<char>(chArray, value[i + 2]) != -1))
								{
									_Buffer.Insert(0, value.Substring(i, (value.IndexOf('>', i) + 1) - i));
									break;
								}
							}
						}
						_Buffer.Append(match.Value);
						int num3 = match.Groups[1].Index + 1;
						length += num3;
						startIndex += num3;
						num2 += num3 - 1;
					}
				}
				else if (num2 >= startIndex)
				{
					_Buffer.Append(ch);
				}
				num2++;
			}
			if ((_OpenedTags.Count > 0) && (num2 < (num - 1)))
			{
				while ((_OpenedTags.Count > 0) && (num2 < (num - 1)))
				{
					Match match2 = Regex.Match(value.Substring(num2), "(</).*?>");
					if (!match2.Success)
					{
						break;
					}
					if (match2.Value[2] == _OpenedTags[_OpenedTags.Count - 1])
					{
						_Buffer.Append(match2.Value);
						_OpenedTags.RemoveAt(_OpenedTags.Count - 1);
					}
					num2 += match2.Value.Length;
				}
			}
			return _Buffer;
		}

		public override string ConvertToStartValue(TweenerCore<string, string, StringOptions> t, string value)
		{
			return value;
		}

		public override void EvaluateAndApply(StringOptions options, Tween t, bool isRelative, DOGetter<string> getter, DOSetter<string> setter, float elapsed, string startValue, string changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			_Buffer.Remove(0, _Buffer.Length);
			if (isRelative && (t.loopType == LoopType.Incremental))
			{
				int num5 = t.isComplete ? (t.completedLoops - 1) : t.completedLoops;
				if (num5 > 0)
				{
					_Buffer.Append(startValue);
					for (int i = 0; i < num5; i++)
					{
						_Buffer.Append(changeValue);
					}
					startValue = _Buffer.ToString();
					_Buffer.Remove(0, _Buffer.Length);
				}
			}
			int num = options.richTextEnabled ? options.startValueStrippedLength : startValue.Length;
			int num2 = options.richTextEnabled ? options.changeValueStrippedLength : changeValue.Length;
			int length = (int) Math.Round((double) (num2 * EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod)));
			if (length > num2)
			{
				length = num2;
			}
			else if (length < 0)
			{
				length = 0;
			}
			if (isRelative)
			{
				_Buffer.Append(startValue);
				if (options.scrambleMode != ScrambleMode.None)
				{
					setter(this.Append(changeValue, 0, length, options.richTextEnabled).AppendScrambledChars((num2 - length), this.ScrambledCharsToUse(options)).ToString());
				}
				else
				{
					setter(this.Append(changeValue, 0, length, options.richTextEnabled).ToString());
				}
			}
			else if (options.scrambleMode != ScrambleMode.None)
			{
				setter(this.Append(changeValue, 0, length, options.richTextEnabled).AppendScrambledChars((num2 - length), this.ScrambledCharsToUse(options)).ToString());
			}
			else
			{
				int num4 = num;
				if ((num - num2) > 0)
				{
					float num7 = ((float) length) / ((float) num2);
					num4 -= (int) (num4 * num7);
				}
				else
				{
					num4 -= length;
				}
				this.Append(changeValue, 0, length, options.richTextEnabled);
				if ((length < num2) && (length < num))
				{
					this.Append(startValue, length, options.richTextEnabled ? (length + num4) : num4, options.richTextEnabled);
				}
				setter(_Buffer.ToString());
			}
		}

		public override float GetSpeedBasedDuration(StringOptions options, float unitsXSecond, string changeValue)
		{
			float num = (options.richTextEnabled ? ((float) options.changeValueStrippedLength) : ((float) changeValue.Length)) / unitsXSecond;
			if (num < 0f)
			{
				num = -num;
			}
			return num;
		}

		public override void Reset(TweenerCore<string, string, StringOptions> t)
		{
			t.startValue = t.endValue = (string) (t.changeValue = null);
		}

		private char[] ScrambledCharsToUse(StringOptions options)
		{
			switch (options.scrambleMode)
			{
			case ScrambleMode.Uppercase:
				return StringPluginExtensions.ScrambledCharsUppercase;

			case ScrambleMode.Lowercase:
				return StringPluginExtensions.ScrambledCharsLowercase;

			case ScrambleMode.Numerals:
				return StringPluginExtensions.ScrambledCharsNumerals;

			case ScrambleMode.Custom:
				return options.scrambledChars;
			}
			return StringPluginExtensions.ScrambledCharsAll;
		}

		public override void SetChangeValue(TweenerCore<string, string, StringOptions> t)
		{
			t.changeValue = t.endValue;
			t.plugOptions.startValueStrippedLength = Regex.Replace(t.startValue, "<[^>]*>", "").Length;
			t.plugOptions.changeValueStrippedLength = Regex.Replace(t.changeValue, "<[^>]*>", "").Length;
		}

		public override void SetFrom(TweenerCore<string, string, StringOptions> t, bool isRelative)
		{
			string endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = endValue;
			t.setter(t.startValue);
		}

		public override void SetRelativeEndValue(TweenerCore<string, string, StringOptions> t)
		{
		}
	}

	internal static class StringPluginExtensions
	{
		// Fields
		private static int _lastRndSeed;
		public static readonly char[] ScrambledCharsAll = new char[] { 
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 
			'Q', 'R', 'S', 'T', 'U', 'V', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 
			'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'x', 
			'y', 'z', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
		};
		public static readonly char[] ScrambledCharsLowercase = new char[] { 
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 
			'q', 'r', 's', 't', 'u', 'v', 'x', 'y', 'z'
		};
		public static readonly char[] ScrambledCharsNumerals = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
		public static readonly char[] ScrambledCharsUppercase = new char[] { 
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 
			'Q', 'R', 'S', 'T', 'U', 'V', 'X', 'Y', 'Z'
		};

		// Methods
		static StringPluginExtensions()
		{
			ScrambledCharsAll.ScrambleChars();
			ScrambledCharsUppercase.ScrambleChars();
			ScrambledCharsLowercase.ScrambleChars();
			ScrambledCharsNumerals.ScrambleChars();
		}

		internal static StringBuilder AppendScrambledChars(this StringBuilder buffer, int length, char[] chars)
		{
			if (length > 0)
			{
				int max = chars.Length;
				int index = _lastRndSeed;
				while (index == _lastRndSeed)
				{
					index = UnityEngine.Random.Range(0, max);
				}
				_lastRndSeed = index;
				for (int i = 0; i < length; i++)
				{
					if (index >= max)
					{
						index = 0;
					}
					buffer.Append(chars[index]);
					index++;
				}
			}
			return buffer;
		}

		internal static void ScrambleChars(this char[] chars)
		{
			int length = chars.Length;
			for (int i = 0; i < length; i++)
			{
				char ch = chars[i];
				int index = UnityEngine.Random.Range(i, length);
				chars[i] = chars[index];
				chars[index] = ch;
			}
		}
	}

	public class UintPlugin : ABSTweenPlugin<uint, uint, NoOptions>
	{
		// Methods
		public override uint ConvertToStartValue(TweenerCore<uint, uint, NoOptions> t, uint value)
		{
			return value;
		}

		public override void EvaluateAndApply(NoOptions options, Tween t, bool isRelative, DOGetter<uint> getter, DOSetter<uint> setter, float elapsed, uint startValue, uint changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			if (t.loopType == LoopType.Incremental)
			{
				startValue += (uint) (changeValue * (t.isComplete ? ((long) (t.completedLoops - 1)) : ((long) t.completedLoops)));
			}
			if (t.isSequenced && (t.sequenceParent.loopType == LoopType.Incremental))
			{
				startValue += (uint) ((changeValue * ((t.loopType == LoopType.Incremental) ? ((long) t.loops) : ((long) 1))) * (t.sequenceParent.isComplete ? ((long) (t.sequenceParent.completedLoops - 1)) : ((long) t.sequenceParent.completedLoops)));
			}
			setter((uint) Math.Round((double) (startValue + (changeValue * EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod)))));
		}

		public override float GetSpeedBasedDuration(NoOptions options, float unitsXSecond, uint changeValue)
		{
			float num = ((float) changeValue) / unitsXSecond;
			if (num < 0f)
			{
				num = -num;
			}
			return num;
		}

		public override void Reset(TweenerCore<uint, uint, NoOptions> t)
		{
		}

		public override void SetChangeValue(TweenerCore<uint, uint, NoOptions> t)
		{
			t.changeValue = t.endValue - t.startValue;
		}

		public override void SetFrom(TweenerCore<uint, uint, NoOptions> t, bool isRelative)
		{
			uint endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = isRelative ? (t.endValue + endValue) : endValue;
			t.setter(t.startValue);
		}

		public override void SetRelativeEndValue(TweenerCore<uint, uint, NoOptions> t)
		{
			t.endValue += t.startValue;
		}
	}
		
	public class UlongPlugin : ABSTweenPlugin<ulong, ulong, NoOptions>
	{
		// Methods
		public override ulong ConvertToStartValue(TweenerCore<ulong, ulong, NoOptions> t, ulong value)
		{
			return value;
		}

		public override void EvaluateAndApply(NoOptions options, Tween t, bool isRelative, DOGetter<ulong> getter, DOSetter<ulong> setter, float elapsed, ulong startValue, ulong changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			if (t.loopType == LoopType.Incremental)
			{
				startValue += changeValue * (t.isComplete ? ((ulong) (t.completedLoops - 1)) : ((ulong) t.completedLoops));
			}
			if (t.isSequenced && (t.sequenceParent.loopType == LoopType.Incremental))
			{
				startValue += (changeValue * ((t.loopType == LoopType.Incremental) ? ((ulong) t.loops) : ((ulong) 1))) * (t.sequenceParent.isComplete ? ((ulong) (t.sequenceParent.completedLoops - 1)) : ((ulong) t.sequenceParent.completedLoops));
			}
			setter(startValue + ((ulong) (changeValue * ((decimal) EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod)))));
		}

		public override float GetSpeedBasedDuration(NoOptions options, float unitsXSecond, ulong changeValue)
		{
			float num = ((float) changeValue) / unitsXSecond;
			if (num < 0f)
			{
				num = -num;
			}
			return num;
		}

		public override void Reset(TweenerCore<ulong, ulong, NoOptions> t)
		{
		}

		public override void SetChangeValue(TweenerCore<ulong, ulong, NoOptions> t)
		{
			t.changeValue = t.endValue - t.startValue;
		}

		public override void SetFrom(TweenerCore<ulong, ulong, NoOptions> t, bool isRelative)
		{
			ulong endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = isRelative ? (t.endValue + endValue) : endValue;
			t.setter(t.startValue);
		}

		public override void SetRelativeEndValue(TweenerCore<ulong, ulong, NoOptions> t)
		{
			t.endValue += t.startValue;
		}
	}
		

	public class Vector2Plugin : ABSTweenPlugin<Vector2, Vector2, VectorOptions>
	{
		// Methods
		public override Vector2 ConvertToStartValue(TweenerCore<Vector2, Vector2, VectorOptions> t, Vector2 value)
		{
			return value;
		}

		public override void EvaluateAndApply(VectorOptions options, Tween t, bool isRelative, DOGetter<Vector2> getter, DOSetter<Vector2> setter, float elapsed, Vector2 startValue, Vector2 changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			if (t.loopType == LoopType.Incremental)
			{
				startValue += (Vector2) (changeValue * (t.isComplete ? ((float) (t.completedLoops - 1)) : ((float) t.completedLoops)));
			}
			if (t.isSequenced && (t.sequenceParent.loopType == LoopType.Incremental))
			{
				startValue += (Vector2) ((changeValue * ((t.loopType == LoopType.Incremental) ? ((float) t.loops) : ((float) 1))) * (t.sequenceParent.isComplete ? ((float) (t.sequenceParent.completedLoops - 1)) : ((float) t.sequenceParent.completedLoops)));
			}
			float num = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			AxisConstraint axisConstraint = options.axisConstraint;
			if (axisConstraint == AxisConstraint.X)
			{
				Vector2 pNewValue = getter();
				pNewValue.x = startValue.x + (changeValue.x * num);
				if (options.snapping)
				{
					pNewValue.x = (float) Math.Round((double) pNewValue.x);
				}
				setter(pNewValue);
			}
			else if (axisConstraint == AxisConstraint.Y)
			{
				Vector2 vector2 = getter();
				vector2.y = startValue.y + (changeValue.y * num);
				if (options.snapping)
				{
					vector2.y = (float) Math.Round((double) vector2.y);
				}
				setter(vector2);
			}
			else
			{
				startValue.x += changeValue.x * num;
				startValue.y += changeValue.y * num;
				if (options.snapping)
				{
					startValue.x = (float) Math.Round((double) startValue.x);
					startValue.y = (float) Math.Round((double) startValue.y);
				}
				setter(startValue);
			}
		}

		public override float GetSpeedBasedDuration(VectorOptions options, float unitsXSecond, Vector2 changeValue)
		{
			return (changeValue.magnitude / unitsXSecond);
		}

		public override void Reset(TweenerCore<Vector2, Vector2, VectorOptions> t)
		{
		}

		public override void SetChangeValue(TweenerCore<Vector2, Vector2, VectorOptions> t)
		{
			AxisConstraint axisConstraint = t.plugOptions.axisConstraint;
			if (axisConstraint == AxisConstraint.X)
			{
				t.changeValue = new Vector2(t.endValue.x - t.startValue.x, 0f);
			}
			else if (axisConstraint == AxisConstraint.Y)
			{
				t.changeValue = new Vector2(0f, t.endValue.y - t.startValue.y);
			}
			else
			{
				t.changeValue = t.endValue - t.startValue;
			}
		}

		public override void SetFrom(TweenerCore<Vector2, Vector2, VectorOptions> t, bool isRelative)
		{
			Vector2 endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = isRelative ? (t.endValue + endValue) : endValue;
			Vector2 pNewValue = t.endValue;
			AxisConstraint axisConstraint = t.plugOptions.axisConstraint;
			if (axisConstraint == AxisConstraint.X)
			{
				pNewValue.x = t.startValue.x;
			}
			else if (axisConstraint == AxisConstraint.Y)
			{
				pNewValue.y = t.startValue.y;
			}
			else
			{
				pNewValue = t.startValue;
			}
			if (t.plugOptions.snapping)
			{
				pNewValue.x = (float) Math.Round((double) pNewValue.x);
				pNewValue.y = (float) Math.Round((double) pNewValue.y);
			}
			t.setter(pNewValue);
		}

		public override void SetRelativeEndValue(TweenerCore<Vector2, Vector2, VectorOptions> t)
		{
			TweenerCore<Vector2, Vector2, VectorOptions> core = t;
			core.endValue += t.startValue;
		}
	}

	public class Vector3ArrayPlugin : ABSTweenPlugin<Vector3, Vector3[], Vector3ArrayOptions>
	{
		// Methods
		public override Vector3[] ConvertToStartValue(TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t, Vector3 value)
		{
			int length = t.endValue.Length;
			Vector3[] vectorArray = new Vector3[length];
			for (int i = 0; i < length; i++)
			{
				if (i == 0)
				{
					vectorArray[i] = value;
				}
				else
				{
					vectorArray[i] = t.endValue[i - 1];
				}
			}
			return vectorArray;
		}

		public override void EvaluateAndApply(Vector3ArrayOptions options, Tween t, bool isRelative, DOGetter<Vector3> getter, DOSetter<Vector3> setter, float elapsed, Vector3[] startValue, Vector3[] changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			Vector3 vector2;
			Vector3 zero = Vector3.zero;
			if (t.loopType == LoopType.Incremental)
			{
				int num7 = t.isComplete ? (t.completedLoops - 1) : t.completedLoops;
				if (num7 > 0)
				{
					int num8 = startValue.Length - 1;
					zero = ((startValue[num8] + changeValue[num8]) - startValue[0]) * num7;
				}
			}
			if (t.isSequenced && (t.sequenceParent.loopType == LoopType.Incremental))
			{
				int num9 = ((t.loopType == LoopType.Incremental) ? t.loops : 1) * (t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
				if (num9 > 0)
				{
					int num10 = startValue.Length - 1;
					zero += ((startValue[num10] + changeValue[num10]) - startValue[0]) * num9;
				}
			}
			int index = 0;
			float time = 0f;
			float num3 = 0f;
			int length = options.durations.Length;
			float num5 = 0f;
			for (int i = 0; i < length; i++)
			{
				num3 = options.durations[i];
				num5 += num3;
				if (elapsed > num5)
				{
					time += num3;
				}
				else
				{
					index = i;
					time = elapsed - time;
					break;
				}
			}
			float num6 = EaseManager.Evaluate(t.easeType, t.customEase, time, num3, t.easeOvershootOrAmplitude, t.easePeriod);
			AxisConstraint axisConstraint = options.axisConstraint;
			if (axisConstraint == AxisConstraint.X)
			{
				vector2 = getter();
				vector2.x = (startValue[index].x + zero.x) + (changeValue[index].x * num6);
				if (options.snapping)
				{
					vector2.x = (float) Math.Round((double) vector2.x);
				}
				setter(vector2);
			}
			else if (axisConstraint == AxisConstraint.Y)
			{
				vector2 = getter();
				vector2.y = (startValue[index].y + zero.y) + (changeValue[index].y * num6);
				if (options.snapping)
				{
					vector2.y = (float) Math.Round((double) vector2.y);
				}
				setter(vector2);
			}
			else if (axisConstraint == AxisConstraint.Z)
			{
				vector2 = getter();
				vector2.z = (startValue[index].z + zero.z) + (changeValue[index].z * num6);
				if (options.snapping)
				{
					vector2.z = (float) Math.Round((double) vector2.z);
				}
				setter(vector2);
			}
			else
			{
				vector2.x = (startValue[index].x + zero.x) + (changeValue[index].x * num6);
				vector2.y = (startValue[index].y + zero.y) + (changeValue[index].y * num6);
				vector2.z = (startValue[index].z + zero.z) + (changeValue[index].z * num6);
				if (options.snapping)
				{
					vector2.x = (float) Math.Round((double) vector2.x);
					vector2.y = (float) Math.Round((double) vector2.y);
					vector2.z = (float) Math.Round((double) vector2.z);
				}
				setter(vector2);
			}
		}

		public override float GetSpeedBasedDuration(Vector3ArrayOptions options, float unitsXSecond, Vector3[] changeValue)
		{
			float num = 0f;
			int length = changeValue.Length;
			for (int i = 0; i < length; i++)
			{
				float num4 = changeValue[i].magnitude / options.durations[i];
				options.durations[i] = num4;
				num += num4;
			}
			return num;
		}

		public override void Reset(TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t)
		{
			t.startValue = t.endValue = (Vector3[]) (t.changeValue = null);
		}

		public override void SetChangeValue(TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t)
		{
			int length = t.endValue.Length;
			t.changeValue = new Vector3[length];
			for (int i = 0; i < length; i++)
			{
				t.changeValue[i] = t.endValue[i] - t.startValue[i];
			}
		}

		public override void SetFrom(TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t, bool isRelative)
		{
		}

		public override void SetRelativeEndValue(TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t)
		{
			int length = t.endValue.Length;
			for (int i = 0; i < length; i++)
			{
				if (i > 0)
				{
					t.startValue[i] = t.endValue[i - 1];
				}
				t.endValue[i] = t.startValue[i] + t.endValue[i];
			}
		}
	}

	public class Vector3Plugin : ABSTweenPlugin<Vector3, Vector3, VectorOptions>
	{
		// Methods
		public override Vector3 ConvertToStartValue(TweenerCore<Vector3, Vector3, VectorOptions> t, Vector3 value)
		{
			return value;
		}

		public override void EvaluateAndApply(VectorOptions options, Tween t, bool isRelative, DOGetter<Vector3> getter, DOSetter<Vector3> setter, float elapsed, Vector3 startValue, Vector3 changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			if (t.loopType == LoopType.Incremental)
			{
				startValue += (Vector3) (changeValue * (t.isComplete ? ((float) (t.completedLoops - 1)) : ((float) t.completedLoops)));
			}
			if (t.isSequenced && (t.sequenceParent.loopType == LoopType.Incremental))
			{
				startValue += (Vector3) ((changeValue * ((t.loopType == LoopType.Incremental) ? ((float) t.loops) : ((float) 1))) * (t.sequenceParent.isComplete ? ((float) (t.sequenceParent.completedLoops - 1)) : ((float) t.sequenceParent.completedLoops)));
			}
			float num = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			AxisConstraint axisConstraint = options.axisConstraint;
			if (axisConstraint == AxisConstraint.X)
			{
				Vector3 pNewValue = getter();
				pNewValue.x = startValue.x + (changeValue.x * num);
				if (options.snapping)
				{
					pNewValue.x = (float) Math.Round((double) pNewValue.x);
				}
				setter(pNewValue);
			}
			else if (axisConstraint == AxisConstraint.Y)
			{
				Vector3 vector2 = getter();
				vector2.y = startValue.y + (changeValue.y * num);
				if (options.snapping)
				{
					vector2.y = (float) Math.Round((double) vector2.y);
				}
				setter(vector2);
			}
			else if (axisConstraint == AxisConstraint.Z)
			{
				Vector3 vector3 = getter();
				vector3.z = startValue.z + (changeValue.z * num);
				if (options.snapping)
				{
					vector3.z = (float) Math.Round((double) vector3.z);
				}
				setter(vector3);
			}
			else
			{
				startValue.x += changeValue.x * num;
				startValue.y += changeValue.y * num;
				startValue.z += changeValue.z * num;
				if (options.snapping)
				{
					startValue.x = (float) Math.Round((double) startValue.x);
					startValue.y = (float) Math.Round((double) startValue.y);
					startValue.z = (float) Math.Round((double) startValue.z);
				}

				if (startValue.x == float.NaN || startValue.y == float.NaN || startValue.z == float.NaN)
				{
					Debug.LogWarningFormat("EvaluateAndApply is not valid. startValue is {0}. changeValue is {1}. axisConstraint is {2}", startValue, changeValue, axisConstraint);
				}

				setter(startValue);
			}
		}

		public override float GetSpeedBasedDuration(VectorOptions options, float unitsXSecond, Vector3 changeValue)
		{
			return (changeValue.magnitude / unitsXSecond);
		}

		public override void Reset(TweenerCore<Vector3, Vector3, VectorOptions> t)
		{
		}

		public override void SetChangeValue(TweenerCore<Vector3, Vector3, VectorOptions> t)
		{
			AxisConstraint axisConstraint = t.plugOptions.axisConstraint;
			if (axisConstraint == AxisConstraint.X)
			{
				t.changeValue = new Vector3(t.endValue.x - t.startValue.x, 0f, 0f);
			}
			else if (axisConstraint == AxisConstraint.Y)
			{
				t.changeValue = new Vector3(0f, t.endValue.y - t.startValue.y, 0f);
			}
			else if (axisConstraint == AxisConstraint.Z)
			{
				t.changeValue = new Vector3(0f, 0f, t.endValue.z - t.startValue.z);
			}
			else
			{
				t.changeValue = t.endValue - t.startValue;
			}
		}

		public override void SetFrom(TweenerCore<Vector3, Vector3, VectorOptions> t, bool isRelative)
		{
			Vector3 endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = isRelative ? (t.endValue + endValue) : endValue;
			Vector3 pNewValue = t.endValue;
			AxisConstraint axisConstraint = t.plugOptions.axisConstraint;
			if (axisConstraint == AxisConstraint.X)
			{
				pNewValue.x = t.startValue.x;
			}
			else if (axisConstraint == AxisConstraint.Y)
			{
				pNewValue.y = t.startValue.y;
			}
			else if (axisConstraint == AxisConstraint.Z)
			{
				pNewValue.z = t.startValue.z;
			}
			else
			{
				pNewValue = t.startValue;
			}
			if (t.plugOptions.snapping)
			{
				pNewValue.x = (float) Math.Round((double) pNewValue.x);
				pNewValue.y = (float) Math.Round((double) pNewValue.y);
				pNewValue.z = (float) Math.Round((double) pNewValue.z);
			}
			t.setter(pNewValue);
		}

		public override void SetRelativeEndValue(TweenerCore<Vector3, Vector3, VectorOptions> t)
		{
			TweenerCore<Vector3, Vector3, VectorOptions> core = t;
			core.endValue += t.startValue;
		}
	}

	public class Vector4Plugin : ABSTweenPlugin<Vector4, Vector4, VectorOptions>
	{
		// Methods
		public override Vector4 ConvertToStartValue(TweenerCore<Vector4, Vector4, VectorOptions> t, Vector4 value)
		{
			return value;
		}

		public override void EvaluateAndApply(VectorOptions options, Tween t, bool isRelative, DOGetter<Vector4> getter, DOSetter<Vector4> setter, float elapsed, Vector4 startValue, Vector4 changeValue, float duration, bool usingInversePosition, UpdateNotice updateNotice)
		{
			if (t.loopType == LoopType.Incremental)
			{
				startValue += (Vector4) (changeValue * (t.isComplete ? ((float) (t.completedLoops - 1)) : ((float) t.completedLoops)));
			}
			if (t.isSequenced && (t.sequenceParent.loopType == LoopType.Incremental))
			{
				startValue += (Vector4) ((changeValue * ((t.loopType == LoopType.Incremental) ? ((float) t.loops) : ((float) 1))) * (t.sequenceParent.isComplete ? ((float) (t.sequenceParent.completedLoops - 1)) : ((float) t.sequenceParent.completedLoops)));
			}
			float num = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			switch (options.axisConstraint)
			{
			case AxisConstraint.Z:
				{
					Vector4 pNewValue = getter();
					pNewValue.z = startValue.z + (changeValue.z * num);
					if (options.snapping)
					{
						pNewValue.z = (float) Math.Round((double) pNewValue.z);
					}
					setter(pNewValue);
					return;
				}
			case AxisConstraint.W:
				{
					Vector4 vector4 = getter();
					vector4.w = startValue.w + (changeValue.w * num);
					if (options.snapping)
					{
						vector4.w = (float) Math.Round((double) vector4.w);
					}
					setter(vector4);
					return;
				}
			case AxisConstraint.X:
				{
					Vector4 vector = getter();
					vector.x = startValue.x + (changeValue.x * num);
					if (options.snapping)
					{
						vector.x = (float) Math.Round((double) vector.x);
					}
					setter(vector);
					return;
				}
			case AxisConstraint.Y:
				{
					Vector4 vector2 = getter();
					vector2.y = startValue.y + (changeValue.y * num);
					if (options.snapping)
					{
						vector2.y = (float) Math.Round((double) vector2.y);
					}
					setter(vector2);
					return;
				}
			}
			startValue.x += changeValue.x * num;
			startValue.y += changeValue.y * num;
			startValue.z += changeValue.z * num;
			startValue.w += changeValue.w * num;
			if (options.snapping)
			{
				startValue.x = (float) Math.Round((double) startValue.x);
				startValue.y = (float) Math.Round((double) startValue.y);
				startValue.z = (float) Math.Round((double) startValue.z);
				startValue.w = (float) Math.Round((double) startValue.w);
			}
			setter(startValue);
		}

		public override float GetSpeedBasedDuration(VectorOptions options, float unitsXSecond, Vector4 changeValue)
		{
			return (changeValue.magnitude / unitsXSecond);
		}

		public override void Reset(TweenerCore<Vector4, Vector4, VectorOptions> t)
		{
		}

		public override void SetChangeValue(TweenerCore<Vector4, Vector4, VectorOptions> t)
		{
			switch (t.plugOptions.axisConstraint)
			{
			case AxisConstraint.Z:
				t.changeValue = new Vector4(0f, 0f, t.endValue.z - t.startValue.z, 0f);
				return;

			case AxisConstraint.W:
				t.changeValue = new Vector4(0f, 0f, 0f, t.endValue.w - t.startValue.w);
				return;

			case AxisConstraint.X:
				t.changeValue = new Vector4(t.endValue.x - t.startValue.x, 0f, 0f, 0f);
				return;

			case AxisConstraint.Y:
				t.changeValue = new Vector4(0f, t.endValue.y - t.startValue.y, 0f, 0f);
				return;
			}
			t.changeValue = t.endValue - t.startValue;
		}

		public override void SetFrom(TweenerCore<Vector4, Vector4, VectorOptions> t, bool isRelative)
		{
			Vector4 endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = isRelative ? (t.endValue + endValue) : endValue;
			Vector4 pNewValue = t.endValue;
			switch (t.plugOptions.axisConstraint)
			{
			case AxisConstraint.Z:
				pNewValue.z = t.startValue.z;
				break;

			case AxisConstraint.W:
				pNewValue.w = t.startValue.w;
				break;

			case AxisConstraint.X:
				pNewValue.x = t.startValue.x;
				break;

			case AxisConstraint.Y:
				pNewValue.y = t.startValue.y;
				break;

			default:
				pNewValue = t.startValue;
				break;
			}
			if (t.plugOptions.snapping)
			{
				pNewValue.x = (float) Math.Round((double) pNewValue.x);
				pNewValue.y = (float) Math.Round((double) pNewValue.y);
				pNewValue.z = (float) Math.Round((double) pNewValue.z);
				pNewValue.w = (float) Math.Round((double) pNewValue.w);
			}
			t.setter(pNewValue);
		}

		public override void SetRelativeEndValue(TweenerCore<Vector4, Vector4, VectorOptions> t)
		{
			TweenerCore<Vector4, Vector4, VectorOptions> core = t;
			core.endValue += t.startValue;
		}
	}
}