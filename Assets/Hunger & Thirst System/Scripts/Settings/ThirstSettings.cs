using UnityEngine;

namespace DeepWolf.HungerThirstSystem
{
    /// <summary>
    /// Thirst related settings.
    /// </summary>
    public class ThirstSettings : SharedSettings
    {
        #region [Fields]
        /// <summary>
        /// At what thirst percentage the dehydration kicks in.
        /// </summary>
        [Tooltip("At what thirst percentage the dehydration kicks in.")]
        [SerializeField] private float _beginDehydrationPercentage = 100.0f;

        /// <summary>
        /// The audio clip to play when dehydration begins.
        /// </summary>
        [Tooltip("The audio clip to play when dehydration begins.")]
        [SerializeField] private AudioClip _dehydrationAudioClip;
        #endregion

        #region [Properties]
        /// <summary>
        /// Gets or sets the [Begin Dehydration At Percentage]
        /// </summary>
        public float BeginDehydrationPercentage
        {
            get { return _beginDehydrationPercentage; }
            set { _beginDehydrationPercentage = value; }
        }

        /// <summary>
        /// Gets the audio clip for dehydration begin.
        /// </summary>
        public AudioClip DehydrationAudioClip { get { return _dehydrationAudioClip; } }

        /// <summary>
        /// Gets the [Is Dehydrated] state.
        /// </summary>
        public bool IsDehydrated { get; set; }
        #endregion
    }
}