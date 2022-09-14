// Cristian Pop - https://boxophobic.com/

using BOXOPHOBIC.Utils.Scripts.StyledInspector;
using UnityEditor;
using UnityEngine;

namespace BOXOPHOBIC.Utils.Editor.StyledInspector
{
    [CustomPropertyDrawer(typeof(StyledBanner))]
    public class StyledBannerAttributeDrawer : PropertyDrawer
    {
        StyledBanner a;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            a = (StyledBanner)attribute;

            var bannerColor = new Color(a.colorR, a.colorG, a.colorB);

            StyledGUI.StyledGUI.DrawInspectorBanner(bannerColor, a.title, a.helpURL);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return -2;
        }
    }
}
