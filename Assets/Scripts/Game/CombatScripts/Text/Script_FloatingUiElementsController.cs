using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_FloatingUiElementsController : MonoBehaviour {

    private static Script_FloatingUiElements FloatingText;
    private static GameObject canvas;
    // Use this for initialization
    public static void Initalize()
    {
        canvas = GameObject.Find("Canvas_PartyStats");

        if (!FloatingText)
        {
            FloatingText = (Script_FloatingUiElements)Resources.Load("Prefabs/Battle/Text/Prefab_Text_DamageParent", typeof(Script_FloatingUiElements));
        }
    }

    public static void CreateFloatingText(string text, Transform location)
    {
        //Camera Camera = GameObject.Find("");
        Script_FloatingUiElements instance = Instantiate(FloatingText);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position + Vector3.up);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.SetText(text);

    }
}
