using UnityEngine;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections.Generic;

namespace DG.Tweening
{
	public static class ShortcutExtensions43
	{
		// Methods
		public static Tweener DOBlendableColor(this SpriteRenderer target, Color endValue, float duration)
		{
			endValue -= target.color;
			Color to = new Color(0f, 0f, 0f, 0f);
			return DOTween.To(() => to, delegate (Color x) {
				Color color = x - to;
				to = x;
				SpriteRenderer renderer1 = target;
				renderer1.color += color;
			}, endValue, duration).Blendable<Color, Color, ColorOptions>().SetTarget<TweenerCore<Color, Color, ColorOptions>>(target);
		}
		public static Tweener DOColor(this SpriteRenderer target, Color endValue, float duration)
		{
			return DOTween.To(() => target.color, delegate (Color x) {
				target.color = x;
			}, endValue, duration).SetTarget<TweenerCore<Color, Color, ColorOptions>>(target);
		}
		public static Tweener DOFade(this SpriteRenderer target, float endValue, float duration)
		{
			return DOTween.ToAlpha(() => target.color, delegate (Color x) {
				target.color = x;
			}, endValue, duration).SetTarget<Tweener>(target);
		}

		public static Sequence DOGradientColor(this Material target, Gradient gradient, float duration)
		{
			Sequence t = DOTween.Sequence();
			GradientColorKey[] colorKeys = gradient.colorKeys;
			int length = colorKeys.Length;
			for (int i = 0; i < length; i++)
			{
				GradientColorKey key = colorKeys[i];
				if ((i == 0) && (key.time <= 0f))
				{
					target.color = key.color;
				}
				else
				{
					float num3 = (i == (length - 1)) ? (duration - t.Duration(false)) : (duration * ((i == 0) ? key.time : (key.time - colorKeys[i - 1].time)));
					t.Append(target.DOColor(key.color, num3).SetEase<Tweener>(Ease.Linear));
				}
			}
			return t;
		}

		public static Sequence DOGradientColor(this SpriteRenderer target, Gradient gradient, float duration)
		{
			Sequence t = DOTween.Sequence();
			GradientColorKey[] colorKeys = gradient.colorKeys;
			int length = colorKeys.Length;
			for (int i = 0; i < length; i++)
			{
				GradientColorKey key = colorKeys[i];
				if ((i == 0) && (key.time <= 0f))
				{
					target.color = key.color;
				}
				else
				{
					float num3 = (i == (length - 1)) ? (duration - t.Duration(false)) : (duration * ((i == 0) ? key.time : (key.time - colorKeys[i - 1].time)));
					t.Append(target.DOColor(key.color, num3).SetEase<Tweener>(Ease.Linear));
				}
			}
			return t;
		}

		public static Sequence DOGradientColor(this Material target, Gradient gradient, string property, float duration)
		{
			Sequence t = DOTween.Sequence();
			GradientColorKey[] colorKeys = gradient.colorKeys;
			int length = colorKeys.Length;
			for (int i = 0; i < length; i++)
			{
				GradientColorKey key = colorKeys[i];
				if ((i == 0) && (key.time <= 0f))
				{
					target.color = key.color;
				}
				else
				{
					float num3 = (i == (length - 1)) ? (duration - t.Duration(false)) : (duration * ((i == 0) ? key.time : (key.time - colorKeys[i - 1].time)));
					t.Append(target.DOColor(key.color, property, num3).SetEase<Tweener>(Ease.Linear));
				}
			}
			return t;
		}

		public static Sequence DOJump(this Rigidbody2D target, Vector2 endValue, float jumpPower, int numJumps, float duration, bool snapping=false)
		{
			if (numJumps < 1)
			{
				numJumps = 1;
			}
			float startPosY = target.position.y;
			float offsetY = -1f;
			bool offsetYSet = false;
			Sequence s = DOTween.Sequence();
			s.Append(DOTween.To(() => target.position, delegate (Vector2 x) {
				target.position = x;
			}, new Vector2(endValue.x, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetEase<Tweener>(Ease.Linear).OnUpdate<Tweener>(delegate {
				if (!offsetYSet)
				{
					offsetYSet = true;
					offsetY = s.isRelative ? endValue.y : (endValue.y - startPosY);
				}
				Vector2 position = target.position;
				position.y += DOVirtual.EasedValue(0f, offsetY, s.ElapsedDirectionalPercentage(), Ease.OutQuad);
				target.position = position;
			})).Join(DOTween.To(() => target.position, delegate (Vector2 x) {
				target.position = x;
			}, new Vector2(0f, jumpPower), duration / ((float) (numJumps * 2))).SetOptions(AxisConstraint.Y, snapping).SetEase<Tweener>(Ease.OutQuad).SetLoops<Tweener>((numJumps * 2), LoopType.Yoyo).SetRelative<Tweener>()).SetTarget<Sequence>(target).SetEase<Sequence>(DOTween.defaultEaseType);
			return s;
		}

		public static Tweener DOMove(this Rigidbody2D target, Vector2 endValue, float duration, bool snapping=false)
		{
			return DOTween.To(() => target.position, new DOSetter<Vector2>(target.MovePosition), endValue, duration).SetOptions(snapping).SetTarget<Tweener>(target);
		}

		public static Tweener DOMoveX(this Rigidbody2D target, float endValue, float duration, bool snapping=false)
		{
			return DOTween.To(() => target.position, new DOSetter<Vector2>(target.MovePosition), new Vector2(endValue, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetTarget<Tweener>(target);
		}

		public static Tweener DOMoveY(this Rigidbody2D target, float endValue, float duration, bool snapping=false)
		{
			return DOTween.To(() => target.position, new DOSetter<Vector2>(target.MovePosition), new Vector2(0f, endValue), duration).SetOptions(AxisConstraint.Y, snapping).SetTarget<Tweener>(target);
		}

		public static Tweener DORotate(this Rigidbody2D target, float endValue, float duration)
		{
			return DOTween.To(() => target.rotation, new DOSetter<float>(target.MoveRotation), endValue, duration).SetTarget<TweenerCore<float, float, FloatOptions>>(target);
		}
	}
}