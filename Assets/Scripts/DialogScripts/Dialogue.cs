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
    public string m_ChatBoxName;
    public DialogueManager.ChatBoxType m_ChatBoxType;
    public DialogueManager.FontTypes m_FontTypes;


    public string m_Name;

    [TextArea(1,3)]
    public string m_Sentances;

    public void Initalize()
    {
        if (m_ChatBoxName != "" && m_ChatBoxName != null)
        {
            m_ChatBoxType = (DialogueManager.ChatBoxType)System.Enum.Parse(typeof(DialogueManager.ChatBoxType), m_ChatBoxName);
        }
    }

	
}
