using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnaroundscript : MonoBehaviour {
    [SerializeField]
    private float MoveSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(transform.up * Time.deltaTime * MoveSpeed);
	}
}
