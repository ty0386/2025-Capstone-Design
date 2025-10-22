using UnityEngine;
using UnityEngine.Events;
using TimeSpan = System.TimeSpan;

namespace DeepWolf.HungerThirstSystem
{
    /// <summary>
    /// The behaviour of the hunger and thirst.
    /// </summary>
    public class HungerThirst : MonoBehaviour
    {
        #region [Fields] Hunger
        /// <summary>
        /// Whether hunger should be used or not.
        /// </summary>
        [Tooltip("Whether hunger should be used or not.")]
        [SerializeField] private bool _useHunger;

        /// <summary>
        /// The settings for hunger.
        /// </summary>
        [SerializeField] private HungerSettings _hungerSettings;

        /// <summary>
        /// The extra hunger amount to add.
        /// </summary>
        private float _extraHungerIncreaseAmount;

        /// <summary>
        /// How much current hunger is.
        /// </summary>
        private float _hunger = 0.0f;

        /// <summary>
        /// The next time the hunger will increase.
        /// </summary>
        private float _nextHungerIncrease = 0.0f;

        /// <summary>
        /// Triggered when hunger changes.
        /// </summary>
        [SerializeField] private UnityEvent _onHungerChanged;
        /// <summary>
        /// Triggered when hunger reaches the max value.
        /// </summary>
        [SerializeField] private UnityEvent _onHungerReachedMax;
        /// <summary>
        /// Triggered when starvation begins.
        /// </summary>
        [SerializeField] private UnityEvent _onStarvationBegin;
        /// <summary>
        /// Triggered when starvation stops.
        /// </summary>
        [SerializeField] private UnityEvent _onStarvationStop;
        /// <summary>
        /// Triggered when the extra pool of the hunger increase amount has changed
        /// </summary>
        [SerializeField] private UnityEvent _onExtraHungerIncreaseAmountChanged;
        #endregion

        #region [Fields] Thirst
        /// <summary>
        /// Whether thirst should be used or not.
        /// </summary>
        [Tooltip("Whether thirst should be used or not.")]
        [SerializeField] private bool _useThirst;

        /// <summary>
        /// The settings for hunger.
        /// </summary>
        [SerializeField] private ThirstSettings _thirstSettings;

        /// <summary>
        /// The extra thirst amount to add.
        /// </summary>
        protected float _extraThirstIncreaseAmount;

        /// <summary>
        /// How much current thirst is.
        /// </summary>
        private float _thirst = 0.0f;

        /// <summary>
        /// The next time the thirst will increase.
        /// </summary>
        private float _nextThirstIncrease = 0.0f;

        /// <summary>
        /// Triggered when thirst changes.
        /// </summary>
        [SerializeField] private UnityEvent _onThirstChanged;
        /// <summary>
        /// Triggered when thirst reaches the max value.
        /// </summary>
        [SerializeField] private UnityEvent _onThirstReachedMax;
        /// <summary>
        /// Triggered when dehydration begins.
        /// </summary>
        [SerializeField] private UnityEvent _onDehydrationBegin;
        /// <summary>
        /// Triggered when dehydration stops.
        /// </summary>
        [SerializeField] private UnityEvent _onDehydrationStop;
        /// <summary>
        /// Triggered when the extra pool of the thirst increase amount has changed
        /// </summary>
        [SerializeField] private UnityEvent _onExtraThirstIncreaseAmountChanged;
        #endregion

        #region [Fields] Audio
        [SerializeField] private AudioSource _audioSource;
        #endregion

        #region [Properties] Hunger
        /// <summary>
        /// Gets or sets the [Use Hunger] state.
        /// </summary>
        public bool UseHunger
        {
            get
            {
                return _useHunger && HungerSettings != null;
            }
            set { _useHunger = value; }
        }

        /// <summary>
        /// Gets or sets the [Hunger Settings].
        /// </summary>
        public HungerSettings HungerSettings
        {
            get { return _hungerSettings; }
            set
            {
                if(value != null)
                {
                    _hungerSettings = value;
                }
                else
                {
                    Debug.LogError("Hunger settings can't be set to null.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the [Extra Hunger Increase] amount.
        /// </summary>
        public float ExtraHungerIncreaseAmount
        {
            get { return _extraHungerIncreaseAmount; }
            set
            {
                _extraHungerIncreaseAmount = Mathf.Clamp(value, 0.0f, 50.0f);

                if (OnExtraHungerIncreaseAmountChanged != null)
                {
                    OnExtraHungerIncreaseAmountChanged.Invoke();
                }
            }
        }

        /// <summary>
        /// Gets the current [Hunger] percentage. Range is 0 - 1.
        /// </summary>
        public float HungerPercentage
        {
            get { return Hunger / HungerSettings.MaxValue; }
        }

        /// <summary>
        /// Gets the current [Hunger] value.
        /// </summary>
        public float Hunger
        {
            get { return _hunger; }
        }

        /// <summary>
        /// Gets the [On Hunger Changed] event
        /// </summary>
        public UnityEvent OnHungerChanged
        {
            get { return _onHungerChanged; }
        }

        /// <summary>
        /// Gets the [On Hunger Reached Max] event
        /// </summary>
        public UnityEvent OnHungerReachedMax
        {
            get { return _onHungerReachedMax; }
        }
        
        /// <summary>
        /// Gets the [On Starvation Begin] event
        /// </summary>
        public UnityEvent OnStarvationBegin
        {
            get { return _onStarvationBegin; }
        }

        /// <summary>
        /// Gets the [On Starvation Stop] event
        /// </summary>
        public UnityEvent OnStarvationStop
        {
            get { return _onStarvationStop; }
        }

        /// <summary>
        /// Gets the [On Extra Hunger Increase Amount Changed] event
        /// </summary>
        public UnityEvent OnExtraHungerIncreaseAmountChanged
        {
            get { return _onExtraHungerIncreaseAmountChanged; }
        }
        #endregion

        #region [Properties] Thirst
        /// <summary>
        /// Gets or sets the [Use Thirst] state.
        /// </summary>
        public bool UseThirst
        {
            get
            {
                return _useThirst && ThirstSettings != null;
            }
            set { _useThirst = value; }
        }

        /// <summary>
        /// Gets or sets the [Thirst Settings].
        /// </summary>
        public ThirstSettings ThirstSettings
        {
            get { return _thirstSettings; }
            set
            {
                if (value != null)
                {
                    _thirstSettings = value;
                }
                else
                {
                    Debug.LogError("Thirst settings can't be set to null.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the [Extra Thirst Increase] amount.
        /// </summary>
        public float ExtraThirstIncreaseAmount
        {
            get { return _extraThirstIncreaseAmount; }
            set
            {
                _extraThirstIncreaseAmount = Mathf.Clamp(value, 0.0f, 50.0f);

                if (OnExtraHungerIncreaseAmountChanged != null)
                {
                    OnExtraHungerIncreaseAmountChanged.Invoke();
                }
            }
        }

        /// <summary>
        /// Gets the current [Thirst] percentage. Range is 0 - 1.
        /// </summary>
        public float ThirstPercentage
        {
            get { return Thirst / ThirstSettings.MaxValue; }
        }
        
        /// <summary>
        /// Gets [Thirst}.
        /// </summary>
        public float Thirst
        {
            get { return _thirst; }
        }

        /// <summary>
        /// Gets the [On Thirst Changed] event
        /// </summary>
        public UnityEvent OnThirstChanged
        {
            get { return _onThirstChanged; }
        }
        
        /// <summary>
        /// Gets the [On Thirst Reached Max] event
        /// </summary>
        public UnityEvent OnThirstReachedMax
        {
            get { return _onThirstReachedMax; }
        }

        /// <summary>
        /// Gets the [On Dehydration Begin] event
        /// </summary>
        public UnityEvent OnDehydrationBegin
        {
            get { return _onDehydrationBegin; }
        }

        /// <summary>
        /// Gets the [On Dehydration Stop] event
        /// </summary>
        public UnityEvent OnDehydrationStop
        {
            get { return _onDehydrationStop; }
        }

        /// <summary>
        /// Gets the [On Extra Thirst Increase Amount Changed] event
        /// </summary>
        public UnityEvent OnExtraThirstIncreaseAmountChanged
        {
            get { return _onExtraThirstIncreaseAmountChanged; }
        }
        #endregion

        #region [Methods] Unity
        void Start()
        {
            if (UseHunger)
            {
                _nextHungerIncrease = Time.time + HungerSettings.IncreaseInterval;
            }

            if (UseThirst)
            {
                _nextThirstIncrease = Time.time + ThirstSettings.IncreaseInterval;
            }
        }

        void Update()
        {
            if (UseHunger)
            {
                if (_nextHungerIncrease <= Time.time)
                {
                    AddHunger(HungerSettings.IncreaseAmount);
                    _nextHungerIncrease = Time.time + HungerSettings.IncreaseInterval;
                }
            }

            if (UseThirst)
            {
                if (_nextThirstIncrease <= Time.time)
                {
                    AddThirst(ThirstSettings.IncreaseAmount);
                    _nextThirstIncrease = Time.time + ThirstSettings.IncreaseInterval;
                }
            }
        }
        #endregion

        #region [Methods] Hunger
        /*============================
         * Hunger methods
         *============================*/
        /// <summary>
        /// Sets hunger.
        /// </summary>
        /// <param name="newHunger">The new hunger value.</param>
        public void SetHunger(float newHunger)
        {
            if (!UseHunger)
            {
                return;
            }

            _hunger = Mathf.Clamp(newHunger, 0, HungerSettings.MaxValue);
            CheckForStarvation();

            if (OnHungerChanged != null)
            {
                OnHungerChanged.Invoke();
            }

            if (HungerPercentage >= 1.0f)
            {
                OnHungerReachedMax.Invoke();
            }
        }

        /// <summary>
        /// Adds hunger.
        /// </summary>
        /// <param name="amount">The amount of hunger to add.</param>
        public void AddHunger(float amount)
        {
            SetHunger(Hunger + (amount + ExtraHungerIncreaseAmount));
        }

        /// <summary>
        /// Reduces hunger.
        /// </summary>
        /// <param name="amount">The amount to reduce with</param>
        public void ReduceHunger(float amount)
        {
            SetHunger(Hunger - amount);
        }

        /// <summary>
        /// <para>Checks for starvation.</para>
        /// <para>If there's any starvation then IsStarving will be set to true.</para>
        /// </summary>
        public void CheckForStarvation()
        {
            if(!UseHunger)
            {
                return;
            }

            bool previousState = HungerSettings.IsStarving;
            float hungerPercentage = Hunger / HungerSettings.MaxValue * 100;
            HungerSettings.IsStarving = hungerPercentage >= HungerSettings.BeginStarvationPercentage;

            if (previousState != HungerSettings.IsStarving)
            {
                if (HungerSettings.IsStarving)
                {
                    if (OnStarvationBegin != null)
                    {
                        OnStarvationBegin.Invoke();
                    }

                    if(HungerSettings.UseSounds)
                    {
                        PlayOneShot(HungerSettings.StarvationAudioClip);
                    }
                }
                else
                {
                    if (OnStarvationStop != null)
                    {
                        OnStarvationStop.Invoke();
                    }
                }
            }
        }

        /// <summary>
        /// Adds to the extra pool of the hunger increase amount will add at the next increase.
        /// </summary>
        /// <param name="addAmount">How much to add.</param>
        public void AddExtraHungerIncreaseAmount(float addAmount)
        {
            ExtraHungerIncreaseAmount += addAmount;
        }

        /// <summary>
        /// Reduces the extra pool of the hunger increase amount will add at the next increase.
        /// </summary>
        /// <param name="reduceAmount">How much to reduce with</param>
        public void ReduceExtraHungerIncreaseAmount(float reduceAmount)
        {
            ExtraHungerIncreaseAmount -= reduceAmount;
        }

        /// <summary>
        /// Returns the estimated time it takes to enter starving state
        /// </summary>
        public TimeSpan GetTimeTillStarvation()
        {
            // Convert the starvation percentage to real numbers
            float beginStarvationValue = HungerSettings.BeginStarvationPercentage / 100.0f * HungerSettings.MaxValue;

            // Calculate the estimated time
            float increaseCount = Mathf.Ceil(beginStarvationValue / HungerSettings.IncreaseAmount); // How many times hunger will be increased
            float totalSeconds = HungerSettings.IncreaseInterval * increaseCount; // The estimated time in seconds
            return TimeSpan.FromSeconds(Mathf.Ceil(totalSeconds));
        }
        #endregion

        #region [Methods] Thirst
        /*============================
         * Thirst methods
         *============================*/
        /// <summary>
        /// Sets thirst.
        /// </summary>
        /// <param name="newThirst">The new thirst value.</param>
        public void SetThirst(float newThirst)
        {
            if (!UseThirst)
            {
                return;
            }

            _thirst = Mathf.Clamp(newThirst, 0, ThirstSettings.MaxValue);
            CheckForDehydration();

            if (OnThirstChanged != null)
            {
                OnThirstChanged.Invoke();
            }
            
            if (ThirstPercentage >= 1.0f)
            {
                OnThirstReachedMax.Invoke();
            }
        }

        /// <summary>
        /// Adds thirst.
        /// </summary>
        /// <param name="amount">The amount of thirst to add.</param>
        public void AddThirst(float amount)
        {
            SetThirst(Thirst + (amount + ExtraThirstIncreaseAmount));
        }

        /// <summary>
        /// Reduces thirst.
        /// </summary>
        /// <param name="amount">The amount to reduce with.</param>
        public void ReduceThirst(float amount)
        {
            SetThirst(Thirst - amount);
        }

        /// <summary>
        /// <para>Checks for dehydration.</para>
        /// <para>If there's any dehydration then IsDehydrated will be set to true.</para>
        /// </summary>
        public void CheckForDehydration()
        {
            if (!UseThirst)
            {
                return;
            }

            bool previousState = ThirstSettings.IsDehydrated;
            float thirstPercentage = ThirstPercentage * 100;
            ThirstSettings.IsDehydrated = thirstPercentage >= ThirstSettings.BeginDehydrationPercentage;

            if (previousState != ThirstSettings.IsDehydrated)
            {
                if (ThirstSettings.IsDehydrated)
                {
                    if (OnDehydrationBegin != null)
                    {
                        OnDehydrationBegin.Invoke();
                    }

                    if (ThirstSettings.UseSounds)
                    {
                        PlayOneShot(ThirstSettings.DehydrationAudioClip);
                    }
                }
                else
                {
                    if (OnDehydrationStop != null)
                    {
                        OnDehydrationStop.Invoke();
                    }
                }
            }
        }

        /// <summary>
        /// Adds to the extra pool of the thirst increase amount will add at the next increase.
        /// </summary>
        /// <param name="addAmount">How much to add.</param>
        public void AddExtraThirstIncreaseAmount(float addAmount)
        {
            ExtraThirstIncreaseAmount += addAmount;
        }

        /// <summary>
        /// Reduces the extra pool of the thirst increase amount will add at the next increase.
        /// </summary>
        /// <param name="reduceAmount">How much to reduce with.</param>
        public void ReduceExtraThirstIncreaseAmount(float reduceAmount)
        {
            ExtraThirstIncreaseAmount -= reduceAmount;
        }

        /// <summary>
        /// Returns the estimated time it takes to enter dehydrated state
        /// </summary>
        public TimeSpan GetTimeTillDehydration()
        {
            // Convert the dehydration percentage to real numbers
            float beginDehydrationValue = ThirstSettings.BeginDehydrationPercentage / 100.0f * ThirstSettings.MaxValue;

            // Calculate the estimated time
            float increaseCount = Mathf.Ceil(beginDehydrationValue / ThirstSettings.IncreaseAmount); // How many times thirst will be increased
            float totalSeconds = ThirstSettings.IncreaseInterval * increaseCount; // The estimated time in seconds
            return TimeSpan.FromSeconds(Mathf.Ceil(totalSeconds));
        }
        #endregion

        #region [Methods] Audio Utilities
        private void PlayOneShot(AudioClip clip)
        {
            if(clip == null)
            {
                return;
            }

            if (_audioSource != null)
            {
                _audioSource.PlayOneShot(clip);
            }
        }
        #endregion
    }
}