using UnityEngine;
using UnityEditor;

namespace DeepWolf.HungerThirstSystem
{
    internal class HungerSettingProperty : SettingProperty<HungerSettings>
    {
        #region [Fields] Serialized Properties
        protected SerializedProperty beginStarvationPercentage;
        protected SerializedProperty starvationAudioClip;
        #endregion

        #region [Properties] Serialized Properties
        public SerializedProperty BeginStarvationPercentage { get { return beginStarvationPercentage; } }
        public SerializedProperty StarvationAudioClip { get { return starvationAudioClip; } }
        #endregion

        internal HungerSettingProperty(Object serializedObject) : base(serializedObject)
        {
            beginStarvationPercentage = this.serializedObject.FindProperty("_beginStarvationPercentage");
            starvationAudioClip = this.serializedObject.FindProperty("_starvationAudioClip");
        }
    }
}