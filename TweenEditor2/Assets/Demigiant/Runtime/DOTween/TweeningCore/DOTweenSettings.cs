using UnityEngine;

namespace DG.Tweening.Core
{
	public class DOTweenSettings : ScriptableObject
	{
		// Nested Types
		public enum SettingsLocation
		{
			AssetsDirectory,
			DOTweenDirectory,
			DemigiantDirectory
		}
		// Fields
		public const string AssetName = "DoTweenSettings";
		public bool defaultAutoKill;
		public AutoPlay defaultAutoPlay;
		public float defaultEaseOvershootOrAmplitude;
		public float defaultEasePeriod;
		public Ease defaultEaseType;
		public LoopType defaultLoopType;
		public bool defaultRecyclable;
		public bool defaultTimeScaleIndependent;
		public UpdateType defaultUpdateType;
		public bool drawGizmos;
		public LogBehaviour logBehaviour;
		public bool showUnityEditorReport;
		public SettingsLocation storeSettingsLocation;
		public float timeScale;
		public bool useSafeMode;
		public bool useSmoothDeltaTime;

		public DOTweenSettings()
		{
			this.useSafeMode = true;
			this.timeScale = 1f;
			this.logBehaviour = LogBehaviour.ErrorsOnly;
			this.drawGizmos = true;
			this.defaultAutoPlay = AutoPlay.All;
			this.defaultEaseType = Ease.OutQuad;
			this.defaultEaseOvershootOrAmplitude = 1.70158f;
			this.defaultAutoKill = true;
		}
	}
}