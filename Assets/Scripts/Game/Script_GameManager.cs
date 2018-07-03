using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_GameManager : MonoBehaviour
{
    public GameObject Overworld_Objects;
    public GameObject Battle_Objects;

	// Use this for initialization
	void Start ()
    {
        Overworld_Objects.SetActive(true);
        Battle_Objects.SetActive(false);

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SwitchToOverworld()
    {
        Overworld_Objects.SetActive(true);
        Battle_Objects.SetActive(false);
    }

    public void SwitchToBattle()
    {
        Overworld_Objects.SetActive(false);
        Battle_Objects.SetActive(true);
    }
}
