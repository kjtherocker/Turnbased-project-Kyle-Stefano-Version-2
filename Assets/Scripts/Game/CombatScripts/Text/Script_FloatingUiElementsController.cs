using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_FloatingUiElementsController : MonoBehaviour {

    private static Script_FloatingUiElements FloatingText;
    private static Script_FloatingUiElements FloatingWeak;
    private static Script_FloatingUiElements FloatingStrong;
    private static Script_FloatingUiElements FloatingMiss;
    private static GameObject canvas;

    public enum UiElementType
    {
        Text,
        Miss,
        Strong,
        Weak
    }

    public UiElementType uiElementType;
    // Use this for initialization
    public static void Initalize()
    {
        canvas = GameObject.Find("Canvas_PartyStats");

        if (!FloatingText)
        {
            FloatingText = (Script_FloatingUiElements)Resources.Load("Prefabs/Battle/Text/Prefab_Text_DamageParent", typeof(Script_FloatingUiElements));
        }
        if (!FloatingWeak)
        {
            FloatingWeak = (Script_FloatingUiElements)Resources.Load("Prefabs/Battle/Text/Prefab_Image_Weak_Parent", typeof(Script_FloatingUiElements));
        }
        if (!FloatingStrong)
        {
            FloatingStrong = (Script_FloatingUiElements)Resources.Load("Prefabs/Battle/Text/Prefab_Image_Strong_Parent", typeof(Script_FloatingUiElements));
        }
        if (!FloatingMiss)
        {
            FloatingMiss = (Script_FloatingUiElements)Resources.Load("Prefabs/Battle/Text/Prefab_Image_Miss_Parent", typeof(Script_FloatingUiElements));
        }

    }


    public static void CreateFloatingText(string text, Transform location, UiElementType a_uiElementtype)
    {
        //Camera Camera = GameObject.Find("");
        if (a_uiElementtype == UiElementType.Text)
        {
            Script_FloatingUiElements instance = Instantiate(FloatingText);
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position + Vector3.up);
            instance.transform.SetParent(canvas.transform, false);
            instance.transform.position = screenPosition;
            instance.SetText(text);
        }
        if (a_uiElementtype == UiElementType.Strong)
        {
            Script_FloatingUiElements instance = Instantiate(FloatingStrong);
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position + Vector3.up);
            instance.transform.SetParent(canvas.transform, false);
            instance.transform.position = screenPosition;

        }
        if (a_uiElementtype == UiElementType.Weak)
        {
            Script_FloatingUiElements instance = Instantiate(FloatingWeak);
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position + Vector3.up);
            instance.transform.SetParent(canvas.transform, false);
            instance.transform.position = screenPosition;

        }
        if (a_uiElementtype == UiElementType.Miss)
        {
            Script_FloatingUiElements instance = Instantiate(FloatingMiss);
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position + Vector3.up);
            instance.transform.SetParent(canvas.transform, false);
            instance.transform.position = screenPosition;

        }


    }

 
}
