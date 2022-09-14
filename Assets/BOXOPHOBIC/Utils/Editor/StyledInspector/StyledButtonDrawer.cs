// Cristian Pop - https://boxophobic.com/

using BOXOPHOBIC.Utils.Scripts.StyledInspector;
using UnityEditor;
using UnityEngine;

namespace BOXOPHOBIC.Utils.Editor.StyledInspector
{
    [CustomPropertyDrawer(typeof(StyledButton))]
    public class StyledButtonAttributeDrawer : PropertyDrawer
    {
        StyledButton a;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            a = (StyledButton)attribute;

            GUILayout.Space(a.Top);

            if (GUILayout.Button(a.Text))
            {
                property.boolValue = true;
            }

            GUILayout.Space(a.Down);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return -2;
        }
    }
}

