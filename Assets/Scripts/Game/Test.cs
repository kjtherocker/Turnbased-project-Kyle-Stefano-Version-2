using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject ObjectToRotateAround;
    private Vector3 targetPoint;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        targetPoint = new Vector3(ObjectToRotateAround.transform.position.x, transform.position.y, ObjectToRotateAround.transform.position.z);

        //targetPosition.y = ModelInGame.transform.position.y;
        transform.LookAt(targetPoint);
    }
}
