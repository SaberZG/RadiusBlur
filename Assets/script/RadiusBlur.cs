using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RadiusBlur : MonoBehaviour
{
    static int radius_blur_dataId = Shader.PropertyToID("_RadiusData"),
        radius_blur_iterationId = Shader.PropertyToID("_RadiusIterationData"),
        radius_blur_centerRange = Shader.PropertyToID("_RadiusCenterRange");

    [SerializeField]
    public Material radius_material;

    [SerializeField]
    public Vector3 radius_data{
        get{
            if(radius_material != null)
            {
                return radius_material.GetVector(radius_blur_dataId);
            }
            return new Vector3(0.5f, 0.5f, 0.01f);
        }
        set{
            if(radius_material != null)
            {
                radius_material.SetVector(radius_blur_dataId, value);
            }
        }
    }

    [SerializeField]
    public int iteration{
        get{
            if(radius_material != null)
            {
                return (int)radius_material.GetVector(radius_blur_iterationId).x;
            }
            return 1;
        }
        set{
            if(radius_material != null)
            {
                float invIteration = 1.0f / value;
                radius_material.SetVector(radius_blur_iterationId, new Vector4(value, invIteration, 0f, 0f));
            }
        }
    }

    public float radius_center_range{
        get{
            if(radius_material != null)
            {
                return radius_material.GetFloat(radius_blur_centerRange);
            }
            return 0f;
        }
        set{
            if(radius_material != null)
            {
                if(value <= 0f)
                {
                    radius_material.DisableKeyword("_USE_CIRCLE_CENTER");
                }
                else
                {
                    radius_material.EnableKeyword("_USE_CIRCLE_CENTER");
                }
                radius_material.SetFloat(radius_blur_centerRange, value);
            }
        }
    }
    static int RADIUS_BLUR_PASS = 0;
    private void Awake() {
        if(radius_material != null)
        {
            radius_data = radius_material.GetVector(radius_blur_dataId);
            iteration = (int)radius_material.GetVector(radius_blur_iterationId).x;
            radius_center_range = radius_material.GetFloat(radius_blur_centerRange);
        }
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest) {
        if(radius_material)
        {
            Graphics.Blit(src, dest, radius_material, RADIUS_BLUR_PASS);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
