using UnityEngine;
using System.Collections;

namespace DG.DemiEditor
{
	public static class StringExtensions
	{
		public static string Parent(this string dir)
		{
			if (dir.Length <= 1) 
			{
				return dir;
			}
			string str = (dir.IndexOf("/") == -1)? @"\" : "/";
			int length = dir.LastIndexOf (str);
			if (length == -1) 
			{
				return dir;
			}
			if (length == (dir.Length - 1)) 
			{
				length = dir.LastIndexOf (str, (int)(length - 1));
				if (length == -1) 
				{
					return dir;
				}
				return dir.Substring (0, length + 1);
			}
			return dir.Substring (0, length);
		}
	}
}
