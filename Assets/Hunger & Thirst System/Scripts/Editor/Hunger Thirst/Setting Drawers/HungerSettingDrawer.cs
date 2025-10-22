using UnityEngine;
using UnityEditor;
using TimeSpan = System.TimeSpan;

namespace DeepWolf.HungerThirstSystem
{
    /// <summary>
    /// Responsible for drawing the <see cref="HungerSettings"/> in the <see cref="HungerThirstEditor"/>
    /// </summary>
    internal class HungerSettingDrawer : SettingDrawer
    {
        #region [Fields] Serialized Properties
        /// <summary>
        /// The <see cref="SerializedProperty"/> for the [Use Hunger] <see cref="bool"/>.
        /// </summary>
        protected SerializedProperty useProp;

        /// <summary>
        /// The <see cref="SerializedProperty"/> for the current selected <see cref="HungerSettings"/> preset.
        /// </summary>
        protected SerializedProperty presetSlotProp;

        protected SerializedProperty onHungerChanged;
        protected SerializedProperty onHungerReachedMax;
        protected SerializedProperty onStarvationBegin;
        protected SerializedProperty onStarvationStop;
        protected SerializedProperty onExtraHungerIncreaseAmountChanged;
        #endregion

        #region [Fields] Setting Properties
        protected HungerSettingProperty selectedPreset;
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
        /// Gets the hunger settings
        /// </summary>
        protected HungerSettings Settings
        {
            get
            {
                return (HungerSettings)presetSlotProp.objectReferenceValue;
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
            presetSlotProp = serializedObject.FindProperty("_hungerSettings");

            #region Setup serialized properties
            useProp = serializedObject.FindProperty("_useHunger");
            onExtraHungerIncreaseAmountChanged = serializedObject.FindProperty("_onExtraHungerIncreaseAmountChanged");

            onHungerChanged = serializedObject.FindProperty("_onHungerChanged");
            onHungerReachedMax = serializedObject.FindProperty("_onHungerReachedMax");
            onStarvationBegin = serializedObject.FindProperty("_onStarvationBegin");
            onStarvationStop = serializedObject.FindProperty("_onStarvationStop");
            #endregion

            SetPreset(Settings);
        }

        #region [Methods] Drawing
        /// <summary>
        /// Draws the <see cref="HungerSettings"/>.
        /// </summary>
        public override void Draw()
        {
            DrawHeader();

            if (useProp.boolValue)
            {
                if (selectedPreset == null)
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
                    EditorGUILayoutUtilities.SliderWithMax(selectedPreset.BeginStarvationPercentage, 1, 100);
                    break;
                case 1:
                    selectedPreset.UseSounds.boolValue = EditorGUILayout.BeginToggleGroup(selectedPreset.UseSounds.displayName, selectedPreset.UseSounds.boolValue);
                    EditorGUI.indentLevel = 1;
                    EditorGUILayout.PropertyField(selectedPreset.StarvationAudioClip);
                    EditorGUI.indentLevel = 0;
                    EditorGUILayout.EndToggleGroup();
                    break;
                case 2:
                    serializedObject.Update();

                    EditorGUILayout.PropertyField(onHungerChanged);
                    EditorGUILayout.PropertyField(onHungerReachedMax);
                    EditorGUILayout.PropertyField(onStarvationBegin);
                    EditorGUILayout.PropertyField(onStarvationStop);
                    EditorGUILayout.PropertyField(onExtraHungerIncreaseAmountChanged);

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
            SetPreset((HungerSettings)EditorGUILayout.ObjectField(Settings, typeof(HungerSettings), false, GUILayout.Width(PresetObjectFieldWidth)));
        }

        /// <summary>
        /// Draws the estimated time till starvation.
        /// </summary>
        protected void DrawEstimation()
        {
            TimeSpan estimatedTimeStarvation = hungerThirst.GetTimeTillStarvation();
            EditorGUILayout.LabelField(string.Format("Estimated time till starvation: {0} hour(s) {1} minute(s) {2} second(s)", Mathf.Floor((float)estimatedTimeStarvation.TotalHours), estimatedTimeStarvation.Minutes, estimatedTimeStarvation.Seconds), EditorStyles.miniLabel);
        }
        #endregion

        /// <summary>
        /// Sets the preset to the specified one.
        /// </summary>
        /// <param name="newPreset">The new preset.</param>
        protected void SetPreset(HungerSettings newPreset)
        {
            if(Settings != newPreset && newPreset != null || selectedPreset == null && newPreset != null)
            {
                selectedPreset = new HungerSettingProperty(newPreset);
                Settings = newPreset;
            }
        }
    }
}