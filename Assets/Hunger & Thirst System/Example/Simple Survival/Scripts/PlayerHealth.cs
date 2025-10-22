using UnityEngine;
using UnityEngine.UI;

namespace DeepWolf.HungerThirstSystem.Examples
{
    public class PlayerHealth : MonoBehaviour
    {
        /*******************
         * Fields
         *******************/
        /// <summary>
        /// The amount of current health.
        /// </summary>
        private float _health = 100;

        /// <summary>
        /// The maximum amount of health.
        /// </summary>
        [Tooltip("The maximum amount of health.")] [SerializeField] private float _maxHealth = 100;

        /// <summary>
        /// The interval for decreasing health.
        /// </summary>
        [Tooltip("The interval for decreasing health.")] [SerializeField] private float _healthDecreaseInterval = 1.0f;

        /// <summary>
        /// The amount to decrease health with.
        /// </summary>
        [Tooltip("The amount to decrease health with.")] [SerializeField] private float _healthDecreaseAmount = 3.0f;

        /// <summary>
        /// When's the next time till health decrease.
        /// </summary>
        private float _nextHealthDecrease = 0.0f;

        /// <summary>
        /// Should the HUD be shown?
        /// </summary>
        [Header("HUD")] [Tooltip("Should the HUD be shown?")] [SerializeField] private bool _showHUD;

        /// <summary>
        /// The health label.
        /// </summary>
        [Tooltip("The health label.")] [SerializeField] private Text _healthLbl;

        /// <summary>
        /// A reference to the hunger thirst script
        /// </summary>
        private HungerThirst _hungerThirst;

        /*******************
         * Properties
         *******************/
        /// <summary>
        /// Gets the health decrease amount.
        /// </summary>
        public float HealthDecreaseAmount
        {
            get
            {
                if(_hungerThirst.HungerSettings != null && _hungerThirst.ThirstSettings != null)
                {
                    bool isStarving = _hungerThirst.HungerSettings.IsStarving;
                    bool isDehydrated = _hungerThirst.ThirstSettings.IsDehydrated;

                    // Health decrease amount is multiplied by 2 if starving and dehydrated. 0 if not starving or dehydrated. 1 if one of them is true
                    return _healthDecreaseAmount * (isStarving && isDehydrated ? 2 : (!isStarving && !isDehydrated ? 0 : 1));
                }
                else
                {
                    bool hasHungerSettings = _hungerThirst.HungerSettings != null;
                    bool hasThirstSettings = _hungerThirst.ThirstSettings != null;

                    return _healthDecreaseAmount * (hasHungerSettings ? (_hungerThirst.HungerSettings.IsStarving ? 1 : 0) : hasThirstSettings ? (_hungerThirst.ThirstSettings.IsDehydrated ? 1 : 0) : 0);
                }
            }
        }

        /*******************
         * Methods
         *******************/
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            _hungerThirst = GetComponent<HungerThirst>();
            _health = _maxHealth;

            _healthLbl.gameObject.SetActive(_showHUD);
            RefreshHUD();
        }

        void Update()
        {
            // Handles the decrease health stuff when starving or dehydrated
            if (_hungerThirst.HungerSettings != null && _hungerThirst.HungerSettings.IsStarving || _hungerThirst.ThirstSettings != null && _hungerThirst.ThirstSettings.IsDehydrated)
            {
                if (_nextHealthDecrease <= Time.time)
                {
                    SubHealth(HealthDecreaseAmount);
                    _nextHealthDecrease = Time.time + _healthDecreaseInterval;
                }
            }
        }

        /// <summary>
        /// Sets the health.
        /// </summary>
        /// <param name="newHealth">The new health value</param>
        private void SetHealth(float newHealth)
        {
            _health = Mathf.Clamp(newHealth, 0, _maxHealth);
            RefreshHUD();
        }

        /// <summary>
        /// Adds health.
        /// </summary>
        /// <param name="amount">The amount of health to add</param>
        public void AddHealth(float amount)
        {
            SetHealth(_health + amount);
        }

        /// <summary>
        /// Subtracts health.
        /// </summary>
        /// <param name="amount">The amount of health to subtract</param>
        public void SubHealth(float amount)
        {
            SetHealth(_health - amount);
        }

        /// <summary>
        /// Refreshes the HUD.
        /// </summary>
        private void RefreshHUD()
        {
            if (!_showHUD || _healthLbl == null)
            {
                return;
            }

            _healthLbl.text = _health.ToString();
        }
    }
}