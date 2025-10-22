using UnityEditor;
using UnityEngine;

namespace DeepWolf.HungerThirstSystem
{
    /// <summary>
    /// Represents a custom inspector editor for <see cref="HungerThirst"/>
    /// </summary>
    [CustomEditor(typeof(HungerThirst))]
    internal class HungerThirstEditor : Editor
    {
        #region [Fields] Serialized Properties
        private SerializedProperty _audioSourceProp;
        #endregion

        #region [Fields] Setting Drawers
        private HungerSettingDrawer _hungerSettingDrawer;
        private ThirstSettingDrawer _thirstSettingDrawer;
        #endregion

        #region [Methods] Unity
        private void OnEnable()
        {
            if(_hungerSettingDrawer == null)
            {
                _hungerSettingDrawer = new HungerSettingDrawer();
            }

            if(_thirstSettingDrawer == null)
            {
                _thirstSettingDrawer = new ThirstSettingDrawer();
            }

            _hungerSettingDrawer.Initialize(serializedObject);
            _thirstSettingDrawer.Initialize(serializedObject);

            #region Setup serialized properties
            _audioSourceProp = serializedObject.FindProperty("_audioSource");
            #endregion
        }

        public override void OnInspectorGUI()
        {
            DrawAudioSource();

            #region Draw hunger and thirst settings
            GUILayout.Space(5);
            _hungerSettingDrawer.Draw();

            GUILayout.Space(5);
            _thirstSettingDrawer.Draw();
            #endregion
        }
        #endregion

        #region [Methods] Drawing
        private void DrawAudioSource()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_audioSourceProp);

            if (GUI.changed)
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
        #endregion
    }
}