namespace DG.Tweening.Core.Enums
{
	public enum FilterType
	{
		All,
		TargetOrId,
		TargetAndId,
		AllExceptTargetsOrIds,
		DOGetter
	}
	public enum OperationType
	{
		Complete,
		Despawn,
		Flip,
		Goto,
		Pause,
		Play,
		PlayForward,
		PlayBackwards,
		Rewind,
		SmoothRewind,
		Restart,
		TogglePause,
		IsTweening
	}
	public enum SpecialStartupMode
	{
		None,
		SetLookAt,
		SetShake,
		SetPunch,
		SetCameraShakePosition
	}
	public enum UpdateMode
	{
		Update,
		Goto,
		IgnoreOnUpdate
	}
	public enum UpdateNotice
	{
		None,
		RewindStep
	}
}