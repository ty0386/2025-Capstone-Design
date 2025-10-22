using UnityEditor;

namespace DeepWolf.HungerThirstSystem
{
    [CustomEditor(typeof(SharedSettings), true)]
    internal class SettingsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("If you want to make changes to the preset, please do it via the Hunger Thirst script component\n\nYou can attach this preset to the preset slot, and then make changes to it.", MessageType.Info);
        }
    }
}