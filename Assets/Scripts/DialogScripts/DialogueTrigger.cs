//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class DialogueTrigger : MonoBehaviour
//{

//    public List<Dialogue> m_Dialogue;
//    public DialogueManager m_DialogueManager;
//    public bool DialogueHasHappend;

//    public void Start()
//    {
//        DialogueHasHappend = false;
//        m_DialogueManager = FindObjectOfType<DialogueManager>();
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.tag == "Player")
//        {
//            if (DialogueHasHappend == false)
//            {
//                m_DialogueManager.StartDialogue(m_Dialogue);
//                DialogueHasHappend = true;
//            }
//        }
//    }

//}
