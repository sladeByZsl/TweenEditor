using System.Reflection;
using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using DG.DemiLib;

namespace DG.DemiEditor
{
	public static class AssemblyExtensions
	{
		public static string ADBDir(this Assembly assembly)
		{
			UriBuilder builder = new UriBuilder (assembly.CodeBase);
			string path = Uri.UnescapeDataString (builder.Path);
			if (path.Substring (path.Length - 3) == "dll") {
				path = Path.GetDirectoryName (path);
			} 
			else 
			{
				path = Path.GetDirectoryName (assembly.Location);
			}

			return path.Substring (Application.dataPath.Length + 1).Replace (DeEditorFileUtils.ADBPathSlashToReplace, DeEditorFileUtils.ADBPathSlash);
		}
	}
}
