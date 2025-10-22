using UnityEngine;
using UnityEditor;

namespace DeepWolf.HungerThirstSystem
{
    internal abstract class SettingProperty<T> where T : SharedSettings
    {
        protected SerializedObject serializedObject;

        #region [Fields] Serialized Properties
        protected SerializedProperty maxValue;
        protected SerializedProperty increaseInterval;
        protected SerializedProperty increaseAmount;
        protected SerializedProperty useSounds;
        #endregion

        #region [Properties] Serialized Properties
        public SerializedProperty MaxValue { get { return maxValue; } }
        public SerializedProperty IncreaseInterval { get { return increaseInterval; } }
        public SerializedProperty IncreaseAmount { get { return increaseAmount; } }
        public SerializedProperty UseSounds { get { return useSounds; } }
        #endregion

        internal SettingProperty(Object serializedObject)
        {
            this.serializedObject = new SerializedObject(serializedObject);

            maxValue = this.serializedObject.FindProperty("maxValue");
            increaseInterval = this.serializedObject.FindProperty("increaseInterval");
            increaseAmount = this.serializedObject.FindProperty("increaseAmount");
            useSounds = this.serializedObject.FindProperty("useSounds");
        }

        public void Update()
        {
            serializedObject.Update();
        }

        public void ApplyChanges()
        {
            serializedObject.ApplyModifiedProperties();
        }
    }
}