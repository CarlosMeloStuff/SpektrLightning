using UnityEngine;
using UnityEditor;

namespace Spektr
{
    [CustomEditor(typeof(LightningRenderer))]
    class LightningRendererEditor : Editor
    {
        GUIStyle _labelStyle;

        void OnEnable()
        {
            _labelStyle = new GUIStyle();
            _labelStyle.fontSize = 16;
            _labelStyle.normal.textColor = Color.red;
        }

        void OnSceneGUI()
        {
            var instance = (LightningRenderer)target;

            EditorGUI.BeginChangeCheck();

            var p0 = instance.emitterPosition;
            var p1 = instance.receiverPosition;
            var r = instance.pointSpread;

            Handles.color = Color.red;

            Handles.Label(p0, "Emitter", _labelStyle);
            Handles.Label(p1, "Receiver", _labelStyle);

            r = Handles.RadiusHandle(Quaternion.identity, p0, r);
            r = Handles.RadiusHandle(Quaternion.identity, p1, r);

            p0 = Handles.PositionHandle(p0, Quaternion.identity);
            p1 = Handles.PositionHandle(p1, Quaternion.identity);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Move Position");

                instance.emitterPosition = p0;
                instance.receiverPosition = p1;
                instance.pointSpread = r;

                EditorUtility.SetDirty(target);
            }
        }
    }
}

