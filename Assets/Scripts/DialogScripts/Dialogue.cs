using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public GameObject m_GameobjectSpawnPoint;
    public GameObject m_GameObjectToAppearInCutscene;

    public bool DestroyGameObjectOnEndOfDialogue;

    public Constants.Portrait m_PortraitType;
    public DialogueManager.ChatBoxType m_ChatBoxType;
    public DialogueManager.FontTypes m_FontTypes;

    public string m_Name;

    [TextArea(1,3)]
    public string m_Sentances;


	
}
