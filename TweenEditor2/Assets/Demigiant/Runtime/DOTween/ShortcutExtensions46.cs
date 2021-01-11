using UnityEngine.UI;
using UnityEngine;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DG.Tweening.Core.Enums;

namespace DG.Tweening
{
	public static class ShortcutExtensions46
	{
		// Methods
		public static Tweener DOAnchorPos(this RectTransform target, Vector2 endValue, float duration, bool snapping=false)
		{
			return DOTween.To(() => target.anchoredPosition, delegate (Vector2 x) {
				target.anchoredPosition = x;
			}, endValue, duration).SetOptions(snapping).SetTarget<Tweener>(target);
		}

		public static Tweener DOAnchorPos3D(this RectTransform target, Vector3 endValue, float duration, bool snapping=false)
		{
			return DOTween.To(() => target.anchoredPosition3D, delegate (Vector3 x) {
				target.anchoredPosition3D = x;
			}, endValue, duration).SetOptions(snapping).SetTarget<Tweener>(target);
		}

		public static Tweener DOAnchorPosX(this RectTransform target, float endValue, float duration, bool snapping=false)
		{
			return DOTween.To(() => target.anchoredPosition, delegate (Vector2 x) {
				target.anchoredPosition = x;
			}, new Vector2(endValue, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetTarget<Tweener>(target);
		}

		public static Tweener DOAnchorPosY(this RectTransform target, float endValue, float duration, bool snapping=false)
		{
			return DOTween.To(() => target.anchoredPosition, delegate (Vector2 x) {
				target.anchoredPosition = x;
			}, new Vector2(0f, endValue), duration).SetOptions(AxisConstraint.Y, snapping).SetTarget<Tweener>(target);
		}

		public static Tweener DOBlendableColor(this Graphic target, Color endValue, float duration)
		{
			endValue -= target.color;
			Color to = new Color(0f, 0f, 0f, 0f);
			return DOTween.To(() => to, delegate (Color x) {
				Color color = x - to;
				to = x;
				Graphic graphic1 = target;
				graphic1.color=graphic1.color + color;
			}, endValue, duration).Blendable<Color, Color, ColorOptions>().SetTarget<TweenerCore<Color, Color, ColorOptions>>(target);
		}

		public static Tweener DOBlendableColor(this Image target, Color endValue, float duration)
		{
			endValue -= target.color;
			Color to = new Color(0f, 0f, 0f, 0f);
			return DOTween.To(() => to, delegate (Color x) {
				Color color = x - to;
				to = x;
				Image image1 = target;
				image1.color = image1.color + color;
			}, endValue, duration).Blendable<Color, Color, ColorOptions>().SetTarget<TweenerCore<Color, Color, ColorOptions>>(target);
		}

		public static Tweener DOBlendableColor(this Text target, Color endValue, float duration)
		{
			endValue -= target.color;
			Color to = new Color(0f, 0f, 0f, 0f);
			return DOTween.To(() => to, delegate (Color x) {
				Color color = x - to;
				to = x;
				Text text1 = target;
				text1.color = (text1.color + color);
			}, endValue, duration).Blendable<Color, Color, ColorOptions>().SetTarget<TweenerCore<Color, Color, ColorOptions>>(target);
		}

		public static Tweener DOColor(this Graphic target, Color endValue, float duration)
		{
			return DOTween.To(() => target.color, delegate (Color x) {
				target.color = x;
			}, endValue, duration).SetTarget<TweenerCore<Color, Color, ColorOptions>>(target);
		}

		public static Tweener DOColor(this Image target, Color endValue, float duration)
		{
			return DOTween.To(() => target.color, delegate (Color x) {
				target.color = x;
			}, endValue, duration).SetTarget<TweenerCore<Color, Color, ColorOptions>>(target);
		}

		public static Tweener DOColor(this Outline target, Color endValue, float duration)
		{
			return DOTween.To(() => target.effectColor, delegate (Color x) {
				target.effectColor=x;
			}, endValue, duration).SetTarget<TweenerCore<Color, Color, ColorOptions>>(target);
		}

		public static Tweener DOColor(this Text target, Color endValue, float duration)
		{
			return DOTween.To(() => target.color, delegate (Color x) {
				target.color=x;
			}, endValue, duration).SetTarget<TweenerCore<Color, Color, ColorOptions>>(target);
		}

		public static Tweener DOFade(this CanvasGroup target, float endValue, float duration)
		{
			return DOTween.To(() => target.alpha, delegate (float x) {
				target.alpha = x;
			}, endValue, duration).SetTarget<TweenerCore<float, float, FloatOptions>>(target);
		}

		public static Tweener DOFade(this Graphic target, float endValue, float duration)
		{
			return DOTween.ToAlpha(() => target.color, delegate (Color x) {
				target.color=x;
			}, endValue, duration).SetTarget<Tweener>(target);
		}

		public static Tweener DOFade(this Image target, float endValue, float duration)
		{
			return DOTween.ToAlpha(() => target.color, delegate (Color x) {
				target.color=x;
			}, endValue, duration).SetTarget<Tweener>(target);
		}

		public static Tweener DOFade(this Outline target, float endValue, float duration)
		{
			return DOTween.ToAlpha(() => target.effectColor, delegate (Color x) {
				target.effectColor=x;
			}, endValue, duration).SetTarget<Tweener>(target);
		}

		public static Tweener DOFade(this Text target, float endValue, float duration)
		{
			return DOTween.ToAlpha(() => target.color, delegate (Color x) {
				target.color=x;
			}, endValue, duration).SetTarget<Tweener>(target);
		}

		public static Tweener DOFillAmount(this Image target, float endValue, float duration)
		{
			if (endValue > 1f)
			{
				endValue = 1f;
			}
			else if (endValue < 0f)
			{
				endValue = 0f;
			}
			return DOTween.To(() => target.fillAmount, delegate (float x) {
				target.fillAmount=x;
			}, endValue, duration).SetTarget<TweenerCore<float, float, FloatOptions>>(target);
		}

		public static Tweener DOFlexibleSize(this LayoutElement target, Vector2 endValue, float duration, bool snapping=false)
		{
			return DOTween.To(() => new Vector2(target.flexibleWidth, target.flexibleHeight), delegate (Vector2 x) {
				target.flexibleWidth=x.x;
				target.flexibleHeight=x.y;
			}, endValue, duration).SetOptions(snapping).SetTarget<Tweener>(target);
		}

		public static Sequence DOGradientColor(this Image target, Gradient gradient, float duration)
		{
			Sequence t = DOTween.Sequence();
			GradientColorKey[] colorKeys = gradient.colorKeys;
			int length = colorKeys.Length;
			for (int i = 0; i < length; i++)
			{
				GradientColorKey key = colorKeys[i];
				if ((i == 0) && (key.time <= 0f))
				{
					target.color=key.color;
				}
				else
				{
					float num3 = (i == (length - 1)) ? (duration - t.Duration(false)) : (duration * ((i == 0) ? key.time : (key.time - colorKeys[i - 1].time)));
					t.Append(target.DOColor(key.color, num3).SetEase<Tweener>(Ease.Linear));
				}
			}
			return t;
		}

		public static Sequence DOJumpAnchorPos(this RectTransform target, Vector2 endValue, float jumpPower, int numJumps, float duration, bool snapping=false)
		{
			if (numJumps < 1)
			{
				numJumps = 1;
			}
			float startPosY = target.anchoredPosition.y;
			float offsetY = -1f;
			bool offsetYSet = false;
			Sequence s = DOTween.Sequence().Append(DOTween.To(() => target.anchoredPosition, delegate (Vector2 x) {
				target.anchoredPosition = x;
			}, new Vector2(endValue.x, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetEase<Tweener>(Ease.Linear)).Join(DOTween.To(() => target.anchoredPosition, delegate (Vector2 x) {
				target.anchoredPosition = x;
			}, new Vector2(0f, jumpPower), duration / ((float) (numJumps * 2))).SetOptions(AxisConstraint.Y, snapping).SetEase<Tweener>(Ease.OutQuad).SetLoops<Tweener>((numJumps * 2), LoopType.Yoyo).SetRelative<Tweener>()).SetTarget<Sequence>(target).SetEase<Sequence>(DOTween.defaultEaseType);
			s.OnUpdate<Sequence>(delegate {
				if (!offsetYSet)
				{
					offsetYSet = true;
					offsetY = s.isRelative ? endValue.y : (endValue.y - startPosY);
				}
				Vector2 anchoredPosition = target.anchoredPosition;
				anchoredPosition.y += DOVirtual.EasedValue(0f, offsetY, s.ElapsedDirectionalPercentage(), Ease.OutQuad);
				target.anchoredPosition = anchoredPosition;
			});
			return s;
		}

		public static Tweener DOMinSize(this LayoutElement target, Vector2 endValue, float duration, bool snapping=false)
		{
			return DOTween.To(() => new Vector2(target.minWidth, target.minHeight), delegate (Vector2 x) {
				target.minWidth=x.x;
				target.minHeight=x.y;
			}, endValue, duration).SetOptions(snapping).SetTarget<Tweener>(target);
		}

		public static Tweener DOPreferredSize(this LayoutElement target, Vector2 endValue, float duration, bool snapping=false)
		{
			return DOTween.To(() => new Vector2(target.preferredWidth, target.preferredHeight), delegate (Vector2 x) {
				target.preferredWidth=x.x;
				target.preferredHeight=x.y;
			}, endValue, duration).SetOptions(snapping).SetTarget<Tweener>(target);
		}

		public static Tweener DOPunchAnchorPos(this RectTransform target, Vector2 punch, float duration, int vibrato=10, float elasticity=1f, bool snapping=false)
		{
			return DOTween.Punch(() => (Vector3) target.anchoredPosition, delegate (Vector3 x) {
				target.anchoredPosition = x;
			}, (Vector3) punch, duration, vibrato, elasticity).SetTarget<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(target).SetOptions(snapping);
		}

		public static Tweener DOScale(this Outline target, Vector2 endValue, float duration)
		{
			return DOTween.To(() => target.effectDistance, delegate (Vector2 x) {
				target.effectDistance=x;
			}, endValue, duration).SetTarget<TweenerCore<Vector2, Vector2, VectorOptions>>(target);
		}

		public static Tweener DOShakeAnchorPos(this RectTransform target, float duration, float strength=100f, int vibrato=10, float randomness=90f, bool snapping=false)
		{
			return DOTween.Shake(() => (Vector3) target.anchoredPosition, delegate (Vector3 x) {
				target.anchoredPosition = x;
			}, duration, strength, vibrato, randomness, true).SetTarget<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(target).SetSpecialStartupMode<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(SpecialStartupMode.SetShake).SetOptions(snapping);
		}

		public static Tweener DOShakeAnchorPos(this RectTransform target, float duration, Vector2 strength, int vibrato=10, float randomness=90f, bool snapping=false)
		{
			return DOTween.Shake(() => (Vector3) target.anchoredPosition, delegate (Vector3 x) {
				target.anchoredPosition = x;
			}, duration, (Vector3) strength, vibrato, randomness).SetTarget<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(target).SetSpecialStartupMode<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(SpecialStartupMode.SetShake).SetOptions(snapping);
		}

		public static Tweener DOSizeDelta(this RectTransform target, Vector2 endValue, float duration, bool snapping=false)
		{
			return DOTween.To(() => target.sizeDelta, delegate (Vector2 x) {
				target.sizeDelta = x;
			}, endValue, duration).SetOptions(snapping).SetTarget<Tweener>(target);
		}

		public static Tweener DOText(this Text target, string endValue, float duration, bool richTextEnabled=true, ScrambleMode scrambleMode=ScrambleMode.None, string scrambleChars=null)
		{
			return DOTween.To(() => target.text, delegate (string x) {
				target.text=x;
			}, endValue, duration).SetOptions(richTextEnabled, scrambleMode, scrambleChars).SetTarget<Tweener>(target);
		}

		public static Tweener DOValue(this Slider target, float endValue, float duration, bool snapping=false)
		{
			return DOTween.To(() => target.value, delegate (float x) {
				target.value=x;
			}, endValue, duration).SetOptions(snapping).SetTarget<Tweener>(target);
		}
	}
}
