using System.Collections;

using System.Collections.Generic;
using UnityEngine;


public class Script_DialogueTrigger : MonoBehaviour
{
    public Script_Dialogue m_Dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<Script_DialogueManager>().StartDialogue(m_Dialogue);

    }

}
