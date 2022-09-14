// Cristian Pop - https://boxophobic.com/

using BOXOPHOBIC.Utils.Scripts.StyledInspector;
using UnityEditor;
using UnityEngine;

namespace BOXOPHOBIC.Utils.Editor.StyledInspector
{
    [CustomPropertyDrawer(typeof(StyledText))]
    public class StyledTextAttributeDrawer : PropertyDrawer
    {
        StyledText a;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            a = (StyledText)attribute;

            GUIStyle styleLabel = new GUIStyle(EditorStyles.label)
            {
                richText = true,
                wordWrap = true
            };

            styleLabel.alignment = a.alignment;

            GUILayout.Space(a.top);

            GUILayout.Label(property.stringValue, styleLabel);

            GUILayout.Space(a.down);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return -2;
        }
    }
}

