using UnityEngine;
using UnityEditor;

namespace SmallGame
{
    [CustomPropertyDrawer(typeof(PlayModeReadOnlyAttribute))]
    public class PlayModeReadOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            bool wasEnabled = GUI.enabled;
            GUI.enabled = !Application.isPlaying;
            EditorGUI.PropertyField(position, property, label);
            GUI.enabled = wasEnabled;
        }
    }

}
