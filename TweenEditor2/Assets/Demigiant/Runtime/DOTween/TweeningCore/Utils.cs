using UnityEngine;

namespace DG.Tweening.Core
{
	internal static class Utils
	{
		// Methods
		internal static float Angle2D(Vector3 from, Vector3 to)
		{
			to -= from;
			Vector2 vector1 = Vector2.right;
			float num = Vector2.Angle(vector1, to);
			if (Vector3.Cross(vector1, to).z > 0f)
			{
				num = 360f - num;
			}
			return (num * -1f);
		}

		internal static Vector3 Vector3FromAngle(float degrees, float magnitude)
		{
			float num = degrees * 0.01745329f;
			return new Vector3(magnitude * Mathf.Cos(num), magnitude * Mathf.Sin(num), 0f);
		}
	}
}