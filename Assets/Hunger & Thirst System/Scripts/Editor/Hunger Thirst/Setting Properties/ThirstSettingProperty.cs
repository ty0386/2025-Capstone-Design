using UnityEngine;
using UnityEditor;

namespace DeepWolf.HungerThirstSystem
{
    internal class ThirstSettingProperty : SettingProperty<ThirstSettings>
    {
        #region [Fields] Serialized Properties
        protected SerializedProperty beginDehydrationPercentage;
        protected SerializedProperty dehydrationAudioClip;
        #endregion

        #region [Properties] Serialized Properties
        public SerializedProperty BeginDehydrationPercentage { get { return beginDehydrationPercentage; } }
        public SerializedProperty DehydrationAudioClip { get { return dehydrationAudioClip; } }
        #endregion

        internal ThirstSettingProperty(Object serializedObject) : base(serializedObject)
        {
            beginDehydrationPercentage = this.serializedObject.FindProperty("_beginDehydrationPercentage");
            dehydrationAudioClip = this.serializedObject.FindProperty("_dehydrationAudioClip");
        }
    }
}