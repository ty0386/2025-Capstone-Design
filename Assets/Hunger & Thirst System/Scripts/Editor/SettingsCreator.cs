using System.IO;
using UnityEngine;
using UnityEditor;

namespace DeepWolf.HungerThirstSystem
{
    internal class SettingsCreator : Editor
    {
        [MenuItem("Assets/Create/Hunger Thirst System/Hunger Settings")]
        static internal void CreateHungerSettings()
        {
            CreateSettingAsset<HungerSettings>();
        }

        [MenuItem("Assets/Create/Hunger Thirst System/Thirst Settings")]
        static internal void CreateThirstSettings()
        {
            CreateSettingAsset<ThirstSettings>();
        }

        static internal void CreateSettingAsset<T>()
        {
            string path = string.Empty;

            // Get selected object in project window
            Object selectedObject = Selection.activeObject;

            if (selectedObject == null)
            {
                path = "Assets";
            }
            else
            {
                path = AssetDatabase.GetAssetPath(selectedObject);
            }

            // Create an instance of the scriptable object with the specified type
            ScriptableObject asset = CreateInstance(typeof(T));

            // Create the asset at the specified path
            AssetDatabase.CreateAsset(asset, Path.Combine(path, string.Format("New{0}.asset", typeof(T).Name)));
            AssetDatabase.SaveAssets();

            // Sets the active selection in the Project window to the new asset
            Selection.activeObject = asset;
        }
    }
}