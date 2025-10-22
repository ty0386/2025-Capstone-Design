using UnityEngine;

namespace DeepWolf.HungerThirstSystem
{
    /// <summary>
    /// The most common settings for thirst and hunger
    /// </summary>
    public abstract class SharedSettings : ScriptableObject
    {
        #region [Fields]
        /// <summary>
        /// The interval for increasing.
        /// </summary>
        [Tooltip("The interval for increasing.")]
        [SerializeField] protected float increaseInterval = 5.0f;

        /// <summary>
        /// The amount to add, after every interval.
        /// </summary>
        [Tooltip("The amount to add, after every interval.")]
        [SerializeField] protected float increaseAmount = 8.0f;

        /// <summary>
        /// The maximum value.
        /// </summary>
        [Tooltip("The maximum value.")]
        [SerializeField] protected float maxValue = 100.0f;

        /// <summary>
        /// Whether or not to play audio clips.
        /// </summary>
        [SerializeField] protected bool useSounds;
        #endregion

        #region [Properties]
        /// <summary>
        /// Gets or sets the [Maximum Value]. Can be overriden
        /// </summary>
        public virtual float MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }

        /// <summary>
        /// Gets or sets the [Increase Interval]. Can be overriden
        /// </summary>
        public virtual float IncreaseInterval
        {
            get { return increaseInterval; }
            set { increaseInterval = value; }
        }

        /// <summary>
        /// Gets or sets the [Increase Amount]. Can be overriden
        /// </summary>
        public virtual float IncreaseAmount
        {
            get { return increaseAmount; }
            set { increaseAmount = value; }
        }

        /// <summary>
        /// Gets or sets the [Sounds]. Can be overriden
        /// </summary>
        public virtual bool UseSounds
        {
            get { return useSounds; }
            set { useSounds = value; }
        }
        #endregion
    }
}