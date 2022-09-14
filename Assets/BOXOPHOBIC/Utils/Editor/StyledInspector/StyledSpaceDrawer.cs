// Cristian Pop - https://boxophobic.com/

using BOXOPHOBIC.Utils.Scripts.StyledInspector;
using UnityEditor;
using UnityEngine;

namespace BOXOPHOBIC.Utils.Editor.StyledInspector
{
    [CustomPropertyDrawer(typeof(StyledSpace))]
    public class StyledSpaceAttributeDrawer : PropertyDrawer
    {
        StyledSpace a;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            a = (StyledSpace)attribute;

            GUILayout.Space(a.space);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return -2;
        }
    }
}
