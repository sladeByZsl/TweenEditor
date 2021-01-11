using System;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;
using DG.Tweening.Core.Enums;
using System.Runtime.InteropServices;

namespace DG.Tweening
{
	public enum AutoPlay
	{
		None,
		AutoPlaySequences,
		AutoPlayTweeners,
		All
	}
	[Flags]
	public enum AxisConstraint
	{
		None = 0,
		W = 0x10,
		X = 2,
		Y = 4,
		Z = 8
	}
			
	public enum Ease
	{
		Unset,
		Linear,
		InSine,
		OutSine,
		InOutSine,
		InQuad,
		OutQuad,
		InOutQuad,
		InCubic,
		OutCubic,
		InOutCubic,
		InQuart,
		OutQuart,
		InOutQuart,
		InQuint,
		OutQuint,
		InOutQuint,
		InExpo,
		OutExpo,
		InOutExpo,
		InCirc,
		OutCirc,
		InOutCirc,
		InElastic,
		OutElastic,
		InOutElastic,
		InBack,
		OutBack,
		InOutBack,
		InBounce,
		OutBounce,
		InOutBounce,
		Flash,
		InFlash,
		OutFlash,
		InOutFlash,
		INTERNAL_Zero,
		INTERNAL_Custom
	}
		
	public enum LogBehaviour
	{
		Default,
		Verbose,
		ErrorsOnly
	}

	public enum LoopType
	{
		Restart,
		Yoyo,
		Incremental
	}
	public enum PathMode
	{
		Ignore,
		Full3D,
		TopDown2D,
		Sidescroller2D
	}

	public enum PathType
	{
		Linear,
		CatmullRom
	}
		
	public enum RotateMode
	{
		Fast,
		FastBeyond360,
		WorldAxisAdd,
		LocalAxisAdd
	}
		
	public enum ScrambleMode
	{
		None,
		All,
		Uppercase,
		Lowercase,
		Numerals,
		Custom
	}
		
	public enum TweenType
	{
		Tweener,
		Sequence,
		Callback
	}
		
	public enum UpdateType
	{
		Normal,
		Late,
		Fixed
	}
		

	[StructLayout(LayoutKind.Sequential)]
	public struct Color2
	{
		public Color ca;
		public Color cb;
		public Color2(Color ca, Color cb)
		{
			this.ca = ca;
			this.cb = cb;
		}

		public static Color2 operator +(Color2 c1, Color2 c2)
		{
			return new Color2(c1.ca + c2.ca, c1.cb + c2.cb);
		}

		public static Color2 operator -(Color2 c1, Color2 c2)
		{
			return new Color2(c1.ca - c2.ca, c1.cb - c2.cb);
		}

		public static Color2 operator *(Color2 c1, float f)
		{
			return new Color2((Color) (c1.ca * f), (Color) (c1.cb * f));
		}
	}
		
	public class DOTween
	{
		// Fields
		private static LogBehaviour _logBehaviour= LogBehaviour.ErrorsOnly;
		public static bool defaultAutoKill = true;
		public static AutoPlay defaultAutoPlay = AutoPlay.All;
		public static float defaultEaseOvershootOrAmplitude= 1.70158f;
		public static float defaultEasePeriod= 0f;
		public static Ease defaultEaseType= Ease.OutQuad;
		public static LoopType defaultLoopType = LoopType.Restart;
		public static bool defaultRecyclable;
		public static bool defaultTimeScaleIndependent= false;
		public static UpdateType defaultUpdateType = UpdateType.Normal;
		public static bool drawGizmos= true;
		public static readonly List<TweenCallback> GizmosDelegates = new List<TweenCallback>();
		public static bool initialized;
		public static DOTweenComponent instance;
		public static bool isDebugBuild;
		public static bool isQuitting;
		public static bool isUnityEditor= Application.isEditor;
		public static int maxActiveSequencesReached;
		public static int maxActiveTweenersReached;
		public static bool showUnityEditorReport = false;
		public static float timeScale= 1f;
		public static bool useSafeMode= true;
		public static bool useSmoothDeltaTime;
		public static readonly string Version="1.1.135";

			
		public DOTween()
		{
		}
		private static TweenerCore<T1, T2, TPlugOptions> ApplyTo<T1, T2, TPlugOptions>(DOGetter<T1> getter, DOSetter<T1> setter, T2 endValue, float duration, ABSTweenPlugin<T1, T2, TPlugOptions> plugin = null) where TPlugOptions: struct
		{
			InitCheck();
			TweenerCore<T1, T2, TPlugOptions> tweener = TweenManager.GetTweener<T1, T2, TPlugOptions>();
			if (!Tweener.Setup<T1, T2, TPlugOptions>(tweener, getter, setter, endValue, duration, plugin))
			{
				TweenManager.Despawn(tweener, true);
				return null;
			}
			return tweener;
		}

		private static void AutoInit()
		{
			bool? recycleAllByDefault = null;
			Init(Resources.Load("DoTweenSettings") as DOTweenSettings, recycleAllByDefault, null, null);
		}

		public static void Clear(bool destroy = false)
		{
			TweenManager.PurgeAll();
			PluginsManager.PurgeAll();
			if (destroy)
			{
				initialized = false;
				useSafeMode = false;
				showUnityEditorReport = false;
				drawGizmos = true;
				timeScale = 1f;
				useSmoothDeltaTime = false;
				logBehaviour = LogBehaviour.ErrorsOnly;
				defaultEaseType = Ease.OutQuad;
				defaultEaseOvershootOrAmplitude = 1.70158f;
				defaultEasePeriod = 0f;
				defaultUpdateType = UpdateType.Normal;
				defaultTimeScaleIndependent = false;
				defaultAutoPlay = AutoPlay.All;
				defaultLoopType = LoopType.Restart;
				defaultAutoKill = true;
				defaultRecyclable = false;
				maxActiveTweenersReached = maxActiveSequencesReached = 0;
				DOTweenComponent.DestroyInstance();
			}
		}

		public static void ClearCachedTweens()
		{
			TweenManager.PurgePools();
		}

		public static int Complete(object targetOrId, bool withCallbacks = false)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return TweenManager.FilteredOperation(OperationType.Complete, FilterType.TargetOrId, targetOrId, false, withCallbacks ? ((float) 1) : ((float) 0), null, null);
		}

		public static int CompleteAll(bool withCallbacks = false)
		{
			return TweenManager.FilteredOperation(OperationType.Complete, FilterType.All, null, false, withCallbacks ? ((float) 1) : ((float) 0), null, null);
		}

		internal static int CompleteAndReturnKilledTot()
		{
			return TweenManager.FilteredOperation(OperationType.Complete, FilterType.All, null, true, 0f, null, null);
		}

		internal static int CompleteAndReturnKilledTot(object targetOrId)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return TweenManager.FilteredOperation(OperationType.Complete, FilterType.TargetOrId, targetOrId, true, 0f, null, null);
		}

		internal static int CompleteAndReturnKilledTotExceptFor(params object[] excludeTargetsOrIds)
		{
			return TweenManager.FilteredOperation(OperationType.Complete, FilterType.AllExceptTargetsOrIds, null, true, 0f, null, excludeTargetsOrIds);
		}

		public static int Flip(object targetOrId)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return TweenManager.FilteredOperation(OperationType.Flip, FilterType.TargetOrId, targetOrId, false, 0f, null, null);
		}

		public static int FlipAll()
		{
			return TweenManager.FilteredOperation(OperationType.Flip, FilterType.All, null, false, 0f, null, null);
		}

		public static int Goto(object targetOrId, float to, bool andPlay=false)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return TweenManager.FilteredOperation(OperationType.Goto, FilterType.TargetOrId, targetOrId, andPlay, to, null, null);
		}

		public static int GotoAll(float to, bool andPlay=false)
		{
			return TweenManager.FilteredOperation(OperationType.Goto, FilterType.All, null, andPlay, to, null, null);
		}

		public static IDOTweenInit Init( bool recycleAllByDefault = false, bool useSafeMode=false, LogBehaviour logBehaviour= LogBehaviour.Default)
		{
			if (initialized)
			{
				return instance;
			}
			if (Application.isPlaying && !isQuitting)
			{
				return Init(Resources.Load("DoTweenSettings") as DOTweenSettings, recycleAllByDefault, useSafeMode, logBehaviour);
			}
			return null;
		}

		private static IDOTweenInit Init(DOTweenSettings settings, bool? recycleAllByDefault, bool? useSafeMode, LogBehaviour? logBehaviour)
		{
			initialized = true;
			if (recycleAllByDefault.HasValue)
			{
				defaultRecyclable = recycleAllByDefault.Value;
			}
			if (useSafeMode.HasValue)
			{
				DOTween.useSafeMode = useSafeMode.Value;
			}
			if (logBehaviour.HasValue)
			{
				DOTween.logBehaviour = logBehaviour.Value;
			}
			DOTweenComponent.Create();
			if (settings != null)
			{
				if (!useSafeMode.HasValue)
				{
					DOTween.useSafeMode = settings.useSafeMode;
				}
				if (!logBehaviour.HasValue)
				{
					DOTween.logBehaviour = settings.logBehaviour;
				}
				if (!recycleAllByDefault.HasValue)
				{
					defaultRecyclable = settings.defaultRecyclable;
				}
				timeScale = settings.timeScale;
				useSmoothDeltaTime = settings.useSmoothDeltaTime;
				defaultRecyclable = !recycleAllByDefault.HasValue ? settings.defaultRecyclable : recycleAllByDefault.Value;
				showUnityEditorReport = settings.showUnityEditorReport;
				drawGizmos = settings.drawGizmos;
				defaultAutoPlay = settings.defaultAutoPlay;
				defaultUpdateType = settings.defaultUpdateType;
				defaultTimeScaleIndependent = settings.defaultTimeScaleIndependent;
				defaultEaseType = settings.defaultEaseType;
				defaultEaseOvershootOrAmplitude = settings.defaultEaseOvershootOrAmplitude;
				defaultEasePeriod = settings.defaultEasePeriod;
				defaultAutoKill = settings.defaultAutoKill;
				defaultLoopType = settings.defaultLoopType;
			}
			if (Debugger.logPriority >= 2)
			{
				Debugger.Log(string.Concat(new object[] { "DOTween initialization (useSafeMode: ", DOTween.useSafeMode.ToString(), ", recycling: ", defaultRecyclable ? "ON" : "OFF", ", logBehaviour: ", DOTween.logBehaviour, ")" }));
			}
			return instance;
		}

		private static void InitCheck()
		{
			if ((!initialized && Application.isPlaying) && !isQuitting)
			{
				AutoInit();
			}
		}

		public static bool IsTweening(object targetOrId)
		{
			return (TweenManager.FilteredOperation(OperationType.IsTweening, FilterType.TargetOrId, targetOrId, false, 0f, null, null) > 0);
		}

		public static int Kill(object targetOrId, bool complete=false)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return ((complete ? CompleteAndReturnKilledTot(targetOrId) : 0) + TweenManager.FilteredOperation(OperationType.Despawn, FilterType.TargetOrId, targetOrId, false, 0f, null, null));
		}

		public static int KillAll(bool complete=false)
		{
			return ((complete ? CompleteAndReturnKilledTot() : 0) + TweenManager.DespawnAll());
		}

		public static int KillAll(bool complete, params object[] idsOrTargetsToExclude)
		{
			if (idsOrTargetsToExclude == null)
			{
				return ((complete ? CompleteAndReturnKilledTot() : 0) + TweenManager.DespawnAll());
			}
			return ((complete ? CompleteAndReturnKilledTotExceptFor(idsOrTargetsToExclude) : 0) + TweenManager.FilteredOperation(OperationType.Despawn, FilterType.AllExceptTargetsOrIds, null, false, 0f, null, idsOrTargetsToExclude));
		}

		public static int Pause(object targetOrId)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return TweenManager.FilteredOperation(OperationType.Pause, FilterType.TargetOrId, targetOrId, false, 0f, null, null);
		}

		public static int PauseAll()
		{
			return TweenManager.FilteredOperation(OperationType.Pause, FilterType.All, null, false, 0f, null, null);
		}

		public static List<Tween> PausedTweens()
		{
			return TweenManager.GetActiveTweens(false);
		}

		public static int Play(object targetOrId)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return TweenManager.FilteredOperation(OperationType.Play, FilterType.TargetOrId, targetOrId, false, 0f, null, null);
		}

		public static int Play(object target, object id)
		{
			if ((target != null) && (id != null))
			{
				return TweenManager.FilteredOperation(OperationType.Play, FilterType.TargetAndId, id, false, 0f, target, null);
			}
			return 0;
		}

		public static int PlayAll()
		{
			return TweenManager.FilteredOperation(OperationType.Play, FilterType.All, null, false, 0f, null, null);
		}

		public static int PlayBackwards(object targetOrId)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return TweenManager.FilteredOperation(OperationType.PlayBackwards, FilterType.TargetOrId, targetOrId, false, 0f, null, null);
		}

		public static int PlayBackwardsAll()
		{
			return TweenManager.FilteredOperation(OperationType.PlayBackwards, FilterType.All, null, false, 0f, null, null);
		}

		public static int PlayForward(object targetOrId)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return TweenManager.FilteredOperation(OperationType.PlayForward, FilterType.TargetOrId, targetOrId, false, 0f, null, null);
		}

		public static int PlayForwardAll()
		{
			return TweenManager.FilteredOperation(OperationType.PlayForward, FilterType.All, null, false, 0f, null, null);
		}

		public static List<Tween> PlayingTweens()
		{
			return TweenManager.GetActiveTweens(true);
		}

		public static TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> Punch(DOGetter<Vector3> getter, DOSetter<Vector3> setter, Vector3 direction, float duration, int vibrato=10, float elasticity=1f)
		{
			if (elasticity > 1f)
			{
				elasticity = 1f;
			}
			else if (elasticity < 0f)
			{
				elasticity = 0f;
			}
			float magnitude = direction.magnitude;
			int num2 = (int) (vibrato * duration);
			if (num2 < 2)
			{
				num2 = 2;
			}
			float num3 = magnitude / ((float) num2);
			float[] durations = new float[num2];
			float num4 = 0f;
			for (int i = 0; i < num2; i++)
			{
				float num7 = ((float) (i + 1)) / ((float) num2);
				float num8 = duration * num7;
				num4 += num8;
				durations[i] = num8;
			}
			float num5 = duration / num4;
			for (int j = 0; j < num2; j++)
			{
				durations[j] *= num5;
			}
			Vector3[] endValues = new Vector3[num2];
			for (int k = 0; k < num2; k++)
			{
				if (k < (num2 - 1))
				{
					if (k == 0)
					{
						endValues[k] = direction;
					}
					else if ((k % 2) != 0)
					{
						endValues[k] = -Vector3.ClampMagnitude(direction, magnitude * elasticity);
					}
					else
					{
						endValues[k] = Vector3.ClampMagnitude(direction, magnitude);
					}
					magnitude -= num3;
				}
				else
				{
					endValues[k] = Vector3.zero;
				}
			}
			return ToArray(getter, setter, endValues, durations).NoFrom<Vector3, Vector3[], Vector3ArrayOptions>().SetSpecialStartupMode<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(SpecialStartupMode.SetPunch);
		}

		public static int Restart(object targetOrId, bool includeDelay)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return TweenManager.FilteredOperation(OperationType.Restart, FilterType.TargetOrId, targetOrId, includeDelay, 0f, null, null);
		}

		public static int Restart(object target, object id, bool includeDelay)
		{
			if ((target != null) && (id != null))
			{
				return TweenManager.FilteredOperation(OperationType.Restart, FilterType.TargetAndId, id, includeDelay, 0f, target, null);
			}
			return 0;
		}

		public static int RestartAll( bool includeDelay=true)
		{
			return TweenManager.FilteredOperation(OperationType.Restart, FilterType.All, null, includeDelay, 0f, null, null);
		}

		public static int Rewind(object targetOrId, bool includeDelay=true)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return TweenManager.FilteredOperation(OperationType.Rewind, FilterType.TargetOrId, targetOrId, includeDelay, 0f, null, null);
		}

		public static int RewindAll(bool includeDelay=true)
		{
			return TweenManager.FilteredOperation(OperationType.Rewind, FilterType.All, null, includeDelay, 0f, null, null);
		}

		public static Sequence Sequence()
		{
			InitCheck();
			Sequence sequence = TweenManager.GetSequence();
			DG.Tweening.Sequence.Setup(sequence);
			return sequence;
		}

		public static void SetTweensCapacity(int tweenersCapacity, int sequencesCapacity)
		{
			TweenManager.SetCapacities(tweenersCapacity, sequencesCapacity);
		}

		public static TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> Shake(DOGetter<Vector3> getter, DOSetter<Vector3> setter, float duration, Vector3 strength, int vibrato=10, float randomness=90f)
		{
			return Shake(getter, setter, duration, strength, vibrato, randomness, false, true);
		}

		public static TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> Shake(DOGetter<Vector3> getter, DOSetter<Vector3> setter, float duration, float strength=3f, int vibrato=10, float randomness=90f, bool ignoreZAxis=true)
		{
			return Shake(getter, setter, duration, new Vector3(strength, strength, strength), vibrato, randomness, ignoreZAxis, false);
		}

		private static TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> Shake(DOGetter<Vector3> getter, DOSetter<Vector3> setter, float duration, Vector3 strength, int vibrato, float randomness, bool ignoreZAxis, bool vectorBased)
		{
			float magnitude = vectorBased ? strength.magnitude : strength.x;
			int num2 = (int) (vibrato * duration);
			if (num2 < 2)
			{
				num2 = 2;
			}
			float num3 = magnitude / ((float) num2);
			float[] durations = new float[num2];
			float num4 = 0f;
			for (int i = 0; i < num2; i++)
			{
				float num8 = ((float) (i + 1)) / ((float) num2);
				float num9 = duration * num8;
				num4 += num9;
				durations[i] = num9;
			}
			float num5 = duration / num4;
			for (int j = 0; j < num2; j++)
			{
				durations[j] *= num5;
			}
			float degrees = UnityEngine.Random.Range((float) 0f, (float) 360f);
			Vector3[] endValues = new Vector3[num2];
			for (int k = 0; k < num2; k++)
			{
				if (k < (num2 - 1))
				{
					if (k > 0)
					{
						degrees = (degrees - 180f) + UnityEngine.Random.Range(-randomness, randomness);
					}
					if (vectorBased)
					{
						Vector3 vector = (Vector3) (Quaternion.AngleAxis(UnityEngine.Random.Range(-randomness, randomness), Vector3.up) * Utils.Vector3FromAngle(degrees, magnitude));
						vector.x = Vector3.ClampMagnitude(vector, strength.x).x;
						vector.y = Vector3.ClampMagnitude(vector, strength.y).y;
						vector.z = Vector3.ClampMagnitude(vector, strength.z).z;
						endValues[k] = vector;
						magnitude -= num3;
						strength = Vector3.ClampMagnitude(strength, magnitude);
					}
					else
					{
						if (ignoreZAxis)
						{
							endValues[k] = Utils.Vector3FromAngle(degrees, magnitude);
						}
						else
						{
							Quaternion quaternion = Quaternion.AngleAxis(UnityEngine.Random.Range(-randomness, randomness), Vector3.up);
							endValues[k] = (Vector3) (quaternion * Utils.Vector3FromAngle(degrees, magnitude));
						}
						magnitude -= num3;
					}
				}
				else
				{
					endValues[k] = Vector3.zero;
				}
			}
			return ToArray(getter, setter, endValues, durations).NoFrom<Vector3, Vector3[], Vector3ArrayOptions>().SetSpecialStartupMode<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(SpecialStartupMode.SetShake);
		}

		public static int SmoothRewind(object targetOrId)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return TweenManager.FilteredOperation(OperationType.SmoothRewind, FilterType.TargetOrId, targetOrId, false, 0f, null, null);
		}

		public static int SmoothRewindAll()
		{
			return TweenManager.FilteredOperation(OperationType.SmoothRewind, FilterType.All, null, false, 0f, null, null);
		}

		internal static TweenerCore<Color2, Color2, ColorOptions> To(DOGetter<Color2> getter, DOSetter<Color2> setter, Color2 endValue, float duration)
		{
			return ApplyTo<Color2, Color2, ColorOptions>(getter, setter, endValue, duration, null);
		}

		public static TweenerCore<double, double, NoOptions> To(DOGetter<double> getter, DOSetter<double> setter, double endValue, float duration)
		{
			return ApplyTo<double, double, NoOptions>(getter, setter, endValue, duration, null);
		}

		public static Tweener To(DOGetter<int> getter, DOSetter<int> setter, int endValue, float duration)
		{
			return ApplyTo<int, int, NoOptions>(getter, setter, endValue, duration, null);
		}

		public static Tweener To(DOGetter<long> getter, DOSetter<long> setter, long endValue, float duration)
		{
			return ApplyTo<long, long, NoOptions>(getter, setter, endValue, duration, null);
		}

		public static TweenerCore<float, float, FloatOptions> To(DOGetter<float> getter, DOSetter<float> setter, float endValue, float duration)
		{
			return ApplyTo<float, float, FloatOptions>(getter, setter, endValue, duration, null);
		}

		public static TweenerCore<string, string, StringOptions> To(DOGetter<string> getter, DOSetter<string> setter, string endValue, float duration)
		{
			return ApplyTo<string, string, StringOptions>(getter, setter, endValue, duration, null);
		}

		public static Tweener To(DOGetter<uint> getter, DOSetter<uint> setter, uint endValue, float duration)
		{
			return ApplyTo<uint, uint, NoOptions>(getter, setter, endValue, duration, null);
		}

		public static Tweener To(DOGetter<ulong> getter, DOSetter<ulong> setter, ulong endValue, float duration)
		{
			return ApplyTo<ulong, ulong, NoOptions>(getter, setter, endValue, duration, null);
		}

		public static TweenerCore<Color, Color, ColorOptions> To(DOGetter<Color> getter, DOSetter<Color> setter, Color endValue, float duration)
		{
			return ApplyTo<Color, Color, ColorOptions>(getter, setter, endValue, duration, null);
		}

		public static TweenerCore<Quaternion, Vector3, QuaternionOptions> To(DOGetter<Quaternion> getter, DOSetter<Quaternion> setter, Vector3 endValue, float duration)
		{
			return ApplyTo<Quaternion, Vector3, QuaternionOptions>(getter, setter, endValue, duration, null);
		}

		public static TweenerCore<Rect, Rect, RectOptions> To(DOGetter<Rect> getter, DOSetter<Rect> setter, Rect endValue, float duration)
		{
			return ApplyTo<Rect, Rect, RectOptions>(getter, setter, endValue, duration, null);
		}

		public static Tweener To(DOGetter<RectOffset> getter, DOSetter<RectOffset> setter, RectOffset endValue, float duration)
		{
			return ApplyTo<RectOffset, RectOffset, NoOptions>(getter, setter, endValue, duration, null);
		}

		public static TweenerCore<Vector2, Vector2, VectorOptions> To(DOGetter<Vector2> getter, DOSetter<Vector2> setter, Vector2 endValue, float duration)
		{
			return ApplyTo<Vector2, Vector2, VectorOptions>(getter, setter, endValue, duration, null);
		}

		public static TweenerCore<Vector3, Vector3, VectorOptions> To(DOGetter<Vector3> getter, DOSetter<Vector3> setter, Vector3 endValue, float duration)
		{
			return ApplyTo<Vector3, Vector3, VectorOptions>(getter, setter, endValue, duration, null);
		}

		public static TweenerCore<Vector4, Vector4, VectorOptions> To(DOGetter<Vector4> getter, DOSetter<Vector4> setter, Vector4 endValue, float duration)
		{
			return ApplyTo<Vector4, Vector4, VectorOptions>(getter, setter, endValue, duration, null);
		}

		public static Tweener To(DOSetter<float> setter, float startValue, float endValue, float duration)
		{
			float v = startValue;
			return To(() => v, delegate (float x) {
				v = x;
				setter(v);
			}, endValue, duration).NoFrom<float, float, FloatOptions>();
		}

		public static TweenerCore<T1, T2, TPlugOptions> To<T1, T2, TPlugOptions>(ABSTweenPlugin<T1, T2, TPlugOptions> plugin, DOGetter<T1> getter, DOSetter<T1> setter, T2 endValue, float duration) where TPlugOptions: struct
		{
			return ApplyTo<T1, T2, TPlugOptions>(getter, setter, endValue, duration, plugin);
		}

		public static Tweener ToAlpha(DOGetter<Color> getter, DOSetter<Color> setter, float endValue, float duration)
		{
			return ApplyTo<Color, Color, ColorOptions>(getter, setter, new Color(0f, 0f, 0f, endValue), duration, null).SetOptions(true);
		}

		public static TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> ToArray(DOGetter<Vector3> getter, DOSetter<Vector3> setter, Vector3[] endValues, float[] durations)
		{
			int length = durations.Length;
			if (length != endValues.Length)
			{
				Debugger.LogError("To Vector3 array tween: endValues and durations arrays must have the same length");
				return null;
			}
			Vector3[] endValue = new Vector3[length];
			float[] numArray = new float[length];
			for (int i = 0; i < length; i++)
			{
				endValue[i] = endValues[i];
				numArray[i] = durations[i];
			}
			float duration = 0f;
			for (int j = 0; j < length; j++)
			{
				duration += numArray[j];
			}
			TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> core1 = ApplyTo<Vector3, Vector3[], Vector3ArrayOptions>(getter, setter, endValue, duration, null).NoFrom<Vector3, Vector3[], Vector3ArrayOptions>();
			core1.plugOptions.durations = numArray;
			return core1;
		}

		public static TweenerCore<Vector3, Vector3, VectorOptions> ToAxis(DOGetter<Vector3> getter, DOSetter<Vector3> setter, float endValue, float duration, AxisConstraint axisConstraint = AxisConstraint.X)
		{
			TweenerCore<Vector3, Vector3, VectorOptions> core1 = ApplyTo<Vector3, Vector3, VectorOptions>(getter, setter, new Vector3(endValue, endValue, endValue), duration, null);
			core1.plugOptions.axisConstraint = axisConstraint;
			return core1;
		}

		public static int TogglePause(object targetOrId)
		{
			if (targetOrId == null)
			{
				return 0;
			}
			return TweenManager.FilteredOperation(OperationType.TogglePause, FilterType.TargetOrId, targetOrId, false, 0f, null, null);
		}

		public static int TogglePauseAll()
		{
			return TweenManager.FilteredOperation(OperationType.TogglePause, FilterType.All, null, false, 0f, null, null);
		}

		public static int TotalPlayingTweens()
		{
			return TweenManager.TotalPlayingTweens();
		}

		public static List<Tween> TweensById(object id, bool playingOnly=false)
		{
			if (id == null)
			{
				return null;
			}
			return TweenManager.GetTweensById(id, playingOnly);
		}

		public static List<Tween> TweensByTarget(object target, bool playingOnly=false)
		{
			return TweenManager.GetTweensByTarget(target, playingOnly);
		}

		public static int Validate()
		{
			return TweenManager.Validate();
		}

		// Properties
		public static LogBehaviour logBehaviour
		{
			get
			{
				return _logBehaviour;
			}
			set
			{
				_logBehaviour = value;
				Debugger.SetLogPriority(_logBehaviour);
			}
		}
	}
}