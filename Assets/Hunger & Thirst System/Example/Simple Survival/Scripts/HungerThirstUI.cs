using UnityEngine;
using UnityEngine.UI;

namespace DeepWolf.HungerThirstSystem.Examples
{
    public class HungerThirstUI : MonoBehaviour
    {
        #region Fields
        /// <summary>
        /// Reference to the hunger and thirst script
        /// </summary>
        [SerializeField] private HungerThirst _hungerThirst;

        /*******************
         * UI - Fields
         *******************/
        /// <summary>
        /// Should the hunger UI be shown?
        /// </summary>
        [Header("[UI]")]
        [Tooltip("Should the hunger UI be shown?")] [SerializeField] private bool _showHungerUI;

        /// <summary>
        /// The hunger label that shows the current hunger.
        /// </summary>
        [Tooltip("The hunger label that shows the current hunger.")] [SerializeField] private Text _hungerLbl;

        /// <summary>
        /// The hunger bar that shows the current hunger.
        /// </summary>
        [Tooltip("The hunger bar that shows the current hunger.")] [SerializeField] private Slider _hungerBar;
        
        /// <summary>
        /// Whether or not the hunger text should be shown as percentage.
        /// </summary>
        [Tooltip("Whether or not the hunger text should be shown as percentage.")] [SerializeField] private bool _showHungerAsPercentage;
        
        /// <summary>
        /// Whether or not the hunger bar should be inverted (if inverted the bar will go down instead if getting more hungry).
        /// </summary>
        [Tooltip("Whether or not the hunger bar should be inverted (if inverted the bar will go down instead if getting more hungry).")] [SerializeField] private bool _invertHunger;

        /// <summary>
        /// Should the thirst UI be shown?
        /// </summary>
        [Tooltip("Should the thirst UI be shown?")] [SerializeField] private bool _showThirstUI;

        /// <summary>
        /// The thirst label that shows the current thirst.
        /// </summary>
        [Tooltip("The thirst label that shows the current thirst.")] [SerializeField] private Text _thirstLbl;

        /// <summary>
        /// The thirst bar that shows the current thirst.
        /// </summary>
        [Tooltip("The thirst bar that shows the current thirst.")] [SerializeField] private Slider _thirstBar;
        
        /// <summary>
        /// Whether or not the thirst text should be shown as percentage.
        /// </summary>
        [Tooltip("Whether or not the thirst text should be shown as percentage.")] [SerializeField] private bool _showThirstAsPercentage;
        
        /// <summary>
        /// Whether or not the thirst bar should be inverted (if inverted the bar will go down instead if getting more thirsty).
        /// </summary>
        [Tooltip("Whether or not the thirst bar should be inverted (if inverted the bar will go down instead if getting more thirsty).")] [SerializeField] private bool _invertThirst;
        #endregion

        /*******************
         * Properties
         *******************/
        #region Properties
        /// <summary>
        /// Gets or sets the show hunger UI state
        /// </summary>
        public bool ShowHungerUI
        {
            get { return _showHungerUI && HungerThirst.UseHunger; }
            set
            {
                _showHungerUI = value;
                CheckUI();
            }
        }

        /// <summary>
        /// Gets or sets the show thirst UI state
        /// </summary>
        public bool ShowThirstUI
        {
            get { return _showThirstUI && HungerThirst.UseThirst; }
            set
            {
                _showThirstUI = value;
                CheckUI();
            }
        }

        private HungerThirst HungerThirst
        {
            get
            {
                if (_hungerThirst == null)
                {
                    _hungerThirst = GetComponent<HungerThirst>();
                }
                return _hungerThirst;
            }
        }
        #endregion

        /*============================
         * Unity methods
         *============================*/
        #region Unity Methods
        private void Start()
        {
            CheckUI();
            if (ShowHungerUI)
            { RefreshHunger(); }

            if (ShowThirstUI)
            { RefreshThirst(); }
        }
        #endregion

        /*============================
         * UI methods
         *============================*/
        #region UI Methods
        /// <summary>
        /// Refreshes the hunger UI.
        /// </summary>
        public void RefreshHunger()
        {
            if (_hungerLbl != null)
            {
                if (_invertHunger)
                {
                    if (_showHungerAsPercentage)
                    {
                        _hungerLbl.text = string.Format("{0}%", Mathf.Round(Mathf.Lerp(100.0f, 0.0f, _hungerThirst.HungerPercentage)).ToString());
                    }
                    else
                    {
                        _hungerLbl.text = Mathf.Round(Mathf.Lerp(_hungerThirst.HungerSettings.MaxValue, 0.0f, _hungerThirst.HungerPercentage)).ToString();
                    }
                }
                else
                {
                    if (_showHungerAsPercentage)
                    {
                        _hungerLbl.text = string.Format("{0}%", Mathf.Round(Mathf.Lerp(0.0f, 100.0f, _hungerThirst.HungerPercentage)).ToString());
                    }
                    else
                    {
                        _hungerLbl.text = Mathf.Round(Mathf.Lerp(0.0f, _hungerThirst.HungerSettings.MaxValue, _hungerThirst.HungerPercentage)).ToString();
                    }
                }
            }

            if (_hungerBar != null)
            {
                if (_invertHunger)
                {
                    _hungerBar.value = Mathf.Lerp(1.0f, 0.0f, HungerThirst.HungerPercentage);
                    return;
                }
                
                _hungerBar.value = Mathf.Lerp(0.0f, 1.0f, HungerThirst.HungerPercentage);
            }
        }

        /// <summary>
        /// Refreshes the thirst UI.
        /// </summary>
        public void RefreshThirst()
        {
            if (_thirstLbl != null)
            {
                if (_invertThirst)
                {
                    if (_showThirstAsPercentage)
                    {
                        _thirstLbl.text = string.Format("{0}%", Mathf.Round(Mathf.Lerp(100.0f, 0.0f, _hungerThirst.ThirstPercentage)).ToString());
                    }
                    else
                    {
                        _thirstLbl.text = Mathf.Round(Mathf.Lerp(_hungerThirst.ThirstSettings.MaxValue, 0.0f, _hungerThirst.ThirstPercentage)).ToString();
                    }
                }
                else
                {
                    if (_showThirstAsPercentage)
                    {
                        _thirstLbl.text = string.Format("{0}%", Mathf.Round(Mathf.Lerp(0.0f, 100.0f, _hungerThirst.ThirstPercentage)).ToString());
                    }
                    else
                    {
                        _thirstLbl.text = Mathf.Round(Mathf.Lerp(0.0f, _hungerThirst.ThirstSettings.MaxValue, _hungerThirst.ThirstPercentage)).ToString();
                    }
                }
            }

            if (_thirstBar != null)
            {
                if (_invertThirst)
                {
                    _thirstBar.value = Mathf.Lerp(1.0f, 0.0f, HungerThirst.ThirstPercentage);
                    return;
                }
                
                _thirstBar.value = _hungerThirst.ThirstPercentage;
            }
        }

        /// <summary>
        /// <para>Checks if the UI needs to be shown or not</para>
        /// <para>If the UI needs to be shown it will enable it, or if the UI needs to be hidden, it will hide it</para>
        /// </summary>
        private void CheckUI()
        {
            if (_thirstLbl != null)
            {
                _thirstLbl.gameObject.SetActive(ShowThirstUI);
                _thirstBar.gameObject.SetActive(ShowThirstUI);
            }
            else
            {
                if (ShowThirstUI)
                {
                    Debug.LogError("Thirst label is missing.", this);
                }
            }

            if (_hungerLbl != null)
            {
                _hungerLbl.gameObject.SetActive(ShowHungerUI);
                _hungerBar.gameObject.SetActive(ShowHungerUI);
            }
            else
            {
                if (ShowHungerUI)
                {
                    Debug.LogError("Hunger label is missing.", this);
                }
            }
        }

        #endregion
    }
}