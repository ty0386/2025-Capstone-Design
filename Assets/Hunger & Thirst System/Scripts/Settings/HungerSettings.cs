using UnityEngine;

namespace DeepWolf.HungerThirstSystem
{
    /// <summary>
    /// Hunger related settings.
    /// </summary>
    public class HungerSettings : SharedSettings
    {
        #region [Fields]
        /// <summary>
        /// At what hunger percentage the starvation kicks in.
        /// </summary>
        [Tooltip("At what hunger percentage the starvation kicks in.")]
        [SerializeField] private float _beginStarvationPercentage = 100.0f;

        /// <summary>
        /// The audio clip to play when starvation begins.
        /// </summary>
        [Tooltip("The audio clip to play when starvation begins.")]
        [SerializeField] private AudioClip _starvationAudioClip;
        #endregion

        #region [Properties]
        /// <summary>
        /// Gets or sets the [Begin Starvation At Percentage]
        /// </summary>
        public float BeginStarvationPercentage
        {
            get { return _beginStarvationPercentage; }
            set { _beginStarvationPercentage = value; }
        }

        /// <summary>
        /// Gets the audio clip for starvation begin.
        /// </summary>
        public AudioClip StarvationAudioClip { get { return _starvationAudioClip; } }

        /// <summary>
        /// Gets the [Is Starving] state.
        /// </summary>
        public bool IsStarving { get; set; }
        #endregion
    }
}