using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_DestroyOnAnimatorFinish : MonoBehaviour {

    public Animator animator;

    // Use this for initialization

    void Start()
    {
        AnimatorClipInfo[] clipInfos = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfos[0].clip.length);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
