using DG.Tweening.Plugins.Core;
using DG.Tweening.Core.Enums;
using System;

namespace DG.Tweening.Core
{
	public delegate T DOGetter<out T>();
	public delegate void DOSetter<in T>(T pNewValue);

	public class TweenerCore<T1, T2, TPlugOptions> : Tweener where TPlugOptions: struct
	{
		// Fields
		private const string _TxtCantChangeSequencedValues = "You cannot change the values of a tween contained inside a Sequence";
		public T2 changeValue;
		public T2 endValue;
		public DOGetter<T1> getter;
		public TPlugOptions plugOptions;
		public DOSetter<T1> setter;
		public T2 startValue;
		internal ABSTweenPlugin<T1, T2, TPlugOptions> tweenPlugin;

		// Methods
		internal TweenerCore()
		{
			base.typeofT1 = typeof(T1);
			base.typeofT2 = typeof(T2);
			base.typeofTPlugOptions = typeof(TPlugOptions);
			base.tweenType = TweenType.Tweener;
			this.Reset();
		}

		internal override bool ApplyTween(float prevPosition, int prevCompletedLoops, int newCompletedSteps, bool useInversePosition, UpdateMode updateMode, UpdateNotice updateNotice)
		{
			float elapsed = useInversePosition ? (base.duration - base.position) : base.position;
			if (DOTween.useSafeMode)
			{
				try
				{
					this.tweenPlugin.EvaluateAndApply(this.plugOptions, this, base.isRelative, this.getter, this.setter, elapsed, this.startValue, this.changeValue, base.duration, useInversePosition, updateNotice);
					goto Label_009E;
				}
				catch
				{
					return true;
				}
			}
			this.tweenPlugin.EvaluateAndApply(this.plugOptions, this, base.isRelative, this.getter, this.setter, elapsed, this.startValue, this.changeValue, base.duration, useInversePosition, updateNotice);
			Label_009E:
			return false;
		}

		public override Tweener ChangeEndValue(object newEndValue, bool snapStartValue)
		{
			return this.ChangeEndValue(newEndValue, -1f, snapStartValue);
		}

		public override Tweener ChangeEndValue(object newEndValue, float newDuration=-1, bool snapStartValue=false)
		{
			if (base.isSequenced)
			{
				if (Debugger.logPriority >= 1)
				{
					Debugger.LogWarning("You cannot change the values of a tween contained inside a Sequence");
				}
				return this;
			}
			Type type = newEndValue.GetType();
			if (type == base.typeofT2)
			{
				return Tweener.DoChangeEndValue<T1, T2, TPlugOptions>((TweenerCore<T1, T2, TPlugOptions>) this, (T2) newEndValue, newDuration, snapStartValue);
			}
			if (Debugger.logPriority >= 1)
			{
				Debugger.LogWarning(string.Concat(new object[] { "ChangeEndValue: incorrect newEndValue type (is ", type, ", should be ", base.typeofT2, ")" }));
			}
			return this;
		}

		public override Tweener ChangeStartValue(object newStartValue, float newDuration=-1)
		{
			if (base.isSequenced)
			{
				if (Debugger.logPriority >= 1)
				{
					Debugger.LogWarning("You cannot change the values of a tween contained inside a Sequence");
				}
				return this;
			}
			Type type = newStartValue.GetType();
			if (type == base.typeofT2)
			{
				return Tweener.DoChangeStartValue<T1, T2, TPlugOptions>((TweenerCore<T1, T2, TPlugOptions>) this, (T2) newStartValue, newDuration);
			}
			if (Debugger.logPriority >= 1)
			{
				Debugger.LogWarning(string.Concat(new object[] { "ChangeStartValue: incorrect newStartValue type (is ", type, ", should be ", base.typeofT2, ")" }));
			}
			return this;
		}

		public override Tweener ChangeValues(object newStartValue, object newEndValue, float newDuration=-1)
		{
			if (base.isSequenced)
			{
				if (Debugger.logPriority >= 1)
				{
					Debugger.LogWarning("You cannot change the values of a tween contained inside a Sequence");
				}
				return this;
			}
			Type type = newStartValue.GetType();
			Type type2 = newEndValue.GetType();
			if (type != base.typeofT2)
			{
				if (Debugger.logPriority >= 1)
				{
					Debugger.LogWarning(string.Concat(new object[] { "ChangeValues: incorrect value type (is ", type, ", should be ", base.typeofT2, ")" }));
				}
				return this;
			}
			if (type2 == base.typeofT2)
			{
				return Tweener.DoChangeValues<T1, T2, TPlugOptions>((TweenerCore<T1, T2, TPlugOptions>) this, (T2) newStartValue, (T2) newEndValue, newDuration);
			}
			if (Debugger.logPriority >= 1)
			{
				Debugger.LogWarning(string.Concat(new object[] { "ChangeValues: incorrect value type (is ", type2, ", should be ", base.typeofT2, ")" }));
			}
			return this;
		}

		internal sealed override void Reset()
		{
			base.Reset();
			if (this.tweenPlugin != null)
			{
				this.tweenPlugin.Reset((TweenerCore<T1, T2, TPlugOptions>) this);
			}
			this.plugOptions = Activator.CreateInstance<TPlugOptions>();
			this.getter = null;
			this.setter = null;
			base.hasManuallySetStartValue = false;
			base.isFromAllowed = true;
		}

		internal override Tweener SetFrom(bool relative)
		{
			this.tweenPlugin.SetFrom((TweenerCore<T1, T2, TPlugOptions>) this, relative);
			base.hasManuallySetStartValue = true;
			return this;
		}

		internal override bool Startup()
		{
			return Tweener.DoStartup<T1, T2, TPlugOptions>((TweenerCore<T1, T2, TPlugOptions>) this);
		}

		internal override float UpdateDelay(float elapsed)
		{
			return Tweener.DoUpdateDelay<T1, T2, TPlugOptions>((TweenerCore<T1, T2, TPlugOptions>) this, elapsed);
		}

		internal override bool Validate()
		{
			try
			{
				this.getter();
			}
			catch
			{
				return false;
			}
			return true;
		}
	}
}