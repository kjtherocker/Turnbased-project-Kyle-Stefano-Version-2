using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiScreen : MonoBehaviour
{
    public GameObject FirstSelected;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void OnPop()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnPush()
    {
        gameObject.SetActive(true);
    }
}
