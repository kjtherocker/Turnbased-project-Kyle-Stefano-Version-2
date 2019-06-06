using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChantController : MonoBehaviour
{
    public int materialIndex = 0;
    public Vector2 uvAnimationRate = new Vector2(1.0f, 0.0f);
    public string textureName = "_MainTex";
    Vector2 uvOffset = Vector2.zero;
    public Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        uvOffset += (uvAnimationRate * Time.deltaTime);
        if (rend.enabled)
        {
            rend.materials[materialIndex].SetTextureOffset(textureName, uvOffset);
        }

    }
}
