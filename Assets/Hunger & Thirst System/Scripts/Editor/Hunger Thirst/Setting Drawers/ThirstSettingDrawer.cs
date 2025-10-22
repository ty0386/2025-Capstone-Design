using UnityEngine;
using UnityEditor;
using TimeSpan = System.TimeSpan;

namespace DeepWolf.HungerThirstSystem
{
    /// <summary>
    /// Responsible for drawing the <see cref="ThirstSettings"/> in the <see cref="HungerThirstEditor"/>
    /// </summary>
    internal class ThirstSettingDrawer : SettingDrawer
    {
        #region [Fields] Serialized Properties
        /// <summary>
        /// The <see cref="SerializedProperty"/> for the [Use Thirst] <see cref="bool"/>.
        /// </summary>
        protected SerializedProperty useProp;

        /// <summary>
        /// The <see cref="SerializedProperty"/> for the current selected <see cref="ThirstSettings"/> preset.
        /// </summary>
        protected SerializedProperty presetSlotProp;

        protected SerializedProperty onThirstChanged;
        protected SerializedProperty onThirstReachedMax;
        protected SerializedProperty onDehydrationBegin;
        protected SerializedProperty onDehydrationStop;
        protected SerializedProperty onExtraThirstIncreaseAmountChanged;
        #endregion

        #region [Fields] Setting Properties
        protected ThirstSettingProperty selectedPreset;
        #endregion

        #region [Fields] Tabs
        protected int selectedTab;
        protected readonly string[] _tabs = { "General", "Sounds", "Events" };
        #endregion

        #region [Fields] References
        protected HungerThirst hungerThirst;
        #endregion

        #region [Properties] Settings
        /// <summary>
        /// Gets the <see cref="ThirstSettings"/>
        /// </summary>
        protected ThirstSettings Settings
        {
            get
            {
                return (ThirstSettings)presetSlotProp.objectReferenceValue;
            }
            set
            {
                presetSlotProp.objectReferenceValue = value;
            }
        }
        #endregion

        public override void Initialize(SerializedObject serializedObject)
        {
            this.serializedObject = serializedObject;
            hungerThirst = (HungerThirst)serializedObject.targetObject;
            presetSlotProp = serializedObject.FindProperty("_thirstSettings");

            #region Setup serialized properties
            useProp = serializedObject.FindProperty("_useThirst");
            onExtraThirstIncreaseAmountChanged = serializedObject.FindProperty("_onExtraThirstIncreaseAmountChanged");

            onThirstChanged = serializedObject.FindProperty("_onThirstChanged");
            onThirstReachedMax = serializedObject.FindProperty("_onThirstReachedMax");
            onDehydrationBegin = serializedObject.FindProperty("_onDehydrationBegin");
            onDehydrationStop = serializedObject.FindProperty("_onDehydrationStop");
            #endregion

            SetPreset(Settings);
        }

        #region [Methods] Drawing
        /// <summary>
        /// Draws the <see cref="ThirstSettings"/>.
        /// </summary>
        public override void Draw()
        {
            DrawHeader();

            if (useProp.boolValue)
            {
                if(selectedPreset == null)
                {
                    EditorGUILayout.HelpBox("To make changes, attach a preset setting to the preset slot.", MessageType.Info);
                }
                else
                {
                    DrawContent();
                }
            }
        }

        /// <summary>
        /// Draws the header.
        /// </summary>
        protected override void DrawHeader()
        {
            serializedObject.Update();

            EditorGUILayout.BeginHorizontal(EditorStyles.toolbarButton);
            EditorGUILayoutUtilities.ToggleLeft(useProp);

            // Make some space between the [Use Setting] toggle and the preset selector. This does so the preset selector will stick to the right side.
            GUILayout.FlexibleSpace();

            DrawPresetSelector();
            EditorGUILayout.EndHorizontal();

            if (GUI.changed)
            {
                serializedObject.ApplyModifiedProperties();
            }
        }

        /// <summary>
        /// Draws the content.
        /// </summary>
        protected override void DrawContent()
        {
            selectedPreset.Update();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            DrawEstimation();
            DrawTabs();

            EditorGUILayout.EndVertical();

            if (GUI.changed)
            {
                selectedPreset.ApplyChanges();
            }
        }

        /// <summary>
        /// Draws the tabs.
        /// </summary>
        protected void DrawTabs()
        {
            selectedTab = GUILayout.SelectionGrid(selectedTab, _tabs, 3, EditorStyles.toolbarButton);
            switch (selectedTab)
            {
                case 0:
                    EditorGUILayoutUtilities.FloatFieldProperty(selectedPreset.MaxValue);
                    EditorGUILayoutUtilities.SliderWithMax(selectedPreset.IncreaseInterval, 1, MaxIncreaseInterval);
                    EditorGUILayoutUtilities.SliderWithMax(selectedPreset.IncreaseAmount, 1, Settings.MaxValue);
                    EditorGUILayoutUtilities.SliderWithMax(selectedPreset.BeginDehydrationPercentage, 1, 100);
                    break;
                case 1:
                    selectedPreset.UseSounds.boolValue = EditorGUILayout.BeginToggleGroup(selectedPreset.UseSounds.displayName, selectedPreset.UseSounds.boolValue);
                    EditorGUI.indentLevel = 1;
                    EditorGUILayout.PropertyField(selectedPreset.DehydrationAudioClip);
                    EditorGUI.indentLevel = 0;
                    EditorGUILayout.EndToggleGroup();
                    break;
                case 2:
                    serializedObject.Update();

                    EditorGUILayout.PropertyField(onThirstChanged);
                    EditorGUILayout.PropertyField(onThirstReachedMax);
                    EditorGUILayout.PropertyField(onDehydrationBegin);
                    EditorGUILayout.PropertyField(onDehydrationStop);
                    EditorGUILayout.PropertyField(onExtraThirstIncreaseAmountChanged);

                    if (GUI.changed)
                    {
                        serializedObject.ApplyModifiedProperties();
                    }
                    break;
            }
        }

        /// <summary>
        /// Draws the preset selector.
        /// </summary>
        /// <param name="presetObject">The preset.</param>
        /// <returns>The new preset.</returns>
        protected void DrawPresetSelector()
        {
            EditorGUILayout.LabelField("Preset", GUILayout.Width(PresetLabelWidth));
            SetPreset((ThirstSettings)EditorGUILayout.ObjectField(Settings, typeof(ThirstSettings), false, GUILayout.Width(PresetObjectFieldWidth)));
        }

        /// <summary>
        /// Draws the estimated time till dehydration.
        /// </summary>
        protected void DrawEstimation()
        {
            TimeSpan estimatedTimeDehydration = hungerThirst.GetTimeTillDehydration();
            EditorGUILayout.LabelField(string.Format("Estimated time till dehydration: {0} hour(s) {1} minute(s) {2} second(s)", Mathf.Floor((float)estimatedTimeDehydration.TotalHours), estimatedTimeDehydration.Minutes, estimatedTimeDehydration.Seconds), EditorStyles.miniLabel);
        }
        #endregion

        /// <summary>
        /// Sets the preset to the specified one.
        /// </summary>
        /// <param name="newPreset">The new preset.</param>
        protected void SetPreset(ThirstSettings newPreset)
        {
            if (Settings != newPreset && newPreset != null || selectedPreset == null && newPreset != null)
            {
                selectedPreset = new ThirstSettingProperty(newPreset);
                Settings = newPreset;
            }
        }
    }
}