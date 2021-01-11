using UnityEngine;
using DG.Tweening.Core;

namespace DG.Tweening
{
	//[AddComponentMenu("")]
	public class DOTweenVisualManager : MonoBehaviour
	{
		// Fields
		private bool _requiresRestartFromSpawnPoint;
		public OnDisableBehaviour onDisableBehaviour;
		public OnEnableBehaviour onEnableBehaviour;
		public VisualManagerPreset preset;

		// Methods
		private void OnDisable()
		{
			ABSAnimationComponent component;
			this._requiresRestartFromSpawnPoint = false;
			switch (this.onDisableBehaviour)
			{
			case OnDisableBehaviour.Pause:
				component = base.GetComponent<ABSAnimationComponent>();
				if (component == null)
				{
					break;
				}
				component.DOPause();
				return;

			case OnDisableBehaviour.Rewind:
				component = base.GetComponent<ABSAnimationComponent>();
				if (component == null)
				{
					break;
				}
				component.DORewind();
				return;

			case OnDisableBehaviour.Kill:
				component = base.GetComponent<ABSAnimationComponent>();
				if (component == null)
				{
					break;
				}
				component.DOKill();
				return;

			case OnDisableBehaviour.KillAndComplete:
				component = base.GetComponent<ABSAnimationComponent>();
				if (component == null)
				{
					break;
				}
				component.DOComplete();
				component.DOKill();
				return;

			case OnDisableBehaviour.DestroyGameObject:
				component = base.GetComponent<ABSAnimationComponent>();
				if (component != null)
				{
					component.DOKill();
				}
				Object.Destroy(base.gameObject);
				break;

			default:
				return;
			}
		}

		private void OnEnable()
		{
			ABSAnimationComponent component;
			switch (this.onEnableBehaviour)
			{
			case OnEnableBehaviour.Play:
				component = base.GetComponent<ABSAnimationComponent>();
				if (component == null)
				{
					break;
				}
				component.DOPlay();
				return;

			case OnEnableBehaviour.Restart:
				component = base.GetComponent<ABSAnimationComponent>();
				if (component == null)
				{
					break;
				}
				component.DORestart(false);
				return;

			case OnEnableBehaviour.RestartFromSpawnPoint:
				this._requiresRestartFromSpawnPoint = true;
				break;

			default:
				return;
			}
		}

		private void Update()
		{
			if (this._requiresRestartFromSpawnPoint)
			{
				this._requiresRestartFromSpawnPoint = false;
				ABSAnimationComponent component = base.GetComponent<ABSAnimationComponent>();
				if (component != null)
				{
					component.DORestart(true);
				}
			}
		}
	}
}