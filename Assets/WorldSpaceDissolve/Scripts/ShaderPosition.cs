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
            Shader.SetGlobalVector("_Position", transform.position);
        }
        else if (Test == true)
        {
            Shader.SetGlobalVector("_Position2", transform.position);
        }
	}
}
