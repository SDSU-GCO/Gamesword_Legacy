using UnityEngine;
using UnityEditor;

public class ExportProjectAsPackageUtility : MonoBehaviour
{
        [MenuItem("Export/Export Project as Package")]
    static void export()
    {
        var path = EditorUtility.SaveFilePanel("Export Project as UnityPackage", "",
                 "New UnityPackage.unitypackage",
                 "unitypackage");

        if (path.Length != 0)
        {
            AssetDatabase.ExportPackage(AssetDatabase.GetAllAssetPaths(), path, ExportPackageOptions.Interactive | ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies | ExportPackageOptions.IncludeLibraryAssets);
        }

        
    }
}