using UnityEngine;
using UnityEditor;


//Code from http://answers.unity3d.com/questions/55118/changing-texture-import-default-settings.html
public class HiResTexturePostProcessor : AssetPostprocessor
{
    void OnPostprocessTexture(Texture2D texture)
    {
        TextureImporter importer = assetImporter as TextureImporter;
        importer.maxTextureSize = 8192;
        importer.compressionQuality = 100;
        importer.textureCompression = TextureImporterCompression.CompressedHQ;

        Object asset = AssetDatabase.LoadAssetAtPath(importer.assetPath, typeof(Texture2D));
        if (asset)
        {
            EditorUtility.SetDirty(asset);
        }
        else
        {
            importer.maxTextureSize = 8192;
            importer.compressionQuality = 100;
            importer.textureCompression = TextureImporterCompression.CompressedHQ;
        }
    }
}