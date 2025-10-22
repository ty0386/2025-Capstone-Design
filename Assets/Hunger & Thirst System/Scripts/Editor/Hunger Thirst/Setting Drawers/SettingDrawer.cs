using UnityEditor;

namespace DeepWolf.HungerThirstSystem
{
    /// <summary>
    /// Responsible for drawing the settings in the <see cref="HungerThirstEditor"/>
    /// </summary>
    internal abstract class SettingDrawer
    {
        #region [Fields] References
        protected SerializedObject serializedObject;
        #endregion

        #region [Fields] Constants
        /**************************
         * Constants
         **************************/
        // Preset Selector
        protected const float PresetObjectFieldWidth = 150;
        protected const float PresetLabelWidth = 40;

        // Sliders
        /// <summary>
        /// The maximum amount of increase interval. This is maximum amount of the slider
        /// </summary>
        protected const float MaxIncreaseInterval = 20;
        #endregion

        public abstract void Initialize(SerializedObject serializedObject);

        /// <summary>
        /// Draws everything.
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Draws the header of the settings.
        /// </summary>
        protected abstract void DrawHeader();

        /// <summary>
        /// Draws the content of the setting.
        /// </summary>
        protected abstract void DrawContent();
    }
}
