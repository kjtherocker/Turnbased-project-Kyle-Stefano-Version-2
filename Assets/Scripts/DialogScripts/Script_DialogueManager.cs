using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Script_DialogueManager : MonoBehaviour
 {
    
    public Text C_NameText;
    public Text C_DialogueText;

    private Queue<string> m_QueueOfSentences;


    void Start () 
	{
        m_QueueOfSentences = new Queue<string>();
    }

    public void StartDialogue(Script_Dialogue InputedDialogue)
    {

        C_NameText.text = InputedDialogue.name;

        m_QueueOfSentences.Clear();

        foreach (string sentences in InputedDialogue.sentences)
        {
            m_QueueOfSentences.Enqueue(sentences);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (m_QueueOfSentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = m_QueueOfSentences.Dequeue();
        C_DialogueText.text = sentence;


    }

    void EndDialogue()
    {
        Debug.Log("End of conversation.");
    }

    void Update () 
	{
		
	
	
	}

}
