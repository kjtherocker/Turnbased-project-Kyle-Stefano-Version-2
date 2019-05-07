using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_FloatingUiElementsController : MonoBehaviour {

    private static Script_FloatingUiElements FloatingText;
    private static Script_FloatingUiElements FloatingWeak;
    private static Script_FloatingUiElements FloatingStrong;
    private static Script_FloatingUiElements FloatingMiss;
    private static Script_FloatingUiElements FloatingAttackUp;
    private static Script_FloatingUiElements FloatingAttackDown;
    private static Script_FloatingUiElements FloatingDefenseUp;
    private static Script_FloatingUiElements FloatingSpeedUp;
    private static GameObject canvas;

    public enum UiElementType
    {
        Text,
        Miss,
        Strong,
        Weak,
        Attackup,
        AttackDown,
        Defenseup,
        SpeedUp
    }

    public UiElementType uiElementType;
    // Use this for initialization
    public static void Initalize()
    {
        canvas = GameObject.Find("Canvas_TurnIndicator");

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
        if (!FloatingAttackUp)
        {
            FloatingAttackUp = (Script_FloatingUiElements)Resources.Load("Prefabs/Battle/Text/Prefab_Image_AttackUp_Parent", typeof(Script_FloatingUiElements));
        }

        if (!FloatingAttackDown)
        {
            FloatingAttackDown = (Script_FloatingUiElements)Resources.Load("Prefabs/Battle/Text/Prefab_Image_AttackDown_Parent", typeof(Script_FloatingUiElements));
        }
        if (!FloatingDefenseUp)
        {
            FloatingDefenseUp = (Script_FloatingUiElements)Resources.Load("Prefabs/Battle/Text/Prefab_Image_SpeedUp_Parent", typeof(Script_FloatingUiElements));
        }
        if (!FloatingSpeedUp)
        {
            FloatingSpeedUp = (Script_FloatingUiElements)Resources.Load("Prefabs/Battle/Text/Prefab_Image_Miss_Parent", typeof(Script_FloatingUiElements));
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
        if (a_uiElementtype == UiElementType.Attackup)
        {
            Script_FloatingUiElements instance = Instantiate(FloatingAttackUp);
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position + Vector3.up);
            instance.transform.SetParent(canvas.transform, false);
            instance.transform.position = screenPosition;

        }
        if (a_uiElementtype == UiElementType.AttackDown)
        {
            Script_FloatingUiElements instance = Instantiate(FloatingAttackDown);
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position + Vector3.up);
            instance.transform.SetParent(canvas.transform, false);
            instance.transform.position = screenPosition;

        }
        if (a_uiElementtype == UiElementType.Defenseup)
        {
            Script_FloatingUiElements instance = Instantiate(FloatingDefenseUp);
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position + Vector3.up);
            instance.transform.SetParent(canvas.transform, false);
            instance.transform.position = screenPosition;

        }
        if (a_uiElementtype == UiElementType.SpeedUp)
        {
            Script_FloatingUiElements instance = Instantiate(FloatingSpeedUp);
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position + Vector3.up);
            instance.transform.SetParent(canvas.transform, false);
            instance.transform.position = screenPosition;

        }


    }

 
}
