#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using MonoReferencing.Public;
using UnityEditor;
using UnityEngine;

namespace MonoReferencing.Editor
{
    [CustomEditor(typeof(MonoReference<>), true)]
    public class MonoReferenceEditor : UnityEditor.Editor
    {
        private const string DataFieldName = "_data";
        private const string ScriptFieldName = "m_Script";

        private SerializedProperty _scriptProperty;
        
        private SerializedProperty _dataProperty;

        private Type _dataType;
        
        private bool _isObjectReference;

        private void OnEnable()
        {
            _scriptProperty = serializedObject.FindProperty(ScriptFieldName);
            
            _dataProperty = serializedObject.FindProperty(DataFieldName);
            
            var targetType = target.GetType();

            while ((targetType.IsGenericType ? targetType.GetGenericTypeDefinition() : targetType)
                   != typeof(MonoReference<>)) targetType = targetType.BaseType;
            
            _dataType = targetType.GenericTypeArguments[0];
            
            _isObjectReference = _dataType.IsSubclassOf(typeof(UnityEngine.Object));
        }

        public override void OnInspectorGUI()
        {
            if (_isObjectReference)
            {
                base.OnInspectorGUI();
                return;
            }
            
            GUI.enabled = false;
            EditorGUILayout.PropertyField(_scriptProperty);
            GUI.enabled = true;
            
            serializedObject.Update();
            
            DrawDataProperty(_dataProperty);
            serializedObject.ApplyModifiedProperties();
        }

        private static void DrawDataProperty(SerializedProperty dataProperty)
        {
            if (dataProperty == null)
            {
                EditorGUILayout.HelpBox("Data property is null", MessageType.Error);
                return;
            }
            
            foreach (var childProperty in GetDataProperties(dataProperty))
            {
                EditorGUILayout.PropertyField(childProperty, new GUIContent(childProperty.displayName), true);
            }
        }

        private static IEnumerable<SerializedProperty> GetDataProperties(SerializedProperty property)
        {
            if (property == null) yield break;
            
            var iterator = property.Copy();
            var endProperty = iterator.GetEndProperty();

            iterator.NextVisible(true);

            while (!SerializedProperty.EqualContents(iterator, endProperty))
            {
                yield return iterator;
                iterator.NextVisible(false);
            }
        }
    }
}
#endif