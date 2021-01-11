using UnityEngine;
using System.IO;

namespace DG.Tweening.Plugins.Core.PathCore
{
	internal abstract class ABSPathDecoder
	{
		// Methods
		protected ABSPathDecoder()
		{
		}

		internal abstract void FinalizePath(Path p, Vector3[] wps, bool isClosedPath);
		internal abstract Vector3 GetPoint(float perc, Vector3[] wps, Path p, DG.Tweening.Plugins.Core.PathCore.LinearDecoder.ControlPoint[] controlPoints);
	}
}