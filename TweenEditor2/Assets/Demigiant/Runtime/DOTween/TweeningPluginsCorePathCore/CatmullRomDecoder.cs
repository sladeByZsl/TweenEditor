using System.IO;
using UnityEngine;
using System;

namespace DG.Tweening.Plugins.Core.PathCore
{
	internal class CatmullRomDecoder : ABSPathDecoder
	{
		// Methods
		internal override void FinalizePath(Path p, Vector3[] wps, bool isClosedPath)
		{
			int length = wps.Length;
			if ((p.controlPoints == null) || (p.controlPoints.Length != 2))
			{
				p.controlPoints = new DG.Tweening.Plugins.Core.PathCore.LinearDecoder.ControlPoint[2];
			}
			if (isClosedPath)
			{
				p.controlPoints[0] = new DG.Tweening.Plugins.Core.PathCore.LinearDecoder.ControlPoint(wps[length - 2], Vector3.zero);
				p.controlPoints[1] = new DG.Tweening.Plugins.Core.PathCore.LinearDecoder.ControlPoint(wps[1], Vector3.zero);
			}
			else
			{
				p.controlPoints[0] = new DG.Tweening.Plugins.Core.PathCore.LinearDecoder.ControlPoint(wps[1], Vector3.zero);
				Vector3 vector = wps[length - 1];
				Vector3 vector2 = vector - wps[length - 2];
				p.controlPoints[1] = new DG.Tweening.Plugins.Core.PathCore.LinearDecoder.ControlPoint(vector + vector2, Vector3.zero);
			}
			p.subdivisions = length * p.subdivisionsXSegment;
			this.SetTimeToLengthTables(p, p.subdivisions);
			this.SetWaypointsLengths(p, p.subdivisionsXSegment);
		}

		internal override Vector3 GetPoint(float perc, Vector3[] wps, Path p, DG.Tweening.Plugins.Core.PathCore.LinearDecoder.ControlPoint[] controlPoints)
		{
			int num = wps.Length - 1;
			int num2 = (int) Math.Floor((double) (perc * num));
			int index = num - 1;
			if (index > num2)
			{
				index = num2;
			}
			float num4 = (perc * num) - index;
			Vector3 vector = (index == 0) ? controlPoints[0].a : wps[index - 1];
			Vector3 vector2 = wps[index];
			Vector3 vector3 = wps[index + 1];
			Vector3 vector4 = ((index + 2) > (wps.Length - 1)) ? controlPoints[1].a : wps[index + 2];
			return (Vector3) (0.5f * (((((((-vector + (3f * vector2)) - (3f * vector3)) + vector4) * ((num4 * num4) * num4)) + (((((2f * vector) - (5f * vector2)) + (4f * vector3)) - vector4) * (num4 * num4))) + ((-vector + vector3) * num4)) + (2f * vector2)));
		}

		internal void SetTimeToLengthTables(Path p, int subdivisions)
		{
			float num = 0f;
			float num2 = 1f / ((float) subdivisions);
			float[] numArray = new float[subdivisions];
			float[] numArray2 = new float[subdivisions];
			Vector3 b = this.GetPoint(0f, p.wps, p, p.controlPoints);
			for (int i = 1; i < (subdivisions + 1); i++)
			{
				float perc = num2 * i;
				Vector3 a = this.GetPoint(perc, p.wps, p, p.controlPoints);
				num += Vector3.Distance(a, b);
				b = a;
				numArray[i - 1] = perc;
				numArray2[i - 1] = num;
			}
			p.length = num;
			p.timesTable = numArray;
			p.lengthsTable = numArray2;
		}

		internal void SetWaypointsLengths(Path p, int subdivisions)
		{
			int length = p.wps.Length;
			float[] numArray = new float[length];
			numArray[0] = 0f;
			DG.Tweening.Plugins.Core.PathCore.LinearDecoder.ControlPoint[] controlPoints = new DG.Tweening.Plugins.Core.PathCore.LinearDecoder.ControlPoint[2];
			Vector3[] wps = new Vector3[2];
			for (int i = 1; i < length; i++)
			{
				controlPoints[0].a = (i == 1) ? p.controlPoints[0].a : p.wps[i - 2];
				wps[0] = p.wps[i - 1];
				wps[1] = p.wps[i];
				controlPoints[1].a = (i == (length - 1)) ? p.controlPoints[1].a : p.wps[i + 1];
				float num3 = 0f;
				float num4 = 1f / ((float) subdivisions);
				Vector3 b = this.GetPoint(0f, wps, p, controlPoints);
				for (int j = 1; j < (subdivisions + 1); j++)
				{
					float perc = num4 * j;
					Vector3 a = this.GetPoint(perc, wps, p, controlPoints);
					num3 += Vector3.Distance(a, b);
					b = a;
				}
				numArray[i] = num3;
			}
			p.wpLengths = numArray;
		}
	}
}