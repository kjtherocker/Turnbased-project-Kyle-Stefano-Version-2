using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingUiElements : MonoBehaviour {

    public Animator animator;
    public Text DamageText;
	// Use this for initialization
	void Start ()
    {
        AnimatorClipInfo[] clipInfos = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfos[0].clip.length);
      //  DamageText = animator.GetComponent<Text>();
	}

    public void SetText(string a_text)
    {
        DamageText = animator.GetComponent<Text>();
        DamageText.text = a_text;
    }
    public void DestroyAll()
    {
        Destroy(gameObject);
    }

}
