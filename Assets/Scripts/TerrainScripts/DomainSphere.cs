using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatNodeDomainSphere : MonoBehaviour
{
    public int materialIndex = 0;
    public Vector2 uvAnimationRate = new Vector2(1.0f, 0.0f);
    public string textureName = "_MainTex";
    Vector2 uvOffset = Vector2.zero;
    public Renderer rend;

    // Use this for initialization
    void Start ()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        uvOffset += (uvAnimationRate * Time.deltaTime);
        if (rend.enabled)
        {
            rend.materials[materialIndex].SetTextureOffset(textureName, uvOffset);
        }
    }
}
