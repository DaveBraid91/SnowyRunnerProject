//#if UNITY_EDITOR

using BOXOPHOBIC.Utils.Scripts.StyledMono;
using UnityEditor;

namespace BOXOPHOBIC.Utils.Editor.StyledMono
{
    [CustomEditor(typeof(StyledScriptableObject), true)]
    [CanEditMultipleObjects]
    public class StyledScriptableObjectEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            DrawPropertiesExcluding(serializedObject, "m_Script");
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();
        }
    }
}
//#endif