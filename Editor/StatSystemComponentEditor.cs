using UnityEngine;
using UnityEditor;
using StudioScor.Utilities;
using StudioScor.Utilities.Editor;

namespace StudioScor.StatSystem.Editor
{
    
    [CustomEditor(typeof(StatSystemComponent))]
    [CanEditMultipleObjects]
    public class StatSystemComponentEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (Application.isPlaying)
            {
                GUILayout.Space(5f);
                SEditorUtility.GUI.DrawLine(4f);
                GUILayout.Space(5f);

                var statSystem = (StatSystemComponent)target;

                var stats = statSystem.Stats;

                GUIStyle title = new();
                GUIStyle level = new();
                GUIStyle plus = new();
                GUIStyle minus = new();
                GUIStyle normal = new();

                title.normal.textColor = Color.white;
                title.alignment = TextAnchor.MiddleCenter;
                title.fontStyle = FontStyle.Bold;

                level.normal.textColor = Color.white;
                level.alignment = TextAnchor.MiddleCenter;

                normal.normal.textColor = Color.white;
                plus.normal.textColor = Color.green;
                minus.normal.textColor = Color.red;

                GUILayout.Label("[ Stat ]", title);

                if (stats is not null)
                {
                    float currentValue;
                    float baseValue;
                    float addValue;
                    bool isZero;
                    bool isPositive;


                    foreach (var stat in stats)
                    {
                        currentValue = stat.Value.Value;
                        baseValue = stat.Value.BaseValue;
                        addValue = currentValue - baseValue;
                        isZero = addValue == 0f;
                        isPositive = addValue.IsPositive();

                        GUILayout.BeginHorizontal();
                        GUILayout.Label(stat.Key.Name, normal);
                        GUILayout.FlexibleSpace();
                        GUILayout.Label(currentValue.ToString("F0"), normal);

                        if (isZero)
                        {
                            GUILayout.Label($" [ {baseValue:F0} ] ", normal);
                        }
                        else
                        {
                            if (isPositive)
                            {
                                GUILayout.Label($" [ {baseValue:F0} + {addValue:F0} ] ", plus);
                            }
                            else
                            {
                                GUILayout.Label($" [ {baseValue:F0} - {-addValue:F0}] ", minus);
                            }
                        }

                        GUILayout.Space(10f);
                        GUILayout.EndHorizontal();

                        SEditorUtility.GUI.DrawLine(1f);
                    }
                }
            }
        }
    }
}
