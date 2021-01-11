
using DG.Tweening.Core;
using UnityEngine;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;

namespace DG.Tweening
{
	public static class TweenExtensions
	{
		// Methods
		public static void Complete(this Tween t)
		{
			t.Complete(false);
		}

		public static void Complete(this Tween t, bool withCallbacks)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
			}
			else if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
			}
			else if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
			}
			else
			{
				TweenManager.Complete(t, true, withCallbacks ? UpdateMode.Update : UpdateMode.Goto);
			}
		}

		public static int CompletedLoops(this Tween t)
		{
			if (t.active)
			{
				return t.completedLoops;
			}
			if (Debugger.logPriority > 0)
			{
				Debugger.LogInvalidTween(t);
			}
			return 0;
		}

		public static float Delay(this Tween t)
		{
			if (t.active)
			{
				return t.delay;
			}
			if (Debugger.logPriority > 0)
			{
				Debugger.LogInvalidTween(t);
			}
			return 0f;
		}

		public static float Duration(this Tween t, bool includeLoops=true)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return 0f;
			}
			if (!includeLoops)
			{
				return t.duration;
			}
			if (t.loops != -1)
			{
				return (t.duration * t.loops);
			}
			return float.PositiveInfinity;
		}

		public static float Elapsed(this Tween t, bool includeLoops=true)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return 0f;
			}
			if (includeLoops)
			{
				return ((((t.position >= t.duration) ? ((float) (t.completedLoops - 1)) : ((float) t.completedLoops)) * t.duration) + t.position);
			}
			return t.position;
		}

		public static float ElapsedDirectionalPercentage(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return 0f;
			}
			float num = t.position / t.duration;
			if (((t.completedLoops <= 0) || (t.loopType != LoopType.Yoyo)) || ((t.isComplete || ((t.completedLoops % 2) == 0)) && (!t.isComplete || ((t.completedLoops % 2) != 0))))
			{
				return num;
			}
			return (1f - num);
		}

		public static float ElapsedPercentage(this Tween t, bool includeLoops=true)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return 0f;
			}
			if (includeLoops)
			{
				return (((((t.position >= t.duration) ? ((float) (t.completedLoops - 1)) : ((float) t.completedLoops)) * t.duration) + t.position) / t.fullDuration);
			}
			return (t.position / t.duration);
		}

		public static void Flip(this Tween t)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
			}
			else if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
			}
			else if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
			}
			else
			{
				TweenManager.Flip(t);
			}
		}

		public static void ForceInit(this Tween t)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
			}
			else if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
			}
			else if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
			}
			else
			{
				TweenManager.ForceInit(t);
			}
		}

		public static void Goto(this Tween t, float to, bool andPlay=false)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
			}
			else if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
			}
			else if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
			}
			else
			{
				if (to < 0f)
				{
					to = 0f;
				}
				TweenManager.Goto(t, to, andPlay, UpdateMode.Goto);
			}
		}

		public static void GotoWaypoint(this Tween t, int waypointIndex, bool andPlay=false)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
			}
			else if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
			}
			else if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
			}
			else
			{
				TweenerCore<Vector3, Path, PathOptions> core = t as TweenerCore<Vector3, Path, PathOptions>;
				if (core == null)
				{
					if (Debugger.logPriority > 1)
					{
						Debugger.LogNonPathTween(t);
					}
				}
				else
				{
					if (!t.startupDone)
					{
						TweenManager.ForceInit(t);
					}
					if (waypointIndex < 0)
					{
						waypointIndex = 0;
					}
					else if (waypointIndex > (core.changeValue.wps.Length - 1))
					{
						waypointIndex = core.changeValue.wps.Length - 1;
					}
					float num = 0f;
					for (int i = 0; i < (waypointIndex + 1); i++)
					{
						num += core.changeValue.wpLengths[i];
					}
					float num2 = num / core.changeValue.length;
					if ((t.loopType == LoopType.Yoyo) && ((t.position < t.duration) ? ((t.completedLoops % 2) > 0) : ((t.completedLoops % 2) == 0)))
					{
						num2 = 1f - num2;
					}
					float to = ((t.isComplete ? ((float) (t.completedLoops - 1)) : ((float) t.completedLoops)) * t.duration) + (num2 * t.duration);
					TweenManager.Goto(t, to, andPlay, UpdateMode.Goto);
				}
			}
		}

		public static bool IsActive(this Tween t)
		{
			return t.active;
		}

		public static bool IsBackwards(this Tween t)
		{
			if (t.active)
			{
				return t.isBackwards;
			}
			if (Debugger.logPriority > 0)
			{
				Debugger.LogInvalidTween(t);
			}
			return false;
		}

		public static bool IsComplete(this Tween t)
		{
			if (t.active)
			{
				return t.isComplete;
			}
			if (Debugger.logPriority > 0)
			{
				Debugger.LogInvalidTween(t);
			}
			return false;
		}

		public static bool IsInitialized(this Tween t)
		{
			if (t.active)
			{
				return t.startupDone;
			}
			if (Debugger.logPriority > 0)
			{
				Debugger.LogInvalidTween(t);
			}
			return false;
		}

		public static bool IsPlaying(this Tween t)
		{
			if (t == null) {
				return false;
			}
			if (t.active)
			{
				return t.isPlaying;
			}
			if (Debugger.logPriority > 0)
			{
				Debugger.LogInvalidTween(t);
			}
			return false;
		}

		public static void Kill(this Tween t, bool complete=false)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
			}
			else if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
			}
			else if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
			}
			else
			{
				if (complete)
				{
					TweenManager.Complete(t, true, UpdateMode.Goto);
					if (t.autoKill && (t.loops >= 0))
					{
						return;
					}
				}
				if (TweenManager.isUpdateLoop)
				{
					t.active = false;
				}
				else
				{
					TweenManager.Despawn(t, true);
				}
			}
		}

		public static int Loops(this Tween t)
		{
			if (t.active)
			{
				return t.loops;
			}
			if (Debugger.logPriority > 0)
			{
				Debugger.LogInvalidTween(t);
			}
			return 0;
		}

		public static Vector3[] PathGetDrawPoints(this Tween t, int subdivisionsXSegment=10)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return null;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return null;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return null;
			}
			TweenerCore<Vector3, Path, PathOptions> core = t as TweenerCore<Vector3, Path, PathOptions>;
			if (core == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNonPathTween(t);
				}
				return null;
			}
			if (core.endValue.isFinalized)
			{
				return Path.GetDrawPoints(core.endValue, subdivisionsXSegment);
			}
			if (Debugger.logPriority > 1)
			{
				Debugger.LogWarning("The path is not finalized yet");
			}
			return null;
		}

		public static Vector3 PathGetPoint(this Tween t, float pathPercentage)
		{
			if (pathPercentage > 1f)
			{
				pathPercentage = 1f;
			}
			else if (pathPercentage < 0f)
			{
				pathPercentage = 0f;
			}
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return Vector3.zero;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return Vector3.zero;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return Vector3.zero;
			}
			TweenerCore<Vector3, Path, PathOptions> core = t as TweenerCore<Vector3, Path, PathOptions>;
			if (core == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNonPathTween(t);
				}
				return Vector3.zero;
			}
			if (core.endValue.isFinalized)
			{
				return core.endValue.GetPoint(pathPercentage, true);
			}
			if (Debugger.logPriority > 1)
			{
				Debugger.LogWarning("The path is not finalized yet");
			}
			return Vector3.zero;
		}

		public static float PathLength(this Tween t)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return -1f;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return -1f;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return -1f;
			}
			TweenerCore<Vector3, Path, PathOptions> core = t as TweenerCore<Vector3, Path, PathOptions>;
			if (core == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNonPathTween(t);
				}
				return -1f;
			}
			if (core.endValue.isFinalized)
			{
				return core.endValue.length;
			}
			if (Debugger.logPriority > 1)
			{
				Debugger.LogWarning("The path is not finalized yet");
			}
			return -1f;
		}

		public static T Pause<T>(this T t) where T: Tween
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return t;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return t;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return t;
			}
			TweenManager.Pause(t);
			return t;
		}

		public static T Play<T>(this T t) where T: Tween
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return t;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return t;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return t;
			}
			TweenManager.Play(t);
			return t;
		}

		public static void PlayBackwards(this Tween t)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
			}
			else if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
			}
			else if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
			}
			else
			{
				TweenManager.PlayBackwards(t);
			}
		}

		public static void PlayForward(this Tween t)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
			}
			else if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
			}
			else if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
			}
			else
			{
				TweenManager.PlayForward(t);
			}
		}

		public static void Restart(this Tween t, bool includeDelay=true)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
			}
			else if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
			}
			else if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
			}
			else
			{
				TweenManager.Restart(t, includeDelay);
			}
		}

		public static void Rewind(this Tween t, bool includeDelay=true)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
			}
			else if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
			}
			else if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
			}
			else
			{
				TweenManager.Rewind(t, includeDelay);
			}
		}

		public static void SmoothRewind(this Tween t)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
			}
			else if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
			}
			else if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
			}
			else
			{
				TweenManager.SmoothRewind(t);
			}
		}

		public static void TogglePause(this Tween t)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
			}
			else if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
			}
			else if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
			}
			else
			{
				TweenManager.TogglePause(t);
			}
		}

		public static YieldInstruction WaitForCompletion(this Tween t)
		{
			if (t.active)
			{
				return DOTween.instance.StartCoroutine(DOTween.instance.WaitForCompletion(t));
			}
			if (Debugger.logPriority > 0)
			{
				Debugger.LogInvalidTween(t);
			}
			return null;
		}

		public static YieldInstruction WaitForElapsedLoops(this Tween t, int elapsedLoops)
		{
			if (t.active)
			{
				return DOTween.instance.StartCoroutine(DOTween.instance.WaitForElapsedLoops(t, elapsedLoops));
			}
			if (Debugger.logPriority > 0)
			{
				Debugger.LogInvalidTween(t);
			}
			return null;
		}

		public static YieldInstruction WaitForKill(this Tween t)
		{
			if (t.active)
			{
				return DOTween.instance.StartCoroutine(DOTween.instance.WaitForKill(t));
			}
			if (Debugger.logPriority > 0)
			{
				Debugger.LogInvalidTween(t);
			}
			return null;
		}

		public static YieldInstruction WaitForPosition(this Tween t, float position)
		{
			if (t.active)
			{
				return DOTween.instance.StartCoroutine(DOTween.instance.WaitForPosition(t, position));
			}
			if (Debugger.logPriority > 0)
			{
				Debugger.LogInvalidTween(t);
			}
			return null;
		}

		public static YieldInstruction WaitForRewind(this Tween t)
		{
			if (t.active)
			{
				return DOTween.instance.StartCoroutine(DOTween.instance.WaitForRewind(t));
			}
			if (Debugger.logPriority > 0)
			{
				Debugger.LogInvalidTween(t);
			}
			return null;
		}

		public static Coroutine WaitForStart(this Tween t)
		{
			if (t.active)
			{
				return DOTween.instance.StartCoroutine(DOTween.instance.WaitForStart(t));
			}
			if (Debugger.logPriority > 0)
			{
				Debugger.LogInvalidTween(t);
			}
			return null;
		}
	}
}