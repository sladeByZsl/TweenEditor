using UnityEngine;
using System.Collections;
using UnityEditor;

namespace DG.DemiEditor
{
	public static class TextureExtensions
	{
		public static void SetFormat(this Texture2D texture, FilterMode filterMode=0, int maxTextureSize=32)
		{
			if (texture != null && texture.wrapMode != TextureWrapMode.Clamp) 
			{
				string assetPath = AssetDatabase.GetAssetPath (texture);
				TextureImporter atPath = AssetImporter.GetAtPath (assetPath) as TextureImporter;
				atPath.textureType = TextureImporterType.GUI;
				atPath.npotScale = 0;
				atPath.filterMode = filterMode;
				atPath.wrapMode = TextureWrapMode.Clamp;
				atPath.maxTextureSize = maxTextureSize;
				//atPath.textureFormat = TextureImporterFormat.AutomaticTruecolor;
				atPath.textureCompression = TextureImporterCompression.CompressedHQ;
                AssetDatabase.ImportAsset (assetPath);
			}
		}
	}
}