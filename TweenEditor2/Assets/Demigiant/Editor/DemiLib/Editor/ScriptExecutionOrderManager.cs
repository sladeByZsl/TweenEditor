using UnityEditor;
using DG.DemiLib.Attributes;
using System;

namespace DG.DemiEditor.AttributesManagers
{
	//监听Unity3d的启动事件检测更新
	[InitializeOnLoad]
	public class ScriptExecutionOrderManager
	{
		static ScriptExecutionOrderManager()
		{
			foreach (MonoScript script in MonoImporter.GetAllRuntimeMonoScripts()) 
			{
				if (script.GetClass () != null) 
				{
					foreach (Attribute attribute in Attribute.GetCustomAttributes(script.GetClass(), typeof(ScriptExecutionOrder))) 
					{
						int executionOrder = MonoImporter.GetExecutionOrder (script);
						int order = ((ScriptExecutionOrder)attribute).order;
						if (executionOrder != order) 
						{
							MonoImporter.SetExecutionOrder (script, order);
						}
					}
				}
			}
		}
	}
}

