using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(RadiusBlur))]
public class RadiusBlurEditor : Editor
{
    RadiusBlur radiusBlur;

    public float centerX = 0.5f;
    public float centerY = 0.5f;
    [Range(0f, 0.03f)]
    public float radiusOffset = 0;
    [Range(1, 30)]
    public int iteration = 1;
    [Range(0f, 1f)]
    public float centerRange = 0f;
    bool first_loaded = true;
    public override void OnInspectorGUI()
    {
        radiusBlur = target as RadiusBlur;
        ShowRadiusData();   
        first_loaded = false; 
    }   

    void ShowRadiusData()
    {
        radiusBlur.radius_material = EditorGUILayout.ObjectField("径向模糊材质", radiusBlur.radius_material, typeof(Material), true) as Material;
        GUILayout.Space(10);

        Vector3 radius_data = radiusBlur.radius_data;
        centerX = EditorGUILayout.Slider("径向模糊中心X", centerX, 0f, 1f);
        centerY = EditorGUILayout.Slider("径向模糊中心Y", centerY, 0f, 1f);
        // 跟径向模糊中心分离开
        GUILayout.Space(10);
        radiusOffset = EditorGUILayout.Slider("偏移范围(0~0.03)", radiusOffset, 0.0f, 0.03f);
        iteration = EditorGUILayout.IntSlider("迭代次数Iteration", iteration, 1, 30);
        centerRange = EditorGUILayout.Slider("模糊内径", centerRange, 0f, 1f);

        if(first_loaded)
        {
            centerX = radius_data.x;
            centerY = radius_data.y;
            radiusOffset = radius_data.z;
            iteration = radiusBlur.iteration;
            centerRange = radiusBlur.radius_center_range;
        }
        if(first_loaded || 
            radius_data.x != centerX ||
            radius_data.y != centerY ||
            radius_data.z != radiusOffset ||
            radiusBlur.iteration != iteration ||
            radiusBlur.radius_center_range != centerRange)
        {
            radiusBlur.iteration = iteration;
            radiusBlur.radius_data = new Vector3(centerX, centerY, radiusOffset);
            radiusBlur.radius_center_range = centerRange;
        }
    }
}
