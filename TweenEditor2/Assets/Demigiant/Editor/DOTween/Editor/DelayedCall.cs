using System;
using UnityEngine;
using UnityEditor;

namespace DG.DOTweenEditor.Core
{
	public class DelayedCall
	{
		// Fields
		private float _startupTime;
		public Action callback;
		public float delay;

		// Methods
		public DelayedCall(float delay, Action callback)
		{
			this.delay = delay;
			this.callback = callback;
			this._startupTime = Time.realtimeSinceStartup;

			EditorApplication.update = (EditorApplication.CallbackFunction) Delegate.Combine(EditorApplication.update, new EditorApplication.CallbackFunction(this.Update));
		}

		private void Update()
		{
			if ((Time.realtimeSinceStartup - this._startupTime) >= this.delay)
			{
				if (EditorApplication.update != null)
				{
					EditorApplication.update = (EditorApplication.CallbackFunction) Delegate.Remove(EditorApplication.update, new EditorApplication.CallbackFunction(this.Update));
				}
				if (this.callback != null)
				{
					this.callback();
				}
			}
		}
	}
}