using System.Reflection;
using System.IO;
using UnityEditor;
using DG.DOTweenEditor.Core;
using UnityEngine;
using System;

namespace DG.DOTweenEditor
{
	internal class DOTweenSetupMenuItem
	{
		// Fields
		private static Assembly _proEditorAssembly;
		private const string _Title = "DOTween Setup";

		// Methods
		public DOTweenSetupMenuItem()
		{
		}
		private static bool Has2DToolkit()
		{
			string[] strArray = Directory.GetDirectories(EditorUtils.projectPath, "TK2DROOT", SearchOption.AllDirectories);
			if (strArray.Length != 0)
			{
				string[] strArray2 = strArray;
				for (int i = 0; i < strArray2.Length; i++)
				{
					if (Directory.GetFiles(strArray2[i], "tk2dSprite.cs", SearchOption.AllDirectories).Length != 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		private static bool HasTextMeshPro()
		{
			string[] strArray = Directory.GetDirectories(EditorUtils.projectPath, "TextMesh Pro", SearchOption.AllDirectories);
			if (strArray.Length != 0)
			{
				string[] strArray2 = strArray;
				for (int i = 0; i < strArray2.Length; i++)
				{
					if (Directory.GetFiles(strArray2[i], "TextMeshPro.cs", SearchOption.AllDirectories).Length != 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		private static int ImportAddons(string version, string addonsDir)
		{
			bool flag = false;
			string[] textArray1 = new string[] { "DOTween" + version + ".dll", "DOTween" + version + ".xml", "DOTween" + version + ".dll.mdb", "DOTween" + version + ".cs" };
			foreach (string str in textArray1)
			{
				string path = addonsDir + str + ".addon";
				string str3 = addonsDir + str;
				if (File.Exists(path))
				{
					if (File.Exists(str3))
					{
						File.Delete(str3);
					}
					File.Move(path, str3);
					flag = true;
				}
			}
			if (!flag)
			{
				return 0;
			}
			return 1;
		}

		private static void ProEditor_AddGlobalDefine(string id)
		{
			if (EditorUtils.hasPro && (ProEditorAssembly() != null))
			{
				object[] parameters = new object[] { id };
				_proEditorAssembly.GetType("DG.DOTweenEditor.Core.ProEditorUtils").GetMethod("AddGlobalDefine", BindingFlags.Public | BindingFlags.Static).Invoke(null, parameters);
			}
		}

		private static void ProEditor_RemoveGlobalDefine(string id)
		{
			if (EditorUtils.hasPro && (ProEditorAssembly() != null))
			{
				object[] parameters = new object[] { id };
				_proEditorAssembly.GetType("DG.DOTweenEditor.Core.ProEditorUtils").GetMethod("RemoveGlobalDefine", BindingFlags.Public | BindingFlags.Static).Invoke(null, parameters);
			}
		}

		private static Assembly ProEditorAssembly()
		{
			if (_proEditorAssembly == null)
			{
				_proEditorAssembly = Assembly.LoadFile(EditorUtils.dotweenProDir + "Editor" + EditorUtils.pathSlash + "DOTweenProEditor.dll");
			}
			return _proEditorAssembly;
		}

		public static void Setup(bool partiallySilent=false)
		{
			if (EditorUtils.DOTweenSetupRequired())
			{
				string str3 = "Based on your Unity version (" + Application.unityVersion + ") and eventual plugins, DOTween will now activate additional tween elements, if available.";
				if (!EditorUtility.DisplayDialog("DOTween Setup", str3, "Ok", "Cancel"))
				{
					return;
				}
			}
			else if (!partiallySilent)
			{
				string str4 = "This project has already been setup for your version of DOTween.\nReimport DOTween if you added new compatible external assets or upgraded your Unity version.";
				if (!EditorUtility.DisplayDialog("DOTween Setup", str4, "Force Setup", "Cancel"))
				{
					return;
				}
			}
			else
			{
				return;
			}
			string dotweenDir = EditorUtils.dotweenDir;
			string dotweenProDir = EditorUtils.dotweenProDir;
			EditorUtility.DisplayProgressBar("DOTween Setup", "Please wait...", 0.25f);
			int totImported = 0;
			char[] separator = new char[] { "."[0] };
			string[] textArray1 = Application.unityVersion.Split(separator);
			int num2 = Convert.ToInt32(textArray1[0]);
			int num3 = Convert.ToInt32(textArray1[1]);
			if (num2 < 4)
			{
				SetupComplete(dotweenDir, dotweenProDir, totImported);
			}
			else
			{
				if (num2 == 4)
				{
					if (num3 < 3)
					{
						SetupComplete(dotweenDir, dotweenProDir, totImported);
						return;
					}
					totImported += ImportAddons("43", dotweenDir);
					if (num3 >= 6)
					{
						totImported += ImportAddons("46", dotweenDir);
					}
				}
				else
				{
					totImported += ImportAddons("43", dotweenDir);
					totImported += ImportAddons("46", dotweenDir);
					totImported += ImportAddons("50", dotweenDir);
				}
				if (EditorUtils.hasPro)
				{
					if (Has2DToolkit())
					{
						totImported += ImportAddons("Tk2d", dotweenProDir);
						ProEditor_AddGlobalDefine("DOTWEEN_TK2D");
					}
					else
					{
						ProEditor_RemoveGlobalDefine("DOTWEEN_TK2D");
					}
					if (HasTextMeshPro())
					{
						totImported += ImportAddons("TextMeshPro", dotweenProDir);
						ProEditor_AddGlobalDefine("DOTWEEN_TMP");
					}
					else
					{
						ProEditor_RemoveGlobalDefine("DOTWEEN_TMP");
					}
				}
				SetupComplete(dotweenDir, dotweenProDir, totImported);
			}
		}

		private static void SetupComplete(string addonsDir, string proAddonsDir, int totImported)
		{
			string[] strArray2;
			int num2;
			int num = 0;
			string[] files = Directory.GetFiles(addonsDir, "*.addon");
			if (files.Length != 0)
			{
				EditorUtility.DisplayProgressBar("DOTween Setup", "Removing " + files.Length + " unused additional files...", 0.5f);
				strArray2 = files;
				for (num2 = 0; num2 < strArray2.Length; num2++)
				{
					num++;
					File.Delete(strArray2[num2]);
				}
			}
			if (EditorUtils.hasPro)
			{
				files = Directory.GetFiles(proAddonsDir, "*.addon");
				if (files.Length != 0)
				{
					EditorUtility.DisplayProgressBar("DOTween Setup", "Removing " + files.Length + " unused additional files...", 0.5f);
					strArray2 = files;
					for (num2 = 0; num2 < strArray2.Length; num2++)
					{
						num++;
						File.Delete(strArray2[num2]);
					}
				}
			}
			files = Directory.GetFiles(addonsDir, "*.addon.meta");
			if (files.Length != 0)
			{
				EditorUtility.DisplayProgressBar("DOTween Setup", "Removing " + files.Length + " unused additional meta files...", 0.75f);
				strArray2 = files;
				for (num2 = 0; num2 < strArray2.Length; num2++)
				{
					File.Delete(strArray2[num2]);
				}
			}
			if (EditorUtils.hasPro)
			{
				files = Directory.GetFiles(proAddonsDir, "*.addon.meta");
				if (files.Length != 0)
				{
					EditorUtility.DisplayProgressBar("DOTween Setup", "Removing " + files.Length + " unused additional meta files...", 0.75f);
					strArray2 = files;
					for (num2 = 0; num2 < strArray2.Length; num2++)
					{
						File.Delete(strArray2[num2]);
					}
				}
			}
			EditorUtility.DisplayProgressBar("DOTween Setup", "Refreshing...", 0.9f);
			AssetDatabase.Refresh();
			EditorUtility.ClearProgressBar();
			EditorUtility.DisplayDialog("DOTween Setup", "DOTween setup is now complete." + ((totImported == 0) ? "" : ("\n" + totImported + " additional libraries were imported or updated.")) + ((num == 0) ? "" : ("\n" + num + " extra files were removed.")), "Ok");
		}
	}
}