using UnityEngine;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using DG.Tweening.Plugins;
using DG.Tweening.Core;
using DG.Tweening.CustomPlugins;
using DG.Tweening.Core.Enums;

namespace DG.Tweening
{
	public static class ShortcutExtensions
	{
		// Methods
		public static Tweener DOAspect(this Camera target, float endValue, float duration)
		{
			return DOTween.To(() => target.aspect, delegate (float x) {
				target.aspect = x;
			}, endValue, duration).SetTarget<TweenerCore<float, float, FloatOptions>>(target);
		}

		public static Tweener DOBlendableColor(this Light target, Color endValue, float duration)
		{
			endValue -= target.color;
			Color to = new Color(0f, 0f, 0f, 0f);
			return DOTween.To(() => to, delegate (Color x) {
				Color color = x - to;
				to = x;
				Light light1 = target;
				light1.color += color;
			}, endValue, duration).Blendable<Color, Color, ColorOptions>().SetTarget<TweenerCore<Color, Color, ColorOptions>>(target);
		}

		public static Tweener DOBlendableColor(this Material target, Color endValue, float duration)
		{
			endValue -= target.color;
			Color to = new Color(0f, 0f, 0f, 0f);
			return DOTween.To(() => to, delegate (Color x) {
				Color color = x - to;
				to = x;
				Material material1 = target;
				material1.color += color;
			}, endValue, duration).Blendable<Color, Color, ColorOptions>().SetTarget<TweenerCore<Color, Color, ColorOptions>>(target);
		}

		public static Tweener DOBlendableColor(this Material target, Color endValue, string property, float duration)
		{
			if (!target.HasProperty(property))
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogMissingMaterialProperty(property);
				}
				return null;
			}
			endValue -= target.color;
			Color to = new Color(0f, 0f, 0f, 0f);
			return DOTween.To(() => to, delegate (Color x) {
				Color color = x - to;
				to = x;
				target.SetColor(property, target.GetColor(property) + color);
			}, endValue, duration).Blendable<Color, Color, ColorOptions>().SetTarget<TweenerCore<Color, Color, ColorOptions>>(target);
		}

		public static Tweener DOBlendableLocalMoveBy(this Transform target, Vector3 byValue, float duration, bool snapping=false)
		{
			Vector3 to = Vector3.zero;
			return DOTween.To(() => to, delegate (Vector3 x) {
				Vector3 vector = x - to;
				to = x;
				Transform transform1 = target;
				transform1.localPosition += vector;
			}, byValue, duration).Blendable<Vector3, Vector3, VectorOptions>().SetOptions(snapping).SetTarget<Tweener>(target);
		}

		public static Tweener DOBlendableLocalRotateBy(this Transform target, Vector3 byValue, float duration, RotateMode mode=RotateMode.Fast)
		{
			Quaternion to = target.localRotation;
			TweenerCore<Quaternion, Vector3, QuaternionOptions> local1 = DOTween.To(() => to, delegate (Quaternion x) {
				Quaternion quaternion = x * Quaternion.Inverse(to);
				to = x;
				target.localRotation = ((target.localRotation * Quaternion.Inverse(target.localRotation)) * quaternion) * target.localRotation;
			}, byValue, duration).Blendable<Quaternion, Vector3, QuaternionOptions>().SetTarget<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(target);
			local1.plugOptions.rotateMode = mode;
			return local1;
		}

		public static Tweener DOBlendableMoveBy(this Transform target, Vector3 byValue, float duration, bool snapping=false)
		{
			Vector3 to = Vector3.zero;
			return DOTween.To(() => to, delegate (Vector3 x) {
				Vector3 vector = x - to;
				to = x;
				Transform transform1 = target;
				transform1.position += vector;
			}, byValue, duration).Blendable<Vector3, Vector3, VectorOptions>().SetOptions(snapping).SetTarget<Tweener>(target);
		}

		public static Tweener DOBlendableRotateBy(this Transform target, Vector3 byValue, float duration, RotateMode mode=RotateMode.Fast)
		{
			Quaternion to = target.rotation;
			TweenerCore<Quaternion, Vector3, QuaternionOptions> local1 = DOTween.To(() => to, delegate (Quaternion x) {
				Quaternion quaternion = x * Quaternion.Inverse(to);
				to = x;
				target.rotation = ((target.rotation * Quaternion.Inverse(target.rotation)) * quaternion) * target.rotation;
			}, byValue, duration).Blendable<Quaternion, Vector3, QuaternionOptions>().SetTarget<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(target);
			local1.plugOptions.rotateMode = mode;
			return local1;
		}

		public static Tweener DOBlendableScaleBy(this Transform target, Vector3 byValue, float duration)
		{
			Vector3 to = Vector3.zero;
			return DOTween.To(() => to, delegate (Vector3 x) {
				Vector3 vector = x - to;
				to = x;
				Transform transform1 = target;
				transform1.localScale += vector;
			}, byValue, duration).Blendable<Vector3, Vector3, VectorOptions>().SetTarget<TweenerCore<Vector3, Vector3, VectorOptions>>(target);
		}

		public static Tweener DOColor(this Camera target, Color endValue, float duration)
		{
			return DOTween.To(() => target.backgroundColor, delegate (Color x) {
				target.backgroundColor = x;
			}, endValue, duration).SetTarget<TweenerCore<Color, Color, ColorOptions>>(target);
		}

		public static Tweener DOColor(this Light target, Color endValue, float duration)
		{
			return DOTween.To(() => target.color, delegate (Color x) {
				target.color = x;
			}, endValue, duration).SetTarget<TweenerCore<Color, Color, ColorOptions>>(target);
		}

		public static Tweener DOColor(this Material target, Color endValue, float duration)
		{
			return DOTween.To(() => target.color, delegate (Color x) {
				target.color = x;
			}, endValue, duration).SetTarget<TweenerCore<Color, Color, ColorOptions>>(target);
		}

		public static Tweener DOColor(this LineRenderer target, Color2 startValue, Color2 endValue, float duration)
		{
			return DOTween.To(() => startValue, delegate (Color2 x) {
				//target.SetColors(x.ca, x.cb);
				target.startColor = x.ca;
				target.endColor = x.cb;
			}, endValue, duration).SetTarget<TweenerCore<Color2, Color2, ColorOptions>>(target);
		}

		public static Tweener DOColor(this Material target, Color endValue, string property, float duration)
		{
			if (target.HasProperty(property))
			{
				return DOTween.To(() => target.GetColor(property), delegate (Color x) {
					target.SetColor(property, x);
				}, endValue, duration).SetTarget<TweenerCore<Color, Color, ColorOptions>>(target);
			}
			if (Debugger.logPriority > 0)
			{
				Debugger.LogMissingMaterialProperty(property);
			}
			return null;
		}

		public static int DOComplete(this Component target, bool withCallbacks=false)
		{
			return DOTween.Complete(target, withCallbacks);
		}

		public static int DOComplete(this Material target, bool withCallbacks=false)
		{
			return DOTween.Complete(target, withCallbacks);
		}

		public static Tweener DOFade(this AudioSource target, float endValue, float duration)
		{
			if (endValue < 0f)
			{
				endValue = 0f;
			}
			else if (endValue > 1f)
			{
				endValue = 1f;
			}
			return DOTween.To(() => target.volume, delegate (float x) {
				target.volume = x;
			}, endValue, duration).SetTarget<TweenerCore<float, float, FloatOptions>>(target);
		}

		public static Tweener DOFade(this Material target, float endValue, float duration)
		{
			return DOTween.ToAlpha(() => target.color, delegate (Color x) {
				target.color = x;
			}, endValue, duration).SetTarget<Tweener>(target);
		}

		public static Tweener DOFade(this Material target, float endValue, string property, float duration)
		{
			if (target.HasProperty(property))
			{
				return DOTween.ToAlpha(() => target.GetColor(property), delegate (Color x) {
					target.SetColor(property, x);
				}, endValue, duration).SetTarget<Tweener>(target);
			}
			if (Debugger.logPriority > 0)
			{
				Debugger.LogMissingMaterialProperty(property);
			}
			return null;
		}

		public static Tweener DOFarClipPlane(this Camera target, float endValue, float duration)
		{
			return DOTween.To(() => target.farClipPlane, delegate (float x) {
				target.farClipPlane = x;
			}, endValue, duration).SetTarget<TweenerCore<float, float, FloatOptions>>(target);
		}

		public static Tweener DOFieldOfView(this Camera target, float endValue, float duration)
		{
			return DOTween.To(() => target.fieldOfView, delegate (float x) {
				target.fieldOfView = x;
			}, endValue, duration).SetTarget<TweenerCore<float, float, FloatOptions>>(target);
		}

		public static int DOFlip(this Component target)
		{
			return DOTween.Flip(target);
		}

		public static int DOFlip(this Material target)
		{
			return DOTween.Flip(target);
		}

		public static Tweener DOFloat(this Material target, float endValue, string property, float duration)
		{
			if (target.HasProperty(property))
			{
				return DOTween.To(() => target.GetFloat(property), delegate (float x) {
					target.SetFloat(property, x);
				}, endValue, duration).SetTarget<TweenerCore<float, float, FloatOptions>>(target);
			}
			if (Debugger.logPriority > 0)
			{
				Debugger.LogMissingMaterialProperty(property);
			}
			return null;
		}

		public static int DOGoto(this Component target, float to, bool andPlay=false)
		{
			return DOTween.Goto(target, to, andPlay);
		}

		public static int DOGoto(this Material target, float to, bool andPlay=false)
		{
			return DOTween.Goto(target, to, andPlay);
		}

		public static Tweener DOIntensity(this Light target, float endValue, float duration)
		{
			return DOTween.To(() => target.intensity, delegate (float x) {
				target.intensity = x;
			}, endValue, duration).SetTarget<TweenerCore<float, float, FloatOptions>>(target);
		}

		public static Sequence DOJump(this Rigidbody target, Vector3 endValue, float jumpPower, int numJumps, float duration, bool snapping=false)
		{
			if (numJumps < 1)
			{
				numJumps = 1;
			}
			float startPosY = target.position.y;
			float offsetY = -1f;
			bool offsetYSet = false;
			Sequence s = DOTween.Sequence();
			s.Append(DOTween.To(() => target.position, new DOSetter<Vector3>(target.MovePosition), new Vector3(endValue.x, 0f, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetEase<Tweener>(Ease.Linear).OnUpdate<Tweener>(delegate {
				if (!offsetYSet)
				{
					offsetYSet = true;
					offsetY = s.isRelative ? endValue.y : (endValue.y - startPosY);
				}
				Vector3 position = target.position;
				position.y += DOVirtual.EasedValue(0f, offsetY, s.ElapsedDirectionalPercentage(), Ease.OutQuad);
				target.MovePosition(position);
			})).Join(DOTween.To(() => target.position, new DOSetter<Vector3>(target.MovePosition), new Vector3(0f, 0f, endValue.z), duration).SetOptions(AxisConstraint.Z, snapping).SetEase<Tweener>(Ease.Linear)).Join(DOTween.To(() => target.position, new DOSetter<Vector3>(target.MovePosition), new Vector3(0f, jumpPower, 0f), duration / ((float) (numJumps * 2))).SetOptions(AxisConstraint.Y, snapping).SetEase<Tweener>(Ease.OutQuad).SetLoops<Tweener>((numJumps * 2), LoopType.Yoyo).SetRelative<Tweener>()).SetTarget<Sequence>(target).SetEase<Sequence>(DOTween.defaultEaseType);
			return s;
		}

		public static Sequence DOJump(this Transform target, Vector3 endValue, float jumpPower, int numJumps, float duration, bool snapping=false)
		{
			if (numJumps < 1)
			{
				numJumps = 1;
			}
			float startPosY = target.position.y;
			float offsetY = -1f;
			bool offsetYSet = false;
			Sequence s = DOTween.Sequence();
			s.Append(DOTween.To(() => target.position, delegate (Vector3 x) {
				target.position = x;
			}, new Vector3(endValue.x, 0f, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetEase<Tweener>(Ease.Linear).OnUpdate<Tweener>(delegate {
				if (!offsetYSet)
				{
					offsetYSet = true;
					offsetY = s.isRelative ? endValue.y : (endValue.y - startPosY);
				}
				Vector3 position = target.position;
				position.y += DOVirtual.EasedValue(0f, offsetY, s.ElapsedDirectionalPercentage(), Ease.OutQuad);
				target.position = position;
			})).Join(DOTween.To(() => target.position, delegate (Vector3 x) {
				target.position = x;
			}, new Vector3(0f, 0f, endValue.z), duration).SetOptions(AxisConstraint.Z, snapping).SetEase<Tweener>(Ease.Linear)).Join(DOTween.To(() => target.position, delegate (Vector3 x) {
				target.position = x;
			}, new Vector3(0f, jumpPower, 0f), duration / ((float) (numJumps * 2))).SetOptions(AxisConstraint.Y, snapping).SetEase<Tweener>(Ease.OutQuad).SetRelative<Tweener>().SetLoops<Tweener>((numJumps * 2), LoopType.Yoyo)).SetTarget<Sequence>(target).SetEase<Sequence>(DOTween.defaultEaseType);
			return s;
		}

		public static int DOKill(this Component target, bool complete=false)
		{
			return DOTween.Kill(target, complete);
		}

		public static int DOKill(this Material target, bool complete=false)
		{
			return DOTween.Kill(target, complete);
		}

		public static Sequence DOLocalJump(this Transform target, Vector3 endValue, float jumpPower, int numJumps, float duration, bool snapping=false)
		{
			if (numJumps < 1)
			{
				numJumps = 1;
			}
			float startPosY = target.localPosition.y;
			float offsetY = -1f;
			bool offsetYSet = false;
			Sequence s = DOTween.Sequence();
			s.Append(DOTween.To(() => target.localPosition, delegate (Vector3 x) {
				target.localPosition = x;
			}, new Vector3(endValue.x, 0f, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetEase<Tweener>(Ease.Linear).OnUpdate<Tweener>(delegate {
				if (!offsetYSet)
				{
					offsetYSet = false;
					offsetY = s.isRelative ? endValue.y : (endValue.y - startPosY);
				}
				Vector3 localPosition = target.localPosition;
				localPosition.y += DOVirtual.EasedValue(0f, offsetY, s.ElapsedDirectionalPercentage(), Ease.OutQuad);
				target.localPosition = localPosition;
			})).Join(DOTween.To(() => target.localPosition, delegate (Vector3 x) {
				target.localPosition = x;
			}, new Vector3(0f, 0f, endValue.z), duration).SetOptions(AxisConstraint.Z, snapping).SetEase<Tweener>(Ease.Linear)).Join(DOTween.To(() => target.localPosition, delegate (Vector3 x) {
				target.localPosition = x;
			}, new Vector3(0f, jumpPower, 0f), duration / ((float) (numJumps * 2))).SetOptions(AxisConstraint.Y, snapping).SetEase<Tweener>(Ease.OutQuad).SetRelative<Tweener>().SetLoops<Tweener>((numJumps * 2), LoopType.Yoyo)).SetTarget<Sequence>(target).SetEase<Sequence>(DOTween.defaultEaseType);
			return s;
		}

		public static Tweener DOLocalMove(this Transform target, Vector3 endValue, float duration, bool snapping=false)
		{
			return DOTween.To(() => target.localPosition, delegate (Vector3 x) {
				if (x.x != float.NaN && x.y != float.NaN && x.z != float.NaN)
					target.localPosition = x;
				else
					Debug.LogWarningFormat("DOlocalMove for '{0}' is not valid. localPosition is {1}.", target.name, x);
			}, endValue, duration).SetOptions(snapping).SetTarget<Tweener>(target);
		}

		public static Tweener DOLocalMoveX(this Transform target, float endValue, float duration, bool snapping=false)
		{
			return DOTween.To(() => target.localPosition, delegate (Vector3 x) {
				target.localPosition = x;
			}, new Vector3(endValue, 0f, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetTarget<Tweener>(target);
		}

		public static Tweener DOLocalMoveY(this Transform target, float endValue, float duration, bool snapping=false)
		{
			return DOTween.To(() => target.localPosition, delegate (Vector3 x) {
				target.localPosition = x;
			}, new Vector3(0f, endValue, 0f), duration).SetOptions(AxisConstraint.Y, snapping).SetTarget<Tweener>(target);
		}

		public static Tweener DOLocalMoveZ(this Transform target, float endValue, float duration, bool snapping=false)
		{
			return DOTween.To(() => target.localPosition, delegate (Vector3 x) {
				target.localPosition = x;
			}, new Vector3(0f, 0f, endValue), duration).SetOptions(AxisConstraint.Z, snapping).SetTarget<Tweener>(target);
		}

		public static TweenerCore<Vector3, Path, PathOptions> DOLocalPath(this Transform target, Path path, float duration, PathMode pathMode=PathMode.Full3D)
		{
			TweenerCore<Vector3, Path, PathOptions> local1 = DOTween.To<Vector3, Path, PathOptions>(PathPlugin.Get(), () => target.localPosition, delegate (Vector3 x) {
				target.localPosition = x;
			}, path, duration).SetTarget<TweenerCore<Vector3, Path, PathOptions>>(target);
			local1.plugOptions.mode = pathMode;
			local1.plugOptions.useLocalPosition = true;
			return local1;
		}

		public static TweenerCore<Vector3, Path, PathOptions> DOLocalPath(this Transform target, Vector3[] path, float duration, PathType pathType=PathType.Linear, PathMode pathMode=PathMode.Full3D, int resolution=10, Color? gizmoColor = null)
		{
			if (resolution < 1)
			{
				resolution = 1;
			}
			Path temp = new Path (pathType, path, resolution, gizmoColor);
			TweenerCore<Vector3, Path, PathOptions> local1 = DOTween.To<Vector3, Path, PathOptions>(PathPlugin.Get(), () => target.localPosition, delegate (Vector3 x) {
				target.localPosition = x;
			}, temp, duration).SetTarget<TweenerCore<Vector3, Path, PathOptions>>(target);
			local1.plugOptions.mode = pathMode;
			local1.plugOptions.useLocalPosition = true;
			return local1;
		}

		public static Tweener DOLocalRotate(this Transform target, Vector3 endValue, float duration, RotateMode mode=RotateMode.Fast)
		{
			TweenerCore<Quaternion, Vector3, QuaternionOptions> t = DOTween.To(() => target.localRotation, delegate (Quaternion x) {
				target.localRotation = x;
			}, endValue, duration);
			t.SetTarget<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(target);
			t.plugOptions.rotateMode = mode;
			return t;
		}

		public static Tweener DOLocalRotateQuaternion(this Transform target, Quaternion endValue, float duration)
		{
			TweenerCore<Quaternion, Quaternion, NoOptions> t = DOTween.To<Quaternion, Quaternion, NoOptions>(PureQuaternionPlugin.Plug(), () => target.localRotation, delegate (Quaternion x) {
				target.localRotation = x;
			}, endValue, duration);
			t.SetTarget<TweenerCore<Quaternion, Quaternion, NoOptions>>(target);
			return t;
		}

		public static Tweener DOLookAt(this Rigidbody target, Vector3 towards, float duration, AxisConstraint axisConstraint=AxisConstraint.None, Vector3? up=null)
		{
			TweenerCore<Quaternion, Vector3, QuaternionOptions> local1 = DOTween.To(() => target.rotation, new DOSetter<Quaternion>(target.MoveRotation), towards, duration).SetTarget<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(target).SetSpecialStartupMode<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(SpecialStartupMode.SetLookAt);
			local1.plugOptions.axisConstraint = axisConstraint;
			local1.plugOptions.up = !up.HasValue ? Vector3.up : up.Value;
			return local1;
		}

		public static Tweener DOLookAt(this Transform target, Vector3 towards, float duration, AxisConstraint axisConstraint=AxisConstraint.None, Vector3? up=null)
		{
			TweenerCore<Quaternion, Vector3, QuaternionOptions> local1 = DOTween.To(() => target.rotation, delegate (Quaternion x) {
				target.rotation = x;
			}, towards, duration).SetTarget<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(target).SetSpecialStartupMode<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(SpecialStartupMode.SetLookAt);
			local1.plugOptions.axisConstraint = axisConstraint;
			local1.plugOptions.up = !up.HasValue ? Vector3.up : up.Value;
			return local1;
		}

		public static Tweener DOMove(this Rigidbody target, Vector3 endValue, float duration, bool snapping=false)
		{
			return DOTween.To(() => target.position, new DOSetter<Vector3>(target.MovePosition), endValue, duration).SetOptions(snapping).SetTarget<Tweener>(target);
		}

		public static Tweener DOMove(this Transform target, Vector3 endValue, float duration, bool snapping=false)
		{
			return DOTween.To(() => target.position, delegate (Vector3 x) {
				target.position = x;
			}, endValue, duration).SetOptions(snapping).SetTarget<Tweener>(target);
		}

		public static Tweener DOMoveX(this Rigidbody target, float endValue, float duration, bool snapping=false)
		{
			return DOTween.To(() => target.position, new DOSetter<Vector3>(target.MovePosition), new Vector3(endValue, 0f, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetTarget<Tweener>(target);
		}

		public static Tweener DOMoveX(this Transform target, float endValue, float duration, bool snapping=false)
		{
			return DOTween.To(() => target.position, delegate (Vector3 x) {
				target.position = x;
			}, new Vector3(endValue, 0f, 0f), duration).SetOptions(AxisConstraint.X, snapping).SetTarget<Tweener>(target);
		}

		public static Tweener DOMoveY(this Rigidbody target, float endValue, float duration, bool snapping=false)
		{
			return DOTween.To(() => target.position, new DOSetter<Vector3>(target.MovePosition), new Vector3(0f, endValue, 0f), duration).SetOptions(AxisConstraint.Y, snapping).SetTarget<Tweener>(target);
		}

		public static Tweener DOMoveY(this Transform target, float endValue, float duration, bool snapping=false)
		{
			return DOTween.To(() => target.position, delegate (Vector3 x) {
				target.position = x;
			}, new Vector3(0f, endValue, 0f), duration).SetOptions(AxisConstraint.Y, snapping).SetTarget<Tweener>(target);
		}

		public static Tweener DOMoveZ(this Rigidbody target, float endValue, float duration, bool snapping=false)
		{
			return DOTween.To(() => target.position, new DOSetter<Vector3>(target.MovePosition), new Vector3(0f, 0f, endValue), duration).SetOptions(AxisConstraint.Z, snapping).SetTarget<Tweener>(target);
		}

		public static Tweener DOMoveZ(this Transform target, float endValue, float duration, bool snapping=false)
		{
			return DOTween.To(() => target.position, delegate (Vector3 x) {
				target.position = x;
			}, new Vector3(0f, 0f, endValue), duration).SetOptions(AxisConstraint.Z, snapping).SetTarget<Tweener>(target);
		}

		public static Tweener DONearClipPlane(this Camera target, float endValue, float duration)
		{
			return DOTween.To(() => target.nearClipPlane, delegate (float x) {
				target.nearClipPlane = x;
			}, endValue, duration).SetTarget<TweenerCore<float, float, FloatOptions>>(target);
		}

		public static Tweener DOOffset(this Material target, Vector2 endValue, float duration)
		{
			return DOTween.To(() => target.mainTextureOffset, delegate (Vector2 x) {
				target.mainTextureOffset = x;
			}, endValue, duration).SetTarget<TweenerCore<Vector2, Vector2, VectorOptions>>(target);
		}

		public static Tweener DOOffset(this Material target, Vector2 endValue, string property, float duration)
		{
			if (target.HasProperty(property))
			{
				return DOTween.To(() => target.GetTextureOffset(property), delegate (Vector2 x) {
					target.SetTextureOffset(property, x);
				}, endValue, duration).SetTarget<TweenerCore<Vector2, Vector2, VectorOptions>>(target);
			}
			if (Debugger.logPriority > 0)
			{
				Debugger.LogMissingMaterialProperty(property);
			}
			return null;
		}

		public static Tweener DOOrthoSize(this Camera target, float endValue, float duration)
		{
			return DOTween.To(() => target.orthographicSize, delegate (float x) {
				target.orthographicSize = x;
			}, endValue, duration).SetTarget<TweenerCore<float, float, FloatOptions>>(target);
		}

		public static TweenerCore<Vector3, Path, PathOptions> DOPath(this Transform target, Path path, float duration, PathMode pathMode= PathMode.Full3D)
		{
			TweenerCore<Vector3, Path, PathOptions> local1 = DOTween.To<Vector3, Path, PathOptions>(PathPlugin.Get(), () => target.position, delegate (Vector3 x) {
				target.position = x;
			}, path, duration).SetTarget<TweenerCore<Vector3, Path, PathOptions>>(target);
			local1.plugOptions.mode = pathMode;
			return local1;
		}

		public static TweenerCore<Vector3, Path, PathOptions> DOPath(this Transform target, Vector3[] path, float duration, PathType pathType=PathType.Linear, PathMode pathMode=PathMode.Full3D, int resolution=10, Color gizmoColor=default(Color))
		{
			if (resolution < 1)
			{
				resolution = 1;
			}
			TweenerCore<Vector3, Path, PathOptions> local1 = DOTween.To<Vector3, Path, PathOptions>(PathPlugin.Get(), () => target.position, delegate (Vector3 x) {
				target.position = x;
			}, new Path(pathType, path, resolution, gizmoColor), duration).SetTarget<TweenerCore<Vector3, Path, PathOptions>>(target);
			local1.plugOptions.mode = pathMode;
			return local1;
		}

		public static int DOPause(this Component target)
		{
			return DOTween.Pause(target);
		}

		public static int DOPause(this Material target)
		{
			return DOTween.Pause(target);
		}

		public static Tweener DOPitch(this AudioSource target, float endValue, float duration)
		{
			return DOTween.To(() => target.pitch, delegate (float x) {
				target.pitch = x;
			}, endValue, duration).SetTarget<TweenerCore<float, float, FloatOptions>>(target);
		}

		public static Tweener DOPixelRect(this Camera target, Rect endValue, float duration)
		{
			return DOTween.To(() => target.pixelRect, delegate (Rect x) {
				target.pixelRect = x;
			}, endValue, duration).SetTarget<TweenerCore<Rect, Rect, RectOptions>>(target);
		}

		public static int DOPlay(this Component target)
		{
			return DOTween.Play(target);
		}

		public static int DOPlay(this Material target)
		{
			return DOTween.Play(target);
		}

		public static int DOPlayBackwards(this Component target)
		{
			return DOTween.PlayBackwards(target);
		}

		public static int DOPlayBackwards(this Material target)
		{
			return DOTween.PlayBackwards(target);
		}

		public static int DOPlayForward(this Component target)
		{
			return DOTween.PlayForward(target);
		}

		public static int DOPlayForward(this Material target)
		{
			return DOTween.PlayForward(target);
		}

		public static Tweener DOPunchPosition(this Transform target, Vector3 punch, float duration, int vibrato=10, float elasticity=1f, bool snapping=false)
		{
			return DOTween.Punch(() => target.localPosition, delegate (Vector3 x) {
				target.localPosition = x;
			}, punch, duration, vibrato, elasticity).SetTarget<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(target).SetOptions(snapping);
		}

		public static Tweener DOPunchRotation(this Transform target, Vector3 punch, float duration, int vibrato=10, float elasticity=1f)
		{
			return DOTween.Punch(() => target.localEulerAngles, delegate (Vector3 x) {
				target.localRotation = Quaternion.Euler(x);
			}, punch, duration, vibrato, elasticity).SetTarget<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(target);
		}

		public static Tweener DOPunchScale(this Transform target, Vector3 punch, float duration, int vibrato=10, float elasticity=1f)
		{
			return DOTween.Punch(() => target.localScale, delegate (Vector3 x) {
				target.localScale = x;
			}, punch, duration, vibrato, elasticity).SetTarget<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(target);
		}

		public static Tweener DORect(this Camera target, Rect endValue, float duration)
		{
			return DOTween.To(() => target.rect, delegate (Rect x) {
				target.rect = x;
			}, endValue, duration).SetTarget<TweenerCore<Rect, Rect, RectOptions>>(target);
		}

		public static Tweener DOResize(this TrailRenderer target, float toStartWidth, float toEndWidth, float duration)
		{
			return DOTween.To(() => new Vector2(target.startWidth, target.endWidth), delegate (Vector2 x) {
				target.startWidth = x.x;
				target.endWidth = x.y;
			}, new Vector2(toStartWidth, toEndWidth), duration).SetTarget<TweenerCore<Vector2, Vector2, VectorOptions>>(target);
		}

		public static int DORestart(this Component target, bool includeDelay=true)
		{
			return DOTween.Restart(target, includeDelay);
		}

		public static int DORestart(this Material target, bool includeDelay=true)
		{
			return DOTween.Restart(target, includeDelay);
		}

		public static int DORewind(this Component target, bool includeDelay=true)
		{
			return DOTween.Rewind(target, includeDelay);
		}

		public static int DORewind(this Material target, bool includeDelay=true)
		{
			return DOTween.Rewind(target, includeDelay);
		}

		public static Tweener DORotate(this Rigidbody target, Vector3 endValue, float duration,RotateMode mode=0)
		{
			TweenerCore<Quaternion, Vector3, QuaternionOptions> t = DOTween.To(() => target.rotation, new DOSetter<Quaternion>(target.MoveRotation), endValue, duration);
			t.SetTarget<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(target);
			t.plugOptions.rotateMode = mode;
			return t;
		}

		public static Tweener DORotate(this Transform target, Vector3 endValue, float duration, RotateMode mode=0)
		{
			TweenerCore<Quaternion, Vector3, QuaternionOptions> t = DOTween.To(() => target.rotation, delegate (Quaternion x) {
				target.rotation = x;
			}, endValue, duration);
			t.SetTarget<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(target);
			t.plugOptions.rotateMode = mode;
			return t;
		}

		public static Tweener DORotateQuaternion(this Transform target, Quaternion endValue, float duration)
		{
			TweenerCore<Quaternion, Quaternion, NoOptions> t = DOTween.To<Quaternion, Quaternion, NoOptions>(PureQuaternionPlugin.Plug(), () => target.rotation, delegate (Quaternion x) {
				target.rotation = x;
			}, endValue, duration);
			t.SetTarget<TweenerCore<Quaternion, Quaternion, NoOptions>>(target);
			return t;
		}

		public static Tweener DOScale(this Transform target, float endValue, float duration)
		{
			Vector3 vector = new Vector3(endValue, endValue, endValue);
			return DOTween.To(() => target.localScale, delegate (Vector3 x) {
				target.localScale = x;
			}, vector, duration).SetTarget<TweenerCore<Vector3, Vector3, VectorOptions>>(target);
		}

		public static Tweener DOScale(this Transform target, Vector3 endValue, float duration)
		{
			return DOTween.To(() => target.localScale, delegate (Vector3 x) {
				target.localScale = x;
			}, endValue, duration).SetTarget<TweenerCore<Vector3, Vector3, VectorOptions>>(target);
		}

		public static Tweener DOScaleX(this Transform target, float endValue, float duration)
		{
			return DOTween.To(() => target.localScale, delegate (Vector3 x) {
				target.localScale = x;
			}, new Vector3(endValue, 0f, 0f), duration).SetOptions(AxisConstraint.X, false).SetTarget<Tweener>(target);
		}

		public static Tweener DOScaleY(this Transform target, float endValue, float duration)
		{
			return DOTween.To(() => target.localScale, delegate (Vector3 x) {
				target.localScale = x;
			}, new Vector3(0f, endValue, 0f), duration).SetOptions(AxisConstraint.Y, false).SetTarget<Tweener>(target);
		}

		public static Tweener DOScaleZ(this Transform target, float endValue, float duration)
		{
			return DOTween.To(() => target.localScale, delegate (Vector3 x) {
				target.localScale = x;
			}, new Vector3(0f, 0f, endValue), duration).SetOptions(AxisConstraint.Z, false).SetTarget<Tweener>(target);
		}

		public static Tweener DOShadowStrength(this Light target, float endValue, float duration)
		{
			return DOTween.To(() => target.shadowStrength, delegate (float x) {
				target.shadowStrength = x;
			}, endValue, duration).SetTarget<TweenerCore<float, float, FloatOptions>>(target);
		}

		public static Tweener DOShakePosition(this Camera target, float duration, float strength=3f, int vibrato=10, float randomness=90f)
		{
			return DOTween.Shake(() => target.transform.localPosition, delegate (Vector3 x) {
				target.transform.localPosition = x;
			}, duration, strength, vibrato, randomness, true).SetTarget<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(target).SetSpecialStartupMode<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(SpecialStartupMode.SetCameraShakePosition);
		}

		public static Tweener DOShakePosition(this Camera target, float duration, Vector3 strength, int vibrato=10, float randomness=90f)
		{
			return DOTween.Shake(() => target.transform.localPosition, delegate (Vector3 x) {
				target.transform.localPosition = x;
			}, duration, strength, vibrato, randomness).SetTarget<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(target).SetSpecialStartupMode<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(SpecialStartupMode.SetCameraShakePosition);
		}

		public static Tweener DOShakePosition(this Transform target, float duration, float strength=1f, int vibrato=10, float randomness=90f, bool snapping=false)
		{
			return DOTween.Shake(() => target.localPosition, delegate (Vector3 x) {
				target.localPosition = x;
			}, duration, strength, vibrato, randomness, false).SetTarget<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(target).SetSpecialStartupMode<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(SpecialStartupMode.SetShake).SetOptions(snapping);
		}

		public static Tweener DOShakePosition(this Transform target, float duration, Vector3 strength, int vibrato=10, float randomness=90f, bool snapping=false)
		{
			return DOTween.Shake(() => target.localPosition, delegate (Vector3 x) {
				target.localPosition = x;
			}, duration, strength, vibrato, randomness).SetTarget<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(target).SetSpecialStartupMode<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(SpecialStartupMode.SetShake).SetOptions(snapping);
		}

		public static Tweener DOShakeRotation(this Camera target, float duration, float strength=90f, int vibrato=10, float randomness=90f)
		{
			return DOTween.Shake(() => target.transform.localEulerAngles, delegate (Vector3 x) {
				target.transform.localRotation = Quaternion.Euler(x);
			}, duration, strength, vibrato, randomness, false).SetTarget<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(target).SetSpecialStartupMode<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(SpecialStartupMode.SetShake);
		}

		public static Tweener DOShakeRotation(this Camera target, float duration, Vector3 strength, int vibrato=10, float randomness=90f)
		{
			return DOTween.Shake(() => target.transform.localEulerAngles, delegate (Vector3 x) {
				target.transform.localRotation = Quaternion.Euler(x);
			}, duration, strength, vibrato, randomness).SetTarget<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(target).SetSpecialStartupMode<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(SpecialStartupMode.SetShake);
		}

		public static Tweener DOShakeRotation(this Transform target, float duration, float strength=90f, int vibrato=10, float randomness=90f)
		{
			return DOTween.Shake(() => target.localEulerAngles, delegate (Vector3 x) {
				target.localRotation = Quaternion.Euler(x);
			}, duration, strength, vibrato, randomness, false).SetTarget<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(target).SetSpecialStartupMode<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(SpecialStartupMode.SetShake);
		}

		public static Tweener DOShakeRotation(this Transform target, float duration, Vector3 strength, int vibrato=10, float randomness=90f)
		{
			return DOTween.Shake(() => target.localEulerAngles, delegate (Vector3 x) {
				target.localRotation = Quaternion.Euler(x);
			}, duration, strength, vibrato, randomness).SetTarget<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(target).SetSpecialStartupMode<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(SpecialStartupMode.SetShake);
		}

		public static Tweener DOShakeScale(this Transform target, float duration, float strength=1f, int vibrato=10, float randomness=90f)
		{
			return DOTween.Shake(() => target.localScale, delegate (Vector3 x) {
				target.localScale = x;
			}, duration, strength, vibrato, randomness, false).SetTarget<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(target).SetSpecialStartupMode<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(SpecialStartupMode.SetShake);
		}

		public static Tweener DOShakeScale(this Transform target, float duration, Vector3 strength, int vibrato=10, float randomness=90f)
		{
			return DOTween.Shake(() => target.localScale, delegate (Vector3 x) {
				target.localScale = x;
			}, duration, strength, vibrato, randomness).SetTarget<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(target).SetSpecialStartupMode<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(SpecialStartupMode.SetShake);
		}

		public static int DOSmoothRewind(this Component target)
		{
			return DOTween.SmoothRewind(target);
		}

		public static int DOSmoothRewind(this Material target)
		{
			return DOTween.SmoothRewind(target);
		}

		public static Tweener DOTiling(this Material target, Vector2 endValue, float duration)
		{
			return DOTween.To(() => target.mainTextureScale, delegate (Vector2 x) {
				target.mainTextureScale = x;
			}, endValue, duration).SetTarget<TweenerCore<Vector2, Vector2, VectorOptions>>(target);
		}

		public static Tweener DOTiling(this Material target, Vector2 endValue, string property, float duration)
		{
			if (target.HasProperty(property))
			{
				return DOTween.To(() => target.GetTextureScale(property), delegate (Vector2 x) {
					target.SetTextureScale(property, x);
				}, endValue, duration).SetTarget<TweenerCore<Vector2, Vector2, VectorOptions>>(target);
			}
			if (Debugger.logPriority > 0)
			{
				Debugger.LogMissingMaterialProperty(property);
			}
			return null;
		}

		public static Tweener DOTime(this TrailRenderer target, float endValue, float duration)
		{
			return DOTween.To(() => target.time, delegate (float x) {
				target.time = x;
			}, endValue, duration).SetTarget<TweenerCore<float, float, FloatOptions>>(target);
		}

		public static int DOTogglePause(this Component target)
		{
			return DOTween.TogglePause(target);
		}

		public static int DOTogglePause(this Material target)
		{
			return DOTween.TogglePause(target);
		}

		public static Tweener DOVector(this Material target, Vector4 endValue, string property, float duration)
		{
			if (target.HasProperty(property))
			{
				return DOTween.To(() => target.GetVector(property), delegate (Vector4 x) {
					target.SetVector(property, x);
				}, endValue, duration).SetTarget<TweenerCore<Vector4, Vector4, VectorOptions>>(target);
			}
			if (Debugger.logPriority > 0)
			{
				Debugger.LogMissingMaterialProperty(property);
			}
			return null;
		}
	}
}