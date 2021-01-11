using UnityEngine;
using System;

namespace DG.Tweening.Plugins.Core.PathCore
{
	[Serializable]
	public class Path
	{
		// Fields
		private static CatmullRomDecoder _catmullRomDecoder;
		private bool _changed;
		private ABSPathDecoder _decoder;
		private Path _incrementalClone;
		private int _incrementalIndex;
		private static LinearDecoder _linearDecoder;
		[SerializeField]
		internal DG.Tweening.Plugins.Core.PathCore.LinearDecoder.ControlPoint[] controlPoints;
		internal Color gizmoColor;
		[SerializeField]
		internal bool isFinalized;
		[SerializeField]
		public float length;
		[SerializeField]
		public float[] lengthsTable;
		public int linearWPIndex;
		public Vector3? lookAtPosition;
		public Vector3[] nonLinearDrawWps;
		[SerializeField]
		public int subdivisions;
		[SerializeField]
		public int subdivisionsXSegment;
		public Vector3 targetPosition;
		[SerializeField]
		public float[] timesTable;
		[SerializeField]
		public PathType type;
		[SerializeField]
		public float[] wpLengths;
		[SerializeField]
		public Vector3[] wps;

		// Methods
		public Path()
		{
			this.linearWPIndex = -1;
			this.gizmoColor = new Color(1f, 1f, 1f, 0.7f);
		}

		public Path(PathType type, Vector3[] waypoints, int subdivisionsXSegment, Color? gizmoColor=null)
		{
			this.linearWPIndex = -1;
			this.gizmoColor = new Color(1f, 1f, 1f, 0.7f);
			this.type = type;
			this.subdivisionsXSegment = subdivisionsXSegment;

			if(gizmoColor.HasValue)
			{
				this.gizmoColor = gizmoColor.Value;
			}

			this.AssignWaypoints(waypoints, true);
			this.AssignDecoder(type);
			if (DOTween.isUnityEditor)
			{
				DOTween.GizmosDelegates.Add(new TweenCallback(this.Draw));
			}
		}

		public void AssignDecoder(PathType pathType)
		{
			this.type = pathType;
			if (pathType == PathType.Linear)
			{
				if (_linearDecoder == null)
				{
					_linearDecoder = new LinearDecoder();
				}
				this._decoder = _linearDecoder;
			}
			else
			{
				if (_catmullRomDecoder == null)
				{
					_catmullRomDecoder = new CatmullRomDecoder();
				}
				this._decoder = _catmullRomDecoder;
			}
		}

		public void AssignWaypoints(Vector3[] newWps,  bool cloneWps=false)
		{
			if (cloneWps)
			{
				int length = newWps.Length;
				this.wps = new Vector3[length];
				for (int i = 0; i < length; i++)
				{
					this.wps[i] = newWps[i];
				}
			}
			else
			{
				this.wps = newWps;
			}
		}

		public Path CloneIncremental(int loopIncrement)
		{
			if (this._incrementalClone != null)
			{
				if (this._incrementalIndex == loopIncrement)
				{
					return this._incrementalClone;
				}
				this._incrementalClone.Destroy();
			}
			int length = this.wps.Length;
			Vector3 vector = this.wps[length - 1] - this.wps[0];
			Vector3[] vectorArray = new Vector3[this.wps.Length];
			for (int i = 0; i < length; i++)
			{
				vectorArray[i] = this.wps[i] + (vector * loopIncrement);
			}
			int num2 = this.controlPoints.Length;
			DG.Tweening.Plugins.Core.PathCore.LinearDecoder.ControlPoint[] pointArray = new DG.Tweening.Plugins.Core.PathCore.LinearDecoder.ControlPoint[num2];
			for (int j = 0; j < num2; j++)
			{
				pointArray[j] = this.controlPoints[j] + ((vector * loopIncrement));
			}
			Vector3[] vectorArray2 = null;
			if (this.nonLinearDrawWps != null)
			{
				int num5 = this.nonLinearDrawWps.Length;
				vectorArray2 = new Vector3[num5];
				for (int k = 0; k < num5; k++)
				{
					vectorArray2[k] = this.nonLinearDrawWps[k] + (vector * loopIncrement);
				}
			}
			this._incrementalClone = new Path();
			this._incrementalIndex = loopIncrement;
			this._incrementalClone.type = this.type;
			this._incrementalClone.subdivisionsXSegment = this.subdivisionsXSegment;
			this._incrementalClone.subdivisions = this.subdivisions;
			this._incrementalClone.wps = vectorArray;
			this._incrementalClone.controlPoints = pointArray;
			if (DOTween.isUnityEditor)
			{
				DOTween.GizmosDelegates.Add(new TweenCallback(this._incrementalClone.Draw));
			}
			this._incrementalClone.length = this.length;
			this._incrementalClone.wpLengths = this.wpLengths;
			this._incrementalClone.timesTable = this.timesTable;
			this._incrementalClone.lengthsTable = this.lengthsTable;
			this._incrementalClone._decoder = this._decoder;
			this._incrementalClone.nonLinearDrawWps = vectorArray2;
			this._incrementalClone.targetPosition = this.targetPosition;
			this._incrementalClone.lookAtPosition = this.lookAtPosition;
			this._incrementalClone.isFinalized = true;
			return this._incrementalClone;
		}

		public float ConvertToConstantPathPerc(float perc)
		{
			if (this.type != PathType.Linear)
			{
				if ((perc > 0f) && (perc < 1f))
				{
					float num = this.length * perc;
					float num2 = 0f;
					float num3 = 0f;
					float num4 = 0f;
					float num5 = 0f;
					int length = this.lengthsTable.Length;
					for (int i = 0; i < length; i++)
					{
						if (this.lengthsTable[i] > num)
						{
							num4 = this.timesTable[i];
							num5 = this.lengthsTable[i];
							if (i > 0)
							{
								num3 = this.lengthsTable[i - 1];
							}
							break;
						}
						num2 = this.timesTable[i];
					}
					perc = num2 + (((num - num3) / (num5 - num3)) * (num4 - num2));
				}
				if (perc > 1f)
				{
					perc = 1f;
					return perc;
				}
				if (perc < 0f)
				{
					perc = 0f;
				}
			}
			return perc;
		}

		public void Destroy()
		{
			if (DOTween.isUnityEditor)
			{
				DOTween.GizmosDelegates.Remove(new TweenCallback(this.Draw));
			}
			this.wps = null;
			this.wpLengths = this.timesTable = (float[]) (this.lengthsTable = null);
			this.nonLinearDrawWps = null;
			this.isFinalized = false;
		}

		public void Draw()
		{
			Draw(this);
		}

		private static void Draw(Path p)
		{
			if (p.timesTable != null)
			{
				Vector3 vector;
				Color gizmoColor = p.gizmoColor;
				gizmoColor.a *= 0.5f;
				Gizmos.color = p.gizmoColor;
				int length = p.wps.Length;
				if (p._changed || ((p.type != PathType.Linear) && (p.nonLinearDrawWps == null)))
				{
					p._changed = false;
					if (p.type != PathType.Linear)
					{
						RefreshNonLinearDrawWps(p);
					}
				}
				if (p.type == PathType.Linear)
				{
					vector = p.wps[0];
					for (int j = 0; j < length; j++)
					{
						Vector3 from = p.wps[j];
						Gizmos.DrawLine(from, vector);
						vector = from;
					}
				}
				else
				{
					vector = p.nonLinearDrawWps[0];
					int num2 = p.nonLinearDrawWps.Length;
					for (int k = 1; k < num2; k++)
					{
						Vector3 vector3 = p.nonLinearDrawWps[k];
						Gizmos.DrawLine(vector3, vector);
						vector = vector3;
					}
				}
				Gizmos.color = gizmoColor;
				for (int i = 0; i < length; i++)
				{
					Gizmos.DrawSphere(p.wps[i], 0.075f);
				}
				if (p.lookAtPosition.HasValue)
				{
					Vector3 to = p.lookAtPosition.Value;
					Gizmos.DrawLine(p.targetPosition, to);
					Gizmos.DrawWireSphere(to, 0.075f);
				}
			}
		}

		public void FinalizePath(bool isClosedPath, AxisConstraint lockPositionAxes, Vector3 currTargetVal)
		{
			if (lockPositionAxes != AxisConstraint.None)
			{
				bool flag = (lockPositionAxes & AxisConstraint.X) == AxisConstraint.X;
				bool flag2 = (lockPositionAxes & AxisConstraint.Y) == AxisConstraint.Y;
				bool flag3 = (lockPositionAxes & AxisConstraint.Z) == AxisConstraint.Z;
				for (int i = 0; i < this.wps.Length; i++)
				{
					Vector3 vector = this.wps[i];
					this.wps[i] = new Vector3(flag ? currTargetVal.x : vector.x, flag2 ? currTargetVal.y : vector.y, flag3 ? currTargetVal.z : vector.z);
				}
			}
			this._decoder.FinalizePath(this, this.wps, isClosedPath);
			this.isFinalized = true;
		}

		public static Vector3[] GetDrawPoints(Path p, int drawSubdivisionsXSegment)
		{
			int length = p.wps.Length;
			if (p.type == PathType.Linear)
			{
				return p.wps;
			}
			int num2 = length * drawSubdivisionsXSegment;
			Vector3[] vectorArray = new Vector3[num2 + 1];
			for (int i = 0; i <= num2; i++)
			{
				float perc = ((float) i) / ((float) num2);
				vectorArray[i] = p.GetPoint(perc, false);
			}
			return vectorArray;
		}

		public Vector3 GetPoint(float perc, bool convertToConstantPerc=false)
		{
			if (convertToConstantPerc)
			{
				perc = this.ConvertToConstantPathPerc(perc);
			}
			return this._decoder.GetPoint(perc, this.wps, this, this.controlPoints);
		}

		public int GetWaypointIndexFromPerc(float perc, bool isMovingForward)
		{
			if (perc >= 1f)
			{
				return (this.wps.Length - 1);
			}
			if (perc > 0f)
			{
				float num = this.length * perc;
				float num2 = 0f;
				int index = 0;
				int length = this.wpLengths.Length;
				while (index < length)
				{
					num2 += this.wpLengths[index];
					if (num2 >= num)
					{
						if (num2 <= num)
						{
							return index;
						}
						if (!isMovingForward)
						{
							return index;
						}
						return (index - 1);
					}
					index++;
				}
			}
			return 0;
		}

		public static void RefreshNonLinearDrawWps(Path p)
		{
			int num = p.wps.Length * 10;
			if ((p.nonLinearDrawWps == null) || (p.nonLinearDrawWps.Length != (num + 1)))
			{
				p.nonLinearDrawWps = new Vector3[num + 1];
			}
			for (int i = 0; i <= num; i++)
			{
				float perc = ((float) i) / ((float) num);
				p.nonLinearDrawWps[i] = p.GetPoint(perc, false);
			}
		}
	}
}