using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderPosition: MonoBehaviour {

    public bool Test;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Test == false)
        {
            Shader.SetGlobalVector("_PositionA", transform.position);
            Shader.SetGlobalVector("_PositionB", transform.position);
            Shader.SetGlobalVector("_PositionC", transform.position);
            Shader.SetGlobalVector("PositionD", transform.position);
        }
        else if (Test == true)
        {
            Shader.SetGlobalVector("_Position2", transform.position);
        }
	}
}
