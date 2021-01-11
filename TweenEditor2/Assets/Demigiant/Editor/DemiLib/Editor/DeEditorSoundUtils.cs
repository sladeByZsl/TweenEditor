using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
using UnityEditor;

namespace DG.DemiLib
{
	public static class DeEditorSoundUtils
	{
		public static void Play(AudioClip audioClip)
		{
			if (audioClip != null) 
			{
				Type[] types = new Type[]{typeof(AudioClip)};
				object[] parameters = new object[]{audioClip};
				Assembly.GetAssembly (typeof(EditorWindow)).CreateInstance ("UnityEditor.AudioUtil").GetType ().GetMethod ("PlayClip", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, Type.DefaultBinder, types, null).Invoke (null, parameters);
			}
		}
		public static void Stop(AudioClip audioClip)
		{
			if (audioClip != null) 
			{
				Type[] types = new Type[]{typeof(AudioClip)};
				object[] parameters = new object[]{audioClip};
				Assembly.GetAssembly (typeof(EditorWindow)).CreateInstance ("UnityEditor.AudioUtil").GetType ().GetMethod ("StopClip", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, Type.DefaultBinder, types, null).Invoke (null, parameters);
			}
		}
		public static void StopAll(AudioClip audioClip)
		{
			Assembly.GetAssembly (typeof(EditorWindow)).CreateInstance ("UnityEditor.AudioUtil").GetType ().GetMethod ("StopAllClips", BindingFlags.Public | BindingFlags.Static).Invoke (null, null);
		}
	}
}
