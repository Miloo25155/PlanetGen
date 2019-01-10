using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WaterSphere))]
public class WaterSphereEditor : Editor
{
    WaterSphere waterSphere;
    Editor waterEditor;

    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();
            if (check.changed)
            {
                waterSphere.GenerateWaterSphere();
            }
        }

        if (GUILayout.Button("Generate Water Sphere"))
        {
            waterSphere.GenerateWaterSphere();
        }

        if (GUILayout.Button("Clear Water Sphere"))
        {
            waterSphere.ClearWaterSphere();
        }

        DrawSettingsEditor(waterSphere.waterSettings, waterSphere.GenerateWaterSphere, ref waterSphere.waterSettingsFoldout, ref waterEditor);
    }

    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor)
    {
        if (settings != null)
        {
            foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);

            using (var check = new EditorGUI.ChangeCheckScope())
            {
                if (foldout)
                {
                    CreateCachedEditor(settings, null, ref editor);
                    editor.OnInspectorGUI();

                    if (check.changed)
                    {
                        if (onSettingsUpdated != null)
                        {
                            onSettingsUpdated();
                        }
                    }
                }
            }
        }
    }

    private void OnEnable()
    {
        waterSphere = (WaterSphere)target;
    }
}
