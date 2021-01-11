using UnityEditor;
using System;
using DG.DOTweenEditor.Core;
using UnityEngine;
using DG.Tweening;

namespace DG.DOTweenEditor
{
	public class UtilityWindowProcessor : AssetPostprocessor
	{
		// Fields
		private static bool _setupDialogRequested;
		//在一些资源被导入后调用
		private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
		{
			if (!_setupDialogRequested)
			{
				EditorUtils.DeleteOldDemiLibCore();
				if (EditorUtils.DOTweenSetupRequired() && ((EditorPrefs.GetString(Application.dataPath + "DOTweenVersion") != (Application.dataPath + DOTween.Version)) || (EditorPrefs.GetString(Application.dataPath + "DOTweenProVersion") != (Application.dataPath + EditorUtils.proVersion))))
				{
					_setupDialogRequested = true;
					EditorPrefs.SetString(Application.dataPath + "DOTweenVersion", Application.dataPath + DOTween.Version);
					EditorPrefs.SetString(Application.dataPath + "DOTweenProVersion", Application.dataPath + EditorUtils.proVersion);
					EditorUtility.DisplayDialog("DOTween", "DOTween needs to be setup.\n\nSelect \"Tools > DOTween Utility Panel\" and press \"Setup DOTween...\" in the panel that opens.", "Ok");
					char[] separator = new char[] { "."[0] };
					if (Convert.ToInt32(Application.unityVersion.Split(separator)[0]) >= 4)
					{
						EditorUtils.DelayedCall(0.5f, new Action(DOTweenUtilityWindow.SetupWindow));
					}
				}
			}

            //LanguageProcessor(importedAssets);
        }

        //多语言处理
        private static void LanguageProcessor(string[] importedAssets)
        {
            if (importedAssets != null && importedAssets.Length > 0)
            {
                EditorUtility.DisplayProgressBar("多语言", "正在处理", 0);
                for (int i = 0; i < importedAssets.Length; i++)
                {
                    EditorUtility.DisplayProgressBar("多语言", "正在处理", i);
                    GameObject gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(importedAssets[i]) as GameObject;
//                     if(gameObject != null)
//                         PrefabApply.LanguageProcessor(gameObject);
                }
                EditorUtility.ClearProgressBar();
            }
        }
	}
}