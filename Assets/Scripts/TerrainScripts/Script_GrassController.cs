using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class Script_GrassController : MonoBehaviour
{

    public float radius = 0.5f;
    public float softness = 0.5f;
    public Vector3 CirclePosition;

    void Update()
    {
        Shader.SetGlobalVector("_GLOBALMaskPosition", CirclePosition);
        Shader.SetGlobalFloat("_GLOBALMaskRadius", radius);
        Shader.SetGlobalFloat("_GLOBALMaskSoftness", softness);
    }
}