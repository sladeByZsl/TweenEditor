using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace DG.Tweening.Plugins.Core.PathCore
{
	internal class LinearDecoder : ABSPathDecoder
	{
		[Serializable, StructLayout(LayoutKind.Sequential)]
		public struct ControlPoint
		{
			public Vector3 a;
			public Vector3 b;
			public ControlPoint(Vector3 a, Vector3 b)
			{
				this.a = a;
				this.b = b;
			}

			public static ControlPoint operator +(ControlPoint cp, Vector3 v)
			{
				return new ControlPoint(cp.a + v, cp.b + v);
			}
		}



		// Methods
		internal override void FinalizePath(Path p, Vector3[] wps, bool isClosedPath)
		{
			p.controlPoints = null;
			p.subdivisions = wps.Length * p.subdivisionsXSegment;
			this.SetTimeToLengthTables(p, p.subdivisions);
		}

		internal override Vector3 GetPoint(float perc, Vector3[] wps, Path p, ControlPoint[] controlPoints)
		{
			if (perc <= 0f)
			{
				p.linearWPIndex = 1;
				return wps[0];
			}
			int index = 0;
			int num2 = 0;
			int length = p.timesTable.Length;
			for (int i = 1; i < length; i++)
			{
				if (p.timesTable[i] >= perc)
				{
					index = i - 1;
					num2 = i;
					break;
				}
			}
			float num4 = p.timesTable[index];
			float num5 = perc - num4;
			float maxLength = p.length * num5;
			Vector3 vector = wps[index];
			Vector3 vector2 = wps[num2];
			p.linearWPIndex = num2;
			return (vector + Vector3.ClampMagnitude(vector2 - vector, maxLength));
		}

		internal void SetTimeToLengthTables(Path p, int subdivisions)
		{
			float num = 0f;
			int length = p.wps.Length;
			float[] numArray = new float[length];
			Vector3 b = p.wps[0];
			for (int i = 0; i < length; i++)
			{
				Vector3 a = p.wps[i];
				float num5 = Vector3.Distance(a, b);
				num += num5;
				b = a;
				numArray[i] = num5;
			}
			float[] numArray2 = new float[length];
			float num3 = 0f;
			for (int j = 1; j < length; j++)
			{
				num3 += numArray[j];
				numArray2[j] = num3 / num;
			}
			p.length = num;
			p.wpLengths = numArray;
			p.timesTable = numArray2;
		}

		internal void SetWaypointsLengths(Path p, int subdivisions)
		{
		}
	}
}