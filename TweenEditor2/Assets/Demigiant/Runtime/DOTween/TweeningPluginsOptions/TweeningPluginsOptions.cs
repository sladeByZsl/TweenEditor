
using UnityEngine;
using System.Runtime.InteropServices;

namespace DG.Tweening.Plugins.Options
{
	[StructLayout(LayoutKind.Sequential)]
	public struct ColorOptions
	{
		public bool alphaOnly;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct FloatOptions
	{
		public bool snapping;
	}

	[StructLayout(LayoutKind.Sequential, Size=1)]
	public struct NoOptions
	{
	}

	public enum OrientType
	{
		None,
		ToPath,
		LookAtTransform,
		LookAtPosition
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct PathOptions
	{
		public PathMode mode;
		public OrientType orientType;
		public AxisConstraint lockPositionAxis;
		public AxisConstraint lockRotationAxis;
		public bool isClosedPath;
		public Vector3 lookAtPosition;
		public Transform lookAtTransform;
		public float lookAhead;
		public bool hasCustomForwardDirection;
		public Quaternion forward;
		public bool useLocalPosition;
		public Transform parent;
		internal Quaternion startupRot;
		internal float startupZRot;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct QuaternionOptions
	{
		internal RotateMode rotateMode;
		internal AxisConstraint axisConstraint;
		internal Vector3 up;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct RectOptions
	{
		public bool snapping;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct StringOptions
	{
		public bool richTextEnabled;
		public ScrambleMode scrambleMode;
		public char[] scrambledChars;
		internal int startValueStrippedLength;
		internal int changeValueStrippedLength;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct Vector3ArrayOptions
	{
		public AxisConstraint axisConstraint;
		public bool snapping;
		internal float[] durations;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct VectorOptions
	{
		public AxisConstraint axisConstraint;
		public bool snapping;
	}

}