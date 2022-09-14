// Cristian Pop - https://boxophobic.com/

using BOXOPHOBIC.Utils.Scripts.StyledInspector;
using UnityEditor;
using UnityEngine;

namespace BOXOPHOBIC.Utils.Editor.StyledInspector
{
    [CustomPropertyDrawer(typeof(StyledIndent))]
    public class StyledIndentAttributeDrawer : PropertyDrawer
    {
        StyledIndent a;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            a = (StyledIndent)attribute;

            EditorGUI.indentLevel = a.indent;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return -2;
        }
    }
}
