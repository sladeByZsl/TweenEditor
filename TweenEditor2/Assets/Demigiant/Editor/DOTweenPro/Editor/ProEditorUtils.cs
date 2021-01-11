using UnityEngine;
using UnityEditor;
using System;

namespace DG.DOTweenEditor.Core
{
	public static class ProEditorUtils
	{
		// Methods
		public static void AddGlobalDefine(string id)
		{
			bool flag = false;
			foreach (BuildTargetGroup group in Enum.GetValues(typeof(BuildTargetGroup)))
			{
				if (group != BuildTargetGroup.Unknown)
				{
					string scriptingDefineSymbolsForGroup = PlayerSettings.GetScriptingDefineSymbolsForGroup(group);
					if (!scriptingDefineSymbolsForGroup.Contains(id))
					{
						flag = true;
						scriptingDefineSymbolsForGroup = scriptingDefineSymbolsForGroup + ((scriptingDefineSymbolsForGroup.Length > 0) ? (";" + id) : id);
						PlayerSettings.SetScriptingDefineSymbolsForGroup(group, scriptingDefineSymbolsForGroup);
					}
				}
			}
			if (flag)
			{
				Debug.Log("DOTween : added global define " + id);
			}
		}

		public static void RemoveGlobalDefine(string id)
		{
			bool flag = false;
			foreach (BuildTargetGroup group in Enum.GetValues(typeof(BuildTargetGroup)))
			{
				if (group != BuildTargetGroup.Unknown)
				{
					string scriptingDefineSymbolsForGroup = PlayerSettings.GetScriptingDefineSymbolsForGroup(group);
					if (scriptingDefineSymbolsForGroup.Contains(id))
					{
						flag = true;
						if (scriptingDefineSymbolsForGroup.Contains(id + ";"))
						{
							scriptingDefineSymbolsForGroup = scriptingDefineSymbolsForGroup.Replace(id + ";", "");
						}
						else if (scriptingDefineSymbolsForGroup.Contains(";" + id))
						{
							scriptingDefineSymbolsForGroup = scriptingDefineSymbolsForGroup.Replace(";" + id, "");
						}
						else
						{
							scriptingDefineSymbolsForGroup = scriptingDefineSymbolsForGroup.Replace(id, "");
						}
						PlayerSettings.SetScriptingDefineSymbolsForGroup(group, scriptingDefineSymbolsForGroup);
					}
				}
			}
			if (flag)
			{
				Debug.Log("DOTween : removed global define " + id);
			}
		}
	}
}