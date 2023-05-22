#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;
#endif

public static class Utils
{
#if UNITY_EDITOR
    public static object GetValue(this SerializedProperty property)
    {
        object obj = property.serializedObject.targetObject;
        Type type = obj.GetType();
        FieldInfo field = type.GetField(property.propertyPath, 
            BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        return field?.GetValue(obj);
    }
#endif
}
