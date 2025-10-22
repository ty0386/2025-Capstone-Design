using UnityEngine;
using UnityEditor;

public static class EditorGUILayoutUtilities
{
    /// <summary>
    /// Draws a slider that shows the maximum amount. The maximum amount can't be changed.
    /// </summary>
    /// <param name="property">The serialized property</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <param name="readOnly">Whether the max value should be readonly or not.</param>
    /// <param name="options">The options, like GUILayout.Width.</param>
    public static void SliderWithMax(SerializedProperty property, float min, float max, params GUILayoutOption[] options)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Slider(property, min, max, options);
        EditorGUILayout.LabelField(string.Format("/ {0}", max), GUILayout.Width(65));
        EditorGUILayout.EndHorizontal();
    }

    /// <summary>
    /// Draws a slider that shows the maximum amount. The maximum amount can't be changed.
    /// </summary>
    /// <param name="label">The label text.</param>
    /// <param name="value">The value.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <param name="readOnly">Whether the max value should be readonly or not.</param>
    /// <param name="options">The options, like GUILayout.Width.</param>
    public static float SliderWithMax(string label, float value, float min, float max, params GUILayoutOption[] options)
    {
        EditorGUILayout.BeginHorizontal();
        float newValue = EditorGUILayout.Slider(label, value, min, max, options);
        EditorGUILayout.LabelField(string.Format("/ {0}", max), GUILayout.Width(65));
        EditorGUILayout.EndHorizontal();

        return newValue;
    }

    /// <summary>
    /// Draws a float field that uses a serialized property.
    /// </summary>
    /// <param name="label">The label text.</param>
    /// <param name="property">The property to use.</param>
    /// <param name="options">The options you want to use.</param>
    public static void FloatFieldProperty(SerializedProperty property, params GUILayoutOption[] options)
    {
        property.floatValue = EditorGUILayout.FloatField(property.displayName, property.floatValue, options);
    }

    public static void ToggleLeft(SerializedProperty property, params GUILayoutOption[] options)
    {
        property.boolValue = EditorGUILayout.ToggleLeft(property.displayName, property.boolValue, options);
    }
}