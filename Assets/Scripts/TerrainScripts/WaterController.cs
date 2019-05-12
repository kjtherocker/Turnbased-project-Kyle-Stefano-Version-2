using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{

    public int materialIndex = 0;
    public Vector2 uvAnimationRate = new Vector2(1.0f, 0.0f);
    public string textureName = "_MainTex";
    Vector2 uvOffset = Vector2.zero;
    public Renderer rend;
    public float WatersAmplitude;
    public float WaterWaves;

    public Vector3 RipplePoint;
    float CanRipple;



    // Use this for initialization
    void Start()
    {

        rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("OdellWaterShader");
        //RipplePoint.Set(8.77f, 92.41f,12.75f);
        //WatersAmplitude = 0.4f;
        StartRipple();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        rend.material.SetFloat("_Amplitude", WatersAmplitude);
        rend.material.SetFloat("_Wave", WaterWaves);

        //rend.material.SetFloat("_IsRippling", CanRipple);




        uvOffset += (uvAnimationRate * Time.deltaTime);
        if (rend.enabled)
        {
            rend.materials[materialIndex].SetTextureOffset(textureName, uvOffset);
        }
    }

    public void StartRipple()
    {
        WaterWaves = 165.13f;
        CanRipple = 1;
    }
}
