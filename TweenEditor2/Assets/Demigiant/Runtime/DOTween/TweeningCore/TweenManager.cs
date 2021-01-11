using System.Collections.Generic;
using DG.Tweening.Core.Enums;
using System;
using UnityEngine;

namespace DG.Tweening.Core
{
	public static class TweenManager
	{
		// Fields
		public static Tween[] _activeTweens = new Tween[200];
		private const int _DefaultMaxSequences = 50;
		private const int _DefaultMaxTweeners = 200;
		private static readonly List<Tween> _KillList = new List<Tween>(200);
		private static int _maxActiveLookupId = -1;
		private static int _maxPooledTweenerId = -1;
		private const string _MaxTweensReached = "Max Tweens reached: capacity has automatically been increased from #0 to #1. Use DOTween.SetTweensCapacity to set it manually at startup";
		private static int _minPooledTweenerId = -1;
		private static readonly Stack<Tween> _PooledSequences = new Stack<Tween>();
		private static Tween[] _pooledTweeners = new Tween[200];
		private static int _reorganizeFromId = -1;
		private static bool _requiresActiveReorganization;
		public static bool hasActiveDefaultTweens;
		public static bool hasActiveFixedTweens;
		public static bool hasActiveLateTweens;
		public static bool hasActiveTweens;
		public static bool isUpdateLoop;
		public static int maxActive = 200;
		public static int maxSequences = 50;
		public static int maxTweeners = 200;
		public static int totActiveDefaultTweens;
		public static int totActiveFixedTweens;
		public static int totActiveLateTweens;
		public static int totActiveSequences;
		public static int totActiveTweeners;
		public static int totActiveTweens;
		public static int totPooledSequences;
		public static int totPooledTweeners;
		public static int totSequences;
		public static int totTweeners;

		// Methods
		private static void AddActiveTween(Tween t)
		{
			if (_requiresActiveReorganization)
			{
				ReorganizeActiveTweens();
			}
			t.active = true;
			t.updateType = DOTween.defaultUpdateType;
			t.isIndependentUpdate = DOTween.defaultTimeScaleIndependent;
			t.activeId = _maxActiveLookupId = totActiveTweens;
			_activeTweens[totActiveTweens] = t;
			if (t.updateType == UpdateType.Normal)
			{
				totActiveDefaultTweens++;
				hasActiveDefaultTweens = true;
			}
			else if (t.updateType == UpdateType.Fixed)
			{
				totActiveFixedTweens++;
				hasActiveFixedTweens = true;
			}
			else
			{
				totActiveLateTweens++;
				hasActiveLateTweens = true;
			}
			totActiveTweens++;
			if (t.tweenType == TweenType.Tweener)
			{
				totActiveTweeners++;
			}
			else
			{
				totActiveSequences++;
			}
			hasActiveTweens = true;
		}

		public static void AddActiveTweenToSequence(Tween t)
		{
			RemoveActiveTween(t);
		}

		private static void ClearTweenArray(Tween[] tweens)
		{
			int length = tweens.Length;
			for (int i = 0; i < length; i++)
			{
				tweens[i] = null;
			}
		}

		public static bool Complete(Tween t, bool modifyActiveLists=true, UpdateMode updateMode=UpdateMode.Goto)
		{
			if (t.loops == -1)
			{
				return false;
			}
			if (t.isComplete)
			{
				return false;
			}
			Tween.DoGoto(t, t.duration, t.loops, updateMode);
			t.isPlaying = false;
			if (t.autoKill)
			{
				if (isUpdateLoop)
				{
					t.active = false;
				}
				else
				{
					Despawn(t, modifyActiveLists);
				}
			}
			return true;
		}

		public static void Despawn(Tween t, bool modifyActiveLists=true)
		{
			if (t.onKill != null)
			{
				Tween.OnTweenCallback(t.onKill);
			}
			if (modifyActiveLists)
			{
				RemoveActiveTween(t);
			}
			if (!t.isRecyclable)
			{
				switch (t.tweenType)
				{
				case TweenType.Tweener:
					totTweeners--;
					break;

				case TweenType.Sequence:
					{
						totSequences--;
						Sequence sequence2 = (Sequence) t;
						int count = sequence2.sequencedTweens.Count;
						for (int i = 0; i < count; i++)
						{
							Despawn(sequence2.sequencedTweens[i], false);
						}
						break;
					}
				}
			}
			else
			{
				switch (t.tweenType)
				{
				case TweenType.Tweener:
					if (_maxPooledTweenerId == -1)
					{
						_maxPooledTweenerId = maxTweeners - 1;
						_minPooledTweenerId = maxTweeners - 1;
					}
					if (_maxPooledTweenerId < (maxTweeners - 1))
					{
						_pooledTweeners[_maxPooledTweenerId + 1] = t;
						_maxPooledTweenerId++;
						if (_minPooledTweenerId > _maxPooledTweenerId)
						{
							_minPooledTweenerId = _maxPooledTweenerId;
						}
					}
					else
					{
						for (int j = _maxPooledTweenerId; j > -1; j--)
						{
							if (_pooledTweeners[j] == null)
							{
								_pooledTweeners[j] = t;
								if (j < _minPooledTweenerId)
								{
									_minPooledTweenerId = j;
								}
								if (_maxPooledTweenerId < _minPooledTweenerId)
								{
									_maxPooledTweenerId = _minPooledTweenerId;
								}
								break;
							}
						}
					}
					totPooledTweeners++;
					break;

				case TweenType.Sequence:
					{
						_PooledSequences.Push(t);
						totPooledSequences++;
						Sequence sequence = (Sequence) t;
						int num = sequence.sequencedTweens.Count;
						for (int k = 0; k < num; k++)
						{
							Despawn(sequence.sequencedTweens[k], false);
						}
						break;
					}
				}
			}
			t.active = false;
			t.Reset();
		}

		public static int DespawnAll()
		{
			int totActiveTweens = TweenManager.totActiveTweens;
			for (int i = 0; i < (_maxActiveLookupId + 1); i++)
			{
				Tween t = _activeTweens[i];
				if (t != null)
				{
					Despawn(t, false);
				}
			}
			ClearTweenArray(_activeTweens);
			hasActiveTweens = hasActiveDefaultTweens = hasActiveLateTweens = hasActiveFixedTweens = false;
			TweenManager.totActiveTweens = totActiveDefaultTweens = totActiveLateTweens = totActiveFixedTweens = 0;
			totActiveTweeners = totActiveSequences = 0;
			_maxActiveLookupId = _reorganizeFromId = -1;
			_requiresActiveReorganization = false;
			return totActiveTweens;
		}

		private static void DespawnTweens(List<Tween> tweens, bool modifyActiveLists=true)
		{
			int count = tweens.Count;
			for (int i = 0; i < count; i++)
			{
				Despawn(tweens[i], modifyActiveLists);
			}
		}

		public static int FilteredOperation(OperationType operationType, FilterType filterType, object id, bool optionalBool, float optionalFloat, object optionalObj=null, object[] optionalArray=null)
		{
			int num = 0;
			bool flag = false;
			int num2 = (optionalArray == null) ? 0 : optionalArray.Length;
			for (int i = _maxActiveLookupId; i > -1; i--)
			{
				int num4;
				object obj2;
				Tween t = _activeTweens[i];
				if ((t == null) || !t.active)
				{
					continue;
				}
				bool flag2 = false;
				switch (filterType)
				{
				case FilterType.All:
					flag2 = true;
					goto Label_00E7;

				case FilterType.TargetOrId:
					flag2 = id.Equals(t.id) || id.Equals(t.target);
					goto Label_00E7;

				case FilterType.TargetAndId:
					flag2 = (id.Equals(t.id) && (optionalObj != null)) && optionalObj.Equals(t.target);
					goto Label_00E7;

				case FilterType.AllExceptTargetsOrIds:
					flag2 = true;
					num4 = 0;
					goto Label_00E2;

				default:
					goto Label_00E7;
				}
				Label_00B0:
				obj2 = optionalArray[num4];
				if (obj2.Equals(t.id) || obj2.Equals(t.target))
				{
					flag2 = false;
					goto Label_00E7;
				}
				num4++;
				Label_00E2:
				if (num4 < num2)
				{
					goto Label_00B0;
				}
				Label_00E7:
				if (flag2)
				{
					switch (operationType)
					{
					case OperationType.Complete:
						{
							bool autoKill = t.autoKill;
							if (Complete(t, false, (optionalFloat > 0f) ? UpdateMode.Update : UpdateMode.Goto))
							{
								num += !optionalBool ? 1 : (autoKill ? 1 : 0);
								if (autoKill)
								{
									if (!isUpdateLoop)
									{
										goto Label_01AF;
									}
									t.active = false;
								}
							}
							break;
						}
					case OperationType.Despawn:
						num++;
						if (!isUpdateLoop)
						{
							goto Label_0145;
						}
						t.active = false;
						break;

					case OperationType.Flip:
						if (Flip(t))
						{
							num++;
						}
						break;

					case OperationType.Goto:
						Goto(t, optionalFloat, optionalBool, UpdateMode.Goto);
						num++;
						break;

					case OperationType.Pause:
						if (Pause(t))
						{
							num++;
						}
						break;

					case OperationType.Play:
						if (Play(t))
						{
							num++;
						}
						break;

					case OperationType.PlayForward:
						if (PlayForward(t))
						{
							num++;
						}
						break;

					case OperationType.PlayBackwards:
						if (PlayBackwards(t))
						{
							num++;
						}
						break;

					case OperationType.Rewind:
						if (Rewind(t, optionalBool))
						{
							num++;
						}
						break;

					case OperationType.SmoothRewind:
						if (SmoothRewind(t))
						{
							num++;
						}
						break;

					case OperationType.Restart:
						if (Restart(t, optionalBool))
						{
							num++;
						}
						break;

					case OperationType.TogglePause:
						if (TogglePause(t))
						{
							num++;
						}
						break;

					case OperationType.IsTweening:
						goto Label_026C;
					}
				}
				continue;
				Label_0145:
				Despawn(t, false);
				flag = true;
				_KillList.Add(t);
				continue;
				Label_01AF:
				flag = true;
				_KillList.Add(t);
				continue;
				Label_026C:
				if (!t.isComplete || !t.autoKill)
				{
					num++;
				}
			}
			if (flag)
			{
				for (int j = _KillList.Count - 1; j > -1; j--)
				{
					RemoveActiveTween(_KillList[j]);
				}
				_KillList.Clear();
			}
			return num;
		}

		public static bool Flip(Tween t)
		{
			t.isBackwards = !t.isBackwards;
			return true;
		}

		public static void ForceInit(Tween t)
		{
			if (!t.startupDone && !t.Startup())
			{
				if (isUpdateLoop)
				{
					t.active = false;
				}
				else
				{
					RemoveActiveTween(t);
				}
			}
		}

		public static List<Tween> GetActiveTweens(bool playing)
		{
			if (_requiresActiveReorganization)
			{
				ReorganizeActiveTweens();
			}
			if (TweenManager.totActiveTweens > 0)
			{
				int totActiveTweens = TweenManager.totActiveTweens;
				List<Tween> list = new List<Tween>(totActiveTweens);
				for (int i = 0; i < totActiveTweens; i++)
				{
					Tween item = _activeTweens[i];
					if (item.isPlaying == playing)
					{
						list.Add(item);
					}
				}
				if (list.Count > 0)
				{
					return list;
				}
			}
			return null;
		}

		public static Sequence GetSequence()
		{
			if (totPooledSequences > 0)
			{
				Sequence sequence1 = (Sequence) _PooledSequences.Pop();
				AddActiveTween(sequence1);
				totPooledSequences--;
				return sequence1;
			}
			if (totSequences >= (TweenManager.maxSequences - 1))
			{
				int maxTweeners = TweenManager.maxTweeners;
				int maxSequences = TweenManager.maxSequences;
				IncreaseCapacities(CapacityIncreaseMode.SequencesOnly);
				if (Debugger.logPriority >= 1)
				{
					Debugger.LogWarning("Max Tweens reached: capacity has automatically been increased from #0 to #1. Use DOTween.SetTweensCapacity to set it manually at startup".Replace("#0", maxTweeners + "/" + maxSequences).Replace("#1", TweenManager.maxTweeners + "/" + TweenManager.maxSequences));
				}
			}
			totSequences++;
			Sequence t = new Sequence();
			AddActiveTween(t);
			return t;
		}

		public static TweenerCore<T1, T2, TPlugOptions> GetTweener<T1, T2, TPlugOptions>() where TPlugOptions: struct
		{
			if (totPooledTweeners > 0)
			{
				Type type = typeof(T1);
				Type type2 = typeof(T2);
				Type type3 = typeof(TPlugOptions);
				for (int i = _maxPooledTweenerId; i > (_minPooledTweenerId - 1); i--)
				{
					Tween tween = _pooledTweeners[i];
					if (((tween != null) && (tween.typeofT1 == type)) && ((tween.typeofT2 == type2) && (tween.typeofTPlugOptions == type3)))
					{
						TweenerCore<T1, T2, TPlugOptions> core1 = (TweenerCore<T1, T2, TPlugOptions>) tween;
						AddActiveTween(core1);
						_pooledTweeners[i] = null;
						if (_maxPooledTweenerId != _minPooledTweenerId)
						{
							if (i == _maxPooledTweenerId)
							{
								_maxPooledTweenerId--;
							}
							else if (i == _minPooledTweenerId)
							{
								_minPooledTweenerId++;
							}
						}
						totPooledTweeners--;
						return core1;
					}
				}
				if (totTweeners >= TweenManager.maxTweeners)
				{
					_pooledTweeners[_maxPooledTweenerId] = null;
					_maxPooledTweenerId--;
					totPooledTweeners--;
					totTweeners--;
				}
			}
			else if (totTweeners >= (TweenManager.maxTweeners - 1))
			{
				int maxTweeners = TweenManager.maxTweeners;
				int maxSequences = TweenManager.maxSequences;
				IncreaseCapacities(CapacityIncreaseMode.TweenersOnly);
				if (Debugger.logPriority >= 1)
				{
					Debugger.LogWarning("Max Tweens reached: capacity has automatically been increased from #0 to #1. Use DOTween.SetTweensCapacity to set it manually at startup".Replace("#0", maxTweeners + "/" + maxSequences).Replace("#1", TweenManager.maxTweeners + "/" + TweenManager.maxSequences));
				}
			}
			totTweeners++;
			TweenerCore<T1, T2, TPlugOptions> t = new TweenerCore<T1, T2, TPlugOptions>();
			AddActiveTween(t);
			return t;
		}

		public static List<Tween> GetTweensById(object id, bool playingOnly)
		{
			if (_requiresActiveReorganization)
			{
				ReorganizeActiveTweens();
			}
			if (TweenManager.totActiveTweens > 0)
			{
				int totActiveTweens = TweenManager.totActiveTweens;
				List<Tween> list = new List<Tween>(totActiveTweens);
				for (int i = 0; i < totActiveTweens; i++)
				{
					Tween item = _activeTweens[i];
					if (((item != null) && object.Equals(id, item.id)) && (!playingOnly || item.isPlaying))
					{
						list.Add(item);
					}
				}
				if (list.Count > 0)
				{
					return list;
				}
			}
			return null;
		}

		public static List<Tween> GetTweensByTarget(object target, bool playingOnly)
		{
			if (_requiresActiveReorganization)
			{
				ReorganizeActiveTweens();
			}
			if (TweenManager.totActiveTweens > 0)
			{
				int totActiveTweens = TweenManager.totActiveTweens;
				List<Tween> list = new List<Tween>(totActiveTweens);
				for (int i = 0; i < totActiveTweens; i++)
				{
					Tween item = _activeTweens[i];
					if ((item.target == target) && (!playingOnly || item.isPlaying))
					{
						list.Add(item);
					}
				}
				if (list.Count > 0)
				{
					return list;
				}
			}
			return null;
		}

		public static bool Goto(Tween t, float to, bool andPlay=false, UpdateMode updateMode=UpdateMode.Goto)
		{
			bool isPlaying = t.isPlaying;
			t.isPlaying = andPlay;
			t.delayComplete = true;
			t.elapsedDelay = t.delay;
			int toCompletedLoops = Mathf.FloorToInt(to / t.duration);
			float toPosition = to % t.duration;
			if ((t.loops != -1) && (toCompletedLoops >= t.loops))
			{
				toCompletedLoops = t.loops;
				toPosition = t.duration;
			}
			else if (toPosition >= t.duration)
			{
				toPosition = 0f;
			}
			bool flag2 = Tween.DoGoto(t, toPosition, toCompletedLoops, updateMode);
			if (((!andPlay & isPlaying) && !flag2) && (t.onPause != null))
			{
				Tween.OnTweenCallback(t.onPause);
			}
			return flag2;
		}

		private static void IncreaseCapacities(CapacityIncreaseMode increaseMode)
		{
			int num = 0;
			int num2 = Mathf.Max((int) (maxTweeners * 1.5f), 200);
			int num3 = Mathf.Max((int) (maxSequences * 1.5f), 50);
			if (increaseMode != CapacityIncreaseMode.TweenersOnly)
			{
				if (increaseMode == CapacityIncreaseMode.SequencesOnly)
				{
					num += num3;
					maxSequences += num3;
				}
				else
				{
					num += num2;
					maxTweeners += num2;
					maxSequences += num3;
					Array.Resize<Tween>(ref _pooledTweeners, maxTweeners);
				}
			}
			else
			{
				num += num2;
				maxTweeners += num2;
				Array.Resize<Tween>(ref _pooledTweeners, maxTweeners);
			}
			maxActive = maxTweeners + maxSequences;
			Array.Resize<Tween>(ref _activeTweens, maxActive);
			if (num > 0)
			{
				_KillList.Capacity += num;
			}
		}

		private static void MarkForKilling(Tween t)
		{
			t.active = false;
			_KillList.Add(t);
		}

		public static bool Pause(Tween t)
		{
			if (!t.isPlaying)
			{
				return false;
			}
			t.isPlaying = false;
			if (t.onPause != null)
			{
				Tween.OnTweenCallback(t.onPause);
			}
			return true;
		}

		public static bool Play(Tween t)
		{
			if (t.isPlaying || ((t.isBackwards || t.isComplete) && (!t.isBackwards || ((t.completedLoops <= 0) && (t.position <= 0f)))))
			{
				return false;
			}
			t.isPlaying = true;
			if (t.playedOnce && (t.onPlay != null))
			{
				Tween.OnTweenCallback(t.onPlay);
			}
			return true;
		}

		public static bool PlayBackwards(Tween t)
		{
			if (!t.isBackwards)
			{
				t.isBackwards = true;
				Play(t);
				return true;
			}
			return Play(t);
		}

		public static bool PlayForward(Tween t)
		{
			if (t.isBackwards)
			{
				t.isBackwards = false;
				Play(t);
				return true;
			}
			return Play(t);
		}

		public static void PurgeAll()
		{
			for (int i = 0; i < totActiveTweens; i++)
			{
				Tween tween = _activeTweens[i];
				if ((tween != null) && (tween.onKill != null))
				{
					Tween.OnTweenCallback(tween.onKill);
				}
			}
			ClearTweenArray(_activeTweens);
			hasActiveTweens = hasActiveDefaultTweens = hasActiveLateTweens = hasActiveFixedTweens = false;
			totActiveTweens = totActiveDefaultTweens = totActiveLateTweens = totActiveFixedTweens = 0;
			totActiveTweeners = totActiveSequences = 0;
			_maxActiveLookupId = _reorganizeFromId = -1;
			_requiresActiveReorganization = false;
			PurgePools();
			ResetCapacities();
			totTweeners = totSequences = 0;
		}

		internal static void PurgePools()
		{
			totTweeners -= totPooledTweeners;
			totSequences -= totPooledSequences;
			ClearTweenArray(_pooledTweeners);
			_PooledSequences.Clear();
			totPooledTweeners = totPooledSequences = 0;
			_minPooledTweenerId = _maxPooledTweenerId = -1;
		}

		private static void RemoveActiveTween(Tween t)
		{
			int activeId = t.activeId;
			t.activeId = -1;
			_requiresActiveReorganization = true;
			if ((_reorganizeFromId == -1) || (_reorganizeFromId > activeId))
			{
				_reorganizeFromId = activeId;
			}
			if ( activeId == -1) 
			{
				activeId = 0;
			}
			_activeTweens[activeId] = null;
			if (t.updateType == UpdateType.Normal)
			{
				if (totActiveDefaultTweens > 0)
				{
					totActiveDefaultTweens--;
					hasActiveDefaultTweens = totActiveDefaultTweens > 0;
				}
				else
				{
					Debugger.LogRemoveActiveTweenError("totActiveDefaultTweens");
				}
			}
			else if (t.updateType == UpdateType.Fixed)
			{
				if (totActiveFixedTweens > 0)
				{
					totActiveFixedTweens--;
					hasActiveFixedTweens = totActiveFixedTweens > 0;
				}
				else
				{
					Debugger.LogRemoveActiveTweenError("totActiveFixedTweens");
				}
			}
			else if (totActiveLateTweens > 0)
			{
				totActiveLateTweens--;
				hasActiveLateTweens = totActiveLateTweens > 0;
			}
			else
			{
				Debugger.LogRemoveActiveTweenError("totActiveLateTweens");
			}
			totActiveTweens--;
			hasActiveTweens = totActiveTweens > 0;
			if (t.tweenType == TweenType.Tweener)
			{
				totActiveTweeners--;
			}
			else
			{
				totActiveSequences--;
			}
			if (totActiveTweens < 0)
			{
				totActiveTweens = 0;
				Debugger.LogRemoveActiveTweenError("totActiveTweens");
			}
			if (totActiveTweeners < 0)
			{
				totActiveTweeners = 0;
				Debugger.LogRemoveActiveTweenError("totActiveTweeners");
			}
			if (totActiveSequences < 0)
			{
				totActiveSequences = 0;
				Debugger.LogRemoveActiveTweenError("totActiveSequences");
			}
		}

		private static void ReorganizeActiveTweens()
		{
			if (totActiveTweens <= 0)
			{
				_maxActiveLookupId = -1;
				_requiresActiveReorganization = false;
				_reorganizeFromId = -1;
			}
			else if (_reorganizeFromId == _maxActiveLookupId)
			{
				_maxActiveLookupId--;
				_requiresActiveReorganization = false;
				_reorganizeFromId = -1;
			}
			else
			{
				int num = 1;
				int num2 = _maxActiveLookupId + 1;
				_maxActiveLookupId = _reorganizeFromId - 1;
				for (int i = _reorganizeFromId + 1; i < num2; i++)
				{
					Tween tween = _activeTweens[i];
					if (tween == null)
					{
						num++;
					}
					else
					{
						tween.activeId = _maxActiveLookupId = i - num;
						_activeTweens[i - num] = tween;
						_activeTweens[i] = null;
					}
				}
				_requiresActiveReorganization = false;
				_reorganizeFromId = -1;
			}
		}

		public static void ResetCapacities()
		{
			SetCapacities(200, 50);
		}

		public static bool Restart(Tween t, bool includeDelay=true)
		{
			t.isBackwards = false;
			Rewind(t, includeDelay);
			t.isPlaying = true;
			if ((!t.isPlaying && t.playedOnce) && (t.onPlay != null))
			{
				Tween.OnTweenCallback(t.onPlay);
			}
			return true;
		}

		public static bool Rewind(Tween t, bool includeDelay=true)
		{
			bool isPlaying = t.isPlaying;
			t.isPlaying = false;
			bool flag2 = false;
			if (t.delay > 0f)
			{
				if (includeDelay)
				{
					flag2 = (t.delay > 0f) && (t.elapsedDelay > 0f);
					t.elapsedDelay = 0f;
					t.delayComplete = false;
				}
				else
				{
					flag2 = t.elapsedDelay < t.delay;
					t.elapsedDelay = t.delay;
					t.delayComplete = true;
				}
			}
			if (((t.position > 0f) || (t.completedLoops > 0)) || !t.startupDone)
			{
				flag2 = true;
				if ((!Tween.DoGoto(t, 0f, 0, UpdateMode.Goto) & isPlaying) && (t.onPause != null))
				{
					Tween.OnTweenCallback(t.onPause);
				}
			}
			return flag2;
		}

		public static void SetCapacities(int tweenersCapacity, int sequencesCapacity)
		{
			if (tweenersCapacity < sequencesCapacity)
			{
				tweenersCapacity = sequencesCapacity;
			}
			maxActive = tweenersCapacity + sequencesCapacity;
			maxTweeners = tweenersCapacity;
			maxSequences = sequencesCapacity;
			Array.Resize<Tween>(ref _activeTweens, maxActive);
			Array.Resize<Tween>(ref _pooledTweeners, tweenersCapacity);
			_KillList.Capacity = maxActive;
		}

		public static void SetUpdateType(Tween t, UpdateType updateType, bool isIndependentUpdate)
		{
			if (!t.active || (t.updateType == updateType))
			{
				t.updateType = updateType;
				t.isIndependentUpdate = isIndependentUpdate;
			}
			else
			{
				if (t.updateType == UpdateType.Normal)
				{
					totActiveDefaultTweens--;
					hasActiveDefaultTweens = totActiveDefaultTweens > 0;
				}
				else if (t.updateType == UpdateType.Fixed)
				{
					totActiveFixedTweens--;
					hasActiveFixedTweens = totActiveFixedTweens > 0;
				}
				else
				{
					totActiveLateTweens--;
					hasActiveLateTweens = totActiveLateTweens > 0;
				}
				t.updateType = updateType;
				t.isIndependentUpdate = isIndependentUpdate;
				if (updateType == UpdateType.Normal)
				{
					totActiveDefaultTweens++;
					hasActiveDefaultTweens = true;
				}
				else if (updateType == UpdateType.Fixed)
				{
					totActiveFixedTweens++;
					hasActiveFixedTweens = true;
				}
				else
				{
					totActiveLateTweens++;
					hasActiveLateTweens = true;
				}
			}
		}

		public static bool SmoothRewind(Tween t)
		{
			bool flag = false;
			if (t.delay > 0f)
			{
				flag = t.elapsedDelay < t.delay;
				t.elapsedDelay = t.delay;
				t.delayComplete = true;
			}
			if (((t.position > 0f) || (t.completedLoops > 0)) || !t.startupDone)
			{
				flag = true;
				if (t.loopType == LoopType.Incremental)
				{
					t.PlayBackwards();
					return flag;
				}
				t.Goto(t.ElapsedDirectionalPercentage() * t.duration, false);
				t.PlayBackwards();
				return flag;
			}
			t.isPlaying = false;
			return flag;
		}

		public static bool TogglePause(Tween t)
		{
			if (t.isPlaying)
			{
				return Pause(t);
			}
			return Play(t);
		}

		public static int TotalPlayingTweens()
		{
			if (!hasActiveTweens)
			{
				return 0;
			}
			if (_requiresActiveReorganization)
			{
				ReorganizeActiveTweens();
			}
			int num = 0;
			for (int i = 0; i < (_maxActiveLookupId + 1); i++)
			{
				Tween tween = _activeTweens[i];
				if ((tween != null) && tween.isPlaying)
				{
					num++;
				}
			}
			return num;
		}

		public static int TotalPooledTweens()
		{
			return (totPooledTweeners + totPooledSequences);
		}

		public static void Update(UpdateType updateType, float deltaTime, float independentTime)
		{
			if (_requiresActiveReorganization)
			{
				ReorganizeActiveTweens();
			}
			isUpdateLoop = true;
			bool flag = false;
			int num = _maxActiveLookupId + 1;
			for (int i = 0; i < num; i++)
			{
				Tween t = _activeTweens[i];
				if ((t != null) && (t.updateType == updateType))
				{
					if (!t.active)
					{
						flag = true;
						MarkForKilling(t);
					}
					else if (t.isPlaying)
					{
						t.creationLocked = true;
						float num3 = (t.isIndependentUpdate ? independentTime : deltaTime) * t.timeScale;
						if (!t.delayComplete)
						{
							num3 = t.UpdateDelay(t.elapsedDelay + num3);
							if (num3 <= -1f)
							{
								flag = true;
								MarkForKilling(t);
								continue;
							}
							if (num3 <= 0f)
							{
								continue;
							}
						}
						if (!t.startupDone && !t.Startup())
						{
							flag = true;
							MarkForKilling(t);
						}
						else
						{
							float position = t.position;
							bool flag2 = position >= t.duration;
							int completedLoops = t.completedLoops;
							if (t.duration <= 0f)
							{
								position = 0f;
								completedLoops = (t.loops == -1) ? (t.completedLoops + 1) : t.loops;
							}
							else
							{
								if (t.isBackwards)
								{
									position -= num3;
									while ((position < 0f) && (completedLoops > 0))
									{
										position += t.duration;
										completedLoops--;
									}
								}
								else
								{
									position += num3;
									while ((position >= t.duration) && ((t.loops == -1) || (completedLoops < t.loops)))
									{
										position -= t.duration;
										completedLoops++;
									}
								}
								if (flag2)
								{
									completedLoops--;
								}
								if ((t.loops != -1) && (completedLoops >= t.loops))
								{
									position = t.duration;
								}
							}
							if (Tween.DoGoto(t, position, completedLoops, UpdateMode.Update))
							{
								flag = true;
								MarkForKilling(t);
							}
						}
					}
				}
			}
			if (flag)
			{
				DespawnTweens(_KillList, false);
				for (int j = _KillList.Count - 1; j > -1; j--)
				{
					RemoveActiveTween(_KillList[j]);
				}
				_KillList.Clear();
			}
			isUpdateLoop = false;
		}

		public static int Validate()
		{
			if (_requiresActiveReorganization)
			{
				ReorganizeActiveTweens();
			}
			int num = 0;
			for (int i = 0; i < (_maxActiveLookupId + 1); i++)
			{
				Tween t = _activeTweens[i];
				if (!t.Validate())
				{
					num++;
					MarkForKilling(t);
				}
			}
			if (num > 0)
			{
				DespawnTweens(_KillList, false);
				for (int j = _KillList.Count - 1; j > -1; j--)
				{
					RemoveActiveTween(_KillList[j]);
				}
				_KillList.Clear();
			}
			return num;
		}

		public static List<object> GetTweenInfo()
		{
			List<object> resultList = new List<object>();
			for (int i = _maxActiveLookupId; i > -1; i--)

			{
				Tween t = _activeTweens[i];

				if ((t == null) || !t.active)

				{

					continue;

				}

				resultList.Add(t.target);
			}
			return resultList;
		}

		// Nested Types
		public enum CapacityIncreaseMode
		{
			TweenersAndSequences,
			TweenersOnly,
			SequencesOnly
		}
	}
}